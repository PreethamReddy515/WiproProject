use sqlpractice 
Go

-- Display List of tables avaialble in current database 

select * from INFORMATION_SCHEMA.TABLES
GO

-- Display information about Emp Table 

sp_help Emp
GO

-- Display all records from Emp table 

select * from Emp 
GO

-- Display Empno, Name, Basic from Emp table 

select Empno 'Employ No', nam 'Employ Name', basic 'Salary'
	from Emp 
GO

-- Display All records whose Basic > 50000 

select * from Emp 
WHERE basic > 50000
GO

-- Display All records whose Dept is 'DOTNET'

select * from Emp 
where Dept='Dotnet'
GO

-- Display all records whose name is 'Swetha'
select * from Emp 
where nam='Swetha'
GO

-- Between...AND : Display records from specific Range (works for numbers and date only) 

select * from EMP 
WHERE Basic Between 50000 and 90000
GO

select * from EMP 
WHERE Basic NOT Between 50000 and 90000
GO

-- IN Clause : Used to check for multiple values of particular column 

-- Display all records whose Dept is Java or Dotnet or Sql 

select * from Emp 
where dept in('Dotnet','Java','Sql')
GO

select * from Emp 
where dept NOT IN('Dotnet','Java','Sql')
GO

select * from Emp 
where nam IN('Manu','Satish','Swapna','Smitha','Rushi')
GO

-- LIKE operator : Used to display data w.r.t. wild cards

-- Display all records whose name starts with 'S'

select * from Emp where nam LIKE 'S%' 
GO

-- Display all records whose name ends with 'A' 

select * from Emp where nam LIKE '%A'
GO


-- Distinct : Eliminate duplicate entries at the time of display 

select Dept from Emp
GO

select distinct Dept from Emp 
GO

-- Order By : Used to display values w.r.t. Particular field(s) in ascending or descending order 

select * from Emp order by nam 
GO

select * from Emp order by Basic DESC
GO

select * from Emp order by Dept, Nam 
GO

select * from Emp order by Dept, Nam DESC
GO




-- Number Functions 

-- abs() : Returns the absolute value 

select Abs(-12)
GO

-- power(n,m) : Returns n power m value

select POWER(2,3) 
GO

-- sqrt(n) : Returns the sqrt value 

select SQRT(49) 
GO

-- CEILING(n) : Returns the greatest integer value 

select CEILING(12.00000001)
GO

-- FLOOR(n) : Returns the smallest integer value 

select FLOOR(12.999999)
GO



-- String Functions

/* charindex() : Used to display the first occurence of the given character  */

select CHARINDEX('l','hello') 
GO

select Nam, CHARINDEX('a',nam) from Emp 
GO

/* Reverse() : Used to display string in reverse order */

select Reverse('Rajesh') 
GO

select Nam,Reverse(Nam) from Emp 
GO

/* Len() : Display's length of string  */ 

select len('Charishma Gada')
GO

select nam, len(nam) from Emp
GO

/* Left() : Displays no.of left-side chars */

select left('Prasanna',4) 
GO

/* Right() : Displays no.of right-side chars */ 

select right('Prasanna',4)
GO

/* Upper() : Dispalys string in Upper Case */ 

select nam, upper(nam) from Emp
GO

/* Lower() : Displays string in Lower Case */ 

select nam, Lower(nam) from Emp 
GO

/* Substring() : Used to display part of the string */ 

select SUBSTRING('welcome to sql',5,8) 
GO

/* Replace() : used to replace old value/string with new value/string in all occurrences */ 

SELECT REPLACE('Dotnet Training','Dotnet','Java') as replaced
GO




-- Date Functions 

-- GetDate() : used to display today's date 

select GETDATE() 
GO

select convert(varchar,GETDATE(),1) 
Go

select convert(varchar,GETDATE(),2) 
Go

select convert(varchar,GETDATE(),101) 
Go

select convert(varchar,GETDATE(),103) 
Go

/* DatePart() : used to display the specific portion of the given date */

select datepart(dd,getdate())
select datepart(mm,getdate())
select datepart(yy,getdate())
select datepart(hh,getdate())
select datepart(mi,getdate())
select datepart(ss,getdate())
select datepart(ms,getdate())
select datepart(dw,getdate())
select datepart(qq,getdate())

-- DateName() : Displays date part in engligh words 

select datename(dw,getdate());


select convert(varchar,DATEPART(dd,getdate())) + '/' + 
convert(varchar,datepart(mm,getdate())) + '/' + 
convert(varchar,DATEPART(yy,getdate()))
GO

select DATENAME(mm, getdate())
GO

/* DateAdd() : Used to add no.of days or months or years to the particular date */

select DATEADD(dd,3,getdate())

select dateadd(mm,3,getdate())

select DATEADD(yy,3,getdate())

/* DateDiff() : used to Display difference between Two Dates */ 

select DATEDIFF(dd,'03/09/1980',getdate())
select DATEDIFF(yy,'03/09/1980',getdate())



-- Aggregate Functions 

-- sum() : used to perform sum operation 

select sum(basic) from Emp 
GO

-- Avg() : Displays avg operation 

select avg(basic) from Emp 
GO

-- Max() : Display max value 

select max(basic) from Emp 
GO

-- Min() : Displays Min. Value

select min(basic) from Emp 
GO

-- count(*) : Displays total no.of records 

select count(*) from Emp 
GO

-- count(column_name) : displays count for that column not null values 

select count(basic) from Emp
Go


