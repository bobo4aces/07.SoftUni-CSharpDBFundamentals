using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using _02.VillainNames;

namespace _07.PrintAllMinionNames
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                string selectCommand = "SELECT Name FROM Minions";
                using (SqlCommand command = new SqlCommand(selectCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> names = new List<string>();

                        while (reader.Read())
                        {
                            names.Add((string)reader[0]);
                        }
                        
                        for (int i = 0; i < names.Count / 2; i++)
                        {
                            Console.WriteLine(names[i]);
                            Console.WriteLine(names[names.Count - 1 - i]);
                        }
                        if (names.Count % 2 != 0)
                        {
                            Console.WriteLine(names[names.Count / 2]);
                        }
                    }
                }
            }
        }
    }
}
