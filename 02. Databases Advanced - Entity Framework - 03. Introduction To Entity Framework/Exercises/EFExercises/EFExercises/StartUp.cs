using SoftUni.Data;
using Microsoft.EntityFrameworkCore;
using System;
using SoftUni.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();
            Console.WriteLine(RemoveTown(softUniContext));
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in context.Employees.OrderBy(e => e.EmployeeId))
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:f2}");
            }
            
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in context.Employees.Where(e=>e.Salary > 50000).OrderBy(e=>e.FirstName))
            {
                stringBuilder.AppendLine($"{employee.FirstName} - {employee.Salary:f2}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var employee in context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName))
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.Department.Name} - ${employee.Salary:f2}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };
            context.Employees.Where(e => e.LastName == "Nakov").FirstOrDefault().Address = address;

            context.SaveChanges();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var addressText in context.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e=>e.Address.AddressText)
                .Take(10))
            {
                stringBuilder.AppendLine(addressText);
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var collection = context.Employees
                .Where(e => e.EmployeesProjects
                        .Any(ep => ep.Project.StartDate.Year >= 2001 && ep.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    EmployeeFullName = e.FirstName + " " + e.LastName,
                    ManagerFullName = e.Manager.FirstName + " " + e.Manager.LastName,
                    ProjectInfo = e.EmployeesProjects
                        .Select(ep => new
                        {
                            ProjectName = ep.Project.Name,
                            StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt"),
                            EndDate = ep.Project.EndDate.HasValue ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt") : "not finished"
                        })
                })
                .Take(10);

            foreach (var item in collection)
            {
                stringBuilder.AppendLine($"{item.EmployeeFullName} - Manager: {item.ManagerFullName}");
                foreach (var project in item.ProjectInfo)
                {
                    stringBuilder.AppendLine($"--{project.ProjectName} - {project.StartDate} - {project.EndDate}");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var addresses = context.Addresses
                .Select(a => new
                {
                    EmployeesCount = a.Employees.Count,
                    TownName = a.Town.Name,
                    a.AddressText
                })
                .OrderByDescending(a => a.EmployeesCount)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            foreach (var address in addresses)
            {
                stringBuilder.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employeeInfo = context.Employees
                .Where(e => e.EmployeeId == 147)
                .First();
            stringBuilder.AppendLine($"{employeeInfo.FirstName} {employeeInfo.LastName} - {employeeInfo.JobTitle}");
            foreach (var project in context.EmployeesProjects.Where(ep=>ep.EmployeeId == 147).Select(e=>e.Project).OrderBy(e=>e.Name))
            {
                stringBuilder.AppendLine($"{project.Name}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    d.Name,
                    ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
                    DepartmentEmployees = d.Employees
                });
            foreach (var department in departments)
            {
                stringBuilder.AppendLine($"{department.Name} - {department.ManagerName}");
                foreach (var employee in department.DepartmentEmployees.OrderBy(e=>e.FirstName).ThenBy(e=>e.LastName))
                {
                    stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name);
            foreach (var project in projects)
            {
                stringBuilder.AppendLine($"{project.Name}");
                stringBuilder.AppendLine($"{project.Description}");
                stringBuilder.AppendLine($"{project.StartDate.ToString("M/d/yyyy h:mm:ss tt")}");
            }
            return stringBuilder.ToString().TrimEnd();
        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employees = context.Employees
                .Where(d =>
                    d.Department.Name == "Engineering" ||
                    d.Department.Name == "Tool Design" ||
                    d.Department.Name == "Marketing" ||
                    d.Department.Name == "Information Services")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    Salary = decimal.Multiply(e.Salary, 1.12m)
                });

            context.SaveChanges();

            foreach (var employee in employees.OrderBy(e=>e.FirstName).ThenBy(e=>e.LastName))
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"));
            foreach (var employee in employees.OrderBy(e=>e.FirstName).ThenBy(e=>e.LastName))
            {
                stringBuilder.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }
            return stringBuilder.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            Project project = context.Projects.Find(2);
            EmployeeProject[] employeeProjects = context.EmployeesProjects.Where(e => e.ProjectId == 2).ToArray();
            context.EmployeesProjects.RemoveRange(employeeProjects);
            context.Projects.Remove(project);
            context.SaveChanges();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var currentProject in context.Projects.Take(10))
            {
                stringBuilder.AppendLine($"{currentProject.Name}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
        public static string RemoveTown(SoftUniContext context)
        {
            Town town = context.Towns
                .Where(t => t.Name == "Seattle")
                .FirstOrDefault();
            Address[] addresses = context.Addresses
                .Where(a => a.TownId == town.TownId)
                .ToArray();
            int addressesCount = addresses.Length;
            Employee[] employees = context.Employees
                .Where(e => addresses
                                .Any(a => a.AddressId == e.Address.AddressId))
                .ToArray();
            employees.Select(e => e.AddressId = null).ToList();
            context.Addresses.RemoveRange(addresses);
            context.Towns.Remove(town);
            return $"{addressesCount} addresses in Seattle were deleted";
        }
    }
}
