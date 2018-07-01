use Prison
go
create procedure SelectPasswordByLogin(@Login as nvarchar(35))as

begin
select top 1 usr.[Password] from Employee emp
join [User] usr on emp.EmployeeID=usr.UserID
where emp.LastName=@Login
end;