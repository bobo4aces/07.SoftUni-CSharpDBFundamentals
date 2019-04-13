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
    public class SetAddressCommand : ICommand
    {
        private readonly EmployeeDbContext context;
        private readonly Mapper mapper;

        public SetAddressCommand(EmployeeDbContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public string Execute(string[] inputArgs)
        {
            this.context.Database.EnsureCreated();

            int employeeId = int.Parse(inputArgs[0]);
            string address = string.Join(" ",inputArgs.Skip(1));

            Employee employee = this.context.Employees
                .Where(e => e.Id == employeeId)
                .FirstOrDefault();
            employee.Address = address;

            if (employee == null)
            {
                throw new ArgumentNullException("Invalid employee Id!");
            }

            this.context.Employees.Update(employee);
            this.context.SaveChanges();

            EmployeeDto employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);
            string result = $"Address successfully set to {employee.Address}!";
            return result;
        }
    }
}
