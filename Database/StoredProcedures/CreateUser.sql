use Prison
go
create procedure [CreateUser](@ID as int,@Email as nvarchar(250),@Password as nvarchar(MAX),@Table RoleIdTable readonly)
as
begin

Insert into [User] (UserID,Email,[Password]) values(@ID,@Email,@Password)

Insert into [UserRoles] (UserID,RoleID) 
select @ID,RoleID
from @Table

end;