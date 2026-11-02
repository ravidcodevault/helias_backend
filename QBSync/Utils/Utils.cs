using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace QB2DB_Sync
{
    public class Utils
    {
		private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

		public string GetLastTimeModified(string table)
        {
            string value = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //string sql = "select Quickbooks.dbo.ufn_Invoice_LastTimeModified()";
                string sql = "SELECT TOP 1 [TimeModified] FROM " + table +
                                " ORDER BY TimeModified DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    value = (string)cmd.ExecuteScalar();
                }
            }

            return value;
        }

        public static DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                // Add DataColumn with the same name and type as the property
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    PropertyInfo prop = Props[i];
                    // Get the value of the property
                    object value = prop.GetValue(item);

                    // Convert null values to DBNull.Value
                    if (value == null)
                    {
                        value = DBNull.Value;
                    }

                    // Inserting property values to DataTable rows
                    values[i] = value;
                }

                dataTable.Rows.Add(values);
            }

            // Return the DataTable
            return dataTable;
        }


    }
}