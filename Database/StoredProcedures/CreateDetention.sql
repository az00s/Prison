use Prison
go
create procedure CreateDetention (@DetaineeID int,@DetentionDate datetime,@DetainedByWhomID int,@DeliveryDate date,@DeliveredByWhomID int,@PlaceID int)as

begin

Insert into [Detention] 
([DetentionDate],[DetainedByWhomID],[DeliveryDate],[DeliveredByWhomID],[PlaceID]) 
values (@DetentionDate,@DetainedByWhomID,@DeliveryDate,@DeliveredByWhomID,@PlaceID) 

insert into DetentionsOfDetainees (DetentionID,DetaineeID)
values (SCOPE_IDENTITY(),@DetaineeID)
end;