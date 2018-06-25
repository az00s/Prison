use Prison
go
ALTER TABLE [dbo].Release  
WITH CHECK ADD  CONSTRAINT [FK_Release_Detainee] FOREIGN KEY([DetaineeID])
REFERENCES [dbo].[Detainee] ([DetaineeID])
on update cascade
on delete cascade

GO

ALTER TABLE [dbo].Release  
WITH CHECK ADD  CONSTRAINT [FK_Release_Detention] FOREIGN KEY([DetentionID])
REFERENCES [dbo].[Detention] ([DetentionID])
on update cascade
on delete cascade

GO

ALTER TABLE [dbo].Release  
WITH CHECK ADD  CONSTRAINT [FK_Release_Employee] FOREIGN KEY([ReleasedByWhomID])
REFERENCES [dbo].[Employee] ([EmployeeID])


GO




