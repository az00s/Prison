use Prison
go
create procedure SelectNumberByID(@ID int) 
as

begin

select * from PhoneNumber
where NumberID=@ID

end;