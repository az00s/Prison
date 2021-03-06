USE [Prison]
GO


create procedure [dbo].[UpdateDetainee](@ID as int,@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@BirstDate as date,@MaritalStatusID as int,@WorkPlace as nvarchar(MAX),@ImagePath as nvarchar(MAX),@ResidenceAddress as nvarchar(MAX),@AdditionalData as nvarchar(MAX),@PhoneTable PhoneNumberTable readonly) as
begin

UPDATE Detainee     
SET 
[FirstName] = @FirstName,
[LastName] =@LastName,
[MiddleName] =@MiddleName,
[BirstDate] = @BirstDate,
[MaritalStatusID] = @MaritalStatusID,
[WorkPlace] =@WorkPlace,
[ImagePath] = @ImagePath,
[ResidenceAddress] = @ResidenceAddress,
[AdditionalData] = @AdditionalData 
WHERE DetaineeID =@ID

delete from PhoneNumber
where DetaineeID=@ID

insert into PhoneNumber (Number,DetaineeID)
select Number,@ID
from @PhoneTable

end;
