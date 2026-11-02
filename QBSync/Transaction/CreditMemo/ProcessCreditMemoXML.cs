using System.Collections.Generic;
using System.Xml;


namespace QB2DB_Sync
{
    public class ProcessCreditMemoXML
    {
        public static List<CreditMemoRet> ConvertCreditMemoXML_to_List(string XMLResponse)
        {
            List<CreditMemoRet> list = new List<CreditMemoRet>();

            XmlNodeList retList = QBSDKHelper.ParseXMLResponse(
                "CreditMemoQueryRs", "CreditMemoRet", XMLResponse, false);

            if (retList == null)
            {
                throw new XMLProcessingException("Unable to parse CreditMemo XML from QuickBook");
            }
            if (retList != null)
            {
                foreach (XmlNode ret in retList)
                {

                    CreditMemoRet item = new CreditMemoRet();
                    List<CreditMemoLineRet> lineList = new List<CreditMemoLineRet>();

                    XmlNodeList fieldList = ret.ChildNodes;

                    foreach (XmlNode f in fieldList)
                    {
                        if (f.Name.Contains("TxnID"))
                        {
                            item.TxnID = f.InnerText;
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
                        else if (f.Name.Contains("TxnNumber"))
                        {
                            item.TxnNumber = f.InnerText;
                        }
                        else if (f.Name.Contains("CustomerRef"))
                        {
                            item.CustomerRefListID = f.ChildNodes[0].InnerText;
                            item.CustomerRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("ARAccountRef"))
                        {
                            item.ARAccountRefListID = f.ChildNodes[0].InnerText;
                            item.ARAccountRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("TemplateRef"))
                        {
                            item.TemplateRefListID = f.ChildNodes[0].InnerText;
                            item.TemplateRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("TxnDate"))
                        {
                            item.TxnDate = f.InnerText;
                        }
                        else if (f.Name.Contains("RefNumber"))
                        {
                            item.RefNumber = f.InnerText;
                        }

						else if (f.Name.Contains("SalesRepRef"))
						{
							item.SalesRepRefListID = f.ChildNodes[0].InnerText;
							item.SalesRepRefFullName = f.ChildNodes[1].InnerText;
						}
						#region Address
						else if (f.Name.Contains("BillAddress"))
                        {
                            XmlNodeList addressFieldList = f.ChildNodes;

                            foreach (XmlNode g in addressFieldList)
                            {
                                if (g.Name.Contains("Addr1"))
                                {
                                    item.BillAddressAddr1 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr2"))
                                {
                                    item.BillAddressAddr2 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr3"))
                                {
                                    item.BillAddressAddr3 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr4"))
                                {
                                    item.BillAddressAddr4 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr5"))
                                {
                                    item.BillAddressAddr5 = g.InnerText;
                                }
                                else if (g.Name.Contains("City"))
                                {
                                    item.BillAddressCity = g.InnerText;
                                }
                                else if (g.Name.Contains("State"))
                                {
                                    item.BillAddressState = g.InnerText;
                                }
                                else if (g.Name.Contains("PostalCode"))
                                {
                                    item.BillAddressPostalCode = g.InnerText;
                                }
                                else if (g.Name.Contains("Country"))
                                {
                                    item.BillAddressCountry = g.InnerText;
                                }
                                else if (g.Name.Contains("Note"))
                                {
                                    item.BillAddressNote = g.InnerText;
                                }
                            }
                        }
                        else if (f.Name.Contains("ShipAddress"))
                        {
                            XmlNodeList addressFieldList = f.ChildNodes;

                            foreach (XmlNode g in addressFieldList)
                            {
                                if (g.Name.Contains("Addr1"))
                                {
                                    item.ShipAddressAddr1 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr2"))
                                {
                                    item.ShipAddressAddr2 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr3"))
                                {
                                    item.ShipAddressAddr3 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr4"))
                                {
                                    item.ShipAddressAddr4 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr5"))
                                {
                                    item.ShipAddressAddr5 = g.InnerText;
                                }
                                else if (g.Name.Contains("City"))
                                {
                                    item.ShipAddressCity = g.InnerText;
                                }
                                else if (g.Name.Contains("State"))
                                {
                                    item.ShipAddressState = g.InnerText;
                                }
                                else if (g.Name.Contains("PostalCode"))
                                {
                                    item.ShipAddressPostalCode = g.InnerText;
                                }
                                else if (g.Name.Contains("Country"))
                                {
                                    item.ShipAddressCountry = g.InnerText;
                                }
                                else if (g.Name.Contains("Note"))
                                {
                                    item.ShipAddressNote = g.InnerText;
                                }
                            }

                        }
                        #endregion

                        else if (f.Name.Contains("IsPending"))
                        {
                            item.IsPending = f.InnerText;
                        }
                        else if (f.Name.Contains("DueDate"))
                        {
                            item.DueDate = f.InnerText;
                        }
                        else if (f.Name.Contains("ShipDate"))
                        {
                            item.ShipDate = f.InnerText;
                        }
                        else if (f.Name.Contains("Subtotal"))
                        {
                            item.Subtotal = f.InnerText;
                        }
                        else if (f.Name.Contains("ItemSalesTaxRef"))
                        {
                            item.ItemSalesTaxRefListID = f.ChildNodes[0].InnerText;
                            item.ItemSalesTaxRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("SalesTaxPercentage"))
                        {
                            item.SalesTaxPercentage = f.InnerText;
                        }
                        else if (f.Name.Contains("SalesTaxTotal"))
                        {
                            item.SalesTaxTotal = f.InnerText;
                        }
                        else if (f.Name.Contains("TotalAmount"))
                        {
                            item.TotalAmount = f.InnerText;
                        }
                        else if (f.Name.Contains("CreditRemaining"))
                        {
                            item.CreditRemaining = f.InnerText;
                        }
                        else if (f.Name.Contains("IsToBePrinted"))
                        {
                            item.IsToBePrinted = f.InnerText;
                        }
                        else if (f.Name.Contains("IsToBeEmailed"))
                        {
                            item.IsToBeEmailed = f.InnerText;
                        }
                        else if (f.Name.Contains("CustomerSalesTaxCodeRef"))
                        {
                            item.CustomerSalesTaxCodeRefListID = f.ChildNodes[0].InnerText;
                            item.CustomerSalesTaxCodeRefFullName = f.ChildNodes[1].InnerText;
                        }

                        #region LineRet
                        else if (f.Name.Contains("CreditMemoLineRet"))
                        {
                            CreditMemoLineRet lineItem = new CreditMemoLineRet();
                            XmlNodeList lineRetFieldList = f.ChildNodes;

                            lineItem.TxnID = item.TxnID;
                            lineItem.EditSequence = item.EditSequence;

                            foreach (XmlNode lf in lineRetFieldList)
                            {
                                if (lf.Name.Contains("TxnLineID"))
                                {
                                    lineItem.TxnLineID = lf.InnerText;
                                }
                                else if (lf.Name.Contains("ItemRef"))
                                {
                                    lineItem.ItemRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.ItemRefFullName = lf.ChildNodes[1].InnerText;
                                }
                                else if (lf.Name.Contains("Desc"))
                                {
                                    lineItem.Desc = lf.InnerText;
                                }
                                else if (lf.Name.Contains("Quantity"))
                                {
                                    lineItem.Quantity = lf.InnerText;
                                }
                                else if (lf.Name.Contains("OverrideUOMSetRef"))
                                {
                                    lineItem.OverrideUOMSetRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.OverrideUOMSetRefFullName = lf.ChildNodes[1].InnerText;
                                }
                                else if (lf.Name.Contains("Rate"))
                                {
                                    lineItem.Rate = lf.InnerText;
                                }
                                else if (lf.Name.Contains("RatePercent"))
                                {
                                    lineItem.RatePercent = lf.InnerText;
                                }
                                else if (lf.Name.Contains("ClassRef"))
                                {
                                    lineItem.ClassRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.ClassRefFullName = lf.ChildNodes[1].InnerText;
                                }

                                else if (lf.Name.Contains("Amount"))
                                {
                                    lineItem.Amount = lf.InnerText;
                                }
                                else if (lf.Name.Contains("InventorySiteRef"))
                                {
                                    lineItem.InventorySiteRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.InventorySiteRefFullName = lf.ChildNodes[1].InnerText;
                                }
                                else if (lf.Name.Contains("InventorySiteLocationRef"))
                                {
                                    lineItem.InventorySiteLocationRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.InventorySiteLocationRefFullName = lf.ChildNodes[1].InnerText;
                                }
                                else if (lf.Name.Contains("SerialNumber"))
                                {
                                    lineItem.SerialNumber = lf.InnerText;
                                }
                                else if (lf.Name.Contains("LotNumber"))
                                {
                                    lineItem.LotNumber = lf.InnerText;
                                }
                                else if (lf.Name.Contains("SalesTaxCodeRef"))
                                {
                                    lineItem.SalesTaxCodeRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.SalesTaxCodeRefFullName = lf.ChildNodes[1].InnerText;
                                }
                            }

                            lineList.Add(lineItem);
                        }
                        #endregion

                        item.LineRetList = lineList;
                    }
                    list.Add(item);
                }
            }
            return list;
        }
    }
}