using P03_SalesDatabase.Data;
using System;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        public static void Main()
        {
            using (SalesContext context = new SalesContext())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
