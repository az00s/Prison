use Prison
go
create procedure DeleteNumber(@ID int) 
as

begin

delete from PhoneNumber
where NumberID=@ID

end;