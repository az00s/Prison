USE [Prison]
GO

create procedure UpdateUser(@ID as int,@Email as nvarchar(250),@Password as nvarchar(max),@Table as RoleIdTable readonly) 
as
begin

Update [User] 
set 
[Email]=@Email,
[Password]=@Password
where UserID=@ID

delete from UserRoles 
where UserID=@ID

Insert into [UserRoles] (UserID,RoleID) 
select @ID,RoleID
from @Table

end;
go
