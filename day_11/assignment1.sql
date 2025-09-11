--Display Last Occurence of given char in a string
select len('ananya')-CHARINDEX('n',Reverse('ananya'))+1
go

--Take Full name as 'venkata narayana' and split them into first nam and last name
SELECT SUBSTRING('venkata narayana',1,CHARINDEX(' ','venkata narayana')-1) as 'firstname', 
SUBSTRING('venkata narayana',CHARINDEX(' ','venkata narayana')+1, len('venkata narayana')) as 'lastname'
go

--in word mississipi count no of i
Select LEN('mississipi')-LEN(REPLACE('mississipi','i',''))
go

--display last day of next month


--display first day of previos month
--select datename(dw, '2025-datepart(mm,dateadd(mm,-1,getdate()))-datepart(yyyy,getdate())')

--disply all fridays of current month