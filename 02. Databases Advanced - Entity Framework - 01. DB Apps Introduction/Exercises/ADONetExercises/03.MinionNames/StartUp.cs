using System;
using System.Data.SqlClient;
using _02.VillainNames;

namespace _03.MinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                sqlConnection.Open();
                int villianId = int.Parse(Console.ReadLine());
                string selectVillian = $"SELECT Name FROM Villains WHERE Id = @Id";
                
                using (SqlCommand sqlCommand = new SqlCommand(selectVillian, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", villianId);
                    string villianName = (string)sqlCommand.ExecuteScalar();
                    if (villianName == null)
                    {
                        Console.WriteLine($"No villain with ID <{villianId}> exists in the database.");
                        return;
                    }
                    Console.WriteLine($"Villain: {villianName}");
                }
                string selectMinions = $@"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";
                
                using (SqlCommand sqlCommand = new SqlCommand(selectMinions, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", villianId);
                    
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long rowNumber = (long)reader[0];
                            string minionName = (string)reader[1];
                            int minionAge = (int)reader[2];
                            Console.WriteLine($"{rowNumber}. {minionName} {minionAge}");
                        }
                        if (!reader.HasRows)
                        {
                            Console.WriteLine($"(no minions)");
                        }
                    }
                }
            }
        }
    }
}
