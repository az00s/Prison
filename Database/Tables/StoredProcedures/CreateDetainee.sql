use Prison
go
create procedure CreateDetainee (@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@BirstDate as date,@MaritalStatusID as int,@WorkPlace as nvarchar(MAX),@ImagePath as nvarchar(MAX),@ResidenceAddress as nvarchar(MAX),@AdditionalData as nvarchar(MAX))
as
begin

Insert into Detainee 
([FirstName],[LastName],[MiddleName],[BirstDate],[MaritalStatusID],[WorkPlace],[ImagePath],[ResidenceAddress],[AdditionalData])
 values(@FirstName,@LastName,@MiddleName,@BirstDate,@MaritalStatusID,@WorkPlace,@ImagePath,@ResidenceAddress,@AdditionalData)

end;