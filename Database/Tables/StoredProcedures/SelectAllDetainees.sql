use Prison
go
create procedure SelectAllDetainees
as
begin

SELECT * FROM Detainee

select 
dof.DetaineeID as 'DetaineeID',
det.DetentionID,
det.DetentionDate,
det.DetainedByWhomID,
det.DeliveryDate,
det.DeliveredByWhomID,
det.ReleasåDate,
det.ReleasedByWhomID,
det.PlaceID,
det.AmountForStaying,
det.PaidAmount
from DetentionsOfDetainees as dof
left join Detention as det on dof.DetentionID=det.DetentionID

end;