use Prison
go
create procedure DeletePlaceOfStay(@ID as int)
as
begin

Delete from PlaceOfStay 
where PlaceID=@ID

end;