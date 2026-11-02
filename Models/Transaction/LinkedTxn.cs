using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class LinkedTxn
    {
        public string TxnID { get; set; }
        public string TxnType { get; set; }
        public string TxnDate { get; set; }
        public string RefNumber { get; set; }
        public string LinkType { get; set; }
        public string Amount { get; set; }
    }
}