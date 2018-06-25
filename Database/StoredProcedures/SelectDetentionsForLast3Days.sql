use Prison
go
create procedure SelectDetentionsForLast3Days
as

begin

select * from Detention dtn
where dtn.DetentionDate> (getdate()-3)

end;