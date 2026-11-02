using QB_SQL_Sync.Models.QBSync;
using System;
using System.Threading;

namespace QB2DB_Sync
{
    internal class Program
    {
        // Preserve the configured ten-minute automatic interval.
        private static readonly TimeSpan SyncInterval = TimeSpan.FromMinutes(10);
        private static readonly AutoResetEvent ManualSyncRequested = new AutoResetEvent(false);
        private static readonly ManualResetEvent StopRequested = new ManualResetEvent(false);
        private static readonly object CommandGate = new object();
        private static long lastCommandTimetoken;

        [STAThread]
        static void Main(string[] args)
        {
            bool ownsRunner;
            using (var runnerMutex = new Mutex(true, @"Local\OYA_QuickBooks_Sync_Helias", out ownsRunner))
            {
                if (!ownsRunner)
                {
                    Console.WriteLine("Another OYA QuickBooks sync runner is already running. Exiting.");
                    return;
                }
                try
                {
                    Console.CancelKeyPress += (sender, e) =>
                    {
                        e.Cancel = true;
                        StopRequested.Set();
                    };
                    Console.WriteLine("OYA QUICKBOOKS SYNC");
                    Console.WriteLine("Automatic interval: 10 minutes. Manual commands run immediately.");
                    using (var commands = new PubNub())
                    {
                        commands.StartCommandListener(QueueManualSync);
                        var processor = new ProcessQB2DBSync();
                        var trigger = "startup";
                        while (!StopRequested.WaitOne(0))
                        {
                            RunSync(processor, trigger);
                            Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss}] Waiting for a manual command or the next 10-minute sync.", DateTime.Now);
                            var wakeReason = WaitHandle.WaitAny(
                                new WaitHandle[] { ManualSyncRequested, StopRequested }, SyncInterval);
                            if (wakeReason == 1) break;
                            trigger = wakeReason == 0 ? "manual command" : "scheduled";
                        }
                    }
                }
                finally
                {
                    runnerMutex.ReleaseMutex();
                }
            }
        }

        private static void QueueManualSync(long timetoken)
        {
            lock (CommandGate)
            {
                if (timetoken <= lastCommandTimetoken) return;
                lastCommandTimetoken = timetoken;
                Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss}] Manual sync command received: {1}. Queued on the single sync worker.",
                    DateTime.Now, timetoken);
                // Coalesce commands while busy into one follow-up run; never run QuickBooks concurrently.
                ManualSyncRequested.Set();
            }
        }

        private static void RunSync(ProcessQB2DBSync processor, string trigger)
        {
            try
            {
                Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss}] Starting sync. Trigger: {1}", DateTime.Now, trigger);
                var syncInfo = SyncServices.GetAllSyncDetails();
                if (syncInfo == null || syncInfo.Count == 0)
                {
                    Console.WriteLine("No synchronization records found; check the SQL connection.");
                    return;
                }
                bool completed = processor.Begin(syncInfo);
                Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss}] {1}. Trigger: {2}", DateTime.Now, completed ? "Sync completed" : "Sync finished with failures", trigger);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss}] SYNC ERROR ({1}): {2}", DateTime.Now, trigger, ex);
            }
        }
    }
}
