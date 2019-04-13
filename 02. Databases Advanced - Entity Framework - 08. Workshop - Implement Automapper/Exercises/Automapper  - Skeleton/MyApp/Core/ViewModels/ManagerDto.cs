using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Core.ViewModels
{
    public class ManagerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<EmployeeDto> ManagedEmployees { get; set; }
        public int ManagedEmployeesCount => this.ManagedEmployees.Count;

        public ManagerDto()
        {
            this.ManagedEmployees = new List<EmployeeDto>();
        }
    }
}
