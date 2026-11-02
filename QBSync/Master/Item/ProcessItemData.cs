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
    public class ProcessItemData
    {
		private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public void InsertItemsToDatabase(List<ItemRet> itemsRetList)
        {
            DeleteExsitingItems(itemsRetList);
            InsertItems(itemsRetList);
        }

        public void DeleteExsitingItems(List<ItemRet> itemRetList)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (var i in itemRetList)
                    {
                        using (SqlCommand cmd = new SqlCommand("Delete Item Where ListID='" + i.ListID + "'", conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        Console.WriteLine("Items Deleted " + i.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception got throwned while Deleting Items", ex);
            }
        }

        private void InsertItems(List<ItemRet> itemRetList)
        {
            try
            {
                #region INSERT SQL QUERY
                string cmdText = "Insert into " +
                "Item " +
                "(ListID, " +
                    "TimeCreated, TimeModified, EditSequence, " +
                    "Type, " +
                    "Name, FullName, BarCodeValue, IsActive, " +
                    "ClassRefListID, ClassRefFullName, " +
                    "ParentRefListID, ParentRefFullName, " +
                    "Sublevel, ItemDesc, IsPrintItemsInGroup, ManufacturerPartNumber, " +
                    "UnitOfMeasureSetRefListID, UnitOfMeasureSetRefFullName, " +
                    "SalesTaxCodeRefListID, SalesTaxCodeRefFullName, " +
                    "DiscountRatePercent, AccountRefListID, AccountRefFullName, " +
                    "SalesDesc, SalesPrice, " +
                    "IncomeAccountRefListID, IncomeAccountRefFullName, " +
                    "PurchaseDesc, PurchaseCost, " +
                    "COGSAccountRefListID, COGSAccountRefFullName, " +
                    "PrefVendorRefListID, PrefVendorRefFullName, " +
                    "AssetAccountRefListID, AssetAccountRefFullName, " +
                    "ReorderPoint, QuantityOnHand, AverageCost, QuantityOnOrder, QuantityOnSalesOrder, " +
                    "SalesOrPurchaseDesc, SalesOrPurchasePrice, SalesOrPurchasePricePercent, " +
                    "SalesOrPurchaseAccountRefListID, SalesOrPurchaseAccountRefFullName, " +
                    "SalesAndPurchaseSalesDesc, SalesAndPurchaseSalesPrice, " +
                    "SalesAndPurchaseIncomeAccountRefListID, SalesAndPurchaseIncomeAccountRefFullName, " +
                    "SalesAndPurchasePurchaseDesc, SalesAndPurchasePurchaseCost, " +
                    "SalesAndPurchaseExpenseAccountRefListID, SalesAndPurchaseExpenseAccountRefFullName, " +
                    "SalesAndPurchasePrefVendorRefListID, SalesAndPurchasePrefVendorRefFullName, " +
                    "SpecialItemType, " +
                    "TaxRate, TaxVendorRefListID, TaxVendorRefFullName" +
                ") " +
                "VALUES " +
                "(@ListID, " +
                    "@TimeCreated, @TimeModified, @EditSequence, " +
                    "@Type, " +
                    "@Name, @FullName, @BarCodeValue, @IsActive, " +
                    "@ClassRefListID, @ClassRefFullName, " +
                    "@ParentRefListID, @ParentRefFullName, " +
                    "@Sublevel, @ItemDesc, @IsPrintItemsInGroup, @ManufacturerPartNumber, " +
                    "@UnitOfMeasureSetRefListID, @UnitOfMeasureSetRefFullName, " +
                    "@SalesTaxCodeRefListID, @SalesTaxCodeRefFullName, " +
                    "@DiscountRatePercent, @AccountRefListID, @AccountRefFullName, " +
                    "@SalesDesc, @SalesPrice, " +
                    "@IncomeAccountRefListID, @IncomeAccountRefFullName, " +
                    "@PurchaseDesc, @PurchaseCost, " +
                    "@COGSAccountRefListID, @COGSAccountRefFullName, " +
                    "@PrefVendorRefListID, @PrefVendorRefFullName, " +
                    "@AssetAccountRefListID, @AssetAccountRefFullName, " +
                    "@ReorderPoint, @QuantityOnHand, @AverageCost, @QuantityOnOrder, @QuantityOnSalesOrder, " +
                    "@SalesOrPurchaseDesc, @SalesOrPurchasePrice, @SalesOrPurchasePricePercent, " +
                    "@SalesOrPurchaseAccountRefListID, @SalesOrPurchaseAccountRefFullName, " +
                    "@SalesAndPurchaseSalesDesc, @SalesAndPurchaseSalesPrice, " +
                    "@SalesAndPurchaseIncomeAccountRefListID, @SalesAndPurchaseIncomeAccountRefFullName, " +
                    "@SalesAndPurchasePurchaseDesc, @SalesAndPurchasePurchaseCost, " +
                    "@SalesAndPurchaseExpenseAccountRefListID, @SalesAndPurchaseExpenseAccountRefFullName, " +
                    "@SalesAndPurchasePrefVendorRefListID, @SalesAndPurchasePrefVendorRefFullName, " +
                    "@SpecialItemType, " +
                    "@TaxRate, @TaxVendorRefListID, @TaxVendorRefFullName" +
                " )";
                #endregion

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    #region INSERT SQL PARAMETER
                    foreach (var i in itemRetList)
                    {
                        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                        {
                            #region parameters
                            SQLHelper.AddWithValue(cmd, "@ListID", i.ListID);
                            SQLHelper.AddWithValue(cmd, "@TimeCreated", i.TimeCreated);
                            SQLHelper.AddWithValue(cmd, "@TimeModified", i.TimeModified);
                            SQLHelper.AddWithValue(cmd, "@EditSequence", i.EditSequence);

                            SQLHelper.AddWithValue(cmd, "@Type", i.Type);

                            cmd.Parameters.AddWithValue("@Name", i.Name);
                            SQLHelper.AddWithValue(cmd, "@FullName", i.FullName);
                            SQLHelper.AddWithValue(cmd, "@BarCodeValue", i.BarCodeValue);
                            SQLHelper.AddWithValue(cmd, "@IsActive", i.IsActive);

                            SQLHelper.AddWithValue(cmd, "@ClassRefListID", i.ClassRefListID);
                            SQLHelper.AddWithValue(cmd, "@ClassRefFullName", i.ClassRefFullName);

                            SQLHelper.AddWithValue(cmd, "@ParentRefListID", i.ParentRefListID);
                            SQLHelper.AddWithValue(cmd, "@ParentRefFullName", i.ParentRefFullName);

                            SQLHelper.AddWithValue(cmd, "@Sublevel", i.Sublevel);
                            SQLHelper.AddWithValue(cmd, "@ItemDesc", i.ItemDesc);
                            SQLHelper.AddWithValue(cmd, "@IsPrintItemsInGroup", i.IsPrintItemsInGroup);
                            SQLHelper.AddWithValue(cmd, "@ManufacturerPartNumber", i.ManufacturerPartNumber);

                            SQLHelper.AddWithValue(cmd, "@UnitOfMeasureSetRefListID", i.UnitOfMeasureSetRefListID);
                            SQLHelper.AddWithValue(cmd, "@UnitOfMeasureSetRefFullName", i.UnitOfMeasureSetRefFullName);

                            SQLHelper.AddWithValue(cmd, "@SalesTaxCodeRefListID", i.SalesTaxCodeRefListID);
                            SQLHelper.AddWithValue(cmd, "@SalesTaxCodeRefFullName", i.SalesTaxCodeRefFullName);

                            SQLHelper.AddWithValue(cmd, "@SalesDesc", i.SalesDesc);
                            SQLHelper.AddWithValue(cmd, "@SalesPrice", i.SalesPrice);

                            SQLHelper.AddWithValue(cmd, "@IncomeAccountRefListID", i.IncomeAccountRefListID);
                            SQLHelper.AddWithValue(cmd, "@IncomeAccountRefFullName", i.IncomeAccountRefFullName);

                            SQLHelper.AddWithValue(cmd, "@PurchaseDesc", i.PurchaseDesc);
                            SQLHelper.AddWithValue(cmd, "@PurchaseCost", i.PurchaseCost);

                            SQLHelper.AddWithValue(cmd, "@COGSAccountRefListID", i.COGSAccountRefListID);
                            SQLHelper.AddWithValue(cmd, "@COGSAccountRefFullName", i.COGSAccountRefFullName);

                            SQLHelper.AddWithValue(cmd, "@PrefVendorRefListID", i.PrefVendorRefListID);
                            SQLHelper.AddWithValue(cmd, "@PrefVendorRefFullName", i.PrefVendorRefFullName);

                            SQLHelper.AddWithValue(cmd, "@AssetAccountRefListID", i.AssetAccountRefListID);
                            SQLHelper.AddWithValue(cmd, "@AssetAccountRefFullName", i.AssetAccountRefFullName);

                            SQLHelper.AddWithValue(cmd, "@DiscountRatePercent", i.DiscountRatePercent);
                            SQLHelper.AddWithValue(cmd, "@AccountRefListID", i.AccountRefListID);
                            SQLHelper.AddWithValue(cmd, "@AccountRefFullName", i.AccountRefFullName);

                            SQLHelper.AddWithValue(cmd, "@ReorderPoint", i.ReorderPoint);
                            SQLHelper.AddWithValue(cmd, "@QuantityOnHand", i.QuantityOnHand);
                            SQLHelper.AddWithValue(cmd, "@AverageCost", i.AverageCost);
                            SQLHelper.AddWithValue(cmd, "@QuantityOnOrder", i.QuantityOnOrder);
                            SQLHelper.AddWithValue(cmd, "@QuantityOnSalesOrder", i.QuantityOnSalesOrder);

                            SQLHelper.AddWithValue(cmd, "@SalesOrPurchaseDesc", i.SalesOrPurchaseDesc);
                            SQLHelper.AddWithValue(cmd, "@SalesOrPurchasePrice", i.SalesOrPurchasePrice);
                            SQLHelper.AddWithValue(cmd, "@SalesOrPurchasePricePercent", i.SalesOrPurchasePricePercent);
                            SQLHelper.AddWithValue(cmd, "@SalesOrPurchaseAccountRefListID", i.SalesOrPurchaseAccountRefListID);
                            SQLHelper.AddWithValue(cmd, "@SalesOrPurchaseAccountRefFullName", i.SalesOrPurchaseAccountRefFullName);

                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchaseSalesDesc", i.SalesOrPurchaseDesc);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchaseSalesPrice", i.SalesOrPurchasePrice);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchaseIncomeAccountRefListID", i.SalesAndPurchaseIncomeAccountRefListID);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchaseIncomeAccountRefFullName", i.SalesAndPurchaseIncomeAccountRefFullName);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchasePurchaseDesc", i.SalesOrPurchaseDesc);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchasePurchaseCost", i.SalesOrPurchasePrice);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchaseExpenseAccountRefListID", i.SalesAndPurchaseExpenseAccountRefListID);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchaseExpenseAccountRefFullName", i.SalesAndPurchaseExpenseAccountRefFullName);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchasePrefVendorRefListID", i.SalesAndPurchasePrefVendorRefListID);
                            SQLHelper.AddWithValue(cmd, "@SalesAndPurchasePrefVendorRefFullName", i.SalesAndPurchasePrefVendorRefFullName);
                            SQLHelper.AddWithValue(cmd, "@SpecialItemType", i.SpecialItemType);
                            SQLHelper.AddWithValue(cmd, "@TaxRate", i.TaxRate);
                            SQLHelper.AddWithValue(cmd, "@TaxVendorRefListID", i.TaxVendorRefListID);
                            SQLHelper.AddWithValue(cmd, "@TaxVendorRefFullName", i.TaxVendorRefFullName);
                            #endregion

                            cmd.ExecuteNonQuery();

                            Console.WriteLine("Added Item : " + i.Name);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new DataProcessingException("Exception got throwned while Inserting Item", ex);
            }
        }
    }
}