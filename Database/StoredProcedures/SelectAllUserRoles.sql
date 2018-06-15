use Prison
go
create procedure SelectAllUserRoles(@Login as nvarchar(35)) as
begin


select 
r.RoleName
from UserRoles ur
left join [Role] r
on ur.RoleID=r.RoleID
where ur.UserID=(select TOP 1 Employee.EmployeeID from Employee where Employee.LastName=@Login)
end;
