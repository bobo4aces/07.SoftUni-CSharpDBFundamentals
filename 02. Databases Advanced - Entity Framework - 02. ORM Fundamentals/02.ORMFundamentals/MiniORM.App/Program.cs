namespace MiniORM.App
{
    using Data;
    using Data.Entities;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            string connectionParams = "Server=.; Database=MiniORM; Integrated Security=true;";

            var context = new SoftUniDbContextClass(connectionParams);

            var emp1 = new Employee()
            {
                FirstName = "I.",
                LastName = "Ivanov",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true,
            };

            context.Employees.Add(emp1);
            context.SaveChanges();

            var employee = context.Employees.Last();
            employee.FirstName = "Name";

            context.SaveChanges();
        }
    }
}
