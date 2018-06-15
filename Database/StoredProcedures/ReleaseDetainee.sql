USE prison
go
create procedure ReleaseDetainee(@ID int,@ReleaseDate date,@ReleasedByWhomID int,@AmountForStaying decimal=0,@PaidAmount decimal=0) 
as
begin
update Detention
set ReleasåDate=@ReleaseDate,
ReleasedByWhomID=@ReleasedByWhomID,
AmountForStaying=@AmountForStaying,
PaidAmount=@PaidAmount
where DetentionID=@ID
end;