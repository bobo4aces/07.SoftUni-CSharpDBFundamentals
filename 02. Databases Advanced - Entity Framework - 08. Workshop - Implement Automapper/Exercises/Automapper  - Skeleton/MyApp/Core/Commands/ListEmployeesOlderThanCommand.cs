using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Core.ViewModels;
using MyApp.Data;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeeDbContext context;
        private readonly Mapper mapper;

        public ListEmployeesOlderThanCommand(EmployeeDbContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public string Execute(string[] inputArgs)
        {
            this.context.Database.EnsureCreated();

            int age = int.Parse(inputArgs[0]);

            List<Employee> employees = this.context.Employees
                .Where(e => e.Birthday.Value.Year + age < DateTime.Now.Year)
                .OrderByDescending(e=>e.Salary)
                .ToList();

            if (employees.Count < 1)
            {
                throw new ArgumentNullException("No such employee!");
            }

            StringBuilder sb = new StringBuilder();

            foreach (var employee in employees.OrderByDescending(e=>e.Salary))
            {
                ManagerDto managerDto = this.mapper.CreateMappedObject<ManagerDto>(employee);
                string managerLastName = managerDto.LastName;
                if (managerDto.ManagedEmployeesCount <= 0)
                {
                    managerLastName = "[no manager]";
                }
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - ${employee.Salary:f2} - Manager: {managerLastName}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
