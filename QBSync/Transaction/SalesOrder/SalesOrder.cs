using QB_SQL_Sync.Models.QBSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
	public static class SalesOrder
	{
		public static bool SyncSalesOrderList(SyncDetails sync, out int count)
		{
            string modifiedDateTimeFrom = sync.LastSyncedOn.ToString(Constants.RequestDateFormat);
            //string modifiedDateTimeFrom = utils.GetLastTimeModified(Constants.SalesOrderTypeName);
            string modifiedDateTimeTo = DateTime.Now.ToString(Constants.RequestDateFormat);
            //if (DateTime.Now.Subtract(sync.LastSyncedOn).Days / 30 > 3)
            //    modifiedDateTimeTo = sync.LastSyncedOn.AddMonths(3).ToString(Constants.RequestDateFormat);
            string SalesOrderResponse = ProcessXMLRequest.GetTransactionData(Constants.SalesOrderTypeName, modifiedDateTimeFrom, modifiedDateTimeTo);
			if (SalesOrderResponse == null)
				throw new DataNotFoundException("SalesOrder response received NULL from QuickBook");
			List<SalesOrderRet> salesOrderReturnList = ProcessSalesOrderXML.ConvertSalesOrderXML_to_List(SalesOrderResponse);
            count = salesOrderReturnList.Count();
            ProcessSalesOrderData salesOrderData = new ProcessSalesOrderData();
			try
			{
                salesOrderData.InsertSalesOrderToDatabase(salesOrderReturnList);
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
