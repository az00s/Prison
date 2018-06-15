use Prison
go
create procedure SelectRoleByID(@ID int)
as
begin
select * from [Role] where RoleID=@ID
end;
go