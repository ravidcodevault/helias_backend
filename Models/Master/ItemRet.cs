using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class ItemRet
    {
        public string Type { get; set; }

        public string ListID { get; set; }
        public string TimeCreated { get; set; }
        public string TimeModified { get; set; }
        public string EditSequence { get; set; }
        public string Name { get; set; }

        public string FullName { get; set; }

        public string BarCodeValue { get; set; }
        public string IsActive { get; set; }

        public string ClassRefListID { get; set; }
        public string ClassRefFullName { get; set; }

        public string ParentRefListID { get; set; }
        public string ParentRefFullName { get; set; }

        public string Sublevel { get; set; }

        public string ItemDesc { get; set; }                    // ItemGroupRet
        public string IsPrintItemsInGroup { get; set; }         // ItemGroupRet

        public string ManufacturerPartNumber { get; set; }         // NonInventory, Inventory, InventoryAssembly

        public string UnitOfMeasureSetRefListID { get; set; }   // Service, NonInventory, Inventory, InventoryAssembly
        public string UnitOfMeasureSetRefFullName { get; set; } // Service, NonInventory, Inventory, InventoryAssembly

        public string SalesTaxCodeRefListID { get; set; }
        public string SalesTaxCodeRefFullName { get; set; }

        public string DiscountRatePercent { get; set; }         // Discount
        public string AccountRefListID { get; set; }            // Discount
        public string AccountRefFullName { get; set; }          // Discount

        public string SalesDesc { get; set; }                   // ItemInventory, InventoryAssembly
        public string SalesPrice { get; set; }                  // ItemInventory, InventoryAssembly

        public string IncomeAccountRefListID { get; set; }      // ItemInventory, InventoryAssembly
        public string IncomeAccountRefFullName { get; set; }    // ItemInventory, InventoryAssembly
        public string PurchaseDesc { get; set; }                // ItemInventor, InventoryAssemblyy
        public string PurchaseCost { get; set; }                // ItemInventory, InventoryAssembly
        public string COGSAccountRefListID { get; set; }        // ItemInventory, InventoryAssembly
        public string COGSAccountRefFullName { get; set; }      // ItemInventory, InventoryAssembly

        public string PrefVendorRefListID { get; set; }         // ItemInventory, InventoryAssembly
        public string PrefVendorRefFullName { get; set; }      // ItemInventory, InventoryAssembly

        public string AssetAccountRefListID { get; set; }       // ItemInventory, InventoryAssembly
        public string AssetAccountRefFullName { get; set; }     // ItemInventory, InventoryAssembly

        public string BuildPoint { get; set; }                  // InventoryAssembly

        public string ReorderPoint { get; set; }                // ItemInventory
        public string QuantityOnHand { get; set; }              // ItemInventory, InventoryAssembly
        public string AverageCost { get; set; }                 // ItemInventory, InventoryAssembly
        public string QuantityOnOrder { get; set; }             // ItemInventory, InventoryAssembly
        public string QuantityOnSalesOrder { get; set; }        // ItemInventory, InventoryAssembly

        public string SalesOrPurchaseDesc { get; set; }        // Service, NonInventory, OtherCharge   
        public string SalesOrPurchasePrice { get; set; }           // Service, NonInventory, OtherCharge   
        public string SalesOrPurchasePricePercent { get; set; }    // Service, NonInventory, OtherCharge   
        public string SalesOrPurchaseAccountRefListID { get; set; }    // Service, NonInventory, OtherCharge   
        public string SalesOrPurchaseAccountRefFullName { get; set; }  // Service, NonInventory, OtherCharge   

        public string SalesAndPurchaseSalesDesc { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchaseSalesPrice { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchaseIncomeAccountRefListID { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchaseIncomeAccountRefFullName { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchasePurchaseDesc { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchasePurchaseCost { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchaseExpenseAccountRefListID { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchaseExpenseAccountRefFullName { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchasePrefVendorRefListID { get; set; }// Service, NonInventory, OtherCharge   
        public string SalesAndPurchasePrefVendorRefFullName { get; set; }// Service, NonInventory, OtherCharge   

        public string SpecialItemType { get; set; }            // OtherCharge        

        //public string ItemInventoryAssemblyLineItemInventoryRefListID { get; set; }     // InventoryAssembly 
        //public string ItemInventoryAssemblyLineItemInventoryRefFullName { get; set; }   // InventoryAssembly
        //public string ItemInventoryAssemblyLineQuantity { get; set; }                   // InventoryAssembly

        public string TaxRate { get; set; }                 // SalesTax
        public string TaxVendorRefListID { get; set; }      // SalesTax
        public string TaxVendorRefFullName { get; set; }    // SalesTax

        public string ExternalGUID { get; set; }

        public string DataExtRetOwnerID { get; set; }
        public string DataExtRetDataExtName { get; set; }
        public string DataExtRetDataExtType { get; set; }
        public string DataExtRetDataExtValue { get; set; }

    }
}
