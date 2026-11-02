using QB_SQL_Sync.Models.QBSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public static class SalesReceipt
    {
        public static bool SyncSalesReceiptList(SyncDetails sync, out int count)
        {
            string modifiedDateTimeFrom = sync.LastSyncedOn.ToString(Constants.RequestDateFormat);

            string modifiedDateTimeTo = DateTime.Now.ToString(Constants.RequestDateFormat);
            //if (DateTime.Now.Subtract(sync.LastSyncedOn).Days / 30 > 3)
            //    modifiedDateTimeTo = sync.LastSyncedOn.AddMonths(3).ToString(Constants.RequestDateFormat);
            string SalesReceiptResponse = ProcessXMLRequest.GetTransactionData(Constants.SalesReceiptTypeName, modifiedDateTimeFrom, modifiedDateTimeTo);
            if (SalesReceiptResponse == null)
                throw new DataNotFoundException("SalesReceipt response received NULL from QuickBook");
            List<SalesReceiptRet> salesReceiptReturnList = ProcessSalesReceiptXML.ConvertSalesReceiptXML_to_List(SalesReceiptResponse);
            count = salesReceiptReturnList.Count();
            ProcessSalesReceiptData salesReceiptSata = new ProcessSalesReceiptData();
            try
            {
                salesReceiptSata.InsertSalesReceiptToDatabase(salesReceiptReturnList);
            }
            catch (Exception ex)
            {
                sync.Exception = ex.ToString();
                SyncServices.UpdateErros(sync);
                return false;
            };
            return true;
        }
    }
}
