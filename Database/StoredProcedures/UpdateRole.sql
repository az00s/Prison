USE [Prison]
GO

create procedure UpdateRole(@ID as int,@RoleName as nvarchar(250)) 
as
begin

Update [Role] 
set 

[RoleName]=@RoleName


where RoleID=@ID
end;
go
