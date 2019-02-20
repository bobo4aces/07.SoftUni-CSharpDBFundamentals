CREATE DATABASE TripService

USE TripService

--1. Database design

CREATE TABLE Cities(
	Id INT PRIMARY KEY IDENTITY
	,[Name] NVARCHAR(20) NOT NULL
	,CountryCode CHAR(2) NOT NULL
)

CREATE TABLE Hotels(
	Id INT PRIMARY KEY IDENTITY
	,[Name] NVARCHAR(30) NOT NULL
	,CityId INT FOREIGN KEY REFERENCES Cities(Id) NOT NULL
	,EmployeeCount INT NOT NULL
	,BaseRate DECIMAL(15,2)
)

CREATE TABLE Rooms(
	Id INT PRIMARY KEY IDENTITY
	,Price DECIMAL(15,2) NOT NULL
	,[Type] NVARCHAR(20) NOT NULL
	,Beds INT NOT NULL
	,HotelId INT FOREIGN KEY REFERENCES Hotels(Id) NOT NULL
)

CREATE TABLE Trips(
	Id INT PRIMARY KEY IDENTITY
	,RoomId INT FOREIGN KEY REFERENCES Rooms(Id) NOT NULL
	,BookDate DATE NOT NULL
	,ArrivalDate DATE NOT NULL
	,ReturnDate DATE NOT NULL
	,CancelDate DATE
)

ALTER TABLE Trips
ADD CONSTRAINT CK_BookDate_ArrivalDate
CHECK(BookDate<ArrivalDate)

ALTER TABLE Trips
ADD CONSTRAINT CK_ArrivalDate_ReturnDate
CHECK(ArrivalDate<ReturnDate)

CREATE TABLE Accounts(
	Id INT PRIMARY KEY IDENTITY
	,FirstName NVARCHAR(50) NOT NULL
	,MiddleName NVARCHAR(20)
	,LastName NVARCHAR(50) NOT NULL
	,CityId INT FOREIGN KEY REFERENCES Cities(Id) NOT NULL
	,BirthDate DATE NOT NULL
	,Email VARCHAR(100) UNIQUE NOT NULL
)

CREATE TABLE AccountsTrips(
	AccountId INT FOREIGN KEY REFERENCES Accounts(Id) NOT NULL
	,TripId INT FOREIGN KEY REFERENCES Trips(Id) NOT NULL
	,Luggage INT CHECK(Luggage >= 0) NOT NULL
)

ALTER TABLE AccountsTrips
ADD CONSTRAINT PK_AccountId_TripId
PRIMARY KEY(AccountId,TripId)

--2. Insert

INSERT INTO Accounts VALUES
('John','Smith','Smith',34,'1975-07-21','j_smith@gmail.com'),
('Gosho',NULL,'Petrov',11,'1978-05-16','g_petrov@gmail.com'),
('Ivan','Petrovich','Pavlov',59,'1849-09-26','i_pavlov@softuni.bg'),
('Friedrich','Wilhelm','Nietzsche',2,'1844-10-15','f_nietzsche@softuni.bg')

INSERT INTO Trips VALUES
(101,	'2015-04-12',	'2015-04-14',	'2015-04-20',	'2015-02-02')
,(102,	'2015-07-07',	'2015-07-15',	'2015-07-22',	'2015-04-29')
,(103,	'2013-07-17',	'2013-07-23',	'2013-07-24',	NULL)
,(104,	'2012-03-17',	'2012-03-31',	'2012-04-01',	'2012-01-10')
,(109,	'2017-08-07',	'2017-08-28',	'2017-08-29',	NULL)

--3. Update

UPDATE Rooms
SET Price *= 1.14
WHERE HotelId IN (5,7,9)

--4. Delete

DELETE FROM AccountsTrips
WHERE AccountId = 47

--5. Bulgarian Cities

SELECT 
	Id
	,[Name] 
FROM Cities
WHERE CountryCode = 'BG'
ORDER BY [Name]

--6. People Born After 1991

SELECT
	CONCAT(FirstName, ' ', ISNULL(MiddleName + ' ',''), LastName) AS [Full Name]
	, YEAR(BirthDate) AS [Birth Year]
FROM Accounts
WHERE YEAR(BirthDate) > 1991
ORDER BY [Birth Year] DESC, FirstName ASC

--7. EEE-Mails

SELECT
	a.FirstName
	,a.LastName
	,FORMAT(a.BirthDate,'MM-dd-yyyy') AS BirthDate
	,c.[Name] AS Hometown
	,a.Email
FROM Accounts AS a
LEFT JOIN Cities AS c
ON c.Id = a.CityId
WHERE a.Email LIKE 'e%'
ORDER BY Hometown DESC

--8. City Statistics

SELECT
	c.[Name] AS City
	,COUNT(h.Id) AS Hotels
FROM Cities AS c
LEFT JOIN Hotels AS h
ON h.CityId = c.Id
GROUP BY c.[Name]
ORDER BY Hotels DESC, City 

--9. Expensive First-Class Rooms

SELECT
	r.Id
	,r.Price
	,h.[Name] AS Hotel
	,c.[Name] AS City
FROM Rooms AS r
LEFT JOIN Hotels AS h
ON h.Id = r.HotelId
LEFT JOIN Cities AS c
ON c.Id = h.CityId
WHERE r.[Type] = 'First Class'
ORDER BY r.Price DESC, r.Id

--10. Longest and Shortest Trips

SELECT
	a.Id AS AccountId
	,a.FirstName + ' ' + a.LastName AS FullName
	,MAX(DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate)) AS LongestTrip
	,MIN(DATEDIFF(DAY,t.ArrivalDate,t.ReturnDate)) AS ShortestTrip
FROM Accounts AS a
JOIN AccountsTrips AS act
ON act.AccountId = a.Id
JOIN Trips AS t
ON t.Id = act.TripId
WHERE a.MiddleName IS NULL AND t.CancelDate IS NULL
GROUP BY a.Id, a.FirstName + ' ' + a.LastName
ORDER BY LongestTrip DESC, a.Id

--11. Metropolis

SELECT TOP 5
	c.Id
	,c.[Name] AS City
	,c.CountryCode AS Country
	,COUNT(a.Id) AS Accounts
FROM Cities AS c
JOIN Accounts AS a
ON a.CityId = c.Id
GROUP BY c.Id,c.[Name],c.CountryCode
ORDER BY COUNT(a.Id) DESC

--12. Romantic Getaways

SELECT
	a.Id
	,a.Email
	,c.[Name] AS City
	,COUNT(t.Id) AS Trips
FROM Accounts AS a
JOIN AccountsTrips AS act
ON act.AccountId = a.Id
JOIN Trips AS t
ON t.Id = act.TripId
JOIN Rooms AS r
ON r.Id = t.RoomId
JOIN Hotels AS h
ON h.Id = r.HotelId
JOIN Cities AS c
ON c.Id = h.CityId
WHERE h.CityId = a.CityId
GROUP BY a.Id, a.Email, c.[Name]
ORDER BY Trips DESC, a.Id

--13. Lucrative Destinations

SELECT TOP 10
	c.Id
	,c.[Name]
	,SUM(h.BaseRate + r.Price) AS [Total Revenue]
	,COUNT(t.Id) AS Trips
FROM Cities AS c
JOIN Hotels AS h
ON h.CityId = c.Id
JOIN Rooms AS r
ON r.HotelId = h.Id
JOIN Trips AS t
ON t.RoomId = r.Id
WHERE YEAR(t.BookDate) = 2016
GROUP BY c.Id, c.[Name]
ORDER BY [Total Revenue] DESC, Trips DESC

--14. Trip Revenues

SELECT
	t.Id
	,h.[Name] AS HotelName
	,r.[Type] AS RoomType
	,SUM(IIF(t.CancelDate IS NOT NULL,0,h.BaseRate + r.Price)) AS Revenue
FROM AccountsTrips AS act
JOIN Trips AS t
ON t.Id = act.TripId
JOIN Rooms AS r
ON r.Id = t.RoomId
JOIN Hotels AS h
ON h.Id = r.HotelId
GROUP BY t.Id,h.[Name],r.[Type]
ORDER BY RoomType, t.Id

--15. Top Travelers

SELECT
	AccountId
	,Email
	,CountryCode
	,Trips
FROM (
		SELECT
			act.AccountId
			,a.Email
			,c.CountryCode
			,COUNT(c.CountryCode) AS Trips
			,DENSE_RANK() OVER (
					PARTITION BY c.CountryCode
					ORDER BY COUNT(c.CountryCode) DESC, act.AccountId
						) AS Ranking
		FROM Accounts AS a
		JOIN AccountsTrips AS act
		ON act.AccountId = a.Id
		JOIN Trips AS t
		ON t.Id = act.TripId
		JOIN Rooms AS r
		ON r.Id = t.RoomId
		JOIN Hotels AS h
		ON h.Id = r.HotelId
		JOIN Cities AS c
		ON c.Id = h.CityId
		GROUP BY act.AccountId,a.Email,c.CountryCode) AS temp
WHERE Ranking = 1
ORDER BY Trips DESC, AccountId

--16. Luggage Fees

