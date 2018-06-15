use Prison
go
create procedure UpdateStatus (@ID int,@StatusName nvarchar(50)) 
as

begin

update MaritalStatus
set StatusName=@StatusName
where StatusID=@ID

end;