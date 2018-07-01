use Prison
go
create procedure DeleteEmployee (@ID as int)
as
begin

Delete from Employee where EmployeeID=@ID

end;