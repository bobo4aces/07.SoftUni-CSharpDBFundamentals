namespace MiniORM.App.Data
{
    using Entities;

    public class SoftUniDbContextClass : DbContext
    {
        public SoftUniDbContextClass(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Employee> Employees { get; }

        public DbSet<Department> Departments { get; }

        public DbSet<Project> Projects { get; }

        public DbSet<EmployeeProject> EmployeesProjects { get; }
    }
}