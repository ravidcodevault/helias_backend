using QB_SQL_Sync.Models.QBSync;
using QBFC16Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
	public static class ReceivePayment
	{
		public static bool SyncReceivePaymentList(SyncDetails sync, out int count)
		{
            string modifiedDateTimeFrom = sync.LastSyncedOn.ToString(Constants.RequestDateFormat);
            //string modifiedDateTimeFrom = utils.GetLastTimeModified(Constants.ReceivePaymentTypeName);
            string modifiedDateTimeTo = DateTime.Now.ToString(Constants.RequestDateFormat);
            //if (DateTime.Now.Subtract(sync.LastSyncedOn).Days / 30 > 3)
            //    modifiedDateTimeTo = sync.LastSyncedOn.AddMonths(3).ToString(Constants.RequestDateFormat);
            string receivePaymentResponse = ProcessXMLRequest.GetTransactionData(Constants.ReceivePaymentTypeName, modifiedDateTimeFrom, modifiedDateTimeTo);
			if (receivePaymentResponse == null)
				throw new DataNotFoundException("ReceivePayment response received NULL from QuickBook");
			List<ReceivePaymentRet> receivePaymentReturnList = ProcessReceivePaymentXML.ConvertReceivePaymentXML_to_List(receivePaymentResponse);
            count = receivePaymentReturnList.Count();
            ProcessReceivePaymentData receivePaymentData = new ProcessReceivePaymentData();
			//receivePaymentData.InsertReceivePaymentToDatabase(receivePaymentReturnList);
            try
            {
                receivePaymentData.InsertReceivePaymentToDatabase(receivePaymentReturnList);
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
