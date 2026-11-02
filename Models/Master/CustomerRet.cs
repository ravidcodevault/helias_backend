using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class CustomerRet
    {
        public string ListID { get; set; } //IDTYPE</ListID> <!-- required -->
        public string TimeCreated { get; set; } //DATETIMETYPE</TimeCreated> <!-- required -->
        public string TimeModified { get; set; } //DATETIMETYPE</TimeModified> <!-- required -->
        public string EditSequence { get; set; } //STRTYPE</EditSequence> <!-- required -->

        public string Name { get; set; } // STRTYPE</Name> <!-- required -->
        public string FullName { get; set; } //STRTYPE</FullName> <!-- required -->
        public string IsActive { get; set; } //BOOLTYPE</IsActive> <!-- optional -->

        //<ClassRef> <!-- optional --> 
        public string ClassRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string ClassRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        //<ParentRef> <!-- optional --> 
        public string ParentRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string ParentRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        public string Sublevel { get; set; } //INTTYPE</Sublevel> <!-- required -->

        public string CompanyName { get; set; } //STRTYPE</CompanyName> <!-- optional -->
        public string Salutation { get; set; } //STRTYPE</Salutation> <!-- optional -->
        public string FirstName { get; set; } //STRTYPE</FirstName> <!-- optional -->
        public string MiddleName { get; set; } //STRTYPE</MiddleName> <!-- optional -->
        public string LastName { get; set; } //STRTYPE</LastName> <!-- optional -->
        public string JobTitle { get; set; } //STRTYPE</JobTitle> <!-- optional -->

        //<BillAddress> <!-- optional --> 
        public string BillAddressAddr1 { get; set; } //STRTYPE</Addr1> <!-- optional -->
        public string BillAddressAddr2 { get; set; } //STRTYPE</Addr2> <!-- optional -->
        public string BillAddressAddr3 { get; set; } //STRTYPE</Addr3> <!-- optional -->
        public string BillAddressAddr4 { get; set; } //STRTYPE</Addr4> <!-- optional -->
        public string BillAddressAddr5 { get; set; } //STRTYPE</Addr5> <!-- optional -->
        public string BillAddressCity { get; set; } //STRTYPE</City> <!-- optional -->
        public string BillAddressState { get; set; } //STRTYPE</State> <!-- optional -->
        public string BillAddressPostalCode { get; set; } //STRTYPE</PostalCode> <!-- optional -->
        public string BillAddressCountry { get; set; } //STRTYPE</Country> <!-- optional -->
        public string BillAddressNote { get; set; } //STRTYPE</Note> <!-- optional -->

        //<BillAddressBlock> <!-- optional --> 
        public string BillAddressBlockAddr1 { get; set; } //STRTYPE</Addr1> <!-- optional -->
        public string BillAddressBlockAddr2 { get; set; } //STRTYPE</Addr2> <!-- optional -->
        public string BillAddressBlockAddr3 { get; set; } //STRTYPE</Addr3> <!-- optional -->
        public string BillAddressBlockAddr4 { get; set; } //STRTYPE</Addr4> <!-- optional -->
        public string BillAddressBlockAddr5 { get; set; } //STRTYPE</Addr5> <!-- optional -->
                                                          //</BillAddressBlock>

        //<ShipAddress> <!-- optional --> 
        public string ShipAddressAddr1 { get; set; } //STRTYPE</Addr1> <!-- optional -->
        public string ShipAddressAddr2 { get; set; } //STRTYPE</Addr2> <!-- optional -->
        public string ShipAddressAddr3 { get; set; } //STRTYPE</Addr3> <!-- optional -->
        public string ShipAddressAddr4 { get; set; } //STRTYPE</Addr4> <!-- optional -->
        public string ShipAddressAddr5 { get; set; } //STRTYPE</Addr5> <!-- optional -->
        public string ShipAddressCity { get; set; } //STRTYPE</City> <!-- optional -->
        public string ShipAddressState { get; set; } //STRTYPE</State> <!-- optional -->
        public string ShipAddressPostalCode { get; set; } //STRTYPE</PostalCode> <!-- optional -->
        public string ShipAddressCountry { get; set; } //STRTYPE</Country> <!-- optional -->
        public string ShipAddressNote { get; set; } //STRTYPE</Note> <!-- optional -->

        //<ShipAddressBlock> <!-- optional --> 
        public string ShipAddressBlockAddr1 { get; set; } //STRTYPE</Addr1> <!-- optional -->
        public string ShipAddressBlockAddr2 { get; set; } //STRTYPE</Addr2> <!-- optional -->
        public string ShipAddressBlockAddr3 { get; set; } //STRTYPE</Addr3> <!-- optional -->
        public string ShipAddressBlockAddr4 { get; set; } //STRTYPE</Addr4> <!-- optional -->
        public string ShipAddressBlockAddr5 { get; set; } //STRTYPE</Addr5> <!-- optional -->
                                                          //</ShipAddressBlock>

        //<ShipToAddress> // must occur 0 - 50 times --> 
        //<Name >STRTYPE</Name> <!-- required -->
        //<Addr1 >STRTYPE</Addr1> <!-- optional -->
        //<Addr2 >STRTYPE</Addr2> <!-- optional -->
        //<Addr3 >STRTYPE</Addr3> <!-- optional -->
        //<Addr4 >STRTYPE</Addr4> <!-- optional -->
        //<Addr5 >STRTYPE</Addr5> <!-- optional -->
        //<City >STRTYPE</City> <!-- optional -->
        //<State >STRTYPE</State> <!-- optional -->
        //<PostalCode >STRTYPE</PostalCode> <!-- optional -->
        //<Country >STRTYPE</Country> <!-- optional -->
        //<Note >STRTYPE</Note> <!-- optional -->
        //<DefaultShipTo >BOOLTYPE</DefaultShipTo> <!-- optional -->
        //</ShipToAddress>

        public string Phone { get; set; } //STRTYPE</Phone> <!-- optional -->
        public string AltPhone { get; set; } //STRTYPE</AltPhone> <!-- optional -->
        public string Fax { get; set; } //STRTYPE</Fax> <!-- optional -->
        public string Email { get; set; } //STRTYPE</Email> <!-- optional -->
        public string Cc { get; set; } //STRTYPE</Cc> <!-- optional -->
        public string Contact { get; set; } //STRTYPE</Contact> <!-- optional -->
        public string AltContact { get; set; } //STRTYPE</AltContact> <!-- optional -->

        //<AdditionalContactRef> // must occur 0 - 8 times --> 
        //<AdditionalContactRefContactName >STRTYPE</ContactName> <!-- required -->
        //<AdditionalContactRefContactValue >STRTYPE</ContactValue> <!-- required -->
        //</AdditionalContactRef>

        //<ContactsRet> // optional, may repeat --> 
        //<ListID >IDTYPE</ListID> <!-- required -->
        //<TimeCreated >DATETIMETYPE</TimeCreated> <!-- required -->
        //<TimeModified >DATETIMETYPE</TimeModified> <!-- required -->
        //<EditSequence >STRTYPE</EditSequence> <!-- required -->
        //<Contact >STRTYPE</Contact> <!-- optional -->
        //<Salutation >STRTYPE</Salutation> <!-- optional -->
        //<FirstName >STRTYPE</FirstName> <!-- required -->
        //<MiddleName >STRTYPE</MiddleName> <!-- optional -->
        //<LastName >STRTYPE</LastName> <!-- optional -->
        //<JobTitle >STRTYPE</JobTitle> <!-- optional -->
        //<AdditionalContactRef> // must occur 0 - 5 times --> 
        //<ContactName >STRTYPE</ContactName> <!-- required -->
        //<ContactValue >STRTYPE</ContactValue> <!-- required -->
        //</AdditionalContactRef>
        //</ContactsRet>

        //<CustomerTypeRef> <!-- optional --> 
        public string CustomerTypeRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string CustomerTypeRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        //<TermsRef> <!-- optional --> 
        public string TermsRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string TermsRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        //<SalesRepRef> <!-- optional --> 
        public string SalesRepRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string SalesRepRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        public string Balance { get; set; } //AMTTYPE</Balance> <!-- optional -->
        public string TotalBalance { get; set; } //AMTTYPE</TotalBalance> <!-- optional -->

        //<SalesTaxCodeRef> <!-- optional --> 
        public string SalesTaxCodeRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string SalesTaxCodeRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        //<ItemSalesTaxRef> <!-- optional --> 
        public string ItemSalesTaxRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string ItemSalesTaxRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        public string ResaleNumber { get; set; } //STRTYPE</ResaleNumber> <!-- optional -->
        public string AccountNumber { get; set; } //STRTYPE</AccountNumber> <!-- optional -->
        public string CreditLimit { get; set; } //AMTTYPE</CreditLimit> <!-- optional -->

        //<PreferredPaymentMethodRef> <!-- optional --> 
        public string PreferredPaymentMethodRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string PreferredPaymentMethodRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        //<CreditCardInfo> <!-- optional --> 
        //    <CreditCardNumber >STRTYPE</CreditCardNumber> <!-- optional -->
        //    <ExpirationMonth >INTTYPE</ExpirationMonth> <!-- optional -->
        //    <ExpirationYear >INTTYPE</ExpirationYear> <!-- optional -->
        //    <NameOnCard >STRTYPE</NameOnCard> <!-- optional -->
        //    <CreditCardAddress >STRTYPE</CreditCardAddress> <!-- optional -->
        //    <CreditCardPostalCode >STRTYPE</CreditCardPostalCode> <!-- optional -->
        //</CreditCardInfo>

        //<!-- JobStatus may have one of the following values: Awarded, Closed, InProgress, None [DEFAULT], NotAwarded, Pending -->
        public string JobStatus { get; set; } //ENUMTYPE</JobStatus> <!-- optional -->
                                              //<JobStartDate >DATETYPE</JobStartDate> <!-- optional -->
                                              //<JobProjectedEndDate >DATETYPE</JobProjectedEndDate> <!-- optional -->
                                              //<JobEndDate >DATETYPE</JobEndDate> <!-- optional -->
                                              //<JobDesc >STRTYPE</JobDesc> <!-- optional -->
                                              //<JobTypeRef> <!-- optional --> 
                                              //    <ListID >IDTYPE</ListID> <!-- optional -->
                                              //    <FullName >STRTYPE</FullName> <!-- optional -->
                                              //</JobTypeRef>
                                              //<Notes >STRTYPE</Notes> <!-- optional -->
                                              //<AdditionalNotesRet> <!-- optional, may repeat --> 
                                              //    <NoteID >INTTYPE</NoteID> <!-- required -->
                                              //    <Date >DATETYPE</Date> <!-- required -->
                                              //    <Note >STRTYPE</Note> <!-- required -->
                                              //</AdditionalNotesRet>

        // PreferredDeliveryMethod may have one of the following values: None [Default], Email, Fax -->
        public string PreferredDeliveryMethod { get; set; } //ENUMTYPE</PreferredDeliveryMethod> <!-- optional -->

        //<PriceLevelRef> <!-- optional --> 
        public string PriceLevelRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string PriceLevelRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

        public string ExternalGUID { get; set; } //GUIDTYPE</ExternalGUID> <!-- optional -->

        //<CurrencyRef> <!-- optional --> 
        public string CurrencyRefListID { get; set; } //IDTYPE</ListID> <!-- optional -->
        public string CurrencyRefFullName { get; set; } //STRTYPE</FullName> <!-- optional -->

    }
}
