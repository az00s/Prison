use Prison
go
CREATE TABLE [dbo].[Detention](
	[DetentionID] [int] Primary Key clustered identity(1,1) NOT NULL,
	[DetentionDate] [date] NOT NULL,
	[DetainedByWhomID] [int] NOT NULL,
	[DeliveryDate] [date] NOT NULL,
	[DeliveredByWhomID] [int] NOT NULL,
	[ReleasåDate] [date] NULL,
	[ReleasedByWhomID] [int] NULL,
	[PlaceID] [int] NOT NULL,
	[AmountForStaying] [decimal](18, 0) NULL,
	[PaidAmount] [decimal](18, 0) NULL,
) 

GO

