use Prison
go
create table [User]
(
UserID int not null primary key,
Email nvarchar(250) not null unique,
[Password] nvarchar(max) not null
)