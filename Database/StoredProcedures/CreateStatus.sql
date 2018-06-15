use Prison
go
create procedure CreateStatus (@StatusName nvarchar(50)) 
as

begin

insert into MaritalStatus(StatusName) values (@StatusName)

end;