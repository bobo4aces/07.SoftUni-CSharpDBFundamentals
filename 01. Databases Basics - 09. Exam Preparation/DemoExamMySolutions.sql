CREATE DATABASE ColonialJourney

--Section 1. DDL (30 pts)

USE ColonialJourney
CREATE TABLE Planets (
	Id INT PRIMARY KEY IDENTITY
	, [Name] VARCHAR(30) NOT NULL
)

CREATE TABLE Spaceports (
	Id INT PRIMARY KEY IDENTITY
	, [Name] VARCHAR(50) NOT NULL
	, PlanetId INT FOREIGN KEY REFERENCES Planets(Id) NOT NULL
)

CREATE TABLE Spaceships (
	Id INT PRIMARY KEY IDENTITY
	, [Name] VARCHAR(50) NOT NULL
	, Manufacturer VARCHAR(30) NOT NULL
	, LightSpeedRate INT DEFAULT 0
)

CREATE TABLE Colonists (
	Id INT PRIMARY KEY IDENTITY
	, FirstName VARCHAR(20) NOT NULL
	, LastName VARCHAR(20) NOT NULL
	, Ucn VARCHAR(10) UNIQUE NOT NULL
	, BirthDate DATE NOT NULL
)

CREATE TABLE Journeys (
	Id INT PRIMARY KEY IDENTITY
	, JourneyStart DATETIME NOT NULL
	, JourneyEnd DATETIME NOT NULL
	, Purpose VARCHAR(11) CHECK (Purpose IN ('Medical', 'Technical', 'Educational', 'Military'))
	, DestinationSpaceportId INT FOREIGN KEY REFERENCES Spaceports(Id) NOT NULL
	, SpaceshipId INT FOREIGN KEY REFERENCES Spaceships(Id) NOT NULL
)

CREATE TABLE TravelCards (
	Id INT PRIMARY KEY IDENTITY
	, CardNumber CHAR(10) UNIQUE NOT NULL
	, JobDuringJourney VARCHAR(8) CHECK (JobDuringJourney IN ('Pilot', 'Engineer', 'Trooper', 'Cleaner', 'Cook'))
	, ColonistId INT FOREIGN KEY REFERENCES Colonists(Id) NOT NULL
	, JourneyId INT FOREIGN KEY REFERENCES Journeys(Id) NOT NULL
)

--Section 2. DML (10 pts)
--2.	Insert

INSERT INTO Planets VALUES 
	('Mars')
	, ('Earth')
	, ('Jupiter')
	, ('Saturn')

INSERT INTO Spaceships VALUES 
	('Golf','VW',3)
	, ('WakaWaka','Wakanda',4)
	, ('Falcon9','SpaceX',1)
	, ('Bed','Vidolov',6)

--3.	Update

UPDATE Spaceships
SET LightSpeedRate += 1
WHERE Id BETWEEN 8 AND 12

--4.	Delete

ALTER TABLE TravelCards
DROP CONSTRAINT FK__TravelCar__Journ__22AA2996

ALTER TABLE TravelCards
ADD CONSTRAINT FK__TravelCar__Journ__22AA2996
FOREIGN KEY (JourneyId) REFERENCES Journeys(Id) ON DELETE CASCADE

DELETE FROM Journeys
WHERE Id <= 3

--Section 3. Querying (40 pts)
--5.	Select all travel cards

SELECT 
	CardNumber
	, JobDuringJourney
FROM TravelCards
ORDER BY CardNumber ASC

--6.	Select all colonists

SELECT 
	Id
	, CONCAT(FirstName, ' ', LastName) AS [FullName]
	, Ucn
FROM Colonists
ORDER BY FirstName ASC, LastName ASC, Id ASC

--7.	Select all military journeys

SELECT 
	Id
	, FORMAT(JourneyStart,'dd/MM/yyyy') AS JourneyStart
	, FORMAT(JourneyEnd,'dd/MM/yyyy') AS JourneyEnd
FROM Journeys
WHERE Purpose = 'Military'
ORDER BY JourneyStart ASC

--8.	Select all pilots

SELECT 
	c.Id
	, CONCAT(c.FirstName, ' ', c.LastName) AS full_name
FROM Colonists AS c
LEFT JOIN TravelCards AS tc
ON c.Id = tc.ColonistId
WHERE tc.JobDuringJourney = 'Pilot'
ORDER BY c.Id ASC

--9.	Count colonists

SELECT 
	COUNT(*) AS [count]
FROM Colonists AS c
LEFT JOIN TravelCards AS tc
ON c.Id = tc.ColonistId
LEFT JOIN Journeys AS j
ON tc.JourneyId = j.Id
WHERE j.Purpose = 'Technical'

--10.	Select the fastest spaceship

SELECT TOP 1
	ss.[Name] AS SpaceshipName
	, sp.[Name] AS SpaceportName
FROM Spaceships AS ss
LEFT JOIN Journeys AS j
ON ss.id = j.SpaceshipId
LEFT JOIN Spaceports AS sp
ON j.DestinationSpaceportId = sp.Id
ORDER BY ss.LightSpeedRate DESC

--11.	Select spaceships with pilots younger than 30 years

SELECT
	ss.[Name]
	, ss.Manufacturer
FROM Spaceships AS ss
LEFT JOIN Journeys AS j
ON ss.id = j.SpaceshipId
LEFT JOIN TravelCards AS tc
ON j.Id = tc.JourneyId
LEFT JOIN Colonists AS c
ON tc.ColonistId = c.Id
WHERE DATEDIFF(year,c.BirthDate,'01/01/2019') < 30 AND tc.JobDuringJourney = 'Pilot'
ORDER BY ss.[Name] ASC, ss.Manufacturer

--12.	Select all educational mission planets and spaceports

SELECT
	p.[Name] AS PlanetName
	, sp.[Name] AS SpaceportName
FROM Planets AS p
LEFT JOIN Spaceports AS sp
ON p.Id = sp.PlanetId
LEFT JOIN Journeys AS j
ON sp.Id = j.DestinationSpaceportId
WHERE j.Purpose = 'Educational'
ORDER BY sp.[Name] DESC

--13.	Select all planets and their journey count

SELECT
	p.[Name] AS PlanetName
	, COUNT(j.Id) AS JourneysCount
FROM Planets AS p
JOIN Spaceports AS sp
ON p.Id = sp.PlanetId
JOIN Journeys AS j
ON sp.Id = j.DestinationSpaceportId
GROUP BY p.[Name]
ORDER BY JourneysCount DESC, PlanetName ASC

--14.	Select the longest journey

SELECT TOP 1
	j.Id
	, p.[Name] AS PlanetName
	, sp.[Name] AS SpaceportName
	, j.Purpose AS JourneyPurpose
FROM Journeys AS j
LEFT JOIN Spaceports AS sp
ON j.DestinationSpaceportId = sp.Id
LEFT JOIN Planets AS p
ON sp.PlanetId = p.Id
ORDER BY DATEDIFF(minute,j.JourneyStart,j.JourneyEnd) ASC

--15.	Select the less popular job

SELECT TOP 1
	j.Id AS JourneyId
	, tc.JobDuringJourney AS JobName
FROM Journeys AS j
LEFT JOIN TravelCards AS tc
ON j.Id = tc.JourneyId
ORDER BY DATEDIFF(minute,j.JourneyStart,j.JourneyEnd) DESC

--16.	Select Second Oldest Important Colonist

SELECT
	*
FROM
	(SELECT
		tc.JobDuringJourney
		, CONCAT(c.FirstName, ' ', c.LastName) AS FullName
		, DENSE_RANK() OVER (PARTITION BY tc.JobDuringJourney ORDER BY c.BirthDate ASC) AS JobRank
	FROM TravelCards AS tc
	LEFT JOIN Colonists AS c
	ON tc.ColonistId = c.Id) AS Ranking
WHERE JobRank = 2

--17.	Planets and Spaceports

SELECT
	p.[Name]
	, COUNT(sp.Id) AS [Count]
FROM Planets AS p
LEFT JOIN Spaceports AS sp
ON p.Id = sp.PlanetId
GROUP BY p.[Name]
ORDER BY [Count] DESC, p.[Name] ASC

--Section 4. Programmability (20 pts)
--18.	Get Colonists Count

GO
CREATE OR ALTER FUNCTION dbo.udf_GetColonistsCount(@PlanetName VARCHAR(30))
RETURNS INT
AS
BEGIN
DECLARE @Count INT;
SET @Count = (
	SELECT
		[Count]
		FROM (
			SELECT
				COUNT(tc.ColonistID) AS [Count]
			FROM Planets AS p
			JOIN Spaceports AS sp
			ON p.Id = sp.PlanetId
			JOIN Journeys AS j
			ON sp.Id = j.DestinationSpaceportId
			JOIN TravelCards AS tc
			ON j.Id = tc.JourneyId
			WHERE p.[Name] = @PlanetName
			GROUP BY p.[Name]
			) AS tbl
)
IF (@Count IS NULL)
BEGIN
RETURN 0
END
RETURN @Count
END
GO
SELECT dbo.udf_GetColonistsCount('a')

--19.	Change Journey Purpose

GO
CREATE PROCEDURE usp_ChangeJourneyPurpose(@JourneyId INT, @NewPurpose VARCHAR(50))
AS
BEGIN TRANSACTION
	IF(NOT EXISTS(SELECT * FROM Journeys WHERE Id=@JourneyId))
	BEGIN
		RAISERROR('The journey does not exist!', 16, 1)
		ROLLBACK
		RETURN
	END
	IF(EXISTS(SELECT Purpose FROM Journeys WHERE Id=@JourneyId AND Purpose = @NewPurpose))
	BEGIN
		RAISERROR('You cannot change the purpose!', 16, 2)
		ROLLBACK
		RETURN
	END
	UPDATE Journeys
	SET Purpose = @NewPurpose
	WHERE Id = @JourneyId
COMMIT

--20.	Deleted Journeys

CREATE TABLE DeletedJourneys(
	Id INT PRIMARY KEY
	, JourneyStart DATETIME NOT NULL
	, JourneyEnd DATETIME NOT NULL
	, Purpose VARCHAR(11) CHECK (Purpose IN ('Medical', 'Technical', 'Educational', 'Military'))
	, DestinationSpaceportId INT FOREIGN KEY REFERENCES Spaceports(Id) NOT NULL
	, SpaceshipId INT FOREIGN KEY REFERENCES Spaceships(Id) NOT NULL
)

GO
CREATE TRIGGER tr_DeleteJourney ON Journeys AFTER DELETE
AS
BEGIN
	INSERT INTO DeletedJourneys
	SELECT
		deleted.Id
		, deleted.JourneyStart
		, deleted.JourneyEnd
		, deleted.Purpose
		, deleted.DestinationSpaceportId
		, deleted.SpaceshipId
	FROM deleted
END