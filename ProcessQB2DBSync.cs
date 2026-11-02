using QB_SQL_Sync.Models.QBSync;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QB2DB_Sync
{
    class ProcessQB2DBSync
    {
        private readonly PubNub pubNub = new PubNub();
        private delegate bool SyncOperation(SyncDetails sync, out int count);
        public bool Begin(List<SyncDetails> details)
        {
            bool success = true;
            success = Run(details, SyncConstants.CUSTOMER, Customer.SyncCustomerList) && success;
            success = Run(details, SyncConstants.ITEM, Item.SyncItemList) && success;
            success = Run(details, SyncConstants.CREDITMEMO, CreditMemo.SyncCreditMemoList) && success;
            success = Run(details, SyncConstants.INVOICE, Invoice.SyncInvoiceList) && success;
            success = Run(details, SyncConstants.RECEIVEPAYMENT, ReceivePayment.SyncReceivePaymentList) && success;
            success = Run(details, SyncConstants.SALESORDER, SalesOrder.SyncSalesOrderList) && success;
            success = Run(details, SyncConstants.SALESRECEIPT, SalesReceipt.SyncSalesReceiptList) && success;
            return success;
        }
        private bool Run(List<SyncDetails> details, string type, SyncOperation operation)
        {
            var sync = details.FirstOrDefault(s => s.SyncType == type);
            if (sync == null) throw new InvalidOperationException("Missing sync configuration: " + type);
            if (!sync.IsActive) return true;
            sync.Status = "Running";
            sync.Description = "Reading QuickBooks changes";
            SyncServices.UpdateLastSyncedAndStatus(sync);
            pubNub.SendMessage(new Dictionary<string,string> { { "msg", "reload" } },
                "Sync started for " + type + " from " + sync.LastSyncedOn.ToString(Constants.RequestDateFormat));
            bool success = false;
            try
            {
                int count;
                success = operation(sync, out count);
                sync.Status = success ? "Success" : "Failed";
                sync.Description = success ? "Synced " + count + " records" : "Import failed; previous checkpoint retained";
            }
            catch (Exception ex)
            {
                sync.Status = "Failed";
                sync.Description = "QuickBooks request/import failed; previous checkpoint retained";
                sync.Exception = ex.ToString();
                SyncServices.UpdateErros(sync);
                Console.WriteLine("SYNC FAILED for " + type + ": " + ex.Message);
            }
            SyncServices.UpdateLastSyncedAndStatus(sync);
            pubNub.SendMessage(new Dictionary<string,string> { { "msg", "reload" } });
            return success;
        }
    }
}
