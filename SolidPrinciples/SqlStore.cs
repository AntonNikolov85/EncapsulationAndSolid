using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPrinciples
{
    public class SqlStore : IStore
    {
        public FileInfo GetFileInfo(int id, string workingDirectory)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Select * from Messages", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"{reader["id"]} - {reader["description"]}");
                        }
                    }
                }
            }

            return path;
        }

        public void WriteAllText(string path, string message)
        {
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();

                string query = "Insert into db.Messages(description) Values(@description)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@description", message);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
