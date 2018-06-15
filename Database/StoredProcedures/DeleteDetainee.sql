use Prison
go
create procedure DeleteDetainee (@ID as int)
as
begin

Delete from Detainee where DetaineeID=@ID

end;