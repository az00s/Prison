use Prison
go
create procedure SelectStatusByID (@ID int) 
as

begin

select * from MaritalStatus
where StatusID=@ID

end;