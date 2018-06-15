use Prison
go
create procedure SelectUserByLogin(@Login as nvarchar(250))
as

begin
declare @UserID as int;
set @UserID=(select Top 1 emp.EmployeeID from Employee emp where emp.LastName=@Login)

select usr.UserID,emp.LastName 'UserName', usr.Email,usr.[Password] from Employee emp
join [User] usr on emp.EmployeeID=usr.UserID
where emp.EmployeeID=@UserID 



select r.RoleID,r.RoleName from [Role] r 
join UserRoles ur 
on  r.RoleID=ur.RoleID
where ur.UserID=@UserID
end;