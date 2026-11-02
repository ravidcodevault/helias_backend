using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QB2DB_Sync
{
    public class ProcessXMLRequest
    {
        public static string GetMasterData(string type, string modDateTimeFrom, string modDateTimeTo)
        {
            string response = null;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <?qbxml version=""12.0""?>
                            <QBXML>
                            <QBXMLMsgsRq onError=""stopOnError"" >");
            sb.Append("<");
            sb.Append(type + "QueryRq");
            sb.Append(@" requestID=""1"" >");
            sb.Append(@"<ActiveStatus>All</ActiveStatus>");
            if (modDateTimeFrom != null && modDateTimeFrom != "")
            {
                sb.Append(@"<FromModifiedDate>" + modDateTimeFrom + "</FromModifiedDate>");
                sb.Append(@"<ToModifiedDate>" + modDateTimeTo + "</ToModifiedDate>");
            }
            sb.Append("</");
            sb.Append(type + "QueryRq");
            sb.Append(">");
            sb.Append("</QBXMLMsgsRq></QBXML>");
            response = ProcessRequest.GetResponseByQBXMLRPRequest(sb.ToString());
            return response;
        }

        public static string GetTransactionData(string type, string dateTimeFrom, string dateTimeTo)
        {
            string response = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <?qbxml version=""12.0""?>
                            <QBXML>
                            <QBXMLMsgsRq onError=""stopOnError"" >");
            sb.Append("<");

            sb.Append(type + "QueryRq");
            sb.Append(@" requestID=""1"" >");
            sb.Append(@"<ModifiedDateRangeFilter>");
            sb.Append(@"<FromModifiedDate>" + dateTimeFrom + "</FromModifiedDate>");
            sb.Append(@"<ToModifiedDate>" + dateTimeTo + "</ToModifiedDate>");
            sb.Append(@"</ModifiedDateRangeFilter>");
            //sb.Append(@"<TxnDateRangeFilter>");
            //sb.Append(@"<FromTxnDate>" + txnDateFrom + "</FromTxnDate>");
            //sb.Append(@"<ToTxnDate>" + txnDateTo + "</ToTxnDate>");
            //sb.Append(@"</TxnDateRangeFilter>");

            if (type != "BuildAssembly")
            {
                sb.Append(@"<IncludeLineItems>true</IncludeLineItems>");
            }

            sb.Append("</");
            sb.Append(type + "QueryRq");
            sb.Append(">");
            sb.Append("</QBXMLMsgsRq></QBXML>");

            response = ProcessRequest.GetResponseByQBXMLRPRequest(sb.ToString());

            return response;
        }
    }
}