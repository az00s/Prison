use Prison
go
create procedure SelectAllDetaineeLastNames
as
begin

Select dtn.DetaineeID,dtn.LastName from Detainee dtn

end;