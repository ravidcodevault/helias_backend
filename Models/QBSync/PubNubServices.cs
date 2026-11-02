using PubnubApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace QB_SQL_Sync.Models.QBSync
{
    public class PubNub : IDisposable
    {
        private const string ChannelName = "OYA_SYNC_UPDATE";
        private readonly Pubnub pubnub;
        private SubscribeCallbackExt commandListener;

        public PubNub()
        {
            var configuration = new PNConfiguration(new UserId("OYA_SYNC_RUNNER"))
            {
                SubscribeKey = ConfigurationManager.AppSettings["PubNub.SubscribeKey"],
                PublishKey = ConfigurationManager.AppSettings["PubNub.PublishKey"],
                SecretKey = ConfigurationManager.AppSettings["PubNub.SecretKey"],
                ReconnectionPolicy = PNReconnectionPolicy.LINEAR
            };
            pubnub = new Pubnub(configuration);
        }

        public void StartCommandListener(Action<long> onStartSync)
        {
            if (commandListener != null) throw new InvalidOperationException("Command listener already started.");
            commandListener = new SubscribeCallbackExt(
                (client, message) =>
                {
                    if (message.Channel != ChannelName || message.Publisher != "OYA_WEB_USER") return;
                    try
                    {
                        var payload = message.Message is string
                            ? JObject.Parse((string)message.Message)
                            : JObject.FromObject(message.Message);
                        if (string.Equals((string)payload["msg"], "start sync", StringComparison.OrdinalIgnoreCase))
                            onStartSync(message.Timetoken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ignoring invalid sync command: " + ex.GetType().Name);
                    }
                },
                (client, presence) => { },
                (client, status) =>
                {
                    if (status.Category == PNStatusCategory.PNConnectedCategory ||
                        status.Category == PNStatusCategory.PNReconnectedCategory)
                        Console.WriteLine("[{0:yyyy-MM-dd HH:mm:ss}] Manual sync listener connected to {1}.",
                            DateTime.Now, ChannelName);
                    else if (status.Error)
                        Console.WriteLine("Sync listener error: HTTP {0}, category {1}.",
                            status.StatusCode, status.Category);
                });
            pubnub.AddListener(commandListener);
            pubnub.Subscribe<object>().Channels(new[] { ChannelName }).Execute();
        }

        public void SendMessage(Dictionary<string, string> message, string consoleLog = "")
        {
            pubnub.Publish().Channel(ChannelName).Message(message)
                .Execute(new PNPublishResultExt((result, status) =>
                {
                    if (!status.Error)
                    {
                        if (!string.IsNullOrWhiteSpace(consoleLog)) Console.WriteLine(consoleLog);
                    }
                    else
                    {
                        Console.WriteLine("Sync status publish failed: HTTP {0}, category {1}.",
                            status.StatusCode, status.Category);
                    }
                }));
        }

        public void Dispose()
        {
            // Destroy closes the subscription and releases SDK resources.
            pubnub.Destroy();
        }
    }
}
