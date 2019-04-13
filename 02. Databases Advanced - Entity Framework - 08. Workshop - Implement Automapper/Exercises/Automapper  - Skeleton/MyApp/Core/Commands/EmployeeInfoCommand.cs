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
    public class EmployeeInfoCommand : ICommand
    {
        private readonly EmployeeDbContext context;
        private readonly Mapper mapper;

        public EmployeeInfoCommand(EmployeeDbContext context, Mapper mapper)
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

            string result = $"ID: {employee.Id} - {employeeDto.FirstName} {employeeDto.LastName} -  ${employeeDto.Salary:f2}";
            return result;
        }
    }
}
