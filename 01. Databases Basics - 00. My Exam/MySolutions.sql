CREATE DATABASE School

USE School

CREATE TABLE Students(
	Id INT PRIMARY KEY IDENTITY
	,FirstName NVARCHAR(30) NOT NULL
	,MiddleName NVARCHAR(25)
	,LastName NVARCHAR(30) NOT NULL
	--Check Age 100
	,Age INT CHECK(Age > 0) NOT NULL
	,[Address] NVARCHAR(50)
	,Phone NCHAR(10)
)

CREATE TABLE Subjects(
	Id INT PRIMARY KEY IDENTITY
	,[Name] NVARCHAR(20) NOT NULL
	,Lessons INT CHECK(Lessons > 0) NOT NULL
)

CREATE TABLE StudentsSubjects(
	Id INT PRIMARY KEY IDENTITY
	,StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL
	,SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL
	,Grade DECIMAL(15,2) CHECK(Grade BETWEEN 2 AND 6) NOT NULL
)

CREATE TABLE Exams(
	Id INT PRIMARY KEY IDENTITY
	,[Date] DATETIME
	,SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL
)

CREATE TABLE StudentsExams(
	StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL
	,ExamId INT FOREIGN KEY REFERENCES Exams(Id) NOT NULL
	,Grade DECIMAL(15,2) CHECK(Grade BETWEEN 2 AND 6) NOT NULL
)

ALTER TABLE StudentsExams
ADD PRIMARY KEY (StudentId,ExamId)

CREATE TABLE Teachers(
	Id INT PRIMARY KEY IDENTITY
	,FirstName NVARCHAR(20) NOT NULL
	,LastName NVARCHAR(20) NOT NULL
	,[Address] NVARCHAR(20) NOT NULL
	,Phone CHAR(10)
	,SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL
)

CREATE TABLE StudentsTeachers(
	StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL
	,TeacherId INT FOREIGN KEY REFERENCES Teachers(Id) NOT NULL
)

ALTER TABLE StudentsTeachers
ADD PRIMARY KEY (StudentId,TeacherId)

--2. Insert

INSERT INTO Teachers VALUES
('Ruthanne','Bamb','84948 Mesta Junction','3105500146',6)
,('Gerrard','Lowin','370 Talisman Plaza','3324874824',2)
,('Merrile','Lambdin','81 Dahle Plaza','4373065154',5)
,('Bert','Ivie','2 Gateway Circle','4409584510',4)

INSERT INTO Subjects VALUES
('Geometry',12)
,('Health',10)
,('Drama',7)
,('Sports',9)

--3. Update

UPDATE StudentsSubjects
SET Grade = 6.00
WHERE SubjectId IN (1,2) AND Grade >= 5.50

--4. Delete

DELETE FROM StudentsTeachers
WHERE TeacherId IN (SELECT Id FROM Teachers WHERE Phone LIKE '%72%')

DELETE FROM Teachers
WHERE Phone LIKE '%72%'

--5. Teen Students

SELECT
	FirstName
	,LastName
	,Age
FROM Students
WHERE Age >= 12
ORDER BY FirstName, LastName

--6. Cool Addresses

SELECT 
	FirstName + ' ' + ISNULL(MiddleName + ' ',' ') + LastName AS [Full Name]
	,[Address]
FROM Students
WHERE [Address] LIKE '%road%'
ORDER BY FirstName, LastName, [Address]

--7. 42 Phones

SELECT
	FirstName
	,[Address]
	,Phone
FROM Students
WHERE MiddleName IS NOT NULL AND Phone LIKE '42%'
ORDER BY FirstName

--8. Students Teachers

SELECT
	FirstName
	,LastName
	,COUNT(TeacherId) AS TeachersCount
FROM Students AS s
JOIN StudentsTeachers AS st
ON st.StudentId = s.Id
GROUP BY FirstName, LastName

--9. Subjects with Students

SELECT
	[Full Name]
	,[Subjects Name]
	,COUNT(s.Id) AS Students
FROM (SELECT
		t.Id
		,t.FirstName + ' ' + t.LastName AS [Full Name]
		,CONCAT(s.[Name], '-', s.Lessons) AS [Subjects Name]
		FROM Teachers AS t
		JOIN Subjects AS s
		ON s.Id = t.SubjectId) AS temp
JOIN StudentsTeachers AS st
ON st.TeacherId = temp.Id
JOIN Students AS s
ON s.Id = st.StudentId
GROUP BY [Full Name], [Subjects Name]
ORDER BY Students DESC,[Full Name],[Subjects Name]

--10. Students to Go

SELECT
	s.FirstName + ' ' + s.LastName AS [Full Name]
FROM Students AS s
LEFT JOIN StudentsExams AS se
ON se.StudentId = s.Id
WHERE se.ExamID IS NULL
ORDER BY [Full Name]

--11. Busiest Teachers

SELECT TOP 10
	t.FirstName
	,t.LastName
	,COUNT(st.StudentId) AS StudentsCount
FROM Teachers AS t
JOIN StudentsTeachers AS st
ON st.TeacherId = t.Id
GROUP BY t.FirstName, t.LastName
ORDER BY StudentsCount DESC, t.FirstName, t.LastName

--12. Top Students

SELECT TOP 10
	s.FirstName
	,s.LastName
	,FORMAT(AVG(se.Grade),'0.00') AS Grade
FROM Students AS s
JOIN StudentsExams AS se
ON se.StudentId = s.Id
GROUP BY s.FirstName, s.LastName
ORDER BY Grade DESC, s.FirstName, s.LastName

--13. Second Highest Grade

SELECT
	FirstName
	,LastName
	,Grade
FROM
(SELECT
	s.FirstName
	,s.LastName
	,FORMAT(ss.Grade,'0.00') AS Grade
	,ROW_NUMBER() OVER (PARTITION BY s.Id ORDER BY ss.Grade DESC) AS Ranking
FROM Students AS s
JOIN StudentsSubjects AS ss
ON ss.StudentId = s.Id
--WHERE s.FirstName = 'Bernardine'
) AS temp
WHERE Ranking = 2
ORDER BY FirstName, LastName

--14. Not So In The Studying

SELECT
	s.FirstName + ' ' + ISNULL(s.MiddleName + ' ', '') + LastName AS [Full Name]
FROM Students AS s
LEFT JOIN StudentsSubjects AS ss
ON ss.StudentId = s.Id
WHERE ss.SubjectId IS NULL
ORDER BY [Full Name]

--15. Top Student per Teacher
SELECT
	[Teacher Full Name]
	,[Subject Name]
	,[Student Full Name]
	,FORMAT(Grade,'0.00')
FROM(
SELECT
	t.FirstName + ' ' + t.LastName AS [Teacher Full Name]
	,sub.[Name] AS [Subject Name]
	,s.FirstName + ' ' + s.LastName AS [Student Full Name]
	,AVG(ss.Grade) AS Grade
	,DENSE_RANK() OVER (PARTITION BY t.FirstName + ' ' + t.LastName ORDER BY AVG(ss.Grade) DESC) AS Ranking
FROM Teachers AS t
JOIN StudentsTeachers AS st
ON st.TeacherId = t.Id
JOIN Students AS s
ON s.Id = st.StudentId
JOIN StudentsSubjects AS ss
ON ss.StudentId = s.Id
JOIN Subjects AS sub
ON sub.Id = ss.SubjectId
WHERE t.SubjectId = sub.Id
GROUP BY t.FirstName + ' ' + t.LastName,sub.[Name],s.FirstName + ' ' + s.LastName) AS temp
WHERE RANKing = 1
ORDER BY [Subject Name],[Teacher Full Name],Grade DESC
--16. Average Grade per Subject

SELECT
	s.[Name]
	,AVG(ss.Grade) AS AverageGrade
FROM StudentsSubjects AS ss
JOIN Subjects AS s
ON s.Id = ss.SubjectId
GROUP BY s.Id, s.[Name]
ORDER BY s.Id

--17. Exams Information

SELECT
	IIF(e.[Date] IS NULL,'TBA',CONCAT('Q',DATEPART(QUARTER,e.[Date]))) AS [Quarter]
	,sub.[Name] AS SubjectName
	,COUNT(s.Id) AS StudentsCount
FROM Exams AS e
JOIN StudentsExams AS se
ON se.ExamId = e.Id
JOIN Students AS s
ON s.Id = se.StudentId
JOIN Subjects AS sub
ON sub.Id = e.SubjectId
WHERE se.Grade >= 4.00
GROUP BY IIF(e.[Date] IS NULL,'TBA',CONCAT('Q',DATEPART(QUARTER,e.[Date]))),sub.[Name]
ORDER BY [Quarter]

--18. Exam Grades

GO
CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(15,2))
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @count INT;
	DECLARE @studentName VARCHAR(50);
	IF(@grade > 6.00)
	BEGIN
		RETURN 'Grade cannot be above 6.00!'
	END
	IF((SELECT Id FROM Students WHERE Id = @studentId) IS NULL)
	BEGIN
		RETURN 'The student with provided id does not exist in the school!'
	END
	RETURN (CONCAT('You have to update ',(SELECT
		COUNT(se.Grade) AS GradeCount
	FROM Students AS s
	JOIN StudentsExams AS se
	ON se.StudentId = s.Id
	WHERE s.Id = @studentId AND se.Grade BETWEEN @grade AND @grade + 0.50
	GROUP BY s.Id),' grades for the student ',(SELECT FirstName FROM Students WHERE Id = @studentId)))
END
GO
SELECT dbo.udf_ExamGradesToUpdate(12, 6.20)

--19. Exclude from school

GO
CREATE PROCEDURE usp_ExcludeFromSchool(@StudentId INT)
AS
IF((SELECT Id FROM Students WHERE Id = @StudentId) IS NULL)
BEGIN
	RAISERROR('This school has no student with the provided id!',12,1)
	RETURN
END
DELETE FROM StudentsTeachers
WHERE StudentId = @StudentId
DELETE FROM StudentsSubjects
WHERE StudentId = @StudentId
DELETE FROM StudentsExams
WHERE StudentId = @StudentId
DELETE FROM Students
WHERE Id = @StudentId

GO
EXEC usp_ExcludeFromSchool 1
SELECT COUNT(*) FROM Students

EXEC usp_ExcludeFromSchool 301

--20. Deleted Student

CREATE TABLE ExcludedStudents(
	StudentId INT
	,StudentName NVARCHAR(50)
)

GO

CREATE TRIGGER tr_ExcludeStudents ON Students AFTER DELETE
AS
BEGIN
	INSERT INTO ExcludedStudents
	SELECT 
		deleted.Id
		,deleted.FirstName + ' ' + deleted.LastName
	FROM deleted
END

DELETE FROM StudentsExams
WHERE StudentId = 1

DELETE FROM StudentsTeachers
WHERE StudentId = 1

DELETE FROM StudentsSubjects
WHERE StudentId = 1

DELETE FROM Students
WHERE Id = 1

SELECT * FROM ExcludedStudents
