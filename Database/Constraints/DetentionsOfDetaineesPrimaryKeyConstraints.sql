use Prison
go
ALTER TABLE [dbo].[DetentionsOfDetainees]  
ADD  CONSTRAINT [PK_DetentionsOfDetainees] Primary KEY(DetaineeID,DetentionID)
GO




