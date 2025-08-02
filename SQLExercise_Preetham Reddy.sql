SELECT * FROM sys.tables;

-- View columns in a specific table
EXEC sp_help 'dbo.tblEmployees';

--1.Write a query to Get a List of Employees who have a one part name
 
 SELECT EmployeeNumber, Name
FROM tblEmployees
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) = 0;

--2; Thrre part name
SELECT EmployeeNumber, Name
FROM tblEmployees
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) = 2;

--3.First, Middle OR Last name as 'Ram' only (no other parts)
SELECT EmployeeNumber, Name
FROM tblEmployees
WHERE Name LIKE 'Ram'
   OR Name LIKE 'Ram %'
   OR Name LIKE '% Ram'
   OR Name LIKE '% Ram %';


--4. Bitwise operators
SELECT
    65 | 11     AS [65 OR 11],
    65 ^ 11     AS [65 XOR 11],
    65 & 11     AS [65 AND 11],
    (12 & 4) | (13 & 1) AS [(12 AND 4) OR (13 AND 1)],
    127 | 64    AS [127 OR 64],
    127 ^ 64    AS [127 XOR 64],
    127 ^ 128   AS [127 XOR 128],
    127 & 64    AS [127 AND 64],
    127 & 128   AS [127 AND 128];

--5.columns from tblCenterMaster
SELECT * FROM dbo.tblCentreMaster;

--6.employee types in the organization
SELECT DISTINCT EmployeeType
FROM tblEmployees;

--7.Get Name, FatherName, DOB of employees based on PresentBasic
-- a. Greater than 3000
SELECT Name, FatherName, DOB
FROM tblEmployees
WHERE PresentBasic > 3000;

-- b. Less than 3000
SELECT Name, FatherName, DOB
FROM tblEmployees
WHERE PresentBasic < 3000;

-- c. Between 3000 and 5000
SELECT Name, FatherName, DOB
FROM tblEmployees
WHERE PresentBasic BETWEEN 3000 AND 5000;

--8.
-- a. Ends with 'KHAN'
SELECT *
FROM tblEmployees
WHERE Name LIKE '%KHAN';

-- b. Starts with 'CHANDRA'
SELECT *
FROM tblEmployees
WHERE Name LIKE 'CHANDRA%';

-- c. Is 'RAMESH' and initial is between A and T
SELECT *
FROM tblEmployees
WHERE Name LIKE '[A-T].RAMESH';

-- Exercise 2 Solutions

-- 1. Total Present Basic per department > 30000
SELECT Department, SUM(PresentBasic) AS TotalPresentBasic
FROM tblEmployees
GROUP BY Department
HAVING SUM(PresentBasic) > 30000
ORDER BY Department;

-- 2. Max, Min, Avg age by ServiceType, ServiceStatus, Centre (in years & months)
SELECT ServiceType, ServiceStatus, Centre,
       MAX(DATEDIFF(MONTH, DOB, GETDATE()) / 12) AS MaxAge_Yrs,
       MIN(DATEDIFF(MONTH, DOB, GETDATE()) / 12) AS MinAge_Yrs,
       AVG(DATEDIFF(MONTH, DOB, GETDATE()) / 12.0) AS AvgAge_Yrs
FROM tblEmployees
GROUP BY ServiceType, ServiceStatus, Centre;

-- 3. Max, Min, Avg service by ServiceType, ServiceStatus, Centre (in years & months)
SELECT ServiceType, ServiceStatus, Centre,
       MAX(DATEDIFF(MONTH, DOJ, GETDATE()) / 12) AS MaxService_Yrs,
       MIN(DATEDIFF(MONTH, DOJ, GETDATE()) / 12) AS MinService_Yrs,
       AVG(DATEDIFF(MONTH, DOJ, GETDATE()) / 12.0) AS AvgService_Yrs
FROM tblEmployees
GROUP BY ServiceType, ServiceStatus, Centre;

-- 4. Departments where total salary > 3x average salary
SELECT Department
FROM tblEmployees
GROUP BY Department
HAVING SUM(PresentBasic) > 3 * AVG(PresentBasic);

-- 5. Departments where total > 2x avg salary AND max basic >= 3x min basic
SELECT Department
FROM tblEmployees
GROUP BY Department
HAVING SUM(PresentBasic) > 2 * AVG(PresentBasic)
   AND MAX(PresentBasic) >= 3 * MIN(PresentBasic);

-- 6. Centers where max length of name is 2x min length
SELECT Centre
FROM tblEmployees
GROUP BY Centre
HAVING MAX(LEN(Name)) >= 2 * MIN(LEN(Name));

-- 7. Max, Min, Avg service by ServiceType, ServiceStatus, Centre (in milliseconds)
SELECT ServiceType, ServiceStatus, Centre,
       MAX(DATEDIFF(MILLISECOND, DOJ, GETDATE())) AS MaxServiceMS,
       MIN(DATEDIFF(MILLISECOND, DOJ, GETDATE())) AS MinServiceMS,
       AVG(DATEDIFF(MILLISECOND, DOJ, GETDATE()) * 1.0) AS AvgServiceMS
FROM tblEmployees
GROUP BY ServiceType, ServiceStatus, Centre;

-- 8. Employees with leading/trailing spaces
SELECT EmployeeNumber, Name
FROM tblEmployees
WHERE Name LIKE ' %' OR Name LIKE '% ';

-- 9. Names with multiple spaces between words
SELECT EmployeeNumber, Name
FROM tblEmployees
WHERE Name LIKE '%  %';

-- 10. Cleaned names (trim + normalize spacing)
SELECT EmployeeNumber,
       LTRIM(RTRIM(
         REPLACE(REPLACE(REPLACE(Name, '  ', ' '), ' .', '.'), '. ', '.')
       )) AS CleanedName
FROM tblEmployees;

-- 11. Max number of words in names
SELECT MAX(LEN(Name) - LEN(REPLACE(Name, ' ', '')) + 1) AS MaxWordCount
FROM tblEmployees;

-- 12. Names that start and end with same character
SELECT *
FROM tblEmployees
WHERE LEFT(Name, 1) = RIGHT(Name, 1);

-- 13. First and second name start with same char
SELECT *
FROM tblEmployees
WHERE LEFT(Name, 1) = SUBSTRING(Name, CHARINDEX(' ', Name) + 1, 1);

-- 14. All words start with same character
SELECT *
FROM tblEmployees
WHERE Name NOT LIKE '% %' -- one word
   OR NOT EXISTS (
     SELECT 1
     FROM STRING_SPLIT(Name, ' ')
     GROUP BY LEFT(value,1)
     HAVING COUNT(*) = 1
   );

-- 15. Any word (excluding initials) starts and ends with same char
SELECT *
FROM tblEmployees
WHERE EXISTS (
    SELECT value
    FROM STRING_SPLIT(Name, ' ')
    WHERE LEN(value) > 1 AND LEFT(value, 1) = RIGHT(value, 1)
);

-- 16. Present basic rounded to 100
-- a. Using ROUND
SELECT * FROM tblEmployees WHERE ROUND(PresentBasic, -2) = PresentBasic;
-- b. Using FLOOR
SELECT * FROM tblEmployees WHERE FLOOR(PresentBasic / 100.0) * 100 = PresentBasic;
-- c. Using MOD
SELECT * FROM tblEmployees WHERE PresentBasic % 100 = 0;
-- d. Using CEILING
SELECT * FROM tblEmployees WHERE CEILING(PresentBasic / 100.0) * 100 = PresentBasic;

-- 17. Departments where all employees have basic rounded to 100
SELECT Department
FROM tblEmployees
GROUP BY Department
HAVING COUNT(*) = SUM(CASE WHEN PresentBasic % 100 = 0 THEN 1 ELSE 0 END);

-- 18. Departments where no employee has basic rounded to 100
SELECT Department
FROM tblEmployees
GROUP BY Department
HAVING SUM(CASE WHEN PresentBasic % 100 = 0 THEN 1 ELSE 0 END) = 0;

-- 19. Bonus eligibility and age info (simplified)
SELECT EmployeeNumber, Name, 
       DATEADD(MONTH, 15, DOJ) AS EligibilityDate,
       DATEDIFF(YEAR, DOB, DATEADD(MONTH, 16, DOJ)) AS AgeOnBonusYear
FROM tblEmployees;

-- 20. Employees eligible for bonus (simplified logic)
SELECT * FROM tblEmployees
WHERE (
    (ServiceType = 1 AND EmployeeType = 1 AND DATEDIFF(YEAR, DOJ, GETDATE()) >= 10 AND (60 - DATEDIFF(YEAR, DOB, GETDATE())) >= 15)
 OR (ServiceType = 1 AND EmployeeType = 2 AND DATEDIFF(YEAR, DOJ, GETDATE()) >= 12 AND (55 - DATEDIFF(YEAR, DOB, GETDATE())) >= 14)
 OR (ServiceType = 1 AND EmployeeType = 3 AND DATEDIFF(YEAR, DOJ, GETDATE()) >= 12 AND (55 - DATEDIFF(YEAR, DOB, GETDATE())) >= 12)
 OR (ServiceType IN (2,3,4) AND DATEDIFF(YEAR, DOJ, GETDATE()) >= 15 AND (65 - DATEDIFF(YEAR, DOB, GETDATE())) >= 20)
);

-- 21. Current date in various formats
SELECT
  CONVERT(VARCHAR, GETDATE(), 100) AS Format100,
  CONVERT(VARCHAR, GETDATE(), 101) AS Format101,
  CONVERT(VARCHAR, GETDATE(), 102) AS Format102,
  CONVERT(VARCHAR, GETDATE(), 103) AS Format103,
  CONVERT(VARCHAR, GETDATE(), 104) AS Format104,
  CONVERT(VARCHAR, GETDATE(), 105) AS Format105;

-- 22. Employees with net payment < basic (tblPayemployeeParamDetails assumed)
SELECT ep.EmployeeNumber, ep.Name, p.PayFromDate, p.NetPayment, e.PresentBasic
FROM tblEmployees e
JOIN tblPayemployeeParamDetails p ON e.EmployeeNumber = p.EmployeeNumber
WHERE p.NetPayment < e.PresentBasic;

