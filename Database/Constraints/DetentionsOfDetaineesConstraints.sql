use Prison
go
ALTER TABLE [dbo].[DetentionsOfDetainees]  
WITH CHECK ADD  CONSTRAINT [FK_DetentionsOfDetainee_Detainee] FOREIGN KEY([DetaineeID])
REFERENCES [dbo].[Detainee] ([DetaineeID])
on update cascade
on delete cascade

GO

ALTER TABLE [dbo].[DetentionsOfDetainees]  
WITH CHECK ADD  CONSTRAINT [FK_DetentionsOfDetainee_Detention] FOREIGN KEY([DetentionID])
REFERENCES [dbo].[Detention] ([DetentionID])

GO





