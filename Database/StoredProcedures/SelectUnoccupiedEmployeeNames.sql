use Prison
go
create procedure SelectUnoccupiedEmployeeNames
as
begin

select emp.EmployeeID,emp.LastName from Employee emp
left join [User] usr on emp.EmployeeID=usr.UserID
where usr.UserID is null
end;