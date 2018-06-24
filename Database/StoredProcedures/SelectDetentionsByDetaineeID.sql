use Prison
go
create procedure SelectDetentionsByDetaineeID(@ID as int) as
begin
select 
Detention.[DetentionID],
[DetentionDate],
[DetainedByWhomID],
[DeliveryDate],
[DeliveredByWhomID],
[PlaceID]
from DetentionsOfDetainees 
inner join Detention 
on Detention.DetentionID = DetentionsOfDetainees.DetentionID 
where DetentionsOfDetainees.DetaineeID = @ID
end;