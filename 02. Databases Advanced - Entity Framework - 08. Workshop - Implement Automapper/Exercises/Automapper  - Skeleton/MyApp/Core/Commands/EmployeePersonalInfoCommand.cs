using AutoMapper;
using MyApp.Core.Commands.Contracts;
using MyApp.Data;
using MyApp.Models;
using MyApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeDbContext context;
        private readonly Mapper mapper;

        public EmployeePersonalInfoCommand(EmployeeDbContext context, Mapper mapper)
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

            EmployeeDto employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"ID: {employeeId} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}");
            sb.AppendLine($"Birthday: {employee.Birthday?.ToString("dd-MM-yyyy")}");
            sb.AppendLine($"Address: {employee.Address}");

            return sb.ToString().TrimEnd();
        }
    }
}
