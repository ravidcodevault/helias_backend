using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class CreditMemoLineRet
    {
        public string TxnID { get; set; }
        public string EditSequence { get; set; }
        public string TxnLineID { get; set; }

        public string ItemRefListID { get; set; }
        public string ItemRefFullName { get; set; }

        public string Desc { get; set; }
        public string Quantity { get; set; }

        public string UnitOfMeasure { get; set; }
        public string OverrideUOMSetRefListID { get; set; }
        public string OverrideUOMSetRefFullName { get; set; }

        public string Rate { get; set; }
        public string RatePercent { get; set; }

        public string ClassRefListID { get; set; }
        public string ClassRefFullName { get; set; }

        public string Amount { get; set; }

        public string InventorySiteRefListID { get; set; }
        public string InventorySiteRefFullName { get; set; }
        public string InventorySiteLocationRefListID { get; set; }
        public string InventorySiteLocationRefFullName { get; set; }

        public string SerialNumber { get; set; }
        public string LotNumber { get; set; }

        public string ServiceDate { get; set; }

        public string SalesTaxCodeRefListID { get; set; }
        public string SalesTaxCodeRefFullName { get; set; }
    }
}