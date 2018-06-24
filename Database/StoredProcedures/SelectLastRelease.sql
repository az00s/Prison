use Prison
go
create procedure SelectLastRelease(@ID int)
as
begin

SELECT TOP 1 *
FROM  Release 
where DetaineeID=@ID
ORDER BY ReleaseID DESC

end;