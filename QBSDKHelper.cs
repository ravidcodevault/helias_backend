using System;
using System.Xml;

namespace QB2DB_Sync
{
    public class QBSDKHelper
    {
        public static XmlNodeList ParseXMLResponse(string queryRs, string rsRet, string response, bool isReport)
        {
            var doc = new XmlDocument();
            doc.LoadXml(response);
            var responses = doc.GetElementsByTagName(queryRs);
            if (responses.Count != 1)
                throw new XMLProcessingException("Expected one " + queryRs + " response from QuickBooks.");
            var rs = responses[0];
            var severity = rs.Attributes["statusSeverity"];
            if (severity != null && string.Equals(severity.Value, "Error", StringComparison.OrdinalIgnoreCase))
                throw new XMLProcessingException("QuickBooks " + rs.Attributes["statusCode"].Value +
                    ": " + rs.Attributes["statusMessage"].Value);
            if (!isReport) return rs.ChildNodes;
            if (rs.ChildNodes.Count == 1 && rs.ChildNodes[0].Name == rsRet) return rs.ChildNodes[0].ChildNodes;
            throw new XMLProcessingException("Unexpected QuickBooks report response.");
        }
    }
}
