using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class Constants
    {
        public const string RequestDateFormat = "yyyy-MM-dd";
        public const string CustomerTypeName = "Customer";
        public const string ItemTypeName = "Item";
        public const string CreditMemoTypeName = "CreditMemo";
        public const string SalesOrderTypeName = "SalesOrder";
        public const string InvoiceTypeName = "Invoice";
        public const string SalesReceiptTypeName = "SalesReceipt";
        public const string ReceivePaymentTypeName = "ReceivePayment";
        public const string ConnectionStringType = "DefaultConnection";
        public static int BatchSize = 500;
    }
}
