use Prison
go
create procedure SelectAllLogins
as
begin

select emp.LastName from Employee emp
join [User] usr on emp.EmployeeID=usr.UserID
 
end;
