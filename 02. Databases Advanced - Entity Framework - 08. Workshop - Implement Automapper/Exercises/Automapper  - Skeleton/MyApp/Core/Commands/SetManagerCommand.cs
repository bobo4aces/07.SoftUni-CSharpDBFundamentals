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
    public class SetManagerCommand : ICommand
    {
        private readonly EmployeeDbContext context;
        private readonly Mapper mapper;

        public SetManagerCommand(EmployeeDbContext context, Mapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public string Execute(string[] inputArgs)
        {
            this.context.Database.EnsureCreated();

            int employeeId = int.Parse(inputArgs[0]);
            int managerId = int.Parse(inputArgs[1]);

            Employee employee = this.context.Employees
                .Where(e => e.Id == employeeId)
                .FirstOrDefault();
            Employee manager = this.context.Employees
                .Where(e => e.Id == managerId)
                .FirstOrDefault();

            if (employee == null)
            {
                throw new ArgumentNullException("Invalid employee ID!");
            }
            if (manager == null)
            {
                throw new ArgumentNullException("Invalid manager ID!");
            }

            manager.ManagedEmployees.Add(employee);
            employee.Manager = manager;
            this.context.SaveChanges();

            EmployeeDto employeeDto = this.mapper.CreateMappedObject<EmployeeDto>(employee);
            ManagerDto managerDto = this.mapper.CreateMappedObject<ManagerDto>(manager);
            string result = $"Succefully set {managerDto.FirstName} {managerDto.LastName} to be a manager of {employeeDto.FirstName} {employeeDto.LastName}";
            return result;
        }
    }
}
