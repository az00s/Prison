use Prison
go
create procedure DeletePosition (@ID int)
as

begin

delete Position 
where PositionID=@ID

end;