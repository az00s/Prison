use Prison
go
create TRIGGER OnDetaineeDelete 
   ON  DetentionsOfDetainees 
   For DELETE
AS 
BEGIN
	
	if((select count(*) from DetentionsOfDetainees dof,deleted where dof.DetentionID=deleted.DetentionID)=0)
	begin
	delete from Detention
	where DetentionID=(select deleted.DetentionID from deleted)
	end;

END
GO
