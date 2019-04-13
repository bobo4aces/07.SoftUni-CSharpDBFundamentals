using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Data
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmployeeDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public EmployeeDbContext()
        {
        }
    }
}
