USE [Prison]
GO

create TRIGGER [dbo].[OnDetaineeDelete] 
   ON  [dbo].[DetentionsOfDetainees] 
   For DELETE
AS 
BEGIN
	
	if((select count(*) from DetentionsOfDetainees dof,deleted where dof.DetentionID=deleted.DetentionID)=0)
	begin
	delete from Detention
	where DetentionID in (select deleted.DetentionID from deleted)
	end;

END
