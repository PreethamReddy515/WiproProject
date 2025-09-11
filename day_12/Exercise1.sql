use EmpSample_#OK
go

SELECT * FROM tblEmployees 
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) = 0;
go

SELECT * FROM tblEmployees
WHERE LEN(Name) - LEN(REPLACE(Name, ' ', '')) = 2;
go

SELECT * FROM tblEmployees 
WHERE 
   Name LIKE 'Ram %'     -- Ram as first name
   OR Name LIKE '% Ram %' -- Ram as middle name
   OR Name LIKE '% Ram';  -- Ram as last name
go

SELECT * FROM tblEmployees
WHERE Name LIKE '%Ram%'
go

SELECT 
    65 | 11     AS res1,
    65 ^ 11     AS res2,
    65 & 11     AS res3,
    (12 & 4) | (13 & 1) AS res4,
    127 | 64    AS res5,
    127 ^ 64    AS res6,
    127 ^ 128   AS res7,
    127 & 64    AS res8,
    127 & 128   AS res9;
go

SELECT * FROM dbo.tblCentreMaster;
go

SELECT Name, FatherName, DOB 
FROM tblEmployees 
WHERE PresentBasic > 3000;
go

SELECT Name, FatherName, DOB 
FROM tblEmployees 
WHERE PresentBasic < 3000;
go

SELECT Name, FatherName, DOB 
FROM tblEmployees 
WHERE PresentBasic BETWEEN 3000 AND 5000;
go

SELECT * FROM tblEmployees
WHERE Name LIKE '%KHAN';
go

SELECT * FROM tblEmployees
WHERE Name LIKE 'CHANDRA%';
go

SELECT * FROM tblEmployees
WHERE Name LIKE '[A-T].RAMESH';
go
