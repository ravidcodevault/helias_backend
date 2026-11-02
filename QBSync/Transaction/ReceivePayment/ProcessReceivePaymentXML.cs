using System.Collections.Generic;
using System.Xml;


namespace QB2DB_Sync
{
    public class ProcessReceivePaymentXML
    {
        public static List<ReceivePaymentRet> ConvertReceivePaymentXML_to_List(string XMLResponse)
        {
            List<ReceivePaymentRet> list = new List<ReceivePaymentRet>();

            XmlNodeList retList = QBSDKHelper.ParseXMLResponse(
                "ReceivePaymentQueryRs", "ReceivePaymentRet", XMLResponse, false);

            if (retList == null)
            {
                throw new XMLProcessingException("Unable to parse ReceivePayment XML from QuickBook");
            }
            if (retList != null)
            {
                foreach (XmlNode ret in retList)
                {
                    ReceivePaymentRet item = new ReceivePaymentRet();
                    List<AppliedToTxnRet> appliedToTxnList = new List<AppliedToTxnRet>();

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
                        else if (f.Name.Contains("TxnDate"))
                        {
                            item.TxnDate = f.InnerText;
                        }
                        else if (f.Name.Contains("RefNumber"))
                        {
                            item.RefNumber = f.InnerText;
                        }
                        else if (f.Name.Contains("TotalAmount"))
                        {
                            item.TotalAmount = f.InnerText;
                        }
                        else if (f.Name.Contains("PaymentMethodRef"))
                        {
                            item.PaymentMethodRefListID = f.ChildNodes[0].InnerText;
                            item.PaymentMethodRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("DepositToAccountRef"))
                        {
                            item.DepositToAccountRefListID = f.ChildNodes[0].InnerText;
                            item.DepositToAccountRefFullName = f.ChildNodes[1].InnerText;
                        }
                        else if (f.Name.Contains("UnusedPayment"))
                        {
                            item.UnusedPayment = f.InnerText;
                        }
                        else if (f.Name.Contains("UnusedCredits"))
                        {
                            item.UnusedCredits = f.InnerText;
                        }
                        else if (f.Name.Contains("AppliedToTxnRet"))
                        {
                            AppliedToTxnRet lineItem = new AppliedToTxnRet();
                            XmlNodeList lineRetFieldList = f.ChildNodes;

                            foreach (XmlNode lf in lineRetFieldList)
                            {
                                if (lf.Name.Contains("TxnID"))
                                {
                                    lineItem.TxnID = lf.InnerText;
                                }
                                else if (lf.Name.Contains("TxnType"))
                                {
                                    lineItem.TxnType = lf.InnerText;
                                }
                                else if (lf.Name.Contains("TxnDate"))
                                {
                                    lineItem.TxnDate = lf.InnerText;
                                }
                                else if (lf.Name.Contains("RefNumber"))
                                {
                                    lineItem.RefNumber = lf.InnerText;
                                }
                                else if (lf.Name.Contains("BalanceRemaining"))
                                {
                                    lineItem.BalanceRemaining = lf.InnerText;
                                }
                                else if (lf.Name == "Amount")
                                {
                                    lineItem.Amount = lf.InnerText;
                                }
                                else if (lf.Name.Contains("DiscountAmount"))
                                {
                                    lineItem.DiscountAmount = lf.InnerText;
                                }
                                else if (lf.Name.Contains("DiscountAccountRef"))
                                {
                                    lineItem.DiscountAccountRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.DiscountAccountRefFullName = lf.ChildNodes[1].InnerText;
                                }
                                else if (lf.Name.Contains("DiscountClassRef"))
                                {
                                    lineItem.DiscountClassRefListID = lf.ChildNodes[0].InnerText;
                                    lineItem.DiscountClassRefFullName = lf.ChildNodes[1].InnerText;
                                }
                            }

                            appliedToTxnList.Add(lineItem);

                        }

                        item.AppliedTxnRetList = appliedToTxnList;

                    }

                    list.Add(item);
                }
            }
            return list;
        }
    }
}