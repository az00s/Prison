use Prison
go
CREATE TABLE [dbo].[PhoneNumber](
	[NumberID] [int] Primary Key clustered identity(1,1),
	[Number] [varchar](50) NOT NULL,
	[DetaineeID] [int] NOT NULL,
 
) 

GO

