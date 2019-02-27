using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using _02.VillainNames;

namespace _05.ChangeTownCasing
{
    public class StartUp
    {
        public static void Main()
        {
            string country = Console.ReadLine();
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                string update = @"UPDATE Towns
   SET Name = UPPER(Name)
 WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";
                using (SqlCommand command = new SqlCommand(update, connection))
                {
                    command.Parameters.AddWithValue("@countryName",country);
                    int result = command.ExecuteNonQuery();
                    if (result <= 0)
                    {
                        Console.WriteLine("No town names were affected.");
                        return;
                    }
                    string select = @"SELECT t.Name 
   FROM Towns as t
   JOIN Countries AS c ON c.Id = t.CountryCode
  WHERE c.Name = @countryName";
                    using (SqlCommand selectCommand = new SqlCommand(select,connection))
                    {
                        selectCommand.Parameters.AddWithValue("@countryName", country);
                        List<string> towns = new List<string>();
                        using (SqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                towns.Add((string)reader[0]);
                            }
                        }
                        Console.WriteLine($"{towns.Count} town names were affected.");
                        Console.WriteLine(string.Join(", ",towns));
                    }
                }
            }
        }
    }
}
