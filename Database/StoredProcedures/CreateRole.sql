use Prison
go
create procedure [CreateRole](@RoleName as nvarchar(250))
as
begin

Insert into [Role] ([RoleName]) values(@RoleName)

end;