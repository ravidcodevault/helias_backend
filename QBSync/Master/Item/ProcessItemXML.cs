using System.Collections.Generic;
using System.Linq;
using System.Xml;


namespace QB2DB_Sync
{
    public class ProcessItemXML
    {
        public static List<ItemRet> ConvertItemXML_to_List(string XMLResponse)
        {
            List<ItemRet> list = new List<ItemRet>();

            string[] caterory = new string[] { "ItemServiceRet", "ItemNonInventoryRet", "ItemOtherChargeRet","ItemInventoryRet","ItemInventoryAssemblyRet","ItemDiscountRet",
                                                "ItemSalesTaxRet", "ItemGroupRet", "ItemPaymentRet", "ItemSubtotalRet", "ItemSalesTaxGroupRet", "ItemFixedAssetRet" };

            XmlNodeList retList = QBSDKHelper.ParseXMLResponse(
                "ItemQueryRs", "ItemRet", XMLResponse, false);

            if (retList == null)
            {
                throw new XMLProcessingException("Unable to parse Item XML from QuickBook");
            }
            if (retList != null)
            {
                foreach (XmlNode ret in retList)
                {
                    if (caterory.Contains(ret.Name))    //"ItemServiceRet", "ItemNonInventoryRet", "ItemOtherChargeRet","ItemInventoryRet","ItemInventoryAssemblyRet","ItemDiscountRet", "ItemSalesTaxRet" };
                    {
                        ItemRet item = new ItemRet();
                        XmlNodeList fieldList = ret.ChildNodes;

                        item.Type = ret.Name;

                        foreach (XmlNode f in fieldList)
                        {
                            if (f.Name.Equals("ListID"))
                            {
                                item.ListID = f.InnerText;
                            }
                            else if (f.Name.Contains("TimeCreated"))
                            {
                                item.TimeCreated = f.InnerText;
                            }
                            else if (f.Name.Contains("TimeModified"))
                            {
                                item.TimeModified = f.InnerText;
                            }
                            else if (f.Name.Contains("EditSequence"))
                            {
                                item.EditSequence = f.InnerText;
                            }
                            else if (f.Name.Equals("Name"))
                            {
                                item.Name = f.InnerText;
                            }
                            else if (f.Name.Contains("FullName"))
                            {
                                item.FullName = f.InnerText;
                            }
                            else if (f.Name.Contains("BarCodeValue"))
                            {
                                item.BarCodeValue = f.InnerText;
                            }
                            else if (f.Name.Contains("IsActive"))
                            {
                                item.IsActive = f.InnerText;
                            }

                            else if (f.Name.Contains("ClassRef"))
                            {
                                item.ClassRefListID = f.ChildNodes[0].InnerText;
                                item.ClassRefFullName = f.ChildNodes[1].InnerText;
                            }
                            else if (f.Name.Contains("ParentRef"))
                            {
                                item.ParentRefListID = f.ChildNodes[0].InnerText;
                                item.ParentRefFullName = f.ChildNodes[1].InnerText;
                            }
                            else if (f.Name.Contains("Sublevel"))
                            {
                                item.Sublevel = f.InnerText;
                            }
                            else if (f.Name.Contains("ItemDesc"))
                            {
                                item.ItemDesc = f.InnerText;
                            }
                            else if (f.Name.Contains("IsPrintItemsInGroup"))
                            {
                                item.IsPrintItemsInGroup = f.InnerText;
                            }

                            // NonInventory, Inventory, InventoryAssembly
                            else if (f.Name.Contains("ManufacturerPartNumber"))
                            {
                                item.ManufacturerPartNumber = f.InnerText;
                            }

                            // Service, NonInventory, Inventory, InventoryAssembly
                            else if (f.Name.Contains("UnitOfMeasureSetRef"))
                            {
                                item.UnitOfMeasureSetRefListID = f.ChildNodes[0].InnerText;
                                item.UnitOfMeasureSetRefFullName = f.ChildNodes[1].InnerText;
                            }
                            else if (f.Name.Contains("SalesTaxCodeRef"))
                            {
                                item.SalesTaxCodeRefListID = f.ChildNodes[0].InnerText;
                                item.SalesTaxCodeRefFullName = f.ChildNodes[1].InnerText;
                            }

                            // Discount
                            else if (f.Name.Contains("DiscountRatePercent"))
                            {
                                item.DiscountRatePercent = f.InnerText;
                            }
                            // Discount
                            else if (f.Name.Equals("AccountRef"))
                            {
                                item.AccountRefListID = f.ChildNodes[0].InnerText;
                                item.AccountRefFullName = f.ChildNodes[1].InnerText;
                            }

                            else if (f.Name.Equals("SalesDesc"))
                            {
                                item.SalesDesc = f.InnerText;
                            }
                            else if (f.Name.Equals("SalesPrice"))
                            {
                                item.SalesPrice = f.InnerText;
                            }
                            else if (f.Name.Equals("IncomeAccountRef"))
                            {
                                item.IncomeAccountRefListID = f.ChildNodes[0].InnerText;
                                item.IncomeAccountRefFullName = f.ChildNodes[1].InnerText;
                            }
                            else if (f.Name.Equals("PurchaseDesc"))
                            {
                                item.PurchaseDesc = f.InnerText;
                            }
                            else if (f.Name.Equals("PurchaseCost"))
                            {
                                item.PurchaseCost = f.InnerText;
                            }
                            else if (f.Name.Equals("COGSAccountRef"))
                            {
                                item.COGSAccountRefListID = f.ChildNodes[0].InnerText;
                                item.COGSAccountRefFullName = f.ChildNodes[1].InnerText;
                            }

                            else if (f.Name.Contains("AssetAccountRef"))
                            {
                                item.AssetAccountRefListID = f.ChildNodes[0].InnerText;
                                item.AssetAccountRefFullName = f.ChildNodes[1].InnerText;
                            }
                            else if (f.Name.Contains("ReorderPoint"))
                            {
                                item.ReorderPoint = f.InnerText;
                            }
                            else if (f.Name.Contains("QuantityOnHand"))
                            {
                                item.QuantityOnHand = f.InnerText;
                            }
                            else if (f.Name.Contains("AverageCost"))
                            {
                                item.AverageCost = f.InnerText;
                            }
                            else if (f.Name.Contains("QuantityOnOrder"))
                            {
                                item.QuantityOnOrder = f.InnerText;
                            }
                            else if (f.Name.Contains("QuantityOnSalesOrder"))
                            {
                                item.QuantityOnSalesOrder = f.InnerText;
                            }

                            // Service, NonInventory, OtherCharge
                            else if (f.Name.Contains("SalesOrPurchase"))
                            {
                                XmlNodeList gList = f.ChildNodes;
                                foreach (XmlNode g in gList)
                                {
                                    if (g.Name.Contains("Desc"))
                                    {
                                        item.SalesOrPurchaseDesc = g.InnerText;
                                    }
                                    else if (g.Name.Contains("Price"))
                                    {
                                        item.SalesOrPurchasePrice = g.InnerText;
                                    }
                                    else if (g.Name.Contains("PricePercent"))
                                    {
                                        item.SalesOrPurchasePricePercent = g.InnerText;
                                    }
                                    else if (g.Name.Contains("AccountRef"))
                                    {
                                        item.SalesOrPurchaseAccountRefListID = g.ChildNodes[0].InnerText;
                                        item.SalesOrPurchaseAccountRefFullName = g.ChildNodes[1].InnerText;
                                    }
                                }
                            }

                            // Service, NonInventory, OtherCharge
                            else if (f.Name.Contains("SalesAndPurchase"))
                            {
                                XmlNodeList gList = f.ChildNodes;
                                foreach (XmlNode g in gList)
                                {
                                    if (g.Name.Contains("SalesDesc"))
                                    {
                                        item.SalesAndPurchaseSalesDesc = g.InnerText;
                                    }
                                    else if (g.Name.Contains("SalesPrice"))
                                    {
                                        item.SalesAndPurchaseSalesPrice = g.InnerText;
                                    }
                                    else if (g.Name.Contains("IncomeAccountRef"))
                                    {
                                        item.SalesAndPurchaseIncomeAccountRefListID = g.ChildNodes[0].InnerText;
                                        item.SalesAndPurchaseIncomeAccountRefFullName = g.ChildNodes[1].InnerText;
                                    }
                                    else if (g.Name.Contains("PurchaseDesc"))
                                    {
                                        item.SalesAndPurchaseSalesDesc = g.InnerText;
                                    }
                                    else if (g.Name.Contains("PurchaseCost"))
                                    {
                                        item.SalesAndPurchaseSalesPrice = g.InnerText;
                                    }
                                    else if (g.Name.Contains("ExpenseAccountRef"))
                                    {
                                        item.SalesAndPurchaseExpenseAccountRefListID = g.ChildNodes[0].InnerText;
                                        item.SalesAndPurchaseExpenseAccountRefFullName = g.ChildNodes[1].InnerText;
                                    }
                                    else if (g.Name.Contains("PrefVendorRef"))
                                    {
                                        item.SalesAndPurchasePrefVendorRefListID = g.ChildNodes[0].InnerText;
                                        item.SalesAndPurchasePrefVendorRefFullName = g.ChildNodes[1].InnerText;
                                    }
                                }
                            }

                            // OtherCharge
                            else if (f.Name.Contains("SpecialItemType"))
                            {
                                item.SpecialItemType = f.InnerText;
                            }

                            //// InventoryAssembly
                            //else if (f.Name.Contains("ItemInventoryAssemblyLineItemInventoryRef"))
                            //{
                            //    item.ItemInventoryAssemblyLineItemInventoryRefListID = f.ChildNodes[0].InnerText;
                            //    item.ItemInventoryAssemblyLineItemInventoryRefFullName = f.ChildNodes[1].InnerText;
                            //}

                            //// InventoryAssembly
                            //else if (f.Name.Contains("ItemInventoryAssemblyLineQuantity"))
                            //{
                            //    item.ItemInventoryAssemblyLineQuantity = f.InnerText;
                            //}

                            // SalesTax
                            else if (f.Name.Contains("TaxRate"))
                            {
                                item.TaxRate = f.InnerText;
                            }
                            else if (f.Name.Contains("TaxVendorRef"))
                            {
                                item.TaxVendorRefListID = f.ChildNodes[0].InnerText;
                                item.TaxVendorRefFullName = f.ChildNodes[1].InnerText;
                            }

                            // Service, NonInventory, OtherCharge, Inventory, InventoryAssembly
                            else if (f.Name.Contains("ExternalGUID"))
                            {
                                item.ExternalGUID = f.InnerText;
                            }
                            else if (f.Name.Contains("DataExtRetOwnerID"))
                            {
                                item.DataExtRetOwnerID = f.InnerText;
                            }
                            else if (f.Name.Contains("DataExtRetDataExtName"))
                            {
                                item.DataExtRetDataExtName = f.InnerText;
                            }
                            else if (f.Name.Contains("DataExtRetDataExtType"))
                            {
                                item.DataExtRetDataExtType = f.InnerText;
                            }
                            else if (f.Name.Contains("DataExtRetDataExtValue"))
                            {
                                item.DataExtRetDataExtValue = f.InnerText;
                            }
                        }

                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}