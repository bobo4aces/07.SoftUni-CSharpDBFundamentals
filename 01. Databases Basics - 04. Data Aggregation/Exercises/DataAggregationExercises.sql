--Problem 1.	Records’ Count

USE Gringotts

SELECT COUNT(*) AS [Count]
FROM WizzardDeposits

--Problem 2.	Longest Magic Wand

SELECT TOP(1) MagicWandSize AS LongestMagicWand
FROM WizzardDeposits
ORDER BY MagicWandSize DESC

--Problem 3.	Longest Magic Wand per Deposit Groups

SELECT 
	DepositGroup
	, MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits
GROUP BY DepositGroup

--Problem 4.	* Smallest Deposit Group per Magic Wand Size

SELECT TOP(2) DepositGroup
FROM (
	SELECT 
		DepositGroup
		, AVG(MagicWandSize) AS AverageMagicWandSize
	FROM WizzardDeposits
	GROUP BY DepositGroup
) AS AverageMagicWandSizeGroup
ORDER BY AverageMagicWandSize ASC

--Problem 5.	Deposits Sum

SELECT
	DepositGroup
	, SUM(DepositAmount) AS TotalSum
FROM
	WizzardDeposits
GROUP BY DepositGroup

--Problem 6.	Deposits Sum for Ollivander Family

SELECT
	DepositGroup
	, SUM(DepositAmount) AS TotalSum
FROM
	WizzardDeposits	
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

--Problem 7.	Deposits Filter

SELECT
	DepositGroup
	, SUM(DepositAmount) AS TotalSum
FROM
	WizzardDeposits	
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY SUM(DepositAmount) DESC

--Problem 8.	 Deposit Charge

SELECT
	DepositGroup
	, MagicWandCreator
	, MIN(DepositCharge) AS MinDepositCharge
FROM
	WizzardDeposits
GROUP BY MagicWandCreator, DepositGroup
ORDER BY MagicWandCreator ASC, DepositGroup ASC

--Problem 9.	Age Groups

SELECT 
	AgeGroup
	, SUM(WizardCount) 
FROM (
	SELECT
		CASE
			WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
			WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
			WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
			WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
			WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
			WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
			WHEN Age >= 61 THEN '[61+]'
		END AS AgeGroup
		, COUNT(*) AS WizardCount
	FROM WizzardDeposits
	GROUP BY Age
	) AS AgeGroups
GROUP BY AgeGroup

--Problem 10.	First Letter

SELECT DISTINCT
	LEFT(FirstName,1) AS FirstLetter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY FirstName
ORDER BY FirstLetter ASC

--Problem 11.	Average Interest 

SELECT
	DepositGroup
	, IsDepositExpired
	, AVG(DepositInterest) AS AverageInterest
FROM WizzardDeposits
WHERE DATEDIFF(d,'01/01/1985',DepositStartDate) > 0
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired ASC

--Problem 13.	Departments Total Salaries

USE SoftUni

SELECT
	DepartmentID
	, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID

--Problem 14.	Employees Minimum Salaries

SELECT
	DepartmentID
	, MIN(Salary) AS MinimumSalary
FROM Employees
WHERE DATEDIFF(d, '01/01/2000', HireDate) > 0
GROUP BY DepartmentID
HAVING DepartmentID IN (2, 5, 7)

--Problem 15.	Employees Average Salaries

--CREATE TABLE SalariesGreaterThan30000 (
--	Id INT PRIMARY KEY IDENTITY
--	, DepartmentID INT
--	, ManagerID INT
--	, Salary MONEY
--)
SELECT * 
INTO SalariesGreaterThan30000 
FROM Employees
WHERE Salary > 30000

DELETE FROM SalariesGreaterThan30000 
WHERE ManagerID = 42

UPDATE SalariesGreaterThan30000
SET Salary += 5000
WHERE DepartmentID = 1

SELECT
	DepartmentID
	, AVG(Salary) AS AverageSalary
FROM SalariesGreaterThan30000
GROUP BY DepartmentID

--Problem 16.	Employees Maximum Salaries

SELECT *
FROM (
	SELECT 
		DepartmentID
		, MAX(Salary) AS MaxSalary
	FROM Employees
	GROUP BY DepartmentID
) AS MaxSalaries
WHERE MaxSalary < 30000 OR MaxSalary > 70000

--Problem 17.	Employees Count Salaries

SELECT
	COUNT(*) AS [Count]
FROM Employees
WHERE ManagerID IS NULL

--Problem 18.	*3rd Highest Salary

SELECT
	DepartmentID
	, Salary AS ThirdHighestSalary
FROM (
	SELECT DISTINCT
		DepartmentID
		, Salary
		, DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) AS [Rank]
	FROM Employees
) AS SalaryRanking
WHERE [Rank] = 3