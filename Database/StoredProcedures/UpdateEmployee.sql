USE [Prison]
GO

create procedure UpdateEmployee(@ID as int,@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@PositionID as int) 
as
begin

Update Employee 
set 

[FirstName]=@FirstName,
[LastName]=@LastName,
[MiddleName]=@MiddleName,
[PositionID]=@PositionID

where EmployeeID=@ID
end;
go
