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
    public class ManagerInfoCommand : ICommand
    {
        private readonly EmployeeDbContext context;
        private readonly Mapper mapper;

        public ManagerInfoCommand(EmployeeDbContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public string Execute(string[] inputArgs)
        {
            this.context.Database.EnsureCreated();

            int employeeId = int.Parse(inputArgs[0]);

            Employee employee = this.context.Employees
                .Where(e => e.Id == employeeId)
                .FirstOrDefault();

            if (employee == null)
            {
                throw new ArgumentNullException("Invalid employee ID!");
            }
            
            ManagerDto managerDto = this.mapper.CreateMappedObject<ManagerDto>(employee);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.ManagedEmployeesCount}");
            if (managerDto.ManagedEmployeesCount > 0)
            {
                foreach (var managedEmployee in managerDto.ManagedEmployees)
                {
                    sb.AppendLine($"\t- {managedEmployee.FirstName} {managedEmployee.LastName} - ${managedEmployee.Salary:f2}");
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
