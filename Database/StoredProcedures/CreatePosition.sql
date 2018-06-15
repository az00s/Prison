use Prison
go
create procedure CreatePosition (@PositionName nvarchar(150))
as

begin

insert into Position (PositionName) values (@PositionName)

end;