using QB_SQL_Sync.Models.QBSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
	public static class Invoice
	{
		public static bool SyncInvoiceList(SyncDetails sync, out int count)
		{
            string modifiedDateTimeFrom = sync.LastSyncedOn.ToString(Constants.RequestDateFormat);
            //string modifiedDateTimeFrom = utils.GetLastTimeModified(Constants.InvoiceTypeName);
            string modifiedDateTimeTo = DateTime.Now.ToString(Constants.RequestDateFormat);
            string invoiceResponse = ProcessXMLRequest.GetTransactionData(Constants.InvoiceTypeName, modifiedDateTimeFrom, modifiedDateTimeTo);
			if (invoiceResponse == null)
				throw new DataNotFoundException("Invoice response received NULL from QuickBook");
			List<InvoiceRet> invoiceRetList = ProcessInvoiceXML.ConvertInvoiceXML_to_List(invoiceResponse);
            count = invoiceRetList.Count();
            ProcessInvoiceData invoiceData = new ProcessInvoiceData();
            try
            {
                invoiceData.InsertInvoiceToDatabase(invoiceRetList);
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
