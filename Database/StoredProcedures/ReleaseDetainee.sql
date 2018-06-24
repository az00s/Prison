USE prison
go
create procedure ReleaseDetainee(@ReleaseDate date,@ReleasedByWhomID int,@AmountForStaying decimal=0,@PaidAmount decimal=0,@DetentionID int,@DetaineeID int) 
as
begin

insert into Release (ReleasåDate,ReleasedByWhomID,AmountForStaying,PaidAmount,DetentionID,DetaineeID)
values (@ReleaseDate,@ReleasedByWhomID,@AmountForStaying,@PaidAmount,@DetentionID,@DetaineeID)

end;