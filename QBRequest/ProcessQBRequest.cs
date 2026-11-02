using QBXMLRP2Lib;
using System;
using System.Configuration;

namespace QB2DB_Sync
{
    public class ProcessRequest
    {
        public static string GetResponseByQBXMLRPRequest(string inputXml)
        {
            RequestProcessor2 rp = null;
            string ticket = null;
            try
            {
                rp = new RequestProcessor2();
                rp.OpenConnection("", "Helias Intranet");
                var expectedFile = ConfigurationManager.AppSettings["QuickBooks.CompanyFile"] ?? "";
                ticket = rp.BeginSession(expectedFile, QBFileMode.qbFileOpenDoNotCare);
                if (!string.IsNullOrWhiteSpace(expectedFile) &&
                    !string.Equals(rp.GetCurrentCompanyFileName(ticket), expectedFile, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("QuickBooks opened a different company file.");
                return rp.ProcessRequest(ticket, inputXml);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("QuickBooks connection/request failed: " + ex.Message, ex);
            }
            finally
            {
                if (ticket != null) rp.EndSession(ticket);
                if (rp != null) rp.CloseConnection();
            }
        }
    }
}
