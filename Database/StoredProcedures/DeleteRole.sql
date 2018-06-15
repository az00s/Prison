use Prison
go
create procedure DeleteRole (@ID as int)
as
begin

Delete from [Role] where RoleID=@ID

end;