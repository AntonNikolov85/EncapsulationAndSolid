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
        public FileInfo GetFileInfo(int id)
        {
            throw new NotImplementedException();
        }

        public Maybe<string> ReadAllText(int id)
        {
            using (SqlConnection connection = new SqlConnection(id.ToString()))
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

            return new Maybe<string>(id.ToString());
        }

        public void WriteAllText(int id, string message)
        {
            using (SqlConnection connection = new SqlConnection(id.ToString()))
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
