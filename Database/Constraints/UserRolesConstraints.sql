use Prison
go
alter table [UserRoles]
add constraint [PK_UserRoles] primary key (UserID,RoleID)
go
alter table [UserRoles]
with check add constraint [FK_User_UserRoles] 
foreign key (UserID) 
references [User](UserID)
on update cascade
on delete cascade

go

alter table[UserRoles]
with check add constraint [FK_Role_UserRoles] 
foreign key (RoleID) 
references [Role](RoleID)
on update cascade
on delete cascade
go 