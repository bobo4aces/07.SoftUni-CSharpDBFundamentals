using System;
using System.Data.SqlClient;
using _02.VillainNames;

namespace _09.IncreaseAgeStorProc
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                string procedure = "usp_GetOlder @id";
                using (SqlCommand command = new SqlCommand(procedure, connection))
                {
                    command.Parameters.AddWithValue("@id", minionId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} - {reader[1]} years old");
                        }
                    }
                }
            }
        }
    }
}
