use Prison
go
create procedure UpdateNumber(@ID int,@Number nvarchar(50),@DetaineeID int) 
as

begin

update PhoneNumber
set Number=@Number, DetaineeID=@DetaineeID
where NumberID=@ID

end;