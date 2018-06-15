use Prison
go
alter table [User]
add constraint [FK_Employee_User] foreign key (UserID)
references Employee(EmployeeID)
go