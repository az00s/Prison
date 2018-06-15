use Prison
go
create procedure DeleteUser (@ID as int)
as
begin

Delete from [User] where UserID=@ID

end;