using System;
using System.Data.SqlClient;
using System.Linq;
using _02.VillainNames;

namespace _04.AddMinion
{
    public class StartUp
    {
        public static void Main()
        {
            string[] minionInfo = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries).ToArray();
            string[] vilianInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            string minionName = minionInfo[1];
            string minionAge = minionInfo[2];
            string minionTown = minionInfo[3];
            string vilianName = vilianInfo[1];
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string command = @"SELECT Id FROM Villains WHERE Name = @Name";
                int? vilianId = SelectOrInsertVilian(vilianName, connection, command);

                command = @"SELECT Id FROM Towns WHERE Name = @townName";
                int? minionTownId = SelectOrInsertTown(minionTown, connection, command);

                command = @"SELECT Id FROM Minions WHERE Name = @Name";
                int minionId = SelectOrInsertMinion(minionName, minionAge, connection, command, minionTownId);

                command = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";
                InsertMinionsVillains(command, connection,vilianId, minionId);
                Console.WriteLine($"Successfully added {minionName} to be minion of {vilianName}.");
            }
        }

        private static void InsertMinionsVillains(string command, SqlConnection connection, int? vilianId, int minionId)
        {
            using (SqlCommand commandInsert = new SqlCommand(command,connection))
            {
                commandInsert.Parameters.AddWithValue("@villainId", vilianId);
                commandInsert.Parameters.AddWithValue("@minionId", minionId);
                commandInsert.ExecuteNonQuery();
            }
        }

        private static int? SelectOrInsertTown(string minionTown, SqlConnection connection, string command)
        {
            using (SqlCommand commandSelect = new SqlCommand(command, connection))
            {
                commandSelect.Parameters.AddWithValue("@townName", minionTown);
                int? minionTownId = (int?)commandSelect.ExecuteScalar();
                if (minionTownId == 0)
                {
                    string insert = @"INSERT INTO Towns (Name) VALUES (@townName)";
                    using (SqlCommand commandInsert = new SqlCommand(insert, connection))
                    {
                        commandInsert.Parameters.AddWithValue("@townName", minionTown);
                        commandInsert.ExecuteNonQuery();
                    }
                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }
                return minionTownId;
            }
        }

        private static int SelectOrInsertMinion(string minionName, string minionAge, SqlConnection connection, string command, int? minionTownId)
        {
            using (SqlCommand commandSelect = new SqlCommand(command, connection))
            {
                commandSelect.Parameters.AddWithValue("@Name", minionName);
                int minionId = (int)commandSelect.ExecuteScalar();
                if (minionId == 0)
                {
                    string insert = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";
                    using (SqlCommand commandInsert = new SqlCommand(insert, connection))
                    {
                        commandInsert.Parameters.AddWithValue("@name", minionName);
                        commandInsert.Parameters.AddWithValue("@age", minionAge);
                        commandInsert.Parameters.AddWithValue("@townId", minionTownId);
                        commandInsert.ExecuteNonQuery();
                    }
                }
                return minionId;
            }
        }

        private static int? SelectOrInsertVilian(string vilianName, SqlConnection connection, string command)
        {
            using (SqlCommand commandSelect = new SqlCommand(command, connection))
            {
                commandSelect.Parameters.AddWithValue("@Name", vilianName);
                int? vilianId = (int?)commandSelect.ExecuteScalar();
                if (vilianId == null)
                {
                    string insert = @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
                    using (SqlCommand commandInsert = new SqlCommand(insert, connection))
                    {
                        commandInsert.Parameters.AddWithValue("@villainName", vilianName);
                        commandInsert.ExecuteNonQuery();
                    }
                    Console.WriteLine($"Villain {vilianName} was added to the database.");
                }
                return vilianId;
            }
        }
    }
}
