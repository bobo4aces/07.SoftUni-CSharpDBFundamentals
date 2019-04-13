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
    public class SetBirthdayCommand : ICommand
    {
        private readonly EmployeeDbContext context;
        private readonly Mapper mapper;

        public SetBirthdayCommand(EmployeeDbContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public string Execute(string[] inputArgs)
        {
            this.context.Database.EnsureCreated();
            int employeeId = int.Parse(inputArgs[0]);
            DateTime date = DateTime.Parse(inputArgs[1]);

            Employee employee = this.context.Employees
                .Where(e => e.Id == employeeId)
                .FirstOrDefault();
            employee.Birthday = date;

            if (employee == null)
            {
                throw new ArgumentNullException("Invalid employee Id!");
            }

            this.context.Employees.Update(employee);
            this.context.SaveChanges();

            EmployeeDto employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);
            string result = $"Birthday successfully set to {employee.Birthday?.ToString("dd-MM-yyyy")}!";
            return result;
        }
    }
}
