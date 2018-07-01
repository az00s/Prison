use Prison
go
create procedure CreateDetainee (@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@BirstDate as date,@MaritalStatusID as int,@WorkPlace as nvarchar(MAX),@ImagePath as nvarchar(MAX),@ResidenceAddress as nvarchar(MAX),@AdditionalData as nvarchar(MAX),@DetentionTable DetentionTable readonly,@PhoneTable PhoneNumberTable readonly)
as
begin

declare 
@DetaineeID int,
@DetentionID int

Insert into Detainee 
([FirstName],[LastName],[MiddleName],[BirstDate],[MaritalStatusID],[WorkPlace],[ImagePath],[ResidenceAddress],[AdditionalData])
 values(@FirstName,@LastName,@MiddleName,@BirstDate,@MaritalStatusID,@WorkPlace,@ImagePath,@ResidenceAddress,@AdditionalData)

set @DetaineeID=SCOPE_IDENTITY()

insert into PhoneNumber (Number,DetaineeID)
select Number,@DetaineeID
from @PhoneTable

if((select TOP 1 DetentionID from @DetentionTable)=0)
begin

Insert into [Detention] 
([DetentionDate],[DetainedByWhomID],[DeliveryDate],[DeliveredByWhomID],[PlaceID]) 
select [DetentionDate],[DetainedByWhomID],[DeliveryDate],[DeliveredByWhomID],[PlaceID] 
from @DetentionTable

set @DetentionID=SCOPE_IDENTITY()

Insert into [DetentionsOfDetainees] (DetaineeID,DetentionID) 
values (@DetaineeID,@DetentionID)
end;

else
begin

Insert into [DetentionsOfDetainees] (DetaineeID,DetentionID) 
select @DetaineeID,DetentionID from @DetentionTable

end;

end;