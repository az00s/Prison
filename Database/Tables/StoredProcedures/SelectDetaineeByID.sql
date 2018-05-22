use Prison
go
create procedure SelectDetaineeByID(@ID as int)
as
begin

Select * from Detainee 
where DetaineeID=@ID

end;
go