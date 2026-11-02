using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class AppliedToTxnRet
    {
        public string TxnID { get; set; }
        public string TxnType { get; set; }
        public string TxnDate { get; set; }
        public string RefNumber { get; set; }
        public string BalanceRemaining { get; set; }
        public string Amount { get; set; }
        public string DiscountAmount { get; set; }
        public string DiscountAccountRefListID { get; set; }
        public string DiscountAccountRefFullName { get; set; }
        public string DiscountClassRefListID { get; set; }
        public string DiscountClassRefFullName { get; set; }
        public List<LinkedTxn> LinkedTxnList { get; set; }

    }
}