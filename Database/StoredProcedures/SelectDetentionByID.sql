use Prison
go
create procedure SelectDetentionByID(@ID int)
as
begin

SELECT  
dtn.DetentionID,
dtn.DetentionDate,
dtn.DetainedByWhomID,
dtn.DeliveryDate,
dtn.DeliveredByWhomID,
dtn.ReleasåDate,
dtn.ReleasedByWhomID,
dtn.PlaceID,
dtn.AmountForStaying,
dtn.PaidAmount
FROM  Detention dtn
where dtn.DetentionID=@ID

end;