use Prison
go
create procedure UpdatePosition (@ID int,@PositionName nvarchar(150))
as

begin

update Position 
set PositionName=@PositionName
where PositionID=@ID

end;