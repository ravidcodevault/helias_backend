using QB_SQL_Sync.Models.QBSync;
using QBFC16Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
	public static class Item
	{
		public static bool SyncItemList(SyncDetails sync, out int count)
		{
            string modifiedDateTimeFrom = sync.LastSyncedOn.ToString(Constants.RequestDateFormat);
            //string modifiedDateTimeFrom = utils.GetLastTimeModified(Constants.ItemTypeName);
            string modifiedDateTimeTo = DateTime.Now.ToString(Constants.RequestDateFormat);
            //if (DateTime.Now.Subtract(sync.LastSyncedOn).Days / 30 > 3)
            //    modifiedDateTimeTo = sync.LastSyncedOn.AddMonths(3).ToString(Constants.RequestDateFormat);
            string itemResponse = ProcessXMLRequest.GetMasterData(Constants.ItemTypeName, modifiedDateTimeFrom, modifiedDateTimeTo);
			if (itemResponse == null)
				throw new DataNotFoundException("Item response received NULL from QuickBook");
			List<ItemRet> itemReturnList = ProcessItemXML.ConvertItemXML_to_List(itemResponse);
            count = itemReturnList.Count();
            ProcessItemData itemData = new ProcessItemData();
			//itemData.InsertItemsToDatabase(itemReturnList);
            try
            {
                itemData.InsertItemsToDatabase(itemReturnList);
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
