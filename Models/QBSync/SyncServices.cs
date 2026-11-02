using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace QB_SQL_Sync.Models.QBSync
{
    public static class SyncServices
    {
        private readonly static string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public static List<SyncDetails> GetAllSyncDetails()
        {
            var syncDetails = new List<SyncDetails>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[SyncStatus]", connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                syncDetails.Add(new SyncDetails
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    SyncType = Convert.ToString(reader["SyncType"]),
                                    LastSyncedOn = Convert.ToDateTime(reader["LastSyncedOn"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                                    Status = Convert.ToString(reader["Status"]),
                                    Description = ""
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return syncDetails;
        }

        public static List<SyncDetails> UpdateLastSyncedAndStatus(SyncDetails sync)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE dbo.SyncStatus SET Status=@Status, Description=@Description, " +
                    "LastSyncedOn=CASE WHEN @Status='Success' THEN GETDATE() ELSE LastSyncedOn END, " +
                    "[Exception]=CASE WHEN @Status IN ('Running','Success') THEN '' ELSE [Exception] END WHERE Id=@Id";
                command.Parameters.AddWithValue("@Status", sync.Status);
                command.Parameters.AddWithValue("@Description", sync.Description ?? "");
                command.Parameters.AddWithValue("@Id", sync.Id);
                connection.Open();
                if (command.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("SyncStatus row not found for " + sync.SyncType);
            }
            return new List<SyncDetails>();
        }
        public static void UpdateErros(SyncDetails sync)
        {
            var syncDetails = new List<SyncDetails>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand("UPDATE [dbo].[SyncStatus] SET [Exception] = @Exception WHERE Id = @Id", connection))
                        {
                            command.Parameters.AddWithValue("@Id", sync.Id);
                            command.Parameters.AddWithValue("@Exception", sync.Exception);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception)
                    {
                        //Ignored
                    }
                    finally
                    {
                        if (connection.State != System.Data.ConnectionState.Closed)
                        {
                            connection.Close();
                            connection.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
    public class SyncDetails
    {
        public int Id { get; set; }
        public string SyncType { get; set; }
        public DateTime LastSyncedOn { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Exception { get; set; }

    }

}
