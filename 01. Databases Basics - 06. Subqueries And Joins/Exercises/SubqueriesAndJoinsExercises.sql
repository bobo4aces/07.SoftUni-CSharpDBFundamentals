--Problem 1.	Employee Address

USE SoftUni

SELECT TOP 5
	e.EmployeeID
	, e.JobTitle
	, e.AddressID
	, a.AddressText
FROM Employees AS e
LEFT JOIN Addresses AS a
ON e.AddressID = a.AddressID
ORDER BY e.AddressID ASC

--Problem 2.	Addresses with Towns

SELECT TOP 50
	e.FirstName
	, e.LastName
	, t.[Name] AS Town
	, a.AddressText
FROM Employees AS e
LEFT JOIN Addresses AS a
ON e.AddressID = a.AddressID
LEFT JOIN Towns AS t
ON a.TownID = t.TownID
ORDER BY e.FirstName ASC, e.LastName

--Problem 3.	Sales Employee

SELECT
	e.EmployeeID
	, e.FirstName
	, e.LastName
	, d.[Name] AS DepartmentName
FROM Employees AS e
LEFT JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE d.[Name] = 'Sales'
ORDER BY e.EmployeeID ASC

--Problem 4.	Employee Departments

SELECT TOP 5
	e.EmployeeID
	, e.FirstName
	, e.Salary
	, d.[Name] AS DepartmentName
FROM Employees AS e
LEFT JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > 15000
ORDER BY e.DepartmentID ASC

--Problem 5.	Employees Without Project

SELECT TOP 3
	e.EmployeeID
	, e.FirstName
FROM Employees AS e
LEFT JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
WHERE ep.ProjectID IS NULL
ORDER BY EmployeeID ASC

--Problem 6.	Employees Hired After

SELECT
	e.FirstName
	, e.LastName
	, e.HireDate
	, d.[Name] AS DeptName
FROM Employees AS e
LEFT JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
WHERE e.HireDate > '1999-01-01' AND d.[Name] IN ('Sales', 'Finance')
ORDER BY e.HireDate ASC

--Problem 7.	Employees with Project

SELECT TOP 5
	e.EmployeeID
	, e.FirstName
	, p.[Name] AS ProjectName
FROM Employees AS e
LEFT JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
LEFT JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE p.StartDate > '2002-08-13' AND p.EndDate IS NULL
ORDER BY e.EmployeeID ASC

--Problem 8.	Employee 24

SELECT
	e.EmployeeID
	, e.FirstName
	, CASE 
		WHEN p.StartDate >= '2005-01-01' THEN NULL
		WHEN p.StartDate < '2005-01-01' THEN p.Name
	  END AS ProjectName
FROM Employees AS e
JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p
ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24

--Problem 9.	Employee Manager

SELECT
	e.EmployeeID
	, e.FirstName
	, e.ManagerID
	, em.[FirstName] AS ManagerName
FROM Employees AS e
JOIN Employees AS em
ON e.ManagerID = em.EmployeeID
WHERE e.ManagerID IN (3, 7)
ORDER BY e.EmployeeID ASC

--Problem 10.	Employee Summary

SELECT TOP 50
	e.EmployeeID
	, e.FirstName + ' ' + e.LastName AS EmployeeName
	, em.FirstName + ' ' + em.LastName AS ManagerName
	, d.[Name] AS DepartmentName
FROM Employees AS e
LEFT JOIN Employees AS em
ON e.ManagerID = em.EmployeeID
LEFT JOIN Departments AS d
ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID ASC

--Problem 11.	Min Average Salary

SELECT TOP 1
	AVG(Salary) AS MinAverageSalary
FROM Employees
GROUP BY DepartmentID
ORDER BY MinAverageSalary

--Problem 12.	Highest Peaks in Bulgaria

USE [Geography]

SELECT
	c.CountryCode
	, m.MountainRange
	, p.PeakName
	, p.Elevation
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
LEFT JOIN Mountains AS m
ON mc.MountainId = m.Id
LEFT JOIN Peaks AS p
ON mc.MountainId = p.MountainId
WHERE c.CountryCode = 'BG' AND p.Elevation > 2835
ORDER BY p.Elevation DESC

--Problem 13.	Count Mountain Ranges

SELECT
	c.CountryCode
	, COUNT(m.MountainRange) AS MountainRanges
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
LEFT JOIN Mountains AS m
ON mc.MountainId = m.Id
WHERE c.CountryName IN ('United States', 'Russia', 'Bulgaria')
GROUP BY c.CountryCode

--Problem 14.	Countries with Rivers

SELECT TOP 5
	c.CountryName
	, r.RiverName
FROM Countries AS c
LEFT JOIN CountriesRivers AS cr
ON c.CountryCode = cr.CountryCode
LEFT JOIN Rivers AS r
ON cr.RiverId = r.Id
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName ASC

--Problem 15.	*Continents and Currencies

SELECT
	c.ContinentCode
	, cu.CurrencyCode
	, COUNT(c.CurrencyCode) AS CurrencyUsage
FROM Countries AS c
LEFT JOIN Currencies AS cu
ON c.CurrencyCode = cu.CurrencyCode
GROUP BY c.ContinentCode, cu.CurrencyCode
HAVING COUNT(c.CountryName) > 1
ORDER BY c.ContinentCode ASC

--Problem 16.	Countries without any Mountains

SELECT
	COUNT(c.CountryCode) AS CountryCode
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
WHERE mc.MountainId IS NULL

--Problem 17.	Highest Peak and Longest River by Country

SELECT TOP 5
	c.CountryName
	, MAX(p.Elevation) AS HighestPeakElevation
	, MAX(r.[Length]) AS LongestRiverLength
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
LEFT JOIN CountriesRivers AS cr
ON c.CountryCode = cr.CountryCode
LEFT JOIN Peaks AS p
ON mc.MountainId = p.MountainId
LEFT JOIN Rivers AS r
ON cr.RiverId = r.Id
GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC

--Problem 18.	* Highest Peak Name and Elevation by Country

SELECT DISTINCT
	c.CountryName
	, p.PeakName AS 'Highest Peak Name'
	, MAX(p.Elevation) AS 'Highest Peak Elevation'
	, m.MountainRange AS Mountain
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
LEFT JOIN Peaks AS p
ON mc.MountainId = p.MountainId
LEFT JOIN Mountains AS m
ON p.MountainId = m.Id
GROUP BY c.CountryName, m.MountainRange, p.PeakName
ORDER BY c.CountryName ASC, 'Highest Peak Name' ASC