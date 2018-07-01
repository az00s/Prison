use Prison
go
create procedure CreateNumber(@Number nvarchar(50),@DetaineeID int) 
as

begin

insert into PhoneNumber(Number,DetaineeID)
values (@Number,@DetaineeID)

end;