use Prison
go

CREATE TYPE DetentionTable AS TABLE ( DetentionID INT,DetentionDate date,DetainedByWhomID int, DeliveryDate date,DeliveredByWhomID int,PlaceID int);