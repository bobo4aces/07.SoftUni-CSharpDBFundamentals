--Problem 1.	One-To-One Relationship

CREATE DATABASE TableRelations

USE TableRelations

CREATE TABLE Persons (
	PersonID INT NOT NULL
	, FirstName VARCHAR(50) NOT NULL
	, Salary MONEY NOT NULL
	, PassportID INT NOT NULL
)

CREATE TABLE Passports (
	PassportID INT NOT NULL
	, PassportNumber VARCHAR(8) NOT NULL
)

INSERT INTO Persons VALUES
	(1, 'Roberto', 43300.00, 102)
	, (2, 'Tom', 56100.00, 103)
	, (3, 'Yana', 60200.00, 101)

INSERT INTO Passports VALUES
	(101, 'N34FG21B')
	, (102, 'K65LO4R7')
	, (103, 'ZE657QP2')

ALTER TABLE Persons
ADD CONSTRAINT PK_PersonID
PRIMARY KEY (PersonID)

ALTER TABLE Passports
ADD CONSTRAINT PK_PassportID
PRIMARY KEY (PassportID)

ALTER TABLE Persons
ADD CONSTRAINT FK_PassportID
FOREIGN KEY (PassportID) REFERENCES Passports(PassportID)

--Problem 2.	One-To-Many Relationship

CREATE TABLE Models(
	ModelID INT NOT NULL
	, [Name] VARCHAR(50) NOT NULL
	, ManufacturerID INT NOT NULL
)

CREATE TABLE Manufacturers(
	ManufacturerID INT NOT NULL
	, [Name] VARCHAR(50) NOT NULL
	, EstablishedOn VARCHAR(50) NOT NULL
)

INSERT INTO Models VALUES
	(101, 'X1', 1)
	, (102, 'i6', 1)
	, (103, 'Model S', 2)
	, (104, 'Model X', 2)
	, (105, 'Model 3', 2)
	, (106, 'Nova', 3)

INSERT INTO Manufacturers VALUES
	(1, 'BMW', '07/03/1916' )
	, (2,	'Tesla', '01/01/2003' )
	, (3,	'Lada', '01/05/1966' )

ALTER TABLE Models
ADD CONSTRAINT PK_ModelID
PRIMARY KEY (ModelID)

ALTER TABLE Manufacturers
ADD CONSTRAINT PK_ManufacturerID
PRIMARY KEY (ManufacturerID)

ALTER TABLE Models
ADD CONSTRAINT FK_ManufacturerID
FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)

--Problem 3.	Many-To-Many Relationship

CREATE TABLE Students (
	StudentID INT NOT NULL
	, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Exams (
	ExamID INT NOT NULL
	, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE StudentsExams (
	StudentID INT NOT NULL
	, ExamID INT NOT NULL
)

INSERT INTO Students VALUES
	(1, 'Mila')                                     
	, (2, 'Toni')
	, (3, 'Ron')

INSERT INTO Exams VALUES
	(101, 'SpringMVC')
	, (102, 'Neo4j')
	, (103, 'Oracle 11g')

INSERT INTO StudentsExams VALUES
	(1, 101)
	, (1, 102)
	, (2, 101)
	, (3, 103)
	, (2, 102)
	, (2, 103)	

ALTER TABLE Students
ADD CONSTRAINT PK_StudentID
PRIMARY KEY (StudentID)

ALTER TABLE Exams
ADD CONSTRAINT PK_ExamID
PRIMARY KEY (ExamID)

ALTER TABLE StudentsExams
ADD CONSTRAINT PK_StudentID_StudentsExams
PRIMARY KEY (StudentID, ExamID)

ALTER TABLE StudentsExams
ADD CONSTRAINT FK_StudentID
FOREIGN KEY (StudentID) REFERENCES Students(StudentID)

ALTER TABLE StudentsExams
ADD CONSTRAINT FK_ExamID
FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)

--Problem 4.	Self-Referencing 

CREATE TABLE Teachers (
	TeacherID INT NOT NULL
	, [Name] VARCHAR(50) NOT NULL
	, ManagerID INT
)

INSERT INTO Teachers VALUES
	(101, 'John',NULL)
	, (102, 'Maya',106)
	, (103, 'Silvia', 106)
	, (104, 'Ted', 105)
	, (105, 'Mark', 101)
	, (106, 'Greta', 101)

ALTER TABLE Teachers
ADD CONSTRAINT PK_TeacherID
PRIMARY KEY (TeacherID)

ALTER TABLE Teachers
ADD CONSTRAINT FK_ManagerID
FOREIGN KEY (ManagerID) REFERENCES Teachers(TeacherID)

--Problem 5.	Online Store Database


CREATE TABLE Cities (
	CityID INT PRIMARY KEY NOT NULL
	, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Customers (
	CustomerID INT PRIMARY KEY NOT NULL
	, [Name] VARCHAR(50) NOT NULL
	, Birthday DATE  NOT NULL
	, CityID INT FOREIGN KEY REFERENCES Cities(CityID)
)

CREATE TABLE Orders (
	OrderID INT PRIMARY KEY NOT NULL
	, CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes (
	ItemTypeID INT PRIMARY KEY NOT NULL
	, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Items (
	ItemID INT PRIMARY KEY NOT NULL
	, [Name] VARCHAR(50) NOT NULL
	, ItemTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems (
	OrderID INT NOT NULL
	, ItemID INT NOT NULL
	, CONSTRAINT PK_OrderItems
	PRIMARY KEY(OrderID, ItemID)
	, CONSTRAINT FK_OrderItems_Orders
	FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
	, CONSTRAINT FK_OrderItems_Items
	FOREIGN KEY (ItemID) REFERENCES Items(ItemID)
)

--Problem 6.	University Database

CREATE DATABASE UniversityDatabase

USE UniversityDatabase

CREATE TABLE Majors (
	MajorID INT PRIMARY KEY NOT NULL
	, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Students (
	StudentID INT PRIMARY KEY NOT NULL
	, StudentNumber INT NOT NULL
	, StudentName VARCHAR(50) NOT NULL
	, MajorID INT FOREIGN KEY REFERENCES Majors(MajorID) NOT NULL
)

CREATE TABLE Payments (
	PaymentID INT PRIMARY KEY NOT NULL
	, PaymentDate DATE NOT NULL
	, PaymentAmount MONEY NOT NULL
	, StudentID INT FOREIGN KEY REFERENCES Students(StudentID) NOT NULL
)

CREATE TABLE Subjects(
	SubjectID INT PRIMARY KEY NOT NULL
	, SubjectName VARCHAR(50) NOT NULL
)

CREATE TABLE Agenda (
	StudentID INT NOT NULL
	, SubjectID INT NOT NULL
	, CONSTRAINT PK_Agenda
	PRIMARY KEY (StudentID, SubjectID)
	, CONSTRAINT FK_Agenda_Students
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
	, CONSTRAINT FK_Agenda_Subjects
	FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
)

--Problem 9.	*Peaks in Rila

USE [Geography]

SELECT 
	m.MountainRange
	, p.PeakName
	, p.Elevation
FROM Peaks AS p
LEFT JOIN Mountains AS m
ON p.MountainId = m.Id
WHERE MountainRange = 'Rila'
ORDER BY p.Elevation DESC