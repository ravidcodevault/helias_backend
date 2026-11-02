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
    public class ProcessReceivePaymentData
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public void InsertReceivePaymentToDatabase(List<ReceivePaymentRet> receivePaymentRetList)
        {
            DeleteExsitingReceivePayment(receivePaymentRetList);
            InsertReceivePayment(receivePaymentRetList);
        }

        public void DeleteExsitingReceivePayment(List<ReceivePaymentRet> receivePaymentRetList)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (var t in receivePaymentRetList)
                    {
                        using (SqlCommand cmd = new SqlCommand("Delete ReceivePayment Where TxnID='" + t.TxnID + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("Delete ReceivePaymentLine Where ReceivePaymentTxnID='" + t.TxnID + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Cleaned ReceivePayment " + t.RefNumber + " " + t.CustomerRefFullName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception got throwned while Deleting Receive Payment", ex);
            }
        }

        private void InsertReceivePayment(List<ReceivePaymentRet> receivePaymentRetList)
        {
            try
            {
                #region INSERT SQL QUERY
                string cmdText = "Insert into " +
                        "ReceivePayment " +
                        "(TxnID,TimeCreated,TimeModified,EditSequence,TxnNumber, " +
                            "CustomerRefListID,CustomerRefFullName, " +
                            "ARAccountRefListID, ARAccountRefFullName, " +
                            "TxnDate, RefNumber, TotalAmount," +
                            "PaymentMethodRefListID, PaymentMethodRefFullName, " +
                            "DepositToAccountRefListID, DepositToAccountRefFullName, " +
                            "UnusedPayment, UnusedCredits " +
                        ") " +
                        "VALUES " +
                        "(@TxnID,@TimeCreated,@TimeModified, @EditSequence,@TxnNumber, " +
                            "@CustomerRefListID,@CustomerRefFullName, " +
                            "@ARAccountRefListID, @ARAccountRefFullName, " +
                            "@TxnDate,@RefNumber, @TotalAmount, " +
                            "@PaymentMethodRefListID, @PaymentMethodRefFullName, " +
                            "@DepositToAccountRefListID, @DepositToAccountRefFullName, " +
                            "@UnusedPayment, @UnusedCredits " +
                        " )";
                #endregion

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    #region INSERT SQL PARAMETER
                    int count = 0;
                    int totalRecords = receivePaymentRetList.Count;

                    while (count < totalRecords)
                    {
                        int remainingRecords = totalRecords - count;
                        int currentBatchSize = Math.Min(remainingRecords, Constants.BatchSize);

                        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                        {
                            for (int i = count; i < count + currentBatchSize; i++)
                            {
                                var t = receivePaymentRetList[i];
                                #region parameters
                                SQLHelper.AddWithValue(cmd, "@TxnID", t.TxnID);
                                SQLHelper.AddWithValue(cmd, "@TimeCreated", t.TimeCreated);
                                SQLHelper.AddWithValue(cmd, "@TimeModified", t.TimeModified);
                                SQLHelper.AddWithValue(cmd, "@EditSequence", t.EditSequence);
                                SQLHelper.AddWithValue(cmd, "@TxnNumber", t.TxnNumber);

                                SQLHelper.AddWithValue(cmd, "@CustomerRefListID", t.CustomerRefListID);
                                SQLHelper.AddWithValue(cmd, "@CustomerRefFullName", t.CustomerRefFullName);
                                SQLHelper.AddWithValue(cmd, "@ARAccountRefListID", t.ARAccountRefListID);
                                SQLHelper.AddWithValue(cmd, "@ARAccountRefFullName", t.ARAccountRefFullName);

                                SQLHelper.AddWithValue(cmd, "@TxnDate", t.TxnDate);
                                SQLHelper.AddWithValue(cmd, "@RefNumber", t.RefNumber);

                                SQLHelper.AddWithValue(cmd, "@TotalAmount", t.TotalAmount);

                                SQLHelper.AddWithValue(cmd, "@PaymentMethodRefListID", t.PaymentMethodRefListID);
                                SQLHelper.AddWithValue(cmd, "@PaymentMethodRefFullName", t.PaymentMethodRefFullName);

                                SQLHelper.AddWithValue(cmd, "@DepositToAccountRefListID", t.DepositToAccountRefListID);
                                SQLHelper.AddWithValue(cmd, "@DepositToAccountRefFullName", t.DepositToAccountRefFullName);

                                SQLHelper.AddWithValue(cmd, "@UnusedPayment", t.UnusedPayment);
                                SQLHelper.AddWithValue(cmd, "@UnusedCredits", t.UnusedCredits);


                                #endregion
                                cmd.ExecuteNonQuery();


                                //foreach (var line in t.AppliedTxnRetList)
                                //{
                                if (t.AppliedTxnRetList.Count > 0)
                                {
                                    InsertAppliedToTxn(conn, t.AppliedTxnRetList, t.TxnID);
                                }
                                //}
                                Console.WriteLine("Added ReceivePayment : " + t.TimeCreated + ": " + t.TimeModified + ": " + t.CustomerRefFullName + ": " + t.RefNumber);
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
                throw new DataProcessingException("Exception got throwned while Inserting Receive Payment", ex);
            }
        }

        public static void InsertAppliedToTxn(SqlConnection conn, List<AppliedToTxnRet> retLine, string receivePaymentTxnID)
        {
            #region INSERT SQL QUERY
            string lineCmdText = "Insert into " +
            "ReceivePaymentLine (" +
                "ReceivePaymentTxnID, TxnID, TxnType, " +
                "TxnDate, RefNumber, " +
                "BalanceRemaining, " +
                "Amount, " +
                "DiscountAmount, " +
                "DiscountAccountRefListID, DiscountAccountRefFullName, " +
                "DiscountClassRefListID, DiscountClassRefFullName) " +

            "VALUES (" +
                "@ReceivePaymentTxnID, @TxnID, @TxnType, " +
                "@TxnDate, @RefNumber," +
                "@BalanceRemaining, " +
                "@Amount, " +
                "@DiscountAmount, " +
                "@DiscountAccountRefListID, @DiscountAccountRefFullName, " +
                "@DiscountClassRefListID, @DiscountClassRefFullName)";
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
                        lineCmd.Parameters.AddWithValue("@ReceivePaymentTxnID", receivePaymentTxnID);
                        lineCmd.Parameters.AddWithValue("@TxnID", line.TxnID);
                        lineCmd.Parameters.AddWithValue("@TxnType", line.TxnType);
                        SQLHelper.AddWithValue(lineCmd, "@TxnDate", line.TxnDate);
                        SQLHelper.AddWithValue(lineCmd, "@RefNumber", line.RefNumber);
                        SQLHelper.AddWithValue(lineCmd, "@BalanceRemaining", line.BalanceRemaining);
                        SQLHelper.AddWithValue(lineCmd, "@Amount", line.Amount);
                        SQLHelper.AddWithValue(lineCmd, "@DiscountAmount", line.DiscountAmount);
                        SQLHelper.AddWithValue(lineCmd, "@DiscountAccountRefListID", line.DiscountAccountRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@DiscountAccountRefFullName", line.DiscountAccountRefFullName);
                        SQLHelper.AddWithValue(lineCmd, "@DiscountClassRefListID", line.DiscountClassRefListID);
                        SQLHelper.AddWithValue(lineCmd, "@DiscountClassRefFullName", line.DiscountClassRefFullName);
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

