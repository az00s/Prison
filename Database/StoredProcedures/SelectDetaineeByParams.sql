USE [Prison]
GO
create procedure [dbo].[SelectDetaineeByParams] (@FirstName nvarchar(35)='%',@LastName nvarchar(35)='%',@MiddleName nvarchar(40)='%',@DetentionDate nvarchar(40)='%',@Address nvarchar(max)='%')
as

begin

declare @StartDate date,
		@EndDate date

if @DetentionDate='%'
	begin
		set @StartDate='17000101';
		set @EndDate=getdate();
	end	
else 
	begin
		set @StartDate=@DetentionDate
		set @EndDate=@DetentionDate
	end

select distinct
dte.DetaineeID,
dte.FirstName,
dte.LastName,
dte.MiddleName,
dte.BirstDate,
dte.MaritalStatusID,
dte.ImagePath,
dte.WorkPlace,
dte.ResidenceAddress,
dte.AdditionalData
from Detainee dte
left join DetentionsOfDetainees dod
on dte.DetaineeID=dod.DetaineeID
join Detention dtn
on dod.DetentionID=dtn.DetentionID
where  
dte.FirstName like '%'+@FirstName+'%' AND 
dte.LastName like '%'+@LastName+'%' AND
dte.MiddleName like '%'+@MiddleName+'%' AND
dte.ResidenceAddress like '%'+@Address+'%' AND
(SELECT CAST(dtn.DetentionDate AS DATE)) between @StartDate and @EndDate

end;

