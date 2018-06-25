use Prison
go

create procedure SelectRelease(@DetaineeID int, @DetentionID int) 
as

begin

select top 1 * from Release 
where DetaineeID=@DetaineeID and DetentionID=@DetentionID
end;