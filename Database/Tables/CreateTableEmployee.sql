use Prison
go
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] Primary Key clustered identity(1,1),
	[FirstName] [nvarchar](35) NOT NULL,
	[LastName] [nvarchar](35) NOT NULL,
	[MiddleName] [nvarchar](40) NULL,
	[PositionID] [int] NOT NULL
	
) 

GO

