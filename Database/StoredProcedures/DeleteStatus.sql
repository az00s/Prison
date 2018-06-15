use Prison
go
create procedure DeleteStatus (@ID int) 
as

begin

delete from MaritalStatus
where StatusID=@ID

end;