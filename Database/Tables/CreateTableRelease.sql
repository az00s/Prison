use Prison
go 
create table Release 
(
ReleaseID int Primary Key identity (1,1),
ReleasåDate datetime not null,
ReleasedByWhomID int not null,
AmountForStaying decimal(18,0) not null,
PaidAmount decimal(18,0) not null,
DetaineeID int not null,
DetentionID int not null
)