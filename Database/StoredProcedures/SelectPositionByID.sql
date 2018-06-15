use Prison
go
create procedure SelectPositionByID(@ID as int)
as
begin

Select * from Position 
where PositionID=@ID

end;