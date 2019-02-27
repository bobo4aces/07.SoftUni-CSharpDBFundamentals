using System;
using System.Data.SqlClient;

namespace _02.VillainNames
{
    public class StartUp
    {
        public static void Main()
        {
            
            using (SqlConnection sqlConnection = new SqlConnection(Configuration.ConnectionString))
            {
                sqlConnection.Open();
                string selectVilliansAndMinions = "SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId GROUP BY v.Id, v.Name HAVING COUNT(mv.VillainId) > 3 ORDER BY COUNT(mv.VillainId)";
                string output = string.Empty;

                
                using (SqlCommand sqlCommand = new SqlCommand(selectVilliansAndMinions, sqlConnection))
                {
                    
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            int age = (int)reader["MinionsCount"];

                            Console.WriteLine($"{name} - {age}");
                        }
                    }
                }
            }
            
        }
    }
}
