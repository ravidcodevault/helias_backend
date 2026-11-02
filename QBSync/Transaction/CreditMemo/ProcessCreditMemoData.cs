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
    public class ProcessCreditMemoData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public void InsertCreditMemoToDatabase(List<CreditMemoRet> creditMemoRetList)
        {
            DeleteExsitingCreditMemo(creditMemoRetList);
            InsertCreditMemo(creditMemoRetList);
        }

        public void DeleteExsitingCreditMemo(List<CreditMemoRet> creditMemoRetList)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (var t in creditMemoRetList)
                    {
                        using (SqlCommand cmd = new SqlCommand("Delete CreditMemo Where TxnID='" + t.TxnID + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = new SqlCommand("Delete CreditMemoLine Where TxnID='" + t.TxnID + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Cleaned CreditMemo " + t.TxnID + " " + t.CustomerRefFullName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception got throwned while Deleting Credit Memo", ex);
            }
        }

        private void InsertCreditMemo(List<CreditMemoRet> creditMemoRetList)
        {
            try
            {
                #region INSERT SQL QUERY
                string cmdText = "Insert into " +
                    "CreditMemo " +
                    "(TxnID,TimeCreated,TimeModified,EditSequence,TxnNumber, " +
                        "CustomerRefListID,CustomerRefFullName, " +
                        "ARAccountRefListID, ARAccountRefFullName, " +
                        "TemplateRefListID, TemplateRefFullName, " +
                        "TxnDate, RefNumber, " +
                        "BillAddressAddr1, BillAddressAddr2, BillAddressAddr3, BillAddressAddr4, BillAddressAddr5, " +
                        "BillAddressCity, BillAddressState, BillAddressPostalCode, BillAddressCountry, BillAddressNote, " +
                        "ShipAddressAddr1, ShipAddressAddr2, ShipAddressAddr3, ShipAddressAddr4, ShipAddressAddr5, " +
                        "ShipAddressCity, ShipAddressState, ShipAddressPostalCode, ShipAddressCountry, ShipAddressNote, " +
                        "IsPending, DueDate, ShipDate, " +
                        "Subtotal, " +
                        "ItemSalesTaxRefListID, ItemSalesTaxRefFullName, " +
                        "SalesTaxPercentage, SalesTaxTotal, TotalAmount, CreditRemaining," +
                        "IsToBePrinted, IsToBeEmailed, " +
						"CustomerSalesTaxCodeRefListID, CustomerSalesTaxCodeRefFullName, SalesRepRefFullName, SalesRepRefListID" +
                    ") " +
                    "VALUES " +
                    "(@TxnID,@TimeCreated,@TimeModified, @EditSequence,@TxnNumber, " +
                        "@CustomerRefListID,@CustomerRefFullName, " +
                        "@ARAccountRefListID, @ARAccountRefFullName, " +
                        "@TemplateRefListID, @TemplateRefFullName, " +
                        "@TxnDate,@RefNumber, " +
                        "@BillAddressAddr1, @BillAddressAddr2, @BillAddressAddr3, @BillAddressAddr4, @BillAddressAddr5, " +
                        "@BillAddressCity, @BillAddressState, @BillAddressPostalCode, @BillAddressCountry, @BillAddressNote, " +
                        "@ShipAddressAddr1, @ShipAddressAddr2, @ShipAddressAddr3, @ShipAddressAddr4, @ShipAddressAddr5, " +
                        "@ShipAddressCity, @ShipAddressState, @ShipAddressPostalCode, @ShipAddressCountry, @ShipAddressNote, " +
                        "@IsPending, @DueDate, @ShipDate, " +
                        "@Subtotal, " +
                        "@ItemSalesTaxRefListID, @ItemSalesTaxRefFullName, " +
                        "@SalesTaxPercentage, @SalesTaxTotal, @TotalAmount, @CreditRemaining," +
                        "@IsToBePrinted, @IsToBeEmailed, " +
					"@CustomerSalesTaxCodeRefListID, @CustomerSalesTaxCodeRefFullName, @SalesRepRefFullName, @SalesRepRefListID" +
                    " )";
                #endregion

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    #region INSERT SQL PARAMETER
                    int count = 0;
                    int totalRecords = creditMemoRetList.Count;

                    while (count < totalRecords)
                    {
                        int remainingRecords = totalRecords - count;
                        int currentBatchSize = Math.Min(remainingRecords, Constants.BatchSize);

                        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                        {
                            for (int i = count; i < count + currentBatchSize; i++)
                            {
                                var t = creditMemoRetList[i];
                                #region parameters
                                cmd.Parameters.AddWithValue("@TxnID", t.TxnID);
                                cmd.Parameters.AddWithValue("@TimeCreated", t.TimeCreated);
                                cmd.Parameters.AddWithValue("@TimeModified", t.TimeModified);
                                cmd.Parameters.AddWithValue("@EditSequence", t.EditSequence);
                                SQLHelper.AddWithValue(cmd, "@TxnNumber", t.TxnNumber);

                                SQLHelper.AddWithValue(cmd, "@CustomerRefListID", t.CustomerRefListID);
                                SQLHelper.AddWithValue(cmd, "@CustomerRefFullName", t.CustomerRefFullName);
                                SQLHelper.AddWithValue(cmd, "@ARAccountRefListID", t.ARAccountRefListID);
                                SQLHelper.AddWithValue(cmd, "@ARAccountRefFullName", t.ARAccountRefFullName);
                                SQLHelper.AddWithValue(cmd, "@TemplateRefListID", t.TemplateRefListID);
                                SQLHelper.AddWithValue(cmd, "@TemplateRefFullName", t.TemplateRefFullName);

                                SQLHelper.AddWithValue(cmd, "@TxnDate", t.TxnDate);
                                SQLHelper.AddWithValue(cmd, "@RefNumber", t.RefNumber);

                                // SQLHelper.AddWithAddressValue(cmd, "Bill", t);
                                // SQLHelper.AddWithAddressValue(cmd, "Ship", t);
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

                                SQLHelper.AddWithValue(cmd, "@IsPending", t.IsPending);
                                SQLHelper.AddWithValue(cmd, "@DueDate", t.DueDate);
                                SQLHelper.AddWithValue(cmd, "@ShipDate", t.ShipDate);
                                SQLHelper.AddWithValue(cmd, "@Subtotal", t.Subtotal);

                                SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefListID", t.ItemSalesTaxRefListID);
                                SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefFullName", t.ItemSalesTaxRefFullName);

                                SQLHelper.AddWithValue(cmd, "@SalesTaxPercentage", t.SalesTaxPercentage);
                                SQLHelper.AddWithValue(cmd, "@SalesTaxTotal", t.SalesTaxTotal);

                                SQLHelper.AddWithValue(cmd, "@TotalAmount", t.TotalAmount);

                                SQLHelper.AddWithValue(cmd, "@CreditRemaining", t.CreditRemaining);

                                SQLHelper.AddWithValue(cmd, "@IsToBePrinted", t.IsToBePrinted);
                                SQLHelper.AddWithValue(cmd, "@IsToBeEmailed", t.IsToBeEmailed);

                                SQLHelper.AddWithValue(cmd, "@CustomerSalesTaxCodeRefListID", t.CustomerSalesTaxCodeRefListID);
                                SQLHelper.AddWithValue(cmd, "@CustomerSalesTaxCodeRefFullName", t.CustomerSalesTaxCodeRefFullName);

								SQLHelper.AddWithValue(cmd, "@SalesRepRefFullName", t.SalesRepRefFullName);
								SQLHelper.AddWithValue(cmd, "@SalesRepRefListID", t.SalesRepRefListID);
								#endregion
								cmd.ExecuteNonQuery();

                                if (t.LineRetList.Count > 0)
                                {
                                    //foreach (var line in t.LineRetList)
                                    //{
                                    InsertCreditMemoLine(conn, t.LineRetList);
                                    //}
                                }
                                Console.WriteLine("Added CreditMemo : " + t.TimeCreated + ": " + t.TimeModified + ": " + t.CustomerRefFullName + ": " + t.RefNumber);
                                cmd.Parameters.Clear();
                            }
                        }
                        count += currentBatchSize;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception got throwned while Inserting CreditMemo", ex);
            }
        }

        public static void InsertCreditMemoLine(SqlConnection conn, List<CreditMemoLineRet> retLine)
        {
            #region INSERT SQL QUERY
            string lineCmdText = "Insert into " +
                        "CreditMemoLine (" +
                            "TxnID, EditSequence, TxnLineID, " +
                            "ItemRefListID, ItemRefFullName, " +
                            "[Desc], " +
                            "Quantity, " +
                            "UnitOfMeasure, OverrideUOMSetRefListID, OverrideUOMSetRefFullName, " +
                            "Rate, RatePercent, " +
                            "ClassRefListID, ClassRefFullName, " +
                            "Amount, " +
                            "InventorySiteRefListID, InventorySiteRefFullName, " +
                            "InventorySiteLocationRefListID, InventorySiteLocationRefFullName, " +
                            "SerialNumber, LotNumber, " +
                            "ServiceDate, " +
                            "SalesTaxCodeRefListID, SalesTaxCodeRefFullName) " +

                        "VALUES (" +
                        "@TxnID, @EditSequence, @TxnLineID, " +
                        "@ItemRefListID, @ItemRefFullName," +
                        "@Desc, " +
                        "@Quantity, " +
                        "@UnitOfMeasure, @OverrideUOMSetRefListID, @OverrideUOMSetRefFullName, " +
                        "@Rate, @RatePercent, " +
                        "@ClassRefListID, @ClassRefFullName, " +
                        "@Amount, " +
                        "@InventorySiteRefListID, @InventorySiteRefFullName, " +
                        "@InventorySiteLocationRefListID, @InventorySiteLocationRefFullName, " +
                        "@SerialNumber, @LotNumber, " +
                        "@ServiceDate, " +
                        "@SalesTaxCodeRefListID, @SalesTaxCodeRefFullName)";

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
                        SQLHelper.AddWithValue(lineCmd, "@ClassRefListID", line.ClassRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@ClassRefFullName", line.ClassRefFullName);
                        SQLHelper.AddWithValue(lineCmd, "@Amount", line.Amount);
                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteRefListID", line.InventorySiteRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteRefFullName", line.InventorySiteRefFullName);
                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteLocationRefListID", line.InventorySiteLocationRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@InventorySiteLocationRefFullName", line.InventorySiteLocationRefFullName);
                        SQLHelper.AddWithValue(lineCmd, "@SerialNumber", line.SerialNumber);
                        SQLHelper.AddWithValue(lineCmd, "@LotNumber", line.LotNumber);
                        SQLHelper.AddWithValue(lineCmd, "@ServiceDate", line.ServiceDate);
                        SQLHelper.AddWithValue(lineCmd, "@SalesTaxCodeRefListID", line.SalesTaxCodeRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@SalesTaxCodeRefFullName", line.SalesTaxCodeRefFullName);
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