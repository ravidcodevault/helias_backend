using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class InvoiceRet
    {
        public string TxnID { get; set; }
        public string TimeCreated { get; set; }
        public string TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string TxnNumber { get; set; }

        public string CustomerRefListID { get; set; }
        public string CustomerRefFullName { get; set; }

        public string ClassRefListID { get; set; }
        public string ClassRefFullName { get; set; }

        public string ARAccountRefListID { get; set; }
        public string ARAccountRefFullName { get; set; }

        public string TemplateRefListID { get; set; }
        public string TemplateRefFullName { get; set; }

        public string TxnDate { get; set; }
        public string RefNumber { get; set; }

        public string BillAddressAddr1 { get; set; }
        public string BillAddressAddr2 { get; set; }
        public string BillAddressAddr3 { get; set; }
        public string BillAddressAddr4 { get; set; }
        public string BillAddressAddr5 { get; set; }
        public string BillAddressCity { get; set; }
        public string BillAddressState { get; set; }
        public string BillAddressPostalCode { get; set; }
        public string BillAddressCountry { get; set; }
        public string BillAddressNote { get; set; }

        public string ShipAddressAddr1 { get; set; }
        public string ShipAddressAddr2 { get; set; }
        public string ShipAddressAddr3 { get; set; }
        public string ShipAddressAddr4 { get; set; }
        public string ShipAddressAddr5 { get; set; }
        public string ShipAddressCity { get; set; }
        public string ShipAddressState { get; set; }
        public string ShipAddressPostalCode { get; set; }
        public string ShipAddressCountry { get; set; }
        public string ShipAddressNote { get; set; }

        public string IsPending { get; set; }

        public string IsFinanceCharge { get; set; }
        public string PONumber { get; set; }
        public string TermsRefListID { get; set; }
        public string TermsRefFullName { get; set; }

        public string DueDate { get; set; }

        public string SalesRepRefListID { get; set; }
        public string SalesRepRefFullName { get; set; }
        public string FOB { get; set; }

        public string ShipDate { get; set; }

        public string ShipMethodRefListID { get; set; }
        public string ShipMethodRefFullName { get; set; }

        public string Subtotal { get; set; }

        public string ItemSalesTaxRefListID { get; set; }
        public string ItemSalesTaxRefFullName { get; set; }
        public string SalesTaxPercentage { get; set; }
        public string SalesTaxTotal { get; set; }

        public string AppliedAmount { get; set; }
        public string BalanceRemaining { get; set; }

        public string Memo { get; set; }
        public string IsPaid { get; set; }

        public string CustomerMsgRefListID { get; set; }
        public string CustomerMsgRefFullName { get; set; }

        public string IsToBePrinted { get; set; }
        public string IsToBeEmailed { get; set; }

        public string CustomerSalesTaxCodeRefListID { get; set; }
        public string CustomerSalesTaxCodeRefFullName { get; set; }
        //public string CustomFieldOther { get; set; }
        public string LinkToTxnID1 { get; set; }

        public List<InvoiceLineRet> LineRetList { get; set; }
    }




}