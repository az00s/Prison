USE [Prison]
GO

create procedure [dbo].[SelectDetaineesByDate](@Date as date)
as
begin

select distinct
det.DetaineeID,
det.FirstName,
det.LastName,
det.MiddleName,
det.BirstDate,
det.MaritalStatusID,
det.ImagePath,
det.WorkPlace,
det.ResidenceAddress,
det.AdditionalData 
from Detainee as det
inner join DetentionsOfDetainees as dof
on det.DetaineeID=dof.DetaineeID
where dof.DetentionID in (select dn.DetentionID from Detention as dn
where (SELECT CAST(dn.DetentionDate AS DATE))=@Date)



end;


