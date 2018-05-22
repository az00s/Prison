USE [Prison]
GO

create procedure CreateEmployee(@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@PositionID as int) 
as
begin

Insert into Employee ([FirstName],[LastName],[MiddleName],[PositionID])
values(@FirstName,@LastName,@MiddleName,@PositionID)

end;
go
