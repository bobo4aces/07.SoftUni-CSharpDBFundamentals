--Problem 1.	Create Database

CREATE DATABASE Minions

--Problem 2.	Create Tables

CREATE TABLE Minions (
	Id INT PRIMARY KEY IDENTITY
	, [Name] VARCHAR(50) NOT NULL
	, Age INT
)

CREATE TABLE Towns (
	Id INT PRIMARY KEY IDENTITY
	, [Name] VARCHAR(50) NOT NULL
)

--Problem 3.	Alter Minions Table

ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

SELECT * FROM Minions

--Problem 4.	Insert Records in Both Tables

INSERT INTO Towns (Id, [Name]) VALUES
	(1, 'Sofia')
	, (2, 'Plovdiv')
	, (3, 'Varna')

INSERT INTO Minions (Id, [Name], Age, TownId) VALUES
	(1, 'Kevin', 22, 1)
	, (2, 'Bob', 15, 3)
	, (3, 'Steward', NULL, 2)

SELECT * FROM Minions

--Problem 5.	Truncate Table Minions

TRUNCATE TABLE Minions

SELECT * FROM Minions

--Problem 6.	Drop All Tables

DROP TABLE Minions
DROP TABLE Towns

--Problem 7.	Create Table People

CREATE TABLE People (
	Id INT PRIMARY KEY IDENTITY
	, [Name] NVARCHAR(200) NOT NULL
	, Picture VARBINARY(2048)
	, Height DECIMAL(4,2)
	, [Weight] DECIMAL(6,2)
	, Gender CHAR(1) NOT NULL
	, Birthdate DATE NOT NULL
	, Biography NTEXT
)

INSERT INTO People([Name], Picture, Height, [Weight], Gender, Birthdate, Biography) VALUES
	('Pesho', NULL, NULL, NULL, 'm', GETDATE(), NULL)
	, ('Pesho', NULL, 1.65, 77.77, 'm', GETDATE(), NULL)
	, ('Pesho', NULL, 1.65, 77.77, 'm', GETDATE(), NULL)
	, ('Pesho', NULL, 1.65, 77.77, 'm', GETDATE(), NULL)
	, ('Pesho', NULL, 1.65, 77.77, 'm', GETDATE(), NULL)

SELECT * FROM People

--Problem 8.	Create Table Users

CREATE TABLE Users (
	Id BIGINT PRIMARY KEY IDENTITY
	, Username VARCHAR(30) NOT NULL
	, [Password] VARCHAR(26) NOT NULL
	, ProfilePicture VARBINARY(900)
	, LastLoginTime DATETIME2
	, IsDeleted BIT
)

INSERT INTO Users(Username, [Password], ProfilePicture, LastLoginTime, IsDeleted) VALUES
	('Pesho1', 123, NULL, NULL, 0)
	, ('Pesho2', 123, NULL, NULL, 0)
	, ('Pesho3', 123, NULL, NULL, 0)
	, ('Pesho4', 123, NULL, NULL, 0)
	, ('Pesho5', 123, NULL, NULL, 0)

SELECT * FROM Users

--Problem 9.	Change Primary Key

ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC07466291BF

ALTER TABLE Users
ADD CONSTRAINT PK_Id
PRIMARY KEY(Id,Username)

--Problem 10.	Add Check Constraint

ALTER TABLE Users
ADD CONSTRAINT PasswordLengthCheck
CHECK (LEN('Password') >= 5)

--Problem 11.	Set Default Value of a Field

ALTER TABLE Users
ADD DEFAULT GETDATE()
FOR LastLoginTime

--Problem 12.	Set Unique Field

ALTER TABLE Users
DROP CONSTRAINT PK_Id

ALTER TABLE Users
ADD CONSTRAINT PK_Id
PRIMARY KEY(Id)

ALTER TABLE Users
ADD CONSTRAINT uq_Username 
UNIQUE (Username)

ALTER TABLE Users
ADD CONSTRAINT UsernameLengthCheck
CHECK (LEN(Username) >= 5)

--Problem 13.	Movies Database

CREATE DATABASE Movies

CREATE TABLE Directors (
	Id INT PRIMARY KEY IDENTITY
	, DirectorName VARCHAR(50) NOT NULL
	, Notes TEXT
)

CREATE TABLE Genres (
	Id INT PRIMARY KEY IDENTITY
	, GenreName VARCHAR(50) NOT NULL
	, Notes TEXT
)

CREATE TABLE Categories (
	Id INT PRIMARY KEY IDENTITY
	, CategoryName VARCHAR(50) NOT NULL
	, Notes TEXT
)

CREATE TABLE Movies (
	Id INT PRIMARY KEY IDENTITY
	, Title VARCHAR(50) NOT NULL
	, DirectorId INT FOREIGN KEY REFERENCES Directors(Id)
	, CopyrightYear CHAR(4)
	, [Length] VARCHAR(50)
	, GenreId INT FOREIGN KEY REFERENCES Genres(Id)
	, CategoryId INT FOREIGN KEY REFERENCES Categories(Id)
	, Rating VARCHAR(50)
	, Notes TEXT
)

INSERT INTO Directors(DirectorName, Notes) VALUES
	('Pesho', NULL)
	, ('Pesho2', NULL)
	, ('Pesho3', NULL)
	, ('Pesho4', NULL)
	, ('Pesho5', NULL)

INSERT INTO Genres(GenreName, Notes) VALUES
	('Mistery', NULL)
	, ('Mistery2', NULL)
	, ('Mistery3', NULL)
	, ('Mistery4', NULL)
	, ('Mistery5', NULL)

INSERT INTO Categories(CategoryName, Notes) VALUES
	('Category', NULL)
	, ('Category2', NULL)
	, ('Category3', NULL)
	, ('Category4', NULL)
	, ('Category5', NULL)

INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes) VALUES
	  ('Title1', 1, 2013, '12:23', 1, 4, 'Great', NULL)
	, ('Title2', 2, 2013, '12:23', 1, 4, 'Great', NULL)
	, ('Title3', 3, 2013, '12:23', 1, 4, 'Great', NULL)
	, ('Title4', 4, 2013, '12:23', 1, 4, 'Great', NULL)
	, ('Title5', 5, 2013, '12:23', 1, 4, 'Great', NULL)

SELECT * FROM Directors
SELECT * FROM Genres
SELECT * FROM Categories
SELECT * FROM Movies

--Problem 14.	Car Rental Database

CREATE DATABASE CarRental

CREATE TABLE Categories (
	Id INT PRIMARY KEY IDENTITY
	, CategoryName VARCHAR(50) NOT NULL
	, DailyRate MONEY NOT NULL
	, WeeklyRate MONEY NOT NULL
	, MonthlyRate MONEY NOT NULL
	, WeekendRate MONEY NOT NULL
)

CREATE TABLE Cars (
	Id INT PRIMARY KEY IDENTITY
	, PlateNumber VARCHAR(50) NOT NULL
	, Manufacturer VARCHAR(50) NOT NULL
	, Model VARCHAR(50) NOT NULL
	, CarYear CHAR(4) NOT NULL
	, CategoryId INT FOREIGN KEY REFERENCES Categories(Id)
	, Doors VARCHAR(50)
	, Picture BINARY
	, Condition VARCHAR(50)
	, Available BIT
)

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY
	, FirstName VARCHAR(50) NOT NULL
	, LastName VARCHAR(50) NOT NULL
	, Title VARCHAR(50) NOT NULL
	, Notes TEXT
)

CREATE TABLE Customers (
	Id INT PRIMARY KEY IDENTITY
	, DriverLicenceNumber VARCHAR(50) NOT NULL
	, FullName VARCHAR(50) NOT NULL
	, [Address] VARCHAR(50) NOT NULL
	, City VARCHAR(50) NOT NULL
	, ZIPCode VARCHAR(50) NOT NULL
	, Notes TEXT
)

CREATE TABLE RentalOrders (
	Id INT PRIMARY KEY IDENTITY
	, EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
	, CustomerId INT FOREIGN KEY REFERENCES Customers(Id)
	, CarId INT FOREIGN KEY REFERENCES Cars(Id)
	, TankLevel REAL NOT NULL
	, KilometrageStart INT NOT NULL
	, KilometrageEnd INT NOT NULL
	, TotalKilometrage AS KilometrageEnd - KilometrageEnd
	, StartDate DATETIME2 NOT NULL
	, EndDate DATETIME2 NOT NULL
	, TotalDates AS DATEDIFF(day,StartDate,EndDate)
	, RateApplied MONEY NOT NULL
	, TaxRate MONEY NOT NULL
	, OrderStatus VARCHAR(50) NOT NULL
	, Notes TEXT
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
	('Category1', 11.11, 77.77, 333.33, 22.22)
	, ('Category2', 12.11, 78.77, 334.33, 23.22)
	, ('Category3', 13.11, 79.77, 335.33, 24.22)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) VALUES
	('CO1111CO', 'Citroen', 'C3', 2003, 1, 4, NULL, 'New', 0)
	, ('CO1112CO', 'Citroen', 'C4', 2004, 2, 4, NULL, 'New', 1)
	, ('CO1113CO', 'Citroen', 'C5', 2005, 3, 4, NULL, 'New', 1)

INSERT INTO Employees (FirstName, LastName, Title, Notes) VALUES
	('Pesho', 'Peshov', 'Worker', NULL)
	, ('Pesho2', 'Peshov2', 'Worker', NULL)
	, ('Pesho3', 'Peshov3', 'Worker', NULL)

INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes) VALUES
	('123456789', 'Gosho Goshov', '5th Avenue', 'New York', 12345, NULL)
	, ('123456788', 'Gosho Goshov2', '4th Avenue', 'New York', 12345, NULL)
	, ('123456787', 'Gosho Goshov3', '3th Avenue', 'New York', 12345, NULL)

INSERT INTO RentalOrders (EmployeeId, 
						CustomerId, 
						CarId, 
						TankLevel, 
						KilometrageStart, 
						KilometrageEnd, 
						StartDate, 
						EndDate,
						RateApplied, 
						TaxRate, 
						OrderStatus, 
						Notes) 
VALUES
	(1, 3, 2, 44, 123455, 123555, GETDATE(), GETDATE(), 12.11, 0.2, 'Completed', NULL)
	, (2, 2, 3, 43, 123456, 123655, GETDATE(), GETDATE(), 13.11, 0.2, 'Completed', NULL)
	, (3, 1, 1, 42, 123457, 123755, GETDATE(), GETDATE(), 11.11, 0.2, 'Completed', NULL)

SELECT * FROM Categories
SELECT * FROM Cars
SELECT * FROM Employees
SELECT * FROM Customers
SELECT * FROM RentalOrders

--Problem 15.	Hotel Database

CREATE DATABASE Hotel

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY
	, FirstName VARCHAR(50) NOT NULL
	, LastName VARCHAR(50) NOT NULL
	, Title VARCHAR(50) NOT NULL
	, Notes TEXT
)

CREATE TABLE Customers (
	AccountNumber VARCHAR(50) PRIMARY KEY
	, FirstName VARCHAR(50) NOT NULL
	, LastName VARCHAR(50) NOT NULL
	, PhoneNumber VARCHAR(50) NOT NULL
	, EmergencyName VARCHAR(50) NOT NULL
	, EmergencyNumber VARCHAR(50) NOT NULL
	, Notes TEXT
)

CREATE TABLE RoomStatus (
	RoomStatus VARCHAR(50) PRIMARY KEY
	, Notes TEXT
)

CREATE TABLE RoomTypes (
	RoomType VARCHAR(50) PRIMARY KEY
	, Notes TEXT
)

CREATE TABLE BedTypes (
	BedType VARCHAR(50) PRIMARY KEY
	, Notes TEXT
)

CREATE TABLE Rooms (
	RoomNumber INT PRIMARY KEY
	, RoomType VARCHAR(50) FOREIGN KEY REFERENCES RoomTypes(RoomType)
	, BedType VARCHAR(50) FOREIGN KEY REFERENCES BedTypes(BedType)
	, Rate MONEY NOT NULL
	, RoomStatus VARCHAR(50) FOREIGN KEY REFERENCES RoomStatus(RoomStatus)
	, Notes Text
)

CREATE TABLE Payments (
	Id INT PRIMARY KEY IDENTITY
	, EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
	, PaymentDate DATETIME2 NOT NULL
	, AccountNumber VARCHAR(50) FOREIGN KEY REFERENCES Customers(AccountNumber)
	, FirstDateOccupied DATETIME2 NOT NULL
	, LastDateOccupied DATETIME2 NOT NULL
	, TotalDays AS DATEDIFF(day, LastDateOccupied, FirstDateOccupied)
	, AmountCharged MONEY NOT NULL
	, TaxRate REAL NOT NULL
	, TaxAmount REAL NOT NULL
	, PaymentTotal MONEY NOT NULL
	, Notes Text
)

CREATE TABLE Occupancies (
	Id INT PRIMARY KEY IDENTITY
	, EmployeeId INT FOREIGN KEY REFERENCES Employees(Id)
	, DateOccupied DATETIME2 NOT NULL
	, AccountNumber VARCHAR(50) FOREIGN KEY REFERENCES Customers(AccountNumber)
	, RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber)
	, RateApplied REAL NOT NULL
	, PhoneCharge MONEY NOT NULL
	, Notes Text
)

INSERT INTO Employees(FirstName, LastName, Title, Notes) VALUES
	('Pesho', 'Peshov', 'Bellboy', NULL)
	, ('Pesho1', 'Peshov1', 'Bellboy1', NULL)
	, ('Pesho2', 'Peshov2', 'Bellboy2', NULL)

INSERT INTO Customers(AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes) VALUES
	('123', 'Gosho', 'Goshov', '0123456789', 'Pesho1', '0987654321', NULL)
	, ('122', 'Gosho1', 'Goshov1', '0123456788', 'Pesho2', '0987654322', NULL)
	, ('111', 'Gosho2', 'Goshov2', '0123456787', 'Pesho3', '0987654323', NULL)

INSERT INTO RoomStatus(RoomStatus, Notes) VALUES
	('Occupied', NULL)
	, ('Not Occupied', NULL)
	, ('Dirty', NULL)

INSERT INTO RoomTypes(RoomType, Notes) VALUES
	('Single', NULL)
	, ('Double', NULL)
	, ('King', NULL)

INSERT INTO BedTypes(BedType, Notes) VALUES
	('Single', NULL)
	, ('Double', NULL)
	, ('King', NULL)

INSERT INTO Rooms(RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes) VALUES
	(123, 'Single', 'Single', 22, 'Occupied', NULL)
	, (124, 'Double', 'Double', 32, 'Not Occupied', NULL)
	, (125, 'King', 'King', 42, 'Dirty', NULL)

INSERT INTO Payments(
					EmployeeId, 
					PaymentDate, 
					AccountNumber, 
					FirstDateOccupied, 
					LastDateOccupied,
					AmountCharged,
					TaxRate,
					TaxAmount,
					PaymentTotal,
					Notes) 
VALUES
	(1, GETDATE(), 123, GETDATE(), GETDATE(), 222, 0.2, 44.4, 266.4, NULL)
	, (2, GETDATE(), 122, GETDATE(), GETDATE(), 222, 0.2, 44.4, 266.4, NULL)
	, (3, GETDATE(), 111, GETDATE(), GETDATE(), 222, 0.2, 44.4, 266.4, NULL)

INSERT INTO Occupancies(
					EmployeeId, 
					DateOccupied, 
					AccountNumber, 
					RoomNumber, 
					RateApplied, 
					PhoneCharge, 
					Notes) 
VALUES
	(1, GETDATE(), 123, 123, 111, 0.2, NULL)
	, (2, GETDATE(), 122, 124, 222, 0.2, NULL)
	, (3, GETDATE(), 111, 125, 333, 0.2, NULL)

--Problem 16.	Create SoftUni Database

CREATE DATABASE SoftUni

CREATE TABLE Towns (
	Id INT PRIMARY KEY IDENTITY
	, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Addresses (
	Id INT PRIMARY KEY IDENTITY
	, AddressText VARCHAR(50) NOT NULL
	, TownId INT FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments (
	Id INT PRIMARY KEY IDENTITY
	, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY
	, FirstName VARCHAR(50) NOT NULL
	, MiddleName VARCHAR(50) NOT NULL
	, LastName VARCHAR(50) NOT NULL
	, JobTitle VARCHAR(50) NOT NULL
	, DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)
	, HireDate DATETIME NOT NULL
	, Salary MONEY NOT NULL
	, AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
)

--Problem 17.	Backup Database -- TODO

--Problem 18.	Basic Insert

INSERT INTO Towns([Name]) VALUES
	('Sofia')
	, ('Plovdiv')
	, ('Varna')
	, ('Burgas')

INSERT INTO Departments([Name]) VALUES
	('Engineering')
	, ('Sales')
	, ('Marketing')
	, ('Software Development')
	, ('Quality Assurance')

INSERT INTO Employees(
						FirstName
						, MiddleName
						, LastName
						, JobTitle
						, DepartmentId
						, HireDate
						, Salary) 
VALUES
	('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '01/02/2013', 3500.00)
	, ('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '02/03/2004', 4000.00)
	, ('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '28/08/2016', 525.25)
	, ('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '09/12/2007', 3000.00)
	, ('Peter', 'Pan', 'Pan', 'Intern', 3, '28/08/2016', 599.88)
