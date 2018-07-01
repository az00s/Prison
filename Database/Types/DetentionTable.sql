use Prison
go

CREATE TYPE DetentionTable AS TABLE ( DetentionID INT,DetentionDate datetime,DetainedByWhomID int, DeliveryDate date,DeliveredByWhomID int,PlaceID int);