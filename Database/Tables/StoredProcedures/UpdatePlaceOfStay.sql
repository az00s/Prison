use Prison
go
create procedure UpdatePlaceOfStay(@ID as int,@Address as nvarchar(300))
as
begin

UPDATE PlaceOfStay 
SET [Address] = @Address 
WHERE PlaceID=@ID

end;