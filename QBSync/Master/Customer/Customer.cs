using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Configuration;
using QB_SQL_Sync.Models.QBSync;

namespace QB2DB_Sync
{
    public static class Customer
    {
        public static bool SyncCustomerList(SyncDetails sync, out int count)
        {
            string modifiedDateTimeFrom = sync.LastSyncedOn.ToString(Constants.RequestDateFormat);
            //string modifiedDateTimeFrom = utils.GetLastTimeModified(Constants.CustomerTypeName);
            string modifiedDateTimeTo = DateTime.Now.ToString(Constants.RequestDateFormat);
            //if (DateTime.Now.Subtract(sync.LastSyncedOn).Days / 30 > 3)
            //    modifiedDateTimeTo = sync.LastSyncedOn.AddMonths(3).ToString(Constants.RequestDateFormat);

            string customerResponse = ProcessXMLRequest.GetMasterData(Constants.CustomerTypeName, modifiedDateTimeFrom, modifiedDateTimeTo);
            if (customerResponse == null)
                throw new DataNotFoundException("Customer response received NULL from QuickBook");
            List<CustomerRet> customerRetList = ProcessCustomerXML.ConvertCustomerXML_to_List(customerResponse);
            count = customerRetList.Count();
            ProcessCustomerData customerData = new ProcessCustomerData();
            try
            {
                customerData.InsertCustomersToDatabase(customerRetList);
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
