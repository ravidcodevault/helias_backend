using System.Collections.Generic;
using System.Xml;


namespace QB2DB_Sync
{
    public class ProcessCustomerXML
    {
        public static List<CustomerRet> ConvertCustomerXML_to_List(string XMLResponse)
        {
            List<CustomerRet> list = new List<CustomerRet>();

            //step4: parse the XML response and show a message
            XmlNodeList retList = QBSDKHelper.ParseXMLResponse(
                "CustomerQueryRs", "CustomerRet", XMLResponse, false);

            if (retList == null)
            {
                throw new XMLProcessingException("Unable to parse Customer XML from QuickBook");
            }
            if (retList != null)
            {
                foreach (XmlNode ret in retList)
                {
                    CustomerRet item = new CustomerRet();
                    XmlNodeList fieldList = ret.ChildNodes;
                    foreach (XmlNode f in fieldList)
                    {
                        if (f.Name.Contains("ListID"))
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
                        else if (f.Name == "Name")    // do not use f.Name.Contains
                        {
                            item.Name = f.InnerText;
                        }
                        else if (f.Name.Contains("FullName"))
                        {
                            item.FullName = f.InnerText;
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
                        else if (f.Name.Contains("CompanyName"))
                        {
                            item.CompanyName = f.InnerText;
                        }
                        else if (f.Name.Contains("Salutation"))
                        {
                            item.Salutation = f.InnerText;
                        }
                        else if (f.Name.Contains("FirstName"))
                        {
                            item.FirstName = f.InnerText;
                        }
                        else if (f.Name.Contains("MiddleName"))
                        {
                            item.MiddleName = f.InnerText;
                        }
                        else if (f.Name.Contains("LastName"))
                        {
                            item.LastName = f.InnerText;
                        }
                        else if (f.Name.Contains("JobTitle"))
                        {
                            item.JobTitle = f.InnerText;
                        }
                        else if (f.Name == "BillAddress")
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
                        else if (f.Name.Contains("BillAddressBlock"))
                        {
                            XmlNodeList addressFieldList = f.ChildNodes;

                            foreach (XmlNode g in addressFieldList)
                            {
                                if (g.Name.Contains("Addr1"))
                                {
                                    item.BillAddressBlockAddr1 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr2"))
                                {
                                    item.BillAddressBlockAddr2 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr3"))
                                {
                                    item.BillAddressBlockAddr3 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr4"))
                                {
                                    item.BillAddressBlockAddr4 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr5"))
                                {
                                    item.BillAddressBlockAddr5 = g.InnerText;
                                }
                            }
                        }
                        else if (f.Name == "ShipAddress")
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
                        else if (f.Name.Contains("ShipAddressBlock"))
                        {
                            XmlNodeList addressFieldList = f.ChildNodes;

                            foreach (XmlNode g in addressFieldList)
                            {
                                if (g.Name.Contains("Addr1"))
                                {
                                    item.ShipAddressBlockAddr1 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr2"))
                                {
                                    item.ShipAddressBlockAddr2 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr3"))
                                {
                                    item.ShipAddressBlockAddr3 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr4"))
                                {
                                    item.ShipAddressBlockAddr4 = g.InnerText;
                                }
                                else if (g.Name.Contains("Addr5"))
                                {
                                    item.ShipAddressBlockAddr5 = g.InnerText;
                                }
                            }

                        }
                        else if (f.Name == "Phone")
                        {
                            item.Phone = f.InnerText;
                        }
                        else if (f.Name.Contains("AltPhone"))
                        {
                            item.AltPhone = f.InnerText;
                        }
                        else if (f.Name.Contains("Fax"))
                        {
                            item.Fax = f.InnerText;
                        }
                        else if (f.Name.Contains("Email"))
                        {
                            item.Email = f.InnerText;
                        }
                        else if (f.Name.Contains("Cc"))
                        {
                            item.Cc = f.InnerText;
                        }
                        else if (f.Name == "Contact")
                        {
                            item.Contact = f.InnerText;
                        }
                        else if (f.Name.Contains("AltContact"))
                        {
                            item.AltContact = f.InnerText;
                        }
                        else if (f.Name.Contains("CustomerTypeRef"))
                        {
                            item.CustomerTypeRefListID = f.ChildNodes[0].InnerText;
                            item.CustomerTypeRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("TermsRef"))
                        {
                            item.TermsRefListID = f.ChildNodes[0].InnerText;
                            item.TermsRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("SalesRepRef"))
                        {
                            item.SalesRepRefListID = f.ChildNodes[0].InnerText;
                            item.SalesRepRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name == "Balance")
                        {
                            item.Balance = f.InnerText;
                        }
                        else if (f.Name.Contains("TotalBalance"))
                        {
                            item.TotalBalance = f.InnerText;
                        }
                        else if (f.Name.Contains("SalesTaxCodeRef"))
                        {
                            item.SalesTaxCodeRefListID = f.ChildNodes[0].InnerText;
                            item.SalesTaxCodeRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("ItemSalesTaxRef"))
                        {
                            item.ItemSalesTaxRefListID = f.ChildNodes[0].InnerText;
                            item.ItemSalesTaxRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("ResaleNumber"))
                        {
                            item.ResaleNumber = f.InnerText;
                        }
                        else if (f.Name.Contains("AccountNumber"))
                        {
                            item.AccountNumber = f.InnerText;
                        }
                        else if (f.Name.Contains("CreditLimit"))
                        {
                            item.CreditLimit = f.InnerText;
                        }
                        else if (f.Name.Contains("PreferredPaymentMethodRef"))
                        {
                            item.PreferredPaymentMethodRefListID = f.ChildNodes[0].InnerText;
                            item.PreferredPaymentMethodRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("JobStatus"))
                        {
                            item.JobStatus = f.InnerText;
                        }
                        else if (f.Name.Contains("PreferredDeliveryMethod"))
                        {
                            item.PreferredDeliveryMethod = f.InnerText;
                        }
                        else if (f.Name.Contains("PriceLevelRef"))
                        {
                            item.PriceLevelRefListID = f.ChildNodes[0].InnerText;
                            item.PriceLevelRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("ExternalGUID"))
                        {
                            item.ExternalGUID = f.InnerText;
                        }
                        else if (f.Name.Contains("CurrencyRef"))
                        {
                            item.CurrencyRefListID = f.ChildNodes[0].InnerText;
                            item.CurrencyRefFullName = f.ChildNodes[1].InnerText;
                        }
                    }

                    list.Add(item);
                }
            }
            return list;
        }
    }
}