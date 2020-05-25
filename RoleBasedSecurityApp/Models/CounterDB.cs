using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RoleBasedSecurityApp.Models
{
    public class CounterDB
    {

        public static int GetCounter()
        {
            string connectionString = GetConnectionString();

            SqlCommand cmd;
            SqlConnection conn;
            SqlDataReader dr;

            string sql = "Select counter from counter where id = 1";

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        using (dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            return Convert.ToInt32(dr["counter"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static bool UpdateCounter(int value)
        {
            string connectionString = GetConnectionString();

            SqlCommand cmd;
            SqlConnection conn;

            string sql = "Update counter set counter = @counter where id = 1";

            int rowsAffected = 0;

            try
            {
                using (conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@counter", value);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                if (rowsAffected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static string GetConnectionString()
        {
            string connectionString = String.Empty;

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");
            
            return connectionString;

            //return "Server = (localdb)\\mssqllocaldb; Database = RBSDb; Trusted_Connection = True; MultipleActiveResultSets = true";
        }
    }
}
