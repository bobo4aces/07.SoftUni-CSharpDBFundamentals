--Problem 1. Employees with Salary Above 35000
USE SoftUni
GO
CREATE PROCEDURE dbo.usp_GetEmployeesSalaryAbove35000
AS
	SELECT
		FirstName AS [First Name]
		,LastName AS [Last Name]
	FROM Employees
	WHERE Salary > 35000

EXEC dbo.usp_GetEmployeesSalaryAbove35000

--Problem 2. Employees with Salary Above Number
GO
CREATE PROCEDURE dbo.usp_GetEmployeesSalaryAboveNumber(@salary DECIMAL(18,4))
AS
	SELECT
		FirstName AS [First Name]
		,LastName AS [Last Name]
	FROM Employees
	WHERE Salary >= @salary

EXEC dbo.usp_GetEmployeesSalaryAboveNumber 48100

--Problem 3. Town Names Starting With

GO
CREATE PROCEDURE dbo.usp_GetTownsStartingWith(@startString VARCHAR(50))
AS
	SELECT 
		[Name] 
	FROM Towns
	WHERE LEFT([Name],LEN(@startString)) = @startString

EXEC dbo.usp_GetTownsStartingWith b

--Problem 4. Employees from Town

GO
CREATE PROCEDURE dbo.usp_GetEmployeesFromTown(@townName VARCHAR(50))
AS
	SELECT 
		FirstName AS [First Name]
		,LastName AS [Last Name]
	FROM Employees AS e
	LEFT JOIN Addresses AS a ON e.AddressID = a.AddressID
	LEFT JOIN Towns AS t ON a.TownID = t.TownID
	WHERE t.[Name] = @townName

EXEC dbo.usp_GetEmployeesFromTown Sofia

--Problem 5. Salary Level Function

GO
CREATE FUNCTION dbo.ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7)
AS
BEGIN
	DECLARE @salaryLevel VARCHAR(7);
	IF(@salary < 30000)
	BEGIN
		SET @salaryLevel = 'Low'
	END
	IF(@salary >= 30000 AND @salary <= 50000)
	BEGIN
		SET @salaryLevel = 'Average'
	END
	IF(@salary > 50000)
	BEGIN
		SET @salaryLevel = 'High'
	END
	RETURN @salaryLevel
END
GO

SELECT
	Salary
	,dbo.ufn_GetSalaryLevel(Salary)
FROM Employees

--Problem 6. Employees by Salary Level

GO
CREATE PROCEDURE dbo.usp_EmployeesBySalaryLevel(@salaryLevel VARCHAR(7))
AS
	SELECT
		FirstName AS [First Name]
		,LastName AS [Last Name]
	FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel

EXEC dbo.usp_EmployeesBySalaryLevel High

--Problem 7. Define Function

GO
CREATE FUNCTION dbo.ufn_IsWordComprised(@setOfLetters VARCHAR(50), @word VARCHAR(50))
RETURNS BIT
BEGIN
	DECLARE @index INT = 1;
	DECLARE @isComprised BIT = 1;
	WHILE (@index <= LEN(@word))
	BEGIN
		IF (CHARINDEX(SUBSTRING(@word,@index,1),@setOfLetters) <= 0)
		BEGIN
			SET @isComprised = 0
		END
		SET @index+=1
	END
	RETURN @isComprised
END

--Problem 9. Find Full Name

GO
USE Bank
GO
CREATE PROCEDURE dbo.usp_GetHoldersFullName
AS
	SELECT 
		CONCAT(FirstName, ' ', LastName) AS [Full Name]
	FROM AccountHolders

EXEC dbo.usp_GetHoldersFullName

--Problem 10. People with Balance Higher Than

GO
CREATE PROCEDURE dbo.usp_GetHoldersWithBalanceHigherThan (@balance MONEY)
AS
	SELECT
		ah.FirstName AS [First Name]
		, ah.LastName AS [Last Name]
	FROM AccountHolders AS ah
	LEFT JOIN Accounts AS a
	ON ah.Id = a.AccountHolderId
	GROUP BY ah.FirstName, ah.LastName
	HAVING SUM(Balance) > @balance
	ORDER BY ah.FirstName ASC, ah.LastName ASC

GO
EXEC dbo.usp_GetHoldersWithBalanceHigherThan 10000

SELECT * FROM AccountHolders AS ah
	LEFT JOIN Accounts AS a
	ON ah.Id = a.AccountHolderId

--Problem 11. Future Value Function

GO
CREATE FUNCTION dbo.ufn_CalculateFutureValue(@sum DECIMAL(18,4)
	, @yearlyInterestRate FLOAT
	, @numberOfYears INT)
RETURNS DECIMAL(18,4)
BEGIN
	DECLARE @result DECIMAL(18,4);
	SET @result = @sum*(POWER((1+@yearlyInterestRate),@numberOfYears))
	RETURN @result
END

GO
DECLARE @value DECIMAL(18,4)
SELECT @value = dbo.ufn_CalculateFutureValue (1000,0.10,5)
PRINT @value

--Problem 12. Calculating Interest

GO
CREATE PROCEDURE dbo.usp_CalculateFutureValueForAccount(@accountID INT, @interestRate FLOAT)
AS
	SELECT
		a.AccountHolderId AS [Account ID]
		, ah.FirstName AS [First Name]
		, ah.LastName AS [Last Name]
		, a.Balance AS [Current Balance]
		, dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, 5) AS [Balance in 5 years]
	FROM Accounts AS a
	LEFT JOIN AccountHolders AS ah
	ON a.AccountHolderId = ah.Id
	WHERE a.Id = @accountID
GO
EXEC dbo.usp_CalculateFutureValueForAccount 1, 0.1

--Problem 14. Create Table Logs

CREATE TABLE Logs(
	LogId INT PRIMARY KEY IDENTITY
	, AccountId INT FOREIGN KEY REFERENCES Accounts(Id)
	, OldSum MONEY NOT NULL
	, NewSum MONEY NOT NULL
)

GO
CREATE TRIGGER dbo.tr_ChangeBalance ON Accounts AFTER UPDATE
AS
BEGIN
	INSERT INTO Logs
	SELECT inserted.Id, deleted.Balance, inserted.Balance
	FROM inserted
	JOIN deleted
	ON inserted.Id = deleted.Id
END

GO

UPDATE Accounts
SET Balance -= 10
WHERE Id = 1

SELECT
	LogId
	, AccountId
	, OldSum
	, NewSum
FROM Logs

--Problem 15. Create Table Emails

CREATE TABLE NotificationEmails(
	Id INT PRIMARY KEY IDENTITY
	, Recipient INT FOREIGN KEY REFERENCES Accounts(Id)
	, [Subject] VARCHAR(100) NOT NULL
	, Body TEXT NOT NULL
)

GO
CREATE TRIGGER tr_CreateEmail ON Logs AFTER INSERT
AS
BEGIN
	INSERT INTO NotificationEmails
	SELECT
		inserted.AccountId
		, CONCAT('Balance change for account: ', inserted.AccountId)
		, CONCAT('On ', GETDATE(), ' your balance was changed from ',
			 inserted.OldSum, ' to ', inserted.NewSum, '.')
	FROM inserted
END

UPDATE Accounts
SET Balance -= 10
WHERE Id = 1

SELECT * FROM NotificationEmails

--Problem 16. Deposit Money

GO
CREATE PROCEDURE usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(18,4))
AS
BEGIN
	IF(@MoneyAmount < 0)
	BEGIN
		RAISERROR('Money must be positive number', 16, 1)
		ROLLBACK
		RETURN
	END
	UPDATE Accounts
	SET Balance += @MoneyAmount
	WHERE Id = @AccountId
END

EXEC dbo.usp_DepositMoney 1, 10

--Problem 17. Withdraw Money

GO
CREATE PROCEDURE usp_WithdrawMoney(@AccountId INT, @MoneyAmount DECIMAL(18,4))
AS
BEGIN
	IF(@MoneyAmount < 0)
	BEGIN
		RAISERROR('Money must be positive number', 16, 1)
		ROLLBACK
		RETURN
	END
	UPDATE Accounts
	SET Balance -= @MoneyAmount
	WHERE Id = @AccountId
END

EXEC dbo.usp_WithdrawMoney 5, 25

--Problem 18. Money Transfer

GO
CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount DECIMAL(18,4))
AS
BEGIN
	IF(@Amount < 0)
	BEGIN
		RAISERROR('Money must be positive number', 16, 1)
		ROLLBACK
		RETURN
	END
	EXEC dbo.usp_DepositMoney @ReceiverId, @Amount
	EXEC dbo.usp_WithdrawMoney @SenderId,@Amount
END

EXEC dbo.usp_TransferMoney 5, 1, 1000

--Problem 21. Employees with Three Projects

GO
USE SoftUni

GO
CREATE PROCEDURE usp_AssignProject(@emloyeeId INT, @projectID INT)
AS
BEGIN
	DECLARE @projectCount INT;
	SET @projectCount = (SELECT COUNT(*)
					FROM Employees AS e
					JOIN EmployeesProjects AS ep
					ON ep.EmployeeID = e.EmployeeID
					WHERE ep.EmployeeID = @emloyeeId)
	
	BEGIN TRANSACTION
		IF (@projectCount >= 3)
		BEGIN
			RAISERROR('The employee has too many projects!', 16, 1)
			ROLLBACK
			RETURN
		END
		INSERT INTO EmployeesProjects VALUES (@emloyeeId, @projectID)
	COMMIT
END

--Problem 22. Delete Employees

GO
USE SoftUni

CREATE TABLE Deleted_Employees(
	EmployeeId INT PRIMARY KEY IDENTITY
	, FirstName VARCHAR(50) NOT NULL
	, LastName VARCHAR(50) NOT NULL
	, MiddleName VARCHAR(50) NOT NULL
	, JobTitle VARCHAR(50) NOT NULL
	, DepartmentId INT
	, Salary MONEY
)

GO
CREATE TRIGGER tr_FireEmployee ON Employees AFTER DELETE
AS
BEGIN
	INSERT INTO Deleted_Employees
	SELECT
		deleted.FirstName
		, deleted.LastName
		, deleted.MiddleName
		, deleted.JobTitle
		, deleted.DepartmentID
		, deleted.Salary
	FROM deleted
END