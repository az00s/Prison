use Prison
go
create procedure SelectPlaceOfStayByID(@ID as int)
as
begin

Select * from PlaceOfStay 
where PlaceID=@ID

end;