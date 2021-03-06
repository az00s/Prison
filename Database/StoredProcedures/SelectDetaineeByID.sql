USE [Prison]
GO

create procedure [dbo].[SelectDetaineeByID](@ID as int)
as
begin

Select * from Detainee 
where DetaineeID=@ID

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

select Number from PhoneNumber
where DetaineeID=@ID
end;
