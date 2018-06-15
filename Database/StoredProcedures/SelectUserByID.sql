use Prison
go
create procedure SelectUserByID(@ID as int) as
begin

select usr.UserID,emp.LastName 'UserName',usr.Email,usr.[Password] from [User] usr
join Employee emp on emp.EmployeeID=usr.UserID
where usr.UserID=@ID

select r.RoleID,r.RoleName from [Role] r 
join UserRoles ur 
on  r.RoleID=ur.RoleID
where ur.UserID=@ID
end;