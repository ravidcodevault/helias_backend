
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Reflection;
//using Microsoft.Extensions.Configuration;

namespace QB2DB_Sync
{
    public class ProcessCustomerData
    {
        //private readonly IConfiguration _config;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;


        //public ProcessCustomerData(IConfiguration config)
        //{
        //    _config = config;
        //    connectionString = _config.GetConnectionString(Constants.ConnectionStringType);
        //}

        public void InsertCustomersToDatabase(List<CustomerRet> customerRetList)
        {
            DeleteExsitingCustomers(customerRetList);
            InsertCustomers(customerRetList);
        }

        private void DeleteExsitingCustomers(List<CustomerRet> customerRetList)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Define the batch size
                    int batchSize = 100; // Choose an appropriate batch size

                    // Calculate the number of batches needed
                    int totalCustomers = customerRetList.Count;
                    int numBatches = (int)Math.Ceiling((double)totalCustomers / batchSize);

                    // Loop through batches
                    for (int i = 0; i < numBatches; i++)
                    {
                        // Create a batch of customers for deletion
                        var batchCustomers = customerRetList.Skip(i * batchSize).Take(batchSize).ToList();

                        // Construct the SQL query for the batch
                        StringBuilder queryBuilder = new StringBuilder();
                        queryBuilder.Append("DELETE FROM Customer WHERE ListID IN (");
                        for (int j = 0; j < batchCustomers.Count; j++)
                        {
                            queryBuilder.Append("@ListID" + j);
                            if (j < batchCustomers.Count - 1)
                            {
                                queryBuilder.Append(", ");
                            }
                        }
                        queryBuilder.Append(")");

                        // Execute the batch query
                        using (SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn))
                        {
                            // Add parameters for ListID values
                            for (int j = 0; j < batchCustomers.Count; j++)
                            {
                                SQLHelper.AddWithValue(cmd, "@ListID" + j, batchCustomers[j].ListID);
                            }

                            // Execute the batch query
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception occurred while deleting customers", ex);
            }
        }

        private void InsertCustomers(List<CustomerRet> customerRetList)
        {
            try
            {
                #region INSERT SQL QUERY
                string cmdText = @"INSERT INTO 
        Customer 
        (ListID, 
            TimeCreated, TimeModified, EditSequence, 
            Name, FullName, IsActive, 
            ClassRefListID, ClassRefFullName,
            ParentRefListID, ParentRefFullName, 
            Sublevel, 
            CompanyName, 
            Salutation, FirstName, MiddleName, LastName, 
            BillAddressAddr1, BillAddressAddr2,BillAddressAddr3, BillAddressAddr4, BillAddressAddr5, 
            BillAddressCity, BillAddressState, BillAddressPostalCode, 
            BillAddressCountry, BillAddressNote, 
            BillAddressBlockAddr1, BillAddressBlockAddr2,BillAddressBlockAddr3, BillAddressBlockAddr4, BillAddressBlockAddr5, 
            ShipAddressAddr1, ShipAddressAddr2, ShipAddressAddr3, ShipAddressAddr4, ShipAddressAddr5, 
            ShipAddressCity, ShipAddressState, ShipAddressPostalCode, 
            ShipAddressCountry, ShipAddressNote, 
            ShipAddressBlockAddr1, ShipAddressBlockAddr2, ShipAddressBlockAddr3, ShipAddressBlockAddr4, ShipAddressBlockAddr5, 
            Phone, AltPhone, Fax, Email, Cc, Contact, AltContact, 
            CustomerTypeRefListID, CustomerTypeRefFullName, 
            TermsRefListID, TermsRefFullName, 
            SalesRepRefListID, SalesRepRefFullName, 
            Balance, TotalBalance, 
            SalesTaxCodeRefListID, SalesTaxCodeRefFullName, 
            ItemSalesTaxRefListID, ItemSalesTaxRefFullName, 
            ResaleNumber, AccountNumber, CreditLimit, 
            PreferredPaymentMethodRefListID, PreferredPaymentMethodRefFullName, 
            JobStatus, 
            PreferredDeliveryMethod, 
            PriceLevelRefListID, PriceLevelRefFullName, 
            CurrencyRefListID, CurrencyRefFullName 
        ) 
        VALUES 
        (   @ListID, 
            @TimeCreated, @TimeModified, @EditSequence, 
            @Name, @FullName, @IsActive, 
            @ClassRefListID, @ClassRefFullName, 
            @ParentRefListID, @ParentRefFullName, 
            @Sublevel, 
            @CompanyName, 
            @Salutation, @FirstName, @MiddleName, @LastName, 
            @BillAddressAddr1, @BillAddressAddr2, @BillAddressAddr3, @BillAddressAddr4, @BillAddressAddr5, 
            @BillAddressCity, @BillAddressState, @BillAddressPostalCode, 
            @BillAddressCountry, @BillAddressNote, 
            @BillAddressBlockAddr1, @BillAddressBlockAddr2, @BillAddressBlockAddr3, @BillAddressBlockAddr4, @BillAddressBlockAddr5, 
            @ShipAddressAddr1, @ShipAddressAddr2, @ShipAddressAddr3, @ShipAddressAddr4, @ShipAddressAddr5, 
            @ShipAddressCity, @ShipAddressState, @ShipAddressPostalCode, 
            @ShipAddressCountry, @ShipAddressNote, 
            @ShipAddressBlockAddr1, @ShipAddressBlockAddr2, @ShipAddressBlockAddr3, @ShipAddressBlockAddr4, @ShipAddressBlockAddr5, 
            @Phone, @AltPhone, @Fax, @Email, @Cc, @Contact, @AltContact, 
            @CustomerTypeRefListID, @CustomerTypeRefFullName, 
            @TermsRefListID, @TermsRefFullName, 
            @SalesRepRefListID, @SalesRepRefFullName, 
            @Balance, @TotalBalance, 
            @SalesTaxCodeRefListID, @SalesTaxCodeRefFullName, 
            @ItemSalesTaxRefListID, @ItemSalesTaxRefFullName, 
            @ResaleNumber, @AccountNumber, @CreditLimit, 
            @PreferredPaymentMethodRefListID, @PreferredPaymentMethodRefFullName, 
            @JobStatus, 
            @PreferredDeliveryMethod, 
            @PriceLevelRefListID, @PriceLevelRefFullName, 
            @CurrencyRefListID, @CurrencyRefFullName)";
                #endregion

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int count = 0;
                    int totalRecords = customerRetList.Count;

                    while (count < totalRecords)
                    {
                        int remainingRecords = totalRecords - count;
                        int currentBatchSize = Math.Min(remainingRecords, Constants.BatchSize);

                        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                        {
                            for (int i = count; i < count + currentBatchSize; i++)
                            {
                                var c = customerRetList[i];

                                #region parameters
                                SQLHelper.AddWithValue(cmd, "@ListID", c.ListID);
                                SQLHelper.AddWithValue(cmd, "@TimeCreated", c.TimeCreated);
                                SQLHelper.AddWithValue(cmd, "@TimeModified", c.TimeModified);
                                SQLHelper.AddWithValue(cmd, "@EditSequence", c.EditSequence);

                                cmd.Parameters.AddWithValue("@Name", c.Name);
                                SQLHelper.AddWithValue(cmd, "@FullName", c.FullName);
                                SQLHelper.AddWithValue(cmd, "@IsActive", c.IsActive);

                                SQLHelper.AddWithValue(cmd, "@ClassRefListID", c.ClassRefListID);
                                SQLHelper.AddWithValue(cmd, "@ClassRefFullName", c.ClassRefFullName);

                                SQLHelper.AddWithValue(cmd, "@ParentRefListID", c.ParentRefListID);
                                SQLHelper.AddWithValue(cmd, "@ParentRefFullName", c.ParentRefFullName);

                                SQLHelper.AddWithValue(cmd, "@Sublevel", c.Sublevel);

                                SQLHelper.AddWithValue(cmd, "@CompanyName", c.CompanyName);
                                SQLHelper.AddWithValue(cmd, "@Salutation", c.Salutation);
                                SQLHelper.AddWithValue(cmd, "@FirstName", c.FirstName);
                                SQLHelper.AddWithValue(cmd, "@MiddleName", c.MiddleName);
                                SQLHelper.AddWithValue(cmd, "@LastName", c.LastName);
                                SQLHelper.AddWithValue(cmd, "@JobTitle", c.JobTitle);

                                // SQLHelper.AddWithAddressValue(cmd, "Bill", c);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr1", c.BillAddressAddr1);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr2", c.BillAddressAddr2);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr3", c.BillAddressAddr3);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr4", c.BillAddressAddr4);
                                SQLHelper.AddWithValue(cmd, "@BillAddressAddr5", c.BillAddressAddr5);
                                SQLHelper.AddWithValue(cmd, "@BillAddressCity", c.BillAddressCity);
                                SQLHelper.AddWithValue(cmd, "@BillAddressState", c.BillAddressState);
                                SQLHelper.AddWithValue(cmd, "@BillAddressPostalCode", c.BillAddressPostalCode);
                                SQLHelper.AddWithValue(cmd, "@BillAddressCountry", c.BillAddressCountry);
                                SQLHelper.AddWithValue(cmd, "@BillAddressNote", c.BillAddressNote);
                                SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr1", c.BillAddressBlockAddr1);
                                SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr2", c.BillAddressBlockAddr2);
                                SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr3", c.BillAddressBlockAddr3);
                                SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr4", c.BillAddressBlockAddr4);
                                SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr5", c.BillAddressBlockAddr5);

                                // SQLHelper.AddWithAddressValue(cmd, "Ship", c);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr1", c.ShipAddressAddr1);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr2", c.ShipAddressAddr2);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr3", c.ShipAddressAddr3);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr4", c.ShipAddressAddr4);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressAddr5", c.ShipAddressAddr5);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressCity", c.ShipAddressCity);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressState", c.ShipAddressState);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressPostalCode", c.ShipAddressPostalCode);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressCountry", c.ShipAddressCountry);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressNote", c.ShipAddressNote);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr1", c.ShipAddressBlockAddr1);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr2", c.ShipAddressBlockAddr2);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr3", c.ShipAddressBlockAddr3);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr4", c.ShipAddressBlockAddr4);
                                SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr5", c.ShipAddressBlockAddr5);

                                SQLHelper.AddWithValue(cmd, "@Phone", c.Phone);
                                SQLHelper.AddWithValue(cmd, "@AltPhone", c.AltPhone);
                                SQLHelper.AddWithValue(cmd, "@Fax", c.Fax);
                                SQLHelper.AddWithValue(cmd, "@Email", c.Email);
                                SQLHelper.AddWithValue(cmd, "@Cc", c.Cc);

                                SQLHelper.AddWithValue(cmd, "@Contact", c.Contact);
                                SQLHelper.AddWithValue(cmd, "@AltContact", c.AltContact);

                                SQLHelper.AddWithValue(cmd, "@CustomerTypeRefListID", c.CustomerTypeRefListID);
                                SQLHelper.AddWithValue(cmd, "@CustomerTypeRefFullName", c.CustomerTypeRefFullName);

                                SQLHelper.AddWithValue(cmd, "@TermsRefListID", c.TermsRefListID);
                                SQLHelper.AddWithValue(cmd, "@TermsRefFullName", c.TermsRefFullName);

                                SQLHelper.AddWithValue(cmd, "@SalesRepRefListID", c.SalesRepRefListID);
                                SQLHelper.AddWithValue(cmd, "@SalesRepRefFullName", c.SalesRepRefFullName);

                                SQLHelper.AddWithValue(cmd, "@Balance", c.Balance);
                                SQLHelper.AddWithValue(cmd, "@TotalBalance", c.TotalBalance);

                                SQLHelper.AddWithValue(cmd, "@SalesTaxCodeRefListID", c.SalesTaxCodeRefListID);
                                SQLHelper.AddWithValue(cmd, "@SalesTaxCodeRefFullName", c.SalesTaxCodeRefFullName);

                                SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefListID", c.ItemSalesTaxRefListID);
                                SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefFullName", c.ItemSalesTaxRefFullName);

                                SQLHelper.AddWithValue(cmd, "@ResaleNumber", c.ResaleNumber);
                                SQLHelper.AddWithValue(cmd, "@AccountNumber", c.AccountNumber);
                                SQLHelper.AddWithValue(cmd, "@CreditLimit", c.CreditLimit);

                                SQLHelper.AddWithValue(cmd, "@PreferredPaymentMethodRefListID", c.PreferredPaymentMethodRefListID);
                                SQLHelper.AddWithValue(cmd, "@PreferredPaymentMethodRefFullName", c.PreferredPaymentMethodRefFullName);

                                SQLHelper.AddWithValue(cmd, "@JobStatus", c.JobStatus);

                                SQLHelper.AddWithValue(cmd, "@PreferredDeliveryMethod", c.PreferredDeliveryMethod);

                                SQLHelper.AddWithValue(cmd, "@PriceLevelRefListID", c.PriceLevelRefListID);
                                SQLHelper.AddWithValue(cmd, "@PriceLevelRefFullName", c.PriceLevelRefFullName);

                                //SQLHelper.AddWithValue(cmd, "@ExternalGUID",i.ExternalGUID );

                                SQLHelper.AddWithValue(cmd, "@CurrencyRefListID", c.CurrencyRefListID);
                                SQLHelper.AddWithValue(cmd, "@CurrencyRefFullName", c.CurrencyRefFullName);

                                #endregion

                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }

                        count += currentBatchSize;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception occurred while inserting customers in batches", ex);
            }
        }


        //private void InsertCustomers(List<CustomerRet> customerRetList)
        //{
        //    try
        //    {
        //        #region INSERT SQL QUERY
        //        string cmdText = @"INSERT INTO 
        //        Customer 
        //        (ListID, 
        //            TimeCreated, TimeModified, EditSequence, 
        //            Name, FullName, IsActive, 
        //            ClassRefListID, ClassRefFullName,
        //            ParentRefListID, ParentRefFullName, 
        //            Sublevel, 
        //            CompanyName, 
        //            Salutation, FirstName, MiddleName, LastName, 
        //            BillAddressAddr1, BillAddressAddr2,BillAddressAddr3, BillAddressAddr4, BillAddressAddr5, 
        //            BillAddressCity, BillAddressState, BillAddressPostalCode, 
        //            BillAddressCountry, BillAddressNote, 
        //            BillAddressBlockAddr1, BillAddressBlockAddr2,BillAddressBlockAddr3, BillAddressBlockAddr4, BillAddressBlockAddr5, 
        //            ShipAddressAddr1, ShipAddressAddr2, ShipAddressAddr3, ShipAddressAddr4, ShipAddressAddr5, 
        //            ShipAddressCity, ShipAddressState, ShipAddressPostalCode, 
        //            ShipAddressCountry, ShipAddressNote, 
        //            ShipAddressBlockAddr1, ShipAddressBlockAddr2, ShipAddressBlockAddr3, ShipAddressBlockAddr4, ShipAddressBlockAddr5, 
        //            Phone, AltPhone, Fax, Email, Cc, Contact, AltContact, 
        //            CustomerTypeRefListID, CustomerTypeRefFullName, 
        //            TermsRefListID, TermsRefFullName, 
        //            SalesRepRefListID, SalesRepRefFullName, 
        //            Balance, TotalBalance, 
        //            SalesTaxCodeRefListID, SalesTaxCodeRefFullName, 
        //            ItemSalesTaxRefListID, ItemSalesTaxRefFullName, 
        //            ResaleNumber, AccountNumber, CreditLimit, 
        //            PreferredPaymentMethodRefListID, PreferredPaymentMethodRefFullName, 
        //            JobStatus, 
        //            PreferredDeliveryMethod, 
        //            PriceLevelRefListID, PriceLevelRefFullName, 
        //            CurrencyRefListID, CurrencyRefFullName 
        //        ) 
        //        VALUES 
        //        (@ListID, 
        //            @TimeCreated, @TimeModified, @EditSequence, 
        //            @Name, @FullName, @IsActive, 
        //            @ClassRefListID, @ClassRefFullName, 
        //            @ParentRefListID, @ParentRefFullName, 
        //            @Sublevel, 
        //            @CompanyName, 
        //            @Salutation, @FirstName, @MiddleName, @LastName, 
        //            @BillAddressAddr1, @BillAddressAddr2, @BillAddressAddr3, @BillAddressAddr4, @BillAddressAddr5, 
        //            @BillAddressCity, @BillAddressState, @BillAddressPostalCode, 
        //            @BillAddressCountry, @BillAddressNote, 
        //            @BillAddressBlockAddr1, @BillAddressBlockAddr2, @BillAddressBlockAddr3, @BillAddressBlockAddr4, @BillAddressBlockAddr5, 
        //            @ShipAddressAddr1, @ShipAddressAddr2, @ShipAddressAddr3, @ShipAddressAddr4, @ShipAddressAddr5, 
        //            @ShipAddressCity, @ShipAddressState, @ShipAddressPostalCode, 
        //            @ShipAddressCountry, @ShipAddressNote, 
        //            @ShipAddressBlockAddr1, @ShipAddressBlockAddr2, @ShipAddressBlockAddr3, @ShipAddressBlockAddr4, @ShipAddressBlockAddr5, 
        //            @Phone, @AltPhone, @Fax, @Email, @Cc, @Contact, @AltContact, 
        //            @CustomerTypeRefListID, @CustomerTypeRefFullName, 
        //            @TermsRefListID, @TermsRefFullName, 
        //            @SalesRepRefListID, @SalesRepRefFullName, 
        //            @Balance, @TotalBalance, 
        //            @SalesTaxCodeRefListID, @SalesTaxCodeRefFullName, 
        //            @ItemSalesTaxRefListID, @ItemSalesTaxRefFullName, 
        //            @ResaleNumber, @AccountNumber, @CreditLimit, 
        //            @PreferredPaymentMethodRefListID, @PreferredPaymentMethodRefFullName, 
        //            @JobStatus, 
        //            @PreferredDeliveryMethod, 
        //            @PriceLevelRefListID, @PriceLevelRefFullName, 
        //            @CurrencyRefListID, @CurrencyRefFullName)";
        //        #endregion

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            #region Query Builder
        //            foreach (var c in customerRetList)
        //            {
        //                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        //                {
        //                    #region parameters
        //                    SQLHelper.AddWithValue(cmd, "@ListID", c.ListID);
        //                    SQLHelper.AddWithValue(cmd, "@TimeCreated", c.TimeCreated);
        //                    SQLHelper.AddWithValue(cmd, "@TimeModified", c.TimeModified);
        //                    SQLHelper.AddWithValue(cmd, "@EditSequence", c.EditSequence);

        //                    cmd.Parameters.AddWithValue("@Name", c.Name);
        //                    SQLHelper.AddWithValue(cmd, "@FullName", c.FullName);
        //                    SQLHelper.AddWithValue(cmd, "@IsActive", c.IsActive);

        //                    SQLHelper.AddWithValue(cmd, "@ClassRefListID", c.ClassRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@ClassRefFullName", c.ClassRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@ParentRefListID", c.ParentRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@ParentRefFullName", c.ParentRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@Sublevel", c.Sublevel);

        //                    SQLHelper.AddWithValue(cmd, "@CompanyName", c.CompanyName);
        //                    SQLHelper.AddWithValue(cmd, "@Salutation", c.Salutation);
        //                    SQLHelper.AddWithValue(cmd, "@FirstName", c.FirstName);
        //                    SQLHelper.AddWithValue(cmd, "@MiddleName", c.MiddleName);
        //                    SQLHelper.AddWithValue(cmd, "@LastName", c.LastName);
        //                    SQLHelper.AddWithValue(cmd, "@JobTitle", c.JobTitle);

        //                    // SQLHelper.AddWithAddressValue(cmd, "Bill", c);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressAddr1", c.BillAddressAddr1);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressAddr2", c.BillAddressAddr2);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressAddr3", c.BillAddressAddr3);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressAddr4", c.BillAddressAddr4);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressAddr5", c.BillAddressAddr5);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressCity", c.BillAddressCity);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressState", c.BillAddressState);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressPostalCode", c.BillAddressPostalCode);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressCountry", c.BillAddressCountry);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressNote", c.BillAddressNote);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr1", c.BillAddressBlockAddr1);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr2", c.BillAddressBlockAddr2);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr3", c.BillAddressBlockAddr3);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr4", c.BillAddressBlockAddr4);
        //                    SQLHelper.AddWithValue(cmd, "@BillAddressBlockAddr5", c.BillAddressBlockAddr5);

        //                    // SQLHelper.AddWithAddressValue(cmd, "Ship", c);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressAddr1", c.ShipAddressAddr1);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressAddr2", c.ShipAddressAddr2);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressAddr3", c.ShipAddressAddr3);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressAddr4", c.ShipAddressAddr4);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressAddr5", c.ShipAddressAddr5);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressCity", c.ShipAddressCity);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressState", c.ShipAddressState);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressPostalCode", c.ShipAddressPostalCode);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressCountry", c.ShipAddressCountry);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressNote", c.ShipAddressNote);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr1", c.ShipAddressBlockAddr1);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr2", c.ShipAddressBlockAddr2);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr3", c.ShipAddressBlockAddr3);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr4", c.ShipAddressBlockAddr4);
        //                    SQLHelper.AddWithValue(cmd, "@ShipAddressBlockAddr5", c.ShipAddressBlockAddr5);

        //                    SQLHelper.AddWithValue(cmd, "@Phone", c.Phone);
        //                    SQLHelper.AddWithValue(cmd, "@AltPhone", c.AltPhone);
        //                    SQLHelper.AddWithValue(cmd, "@Fax", c.Fax);
        //                    SQLHelper.AddWithValue(cmd, "@Email", c.Email);
        //                    SQLHelper.AddWithValue(cmd, "@Cc", c.Cc);

        //                    SQLHelper.AddWithValue(cmd, "@Contact", c.Contact);
        //                    SQLHelper.AddWithValue(cmd, "@AltContact", c.AltContact);

        //                    SQLHelper.AddWithValue(cmd, "@CustomerTypeRefListID", c.CustomerTypeRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@CustomerTypeRefFullName", c.CustomerTypeRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@TermsRefListID", c.TermsRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@TermsRefFullName", c.TermsRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@SalesRepRefListID", c.SalesRepRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@SalesRepRefFullName", c.SalesRepRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@Balance", c.Balance);
        //                    SQLHelper.AddWithValue(cmd, "@TotalBalance", c.TotalBalance);

        //                    SQLHelper.AddWithValue(cmd, "@SalesTaxCodeRefListID", c.SalesTaxCodeRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@SalesTaxCodeRefFullName", c.SalesTaxCodeRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefListID", c.ItemSalesTaxRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@ItemSalesTaxRefFullName", c.ItemSalesTaxRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@ResaleNumber", c.ResaleNumber);
        //                    SQLHelper.AddWithValue(cmd, "@AccountNumber", c.AccountNumber);
        //                    SQLHelper.AddWithValue(cmd, "@CreditLimit", c.CreditLimit);

        //                    SQLHelper.AddWithValue(cmd, "@PreferredPaymentMethodRefListID", c.PreferredPaymentMethodRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@PreferredPaymentMethodRefFullName", c.PreferredPaymentMethodRefFullName);

        //                    SQLHelper.AddWithValue(cmd, "@JobStatus", c.JobStatus);

        //                    SQLHelper.AddWithValue(cmd, "@PreferredDeliveryMethod", c.PreferredDeliveryMethod);

        //                    SQLHelper.AddWithValue(cmd, "@PriceLevelRefListID", c.PriceLevelRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@PriceLevelRefFullName", c.PriceLevelRefFullName);

        //                    //SQLHelper.AddWithValue(cmd, "@ExternalGUID",i.ExternalGUID );

        //                    SQLHelper.AddWithValue(cmd, "@CurrencyRefListID", c.CurrencyRefListID);
        //                    SQLHelper.AddWithValue(cmd, "@CurrencyRefFullName", c.CurrencyRefFullName);

        //                    #endregion

        //                    cmd.ExecuteNonQuery();

        //                    //Console.WriteLine("Added Customer : " + c.Name);
        //                }
        //            }
        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DataProcessingException("Exception got throwned while Inserting Customer", ex);
        //    }
        //}


        // Custom data reader implementation
        public class ObjectDataReader<T> : IDataReader
        {
            private IEnumerator<T> enumerator;
            private Dictionary<string, PropertyInfo> properties;
            private T current;

            public ObjectDataReader(IEnumerable<T> collection)
            {
                this.enumerator = collection.GetEnumerator();
                this.properties = typeof(T).GetProperties().ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
            }

            public object this[int i] => GetValue(i);

            public object this[string name] => GetValue(GetOrdinal(name));

            public int Depth => 0;

            public bool IsClosed => false;

            public int RecordsAffected => -1;

            public int FieldCount => typeof(T).GetProperties().Length;

            public void Close() { }

            public void Dispose()
            {
                enumerator.Dispose();
            }

            public bool GetBoolean(int i) => (bool)GetValue(i);

            public byte GetByte(int i) => (byte)GetValue(i);

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => throw new NotImplementedException();

            public char GetChar(int i) => (char)GetValue(i);

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => throw new NotImplementedException();

            public IDataReader GetData(int i) => throw new NotImplementedException();

            public string GetDataTypeName(int i) => throw new NotImplementedException();

            public DateTime GetDateTime(int i) => (DateTime)GetValue(i);

            public decimal GetDecimal(int i) => (decimal)GetValue(i);

            public double GetDouble(int i) => (double)GetValue(i);

            public Type GetFieldType(int i) => properties.Values.ElementAt(i).PropertyType;

            public float GetFloat(int i) => (float)GetValue(i);

            public Guid GetGuid(int i) => (Guid)GetValue(i);

            public short GetInt16(int i) => (short)GetValue(i);

            public int GetInt32(int i) => (int)GetValue(i);

            public long GetInt64(int i) => (long)GetValue(i);

            public string GetName(int i) => properties.Values.ElementAt(i).Name;

            public int GetOrdinal(string name) => Array.IndexOf(properties.Keys.ToArray(), name);

            public DataTable GetSchemaTable() => throw new NotImplementedException();

            public string GetString(int i) => (string)GetValue(i);

            public object GetValue(int i) => properties.Values.ElementAt(i).GetValue(current);

            public int GetValues(object[] values)
            {
                int count = Math.Min(values.Length, properties.Count);
                for (int i = 0; i < count; i++)
                {
                    values[i] = GetValue(i);
                }
                return count;
            }

            public bool IsDBNull(int i) => GetValue(i) == null;

            public bool NextResult() => throw new NotImplementedException();

            public bool Read()
            {
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    return true;
                }
                return false;
            }
        }
    }
}