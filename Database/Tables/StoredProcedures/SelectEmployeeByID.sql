use Prison
go
create procedure SelectEmployeeByID(@ID as int)
as
begin

Select * from Employee 
where EmployeeID=@ID

end;