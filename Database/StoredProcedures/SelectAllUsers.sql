use Prison
go
create procedure SelectAllUsers as
begin

select usr.UserID,emp.LastName 'UserName',usr.Email,usr.[Password] from [User] usr
join Employee emp on emp.EmployeeID=usr.UserID
end;