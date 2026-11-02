
using System;
using System.Data.SqlClient;
using System.Xml;

namespace QB2DB_Sync
{
    public class SQLHelper
    {
        public static void AddWithValue(SqlCommand cmd, string parameterName, object value)
        {
            if (value != null)
            {
                cmd.Parameters.AddWithValue(parameterName, value);
            }
            else
            {
                cmd.Parameters.AddWithValue(parameterName, DBNull.Value);
            }
        }

        // public static void AddWithAddressValue(SqlCommand cmd, string billOrShip, CustomerRet item)
        //         {
        //             //QBAddress address = null; i.BillAddress;

        //             if (billOrShip == "Bill")
        //             {
        //                 AddWithValue(cmd, "@BillAddressAddr1", item.BillAddressAddr1);
        //         AddWithValue(cmd, "@BillAddressAddr2", item.BillAddressAddr2);
        //         AddWithValue(cmd, "@BillAddressAddr3", item.BillAddressAddr3);
        //         AddWithValue(cmd, "@BillAddressAddr4", item.BillAddressAddr4);
        //         AddWithValue(cmd, "@BillAddressAddr5", item.BillAddressAddr5);
        //         AddWithValue(cmd, "@BillAddressCity", item.BillAddressCity);
        //         AddWithValue(cmd, "@BillAddressState", item.BillAddressState);
        //         AddWithValue(cmd, "@BillAddressPostalCode", item.BillAddressPostalCode);
        //         AddWithValue(cmd, "@BillAddressCountry", item.BillAddressCountry);
        //         AddWithValue(cmd, "@BillAddressNote", item.BillAddressNote);

        //         AddWithValue(cmd, "@BillAddressBlockAddr1", item.BillAddressBlockAddr1);
        //         AddWithValue(cmd, "@BillAddressBlockAddr2", item.BillAddressBlockAddr2);
        //         AddWithValue(cmd, "@BillAddressBlockAddr3", item.BillAddressBlockAddr3);
        //         AddWithValue(cmd, "@BillAddressBlockAddr4", item.BillAddressBlockAddr4);
        //         AddWithValue(cmd, "@BillAddressBlockAddr5", item.BillAddressBlockAddr5);
        //     }
        //             else if (billOrShip == "Ship")
        //             {
        //                 AddWithValue(cmd, "@ShipAddressAddr1", item.ShipAddressAddr1);
        //     AddWithValue(cmd, "@ShipAddressAddr2", item.ShipAddressAddr2);
        //     AddWithValue(cmd, "@ShipAddressAddr3", item.ShipAddressAddr3);
        //     AddWithValue(cmd, "@ShipAddressAddr4", item.ShipAddressAddr4);
        //     AddWithValue(cmd, "@ShipAddressAddr5", item.ShipAddressAddr5);
        //     AddWithValue(cmd, "@ShipAddressCity", item.ShipAddressCity);
        //     AddWithValue(cmd, "@ShipAddressState", item.ShipAddressState);
        //     AddWithValue(cmd, "@ShipAddressPostalCode", item.ShipAddressPostalCode);
        //     AddWithValue(cmd, "@ShipAddressCountry", item.ShipAddressCountry);
        //     AddWithValue(cmd, "@ShipAddressNote", item.ShipAddressNote);

        //     AddWithValue(cmd, "@ShipAddressBlockAddr1", item.ShipAddressBlockAddr1);
        //     AddWithValue(cmd, "@ShipAddressBlockAddr2", item.ShipAddressBlockAddr2);
        //     AddWithValue(cmd, "@ShipAddressBlockAddr3", item.ShipAddressBlockAddr3);
        //     AddWithValue(cmd, "@ShipAddressBlockAddr4", item.ShipAddressBlockAddr4);
        //     AddWithValue(cmd, "@ShipAddressBlockAddr5", item.ShipAddressBlockAddr5);
        // }
        //         }
    }
}