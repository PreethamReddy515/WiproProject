use EmpSample_#OK
go 

--GET EMPLOYEES WITH A ONE PART NAME
Select * from tblEmployees where len(Name)-len(replace(Name,' ',''))=0;
go

--GET EMPLOYEES WITH A THREE PART NAME
Select * from tblEmployees where len(Name)-len(replace(Name,' ',''))=2;
go

Select * from tblEmployees where Name like 'Ram %' or Name like '% Ram %' or Name like '% Ram';
go


select 65|11 as result1;
select 65^11 as result2;
select 65&11 as result3;
select (12&4)|(13&1) as result4;
select 127|64 as result5;
select 127^64 as result6;
select 127^128 as result7;
select 127&64 as result8;
select 127&128 as result9;
go


select * from dbo.tblCentreMaster;
go


select distinct EmployeeType from tblEmployees;
go

select Name, FatherName, DOB from tblEmployees where PresentBasic>3000
go

select Name, FatherName, DOB from tblEmployees where PresentBasic<3000
go 

select Name, FatherName, DOB from tblEmployees where PresentBasic between 3000 and 5000
go

Select * from tblEmployees where Name like '%Khan';
go 

Select * from tblEmployees where Name like 'Chandra%';
go

Select * from tblEmployees where Name like '[A-T]%Ramesh';
go

--select dept, sum(PresentBasic) as totalbasic from tblEmployees 
--group by dept 
--having sum(PresentBasic)>50000 
--order by dept;
--go
