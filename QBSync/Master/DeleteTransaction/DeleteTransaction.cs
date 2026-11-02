using QB2DB_Sync;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QB_SQL_Sync.QBSync.Master.DeleteTransaction
{
    public class DeleteTransaction
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public void DeleteExsitingTransaction(int month, int year)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create a SqlCommand to execute the stored procedure
                    using (SqlCommand command = new SqlCommand("uspDeleteCurrentTransactions", connection))
                    {
                        // Specify that the SqlCommand is a stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add the parameters for the stored procedure
                        command.Parameters.AddWithValue("@month", month);
                        command.Parameters.AddWithValue("@year", year);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        Console.WriteLine("Transactions deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    throw new DataProcessingException("Exception occurred while deleting Transactions", ex);
                }
            }
        }
    }
}
