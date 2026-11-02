using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class ReceivePaymentRet
    {
        public string TxnID { get; set; }
        public string TimeCreated { get; set; }
        public string TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string TxnNumber { get; set; }
        public string CustomerRefListID { get; set; }
        public string CustomerRefFullName { get; set; }
        public string ARAccountRefListID { get; set; }
        public string ARAccountRefFullName { get; set; }
        public string TxnDate { get; set; }
        public string RefNumber { get; set; }

        public string TotalAmount { get; set; }

        public string PaymentMethodRefListID { get; set; }
        public string PaymentMethodRefFullName { get; set; }

        public string DepositToAccountRefListID { get; set; }
        public string DepositToAccountRefFullName { get; set; }

        public string UnusedPayment { get; set; }
        public string UnusedCredits { get; set; }

        public List<AppliedToTxnRet> AppliedTxnRetList { get; set; }

    }
    
}