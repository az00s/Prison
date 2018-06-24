use Prison
go
CREATE TABLE [dbo].[Detention](
	[DetentionID] [int] Primary Key identity(1,1) NOT NULL,
	[DetentionDate] [date] NOT NULL,
	[DetainedByWhomID] [int] NOT NULL,
	[DeliveryDate] [date] NOT NULL,
	[DeliveredByWhomID] [int] NOT NULL,
	[PlaceID] [int] NOT NULL,
) 

GO

