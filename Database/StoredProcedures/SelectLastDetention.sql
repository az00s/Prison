use Prison
go
create procedure SelectLastDetention(@ID int)
as
begin

SELECT TOP 1 
dtn.DetentionID,
dtn.DetentionDate,
dtn.DetainedByWhomID,
dtn.DeliveryDate,
dtn.DeliveredByWhomID,
dtn.PlaceID
FROM  Detention dtn
left join DetentionsOfDetainees dof 
on dtn.DetentionID=dof.DetentionID
where dof.DetaineeID=@ID
ORDER BY dtn.DetentionID DESC

end;