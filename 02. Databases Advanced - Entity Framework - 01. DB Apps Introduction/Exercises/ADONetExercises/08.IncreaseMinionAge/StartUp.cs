using System;
using System.Data.SqlClient;
using System.Linq;
using _02.VillainNames;

namespace _08.IncreaseMinionAge
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] minionsIds = Console.ReadLine()
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string updateCommand = @"UPDATE Minions
   SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
 WHERE Id = @Id";

                
                foreach (var minionsId in minionsIds)
                {
                    using (SqlCommand command = new SqlCommand(updateCommand, connection))
                    {
                        command.Parameters.AddWithValue("@Id", minionsId);
                        command.ExecuteNonQuery();
                    }
                }

                string selectCommand = "SELECT Name, Age FROM Minions";

                using (SqlCommand command = new SqlCommand(selectCommand, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]}");
                        }
                    }
                }
            }
        }
    }
}
