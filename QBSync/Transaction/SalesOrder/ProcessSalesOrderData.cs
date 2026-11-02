using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Data.SqlClient;
using System.Configuration;

namespace QB2DB_Sync
{
    public class ProcessSalesOrderData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public void InsertSalesOrderToDatabase(List<SalesOrderRet> salesOrderRetList)
        {
            DeleteExsitingSalesOrder(salesOrderRetList);
            InsertSalesOrder(salesOrderRetList);
        }

        public void DeleteExsitingSalesOrder(List<SalesOrderRet> salesOrderRetList)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (var t in salesOrderRetList)
                    {

                        using (SqlCommand cmd = new SqlCommand("Delete SalesOrder Where TxnID='" + t.TxnID + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("Delete SalesOrderLine Where TxnID='" + t.TxnID + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Deleted SalesOrder " + t.TxnDate + " " + t.TxnID + " " + t.CustomerRefFullName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception got throwned while Deleting SalesOrder", ex);
            }
        }

        private void InsertSalesOrder(List<SalesOrderRet> salesOrderRetList)
        {
            try
            {

                #region INSERT SQL QUERY
                string cmdText = "Insert into " +
                    "SalesOrder " +
                    "(TxnID,TimeCreated,TimeModified,EditSequence,TxnNumber, " +
                        "CustomerRefListID,CustomerRefFullName, " +
                        "ClassRefListID, ClassRefFullName, " +
                        "TemplateRefListID, TemplateRefFullName, " +

                        "TxnDate, RefNumber, " +

                        "BillAddressAddr1, BillAddressAddr2, BillAddressAddr3, " +
                        "BillAddressAddr4, BillAddressAddr5, " +
                        "BillAddressCity, BillAddressState, BillAddressPostalCode, " +
                        "BillAddressCountry, BillAddressNote, " +

                        "ShipAddressAddr1, ShipAddressAddr2, ShipAddressAddr3, " +
                        "ShipAddressAddr4, ShipAddressAddr5, " +
                        "ShipAddressCity, ShipAddressState, ShipAddressPostalCode, " +
                        "ShipAddressCountry, ShipAddressNote, " +

                        "PONumber, " +
                        "TermsRefListID, TermsRefFullName," +
                        "DueDate, " +
                        "SalesRepRefListID, SalesRepRefFullName, " +
                        "FOB, ShipDate, ShipMethodRefListID, ShipMethodRefFullName, " +
                        "Subtotal, " +
                        "ItemSalesTaxRefListID, ItemSalesTaxRefFullName, " +
                        "SalesTaxPercentage, SalesTaxTotal, " +
                        "TotalAmount, " +
                        "CurrencyRefListID, CurrencyRefFullName, " +
                        "ExchangeRate, TotalAmountInHomeCurrency, " +

                        "IsManuallyClosed, IsFullyInvoiced, " +
                        "Memo, " +
                        "CustomerMsgRefListID,CustomerMsgRefFullName," +
                        "IsToBePrinted, IsToBeEmailed, " +
                        "CustomerSalesTaxCodeRefListID, CustomerSalesTaxCodeRefFullName" +
                    ") " +
                    "VALUES " +
                    "(@TxnID,@TimeCreated,@TimeModified, @EditSequence,@TxnNumber, " +
                        "@CustomerRefListID,@CustomerRefFullName, " +
                        "@ClassRefListID, @ClassRefFullName, " +
                        "@TemplateRefListID, @TemplateRefFullName, " +
                        "@TxnDate,@RefNumber, " +

                        "@BillAddressAddr1, @BillAddressAddr2, @BillAddressAddr3, " +
                        "@BillAddressAddr4, @BillAddressAddr5, " +
                        "@BillAddressCity, @BillAddressState, @BillAddressPostalCode, " +
                        "@BillAddressCountry, @BillAddressNote, " +

                        "@ShipAddressAddr1, @ShipAddressAddr2, @ShipAddressAddr3, " +
                        "@ShipAddressAddr4, @ShipAddressAddr5, " +
                        "@ShipAddressCity, @ShipAddressState, @ShipAddressPostalCode, " +
                        "@ShipAddressCountry, @ShipAddressNote, " +

                        "@PONumber, " +
                        "@TermsRefListID, @TermsRefFullName," +
                        "@DueDate, " +
                        "@SalesRepRefListID, @SalesRepRefFullName, " +
                        "@FOB, @ShipDate, @ShipMethodRefListID, @ShipMethodRefFullName, " +
                        "@Subtotal, " +
                        "@ItemSalesTaxRefListID, @ItemSalesTaxRefFullName, " +
                        "@SalesTaxPercentage, @SalesTaxTotal, " +
                        "@TotalAmount, " +
                        "@CurrencyRefListID, @CurrencyRefFullName, " +
                        "@ExchangeRate, @TotalAmountInHomeCurrency, " +

                        "@IsManuallyClosed, @IsFullyInvoiced, " +
                        "@Memo, " +
                        "@CustomerMsgRefListID,@CustomerMsgRefFullName," +
                        "@IsToBePrinted, @IsToBeEmailed, " +
                    "@CustomerSalesTaxCodeRefListID, @CustomerSalesTaxCodeRefFullName" +
                    " )";
                #endregion

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //#region INSERT SQL PARAMETER
                    int count = 0;
                    int totalRecords = salesOrderRetList.Count;

                    while (count < totalRecords)
                    {
                        int remainingRecords = totalRecords - count;
                        int currentBatchSize = Math.Min(remainingRecords, Constants.BatchSize);

                        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                        {
                            for (int i = count; i < count + currentBatchSize; i++)
                            {
                                var t = salesOrderRetList[i];
                                #region parameters
                                SQLHelper.AddWithValue(cmd, "@TxnID", t.TxnID);
                                SQLHelper.AddWithValue(cmd, "@TimeCreated", t.TimeCreated);
                                SQLHelper.AddWithValue(cmd, "@TimeModified", t.TimeModified);
                                SQLHelper.AddWithValue(cmd, "@EditSequence", t.EditSequence);
                                SQLHelper.AddWithValue(cmd, "@TxnNumber", t.TxnNumber);

                                SQLHelper.AddWithValue(cmd, "@CustomerRefListID", t.CustomerRefListID);
                                SQLHelper.AddWithValue(cmd, "@CustomerRefFullName", t.CustomerRefFullName);
                                SQLHelper.AddWithValue(cmd, "@ClassRefListID", t.ClassRefListID);
                                SQLHelper.AddWithValue(cmd, "@ClassRefFullName", t.ClassRefFullName);
                                SQLHelper.AddWithValue(cmd, "@TemplateRefListID", t.TemplateRefListID);
                                SQLHelper.AddWithValue(cmd, "@TemplateRefFullName", t.TemplateRefFullName);

                                SQLHelper.AddWithValue(cmd, "@TxnDate", t.TxnDate);
                                SQLHelper.AddWithValue(cmd, "@RefNumber", t.RefNumber);

                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr1", t.BillAddressAddr1);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr2", t.BillAddressAddr2);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr3", t.BillAddressAddr3);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr4", t.BillAddressAddr4);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr5", t.BillAddressAddr5);
                                SQLHelper.AddWithValue(cmd, "@BillAddressCity", t.BillAddressCity);
                                SQLHelper.AddWithValue(cmd, "@BillAddressState", t.BillAddressState);
                                SQLHelper.AddWithValue(cmd, "@BillAddressPostalCode", t.BillAddressPostalCode);
                                SQLHelper.AddWithValue(cmd, "@BillAddressCountry", t.BillAddressCountry);
                                SQLHelper.AddWithValue(cmd, "@BillAddressNote", t.BillAddressNote);

                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr1", t.ShipAddressAddr1);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr2", t.ShipAddressAddr2);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr3", t.ShipAddressAddr3);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr4", t.ShipAddressAddr4);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr5", t.ShipAddressAddr5);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressCity", t.ShipAddressCity);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressState", t.ShipAddressState);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressPostalCode", t.ShipAddressPostalCode);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressCountry", t.ShipAddressCountry);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressNote", t.ShipAddressNote);

                                SQLHelper.AddWithValue(cmd, "@PONumber", t.PONumber);
                                SQLHelper.AddWithValue(cmd, "@TermsRefListID", t.TermsRefListID);
                                SQLHelper.AddWithValue(cmd, "@TermsRefFullName", t.TermsRefFullName);

                                SQLHelper.AddWithValue(cmd, "@DueDate", t.DueDate);

                                SQLHelper.AddWithValue(cmd, "@SalesRepRefListID", t.SalesRepRefListID);
                                SQLHelper.AddWithValue(cmd, "@SalesRepRefFullName", t.SalesRepRefFullName);

                                SQLHelper.AddWithValue(cmd, "@FOB", t.FOB);
                                SQLHelper.AddWithValue(cmd, "@ShipDate", t.ShipDate);
                                SQLHelper.AddWithValue(cmd, "@ShipMethodRefListID", t.ShipMethodRefListID);
                                SQLHelper.AddWithValue(cmd, "@ShipMethodRefFullName", t.ShipMethodRefFullName);

                                SQLHelper.AddWithValue(cmd, "@Subtotal", t.Subtotal);

                                SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefListID", t.ItemSalesTaxRefListID);
                                SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefFullName", t.ItemSalesTaxRefFullName);
                                SQLHelper.AddWithValue(cmd, "@SalesTaxPercentage", t.SalesTaxPercentage);
                                SQLHelper.AddWithValue(cmd, "@SalesTaxTotal", t.SalesTaxTotal);

                                SQLHelper.AddWithValue(cmd, "@TotalAmount", t.TotalAmount);

                                SQLHelper.AddWithValue(cmd, "@CurrencyRefListID", t.CurrencyRefListID);
                                SQLHelper.AddWithValue(cmd, "@CurrencyRefFullName", t.CurrencyRefFullName);
                                SQLHelper.AddWithValue(cmd, "@ExchangeRate", t.ExchangeRate);
                                SQLHelper.AddWithValue(cmd, "@TotalAmountInHomeCurrency", t.TotalAmountInHomeCurrency);


                                SQLHelper.AddWithValue(cmd, "@IsManuallyClosed", t.IsManuallyClosed);
                                SQLHelper.AddWithValue(cmd, "@IsFullyInvoiced", t.IsFullyInvoiced);

                                SQLHelper.AddWithValue(cmd, "@Memo", t.Memo);
                                SQLHelper.AddWithValue(cmd, "@CustomerMsgRefListID", t.CustomerMsgRefListID);
                                SQLHelper.AddWithValue(cmd, "@CustomerMsgRefFullName", t.CustomerMsgRefFullName);

                                SQLHelper.AddWithValue(cmd, "@IsToBePrinted", t.IsToBePrinted);
                                SQLHelper.AddWithValue(cmd, "@IsToBeEmailed", t.IsToBeEmailed);

                                SQLHelper.AddWithValue(cmd, "@CustomerSalesTaxCodeRefListID", t.CustomerSalesTaxCodeRefListID);
                                SQLHelper.AddWithValue(cmd, "@CustomerSalesTaxCodeRefFullName", t.CustomerSalesTaxCodeRefFullName);

                                #endregion

                                cmd.ExecuteNonQuery();

                                //foreach (var line in t.LineRetList)
                                //{
                                if (t.LineRetList.Count > 0)
                                {
                                    InsertSalesOrderLine(conn, t.LineRetList);
                                }
                                //}
                                Console.WriteLine("Added SalesOrder : " + t.TimeCreated + ": " + t.TimeModified + ": " + t.CustomerRefFullName + ": " + t.RefNumber);
                                cmd.Parameters.Clear();
                            }
                        }
                        count += currentBatchSize;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception got throwned while Inserting SalesOrder", ex);
            }
        }

        public static void InsertSalesOrderLine(SqlConnection conn, List<SalesOrderLineRet> retLine)
        {
            #region INSERT SQL QUERY
            string lineCmdText = "Insert into " +
            "SalesOrderLine (" +
                "TxnID, EditSequence, TxnLineID, " +
                "ItemRefListID, ItemRefFullName, " +

                "[Desc], " +    //"Desc, " +{"Incorrect syntax near the keyword 'Desc'."}
                "Quantity, " +

                "UnitOfMeasure, " +
                "OverrideUOMSetRefListID, " +
                "OverrideUOMSetRefFullName, " +

                "Rate, RatePercent, " +
                "Amount, " +

                "InventorySiteRefListID, " +
                "InventorySiteRefFullName, " +
                "InventorySiteLocationRefListID, " +
                "InventorySiteLocationRefFullName, " +

                "SerialNumber, LotNumber, " +

                "SalesTaxCodeRefListID, SalesTaxCodeRefFullName, " +
                "Invoiced, IsManuallyClosed) " +

            "VALUES (" +
                "@TxnID, @EditSequence, @TxnLineID, " +
                "@ItemRefListID, @ItemRefFullName," +
                "@Desc, " +
                "@Quantity, " +

                "@UnitOfMeasure, " +
                "@OverrideUOMSetRefListID, " +
                "@OverrideUOMSetRefFullName, " +

                "@Rate, @RatePercent, " +
                "@Amount, " +

                "@InventorySiteRefListID, " +
                "@InventorySiteRefFullName, " +
                "@InventorySiteLocationRefListID, " +
                "@InventorySiteLocationRefFullName, " +

                "@SerialNumber, @LotNumber, " +

                "@SalesTaxCodeRefListID, @SalesTaxCodeRefFullName, " +
                "@Invoiced, @IsManuallyClosed)";
            #endregion         

            int count = 0;
            int totalRecords = retLine.Count;

            while (count < totalRecords)
            {
                int remainingRecords = totalRecords - count;
                int currentBatchSize = Math.Min(remainingRecords, Constants.BatchSize);

                using (SqlCommand lineCmd = new SqlCommand(lineCmdText, conn))
                {
                    for (int i = count; i < count + currentBatchSize; i++)
                    {
                        var line = retLine[i];
                        #region parameters
                        SQLHelper.AddWithValue(lineCmd, "@TxnID", line.TxnID);
                        SQLHelper.AddWithValue(lineCmd, "@EditSequence", line.EditSequence);
                        SQLHelper.AddWithValue(lineCmd, "@TxnLineID", line.TxnLineID);
                        SQLHelper.AddWithValue(lineCmd, "@ItemRefListID", line.ItemRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@ItemRefFullName", line.ItemRefFullName);
                        SQLHelper.AddWithValue(lineCmd, "@Desc", line.Desc);
                        SQLHelper.AddWithValue(lineCmd, "@Quantity", line.Quantity);

                        SQLHelper.AddWithValue(lineCmd, "@UnitOfMeasure", line.UnitOfMeasure);
                        SQLHelper.AddWithValue(lineCmd, "@OverrideUOMSetRefListID", line.OverrideUOMSetRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@OverrideUOMSetRefFullName", line.OverrideUOMSetRefFullName);

                        SQLHelper.AddWithValue(lineCmd, "@Rate", line.Rate);
                        SQLHelper.AddWithValue(lineCmd, "@RatePercent", line.RatePercent);
                        SQLHelper.AddWithValue(lineCmd, "@Amount", line.Amount);

                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteRefListID", line.InventorySiteRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteRefFullName", line.InventorySiteRefFullName);
                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteLocationRefListID", line.InventorySiteLocationRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteLocationRefFullName", line.InventorySiteLocationRefFullName);

                        SQLHelper.AddWithValue(lineCmd, "@SerialNumber", line.SerialNumber);
                        SQLHelper.AddWithValue(lineCmd, "@LotNumber", line.LotNumber);

                        SQLHelper.AddWithValue(lineCmd, "@SalesTaxCodeRefListID", line.SalesTaxCodeRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@SalesTaxCodeRefFullName", line.SalesTaxCodeRefFullName);

                        SQLHelper.AddWithValue(lineCmd, "@Invoiced", line.Invoiced);
                        SQLHelper.AddWithValue(lineCmd, "@IsManuallyClosed", line.IsManuallyClosed);

                        #endregion
                        lineCmd.ExecuteNonQuery();
                        lineCmd.Parameters.Clear();
                    }
                }
                count += currentBatchSize;
            }
        }
    }
}