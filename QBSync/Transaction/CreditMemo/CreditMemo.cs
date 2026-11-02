using QB_SQL_Sync.Models.QBSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
	public static class CreditMemo
	{
		public static bool SyncCreditMemoList(SyncDetails sync, out int count)
		{
            string modifiedDateTimeFrom = sync.LastSyncedOn.ToString(Constants.RequestDateFormat);
            //string modifiedDateTimeFrom = utils.GetLastTimeModified(Constants.CreditMemoTypeName);      
            string modifiedDateTimeTo = DateTime.Now.ToString(Constants.RequestDateFormat);
            //if (DateTime.Now.Subtract(sync.LastSyncedOn).Days / 30 > 3)
            //    modifiedDateTimeTo = sync.LastSyncedOn.AddMonths(3).ToString(Constants.RequestDateFormat);
            string CreditMemoResponse = ProcessXMLRequest.GetTransactionData(Constants.CreditMemoTypeName, modifiedDateTimeFrom, modifiedDateTimeTo);
			if (CreditMemoResponse == null)
				throw new DataNotFoundException("CreditMemo response received NULL from QuickBook");
			List<CreditMemoRet> creditMemoReturnList = ProcessCreditMemoXML.ConvertCreditMemoXML_to_List(CreditMemoResponse);
            count = creditMemoReturnList.Count();
            ProcessCreditMemoData creditMemoData = new ProcessCreditMemoData();
			//creditMemoData.InsertCreditMemoToDatabase(creditMemoReturnList);
            try
            {
                creditMemoData.InsertCreditMemoToDatabase(creditMemoReturnList);
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
