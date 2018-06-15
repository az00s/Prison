use Prison
go
create table [Role]
(
RoleID int not null primary key identity(1,1),
RoleName nvarchar(250) not null unique
)