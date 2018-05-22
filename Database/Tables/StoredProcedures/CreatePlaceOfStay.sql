use Prison
go
create procedure CreatePlaceOfStay(@Address as nvarchar(300))
as
begin

Insert into PlaceOfStay ([Address]) values(@Address)

end;