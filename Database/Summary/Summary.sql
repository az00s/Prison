CREATE DATABASE Prison
go
ALTER DATABASE Prison 
SET AUTO_CLOSE OFF  
go 
Use Prison;
GO
CREATE LOGIN PrisonAdmin WITH PASSWORD = 'ABCD'
GO 

Use Prison;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'PrisonAdmin')
BEGIN
    CREATE USER [PrisonAdmin] FOR LOGIN [PrisonAdmin]
    EXEC sp_addrolemember N'db_owner', N'PrisonAdmin'
END;
GO

Use Prison;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'IIS APPPOOL\Prison')
BEGIN
    CREATE USER [IIS APPPOOL\Prison] FOR LOGIN [PrisonAdmin]
    EXEC sp_addrolemember N'db_owner', N'IIS APPPOOL\Prison'
END;
GO

Use Prison;
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'IIS APPPOOL\DefaultAppPool')
BEGIN
    CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [PrisonAdmin]
    EXEC sp_addrolemember N'db_owner', N'IIS APPPOOL\DefaultAppPool'
END;
GO

/*-----------------------------------------------------------------------------------------------------------*/

use Prison
go
create table Detainee
(
	[DetaineeID] [int] Primary Key clustered identity (1,1),
	[FirstName] [nvarchar](35) NOT NULL,
	[LastName] [nvarchar](35) NOT NULL,
	[MiddleName] [nvarchar](40) not NULL default '',
	[BirstDate] [date] NOT NULL,
	[MaritalStatusID] [int] NOT NULL,
	[WorkPlace] [nvarchar](max) NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[ResidenceAddress] [nvarchar](max) NOT NULL,
	[AdditionalData] [nvarchar](max) NULL

)
GO

use Prison
go
CREATE TABLE [dbo].[Detention](
	[DetentionID] [int] Primary Key identity(1,1) NOT NULL,
	[DetentionDate] [date] NOT NULL,
	[DetainedByWhomID] [int] NOT NULL,
	[DeliveryDate] [date] NOT NULL,
	[DeliveredByWhomID] [int] NOT NULL,
	[PlaceID] [int] NOT NULL
) 

GO

use Prison
go
CREATE TABLE [dbo].[PhoneNumber](
	[NumberID] [int] Primary Key clustered identity(1,1),
	[Number] [varchar](50) NOT NULL,
	[DetaineeID] [int] NOT NULL,
 
) 

GO

use Prison
go
create table [Role]
(
RoleID int not null primary key identity(1,1),
RoleName nvarchar(250) not null unique
)

use Prison
go
create table [User]
(
UserID int not null primary key,
Email nvarchar(250) not null unique,
[Password] nvarchar(max) not null
)

use Prison
go
create table [UserRoles]
(
UserID int not null,
RoleID int not null
)

use Prison
go
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] Primary Key clustered identity(1,1),
	[FirstName] [nvarchar](35) NOT NULL,
	[LastName] [nvarchar](35) NOT NULL,
	[MiddleName] [nvarchar](40) NULL,
	[PositionID] [int] NOT NULL
	
) 

GO

use Prison
go
CREATE TABLE [dbo].[DetentionsOfDetainees](
	[DetaineeID] [int]  NOT NULL,
	[DetentionID] [int]  NOT NULL,
 
) 

GO

use Prison
go
CREATE TABLE [dbo].[PlaceOfStay](
	[PlaceID] [int] Primary Key clustered identity(1,1),
	[Address] [nvarchar](300) NOT NULL,
 
) 

GO

use Prison
go
CREATE TABLE [dbo].[MaritalStatus](
	[StatusID] [int] Primary Key clustered identity(1,1),
	[StatusName] [nvarchar](50) NOT NULL,
 
) 

GO

use Prison
go
CREATE TABLE [dbo].[Position](
	[PositionID] [int] Primary Key clustered identity(1,1),
	[PositionName] [nvarchar](150) NOT NULL,
 
) 

GO

/*-----------------------------------------------------------------------------------------------------------*/
use Prison
go

CREATE TYPE DetentionTable AS TABLE ( DetentionID INT,DetentionDate date,DetainedByWhomID int, DeliveryDate date,DeliveredByWhomID int,PlaceID int);

USE [Prison]
GO

CREATE TYPE [dbo].[PhoneNumberTable] AS TABLE(
	[Number] [varchar](50) NULL
)
GO


use Prison
go

CREATE TYPE RoleIdTable AS TABLE ( RoleID INT );

/*-----------------------------------------------------------------------------------------------------------*/
use Prison
go
create procedure CreateDetainee (@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@BirstDate as date,@MaritalStatusID as int,@WorkPlace as nvarchar(MAX),@ImagePath as nvarchar(MAX),@ResidenceAddress as nvarchar(MAX),@AdditionalData as nvarchar(MAX),@DetentionTable DetentionTable readonly,@PhoneTable PhoneNumberTable readonly)
as
begin

declare 
@DetaineeID int,
@DetentionID int

Insert into Detainee 
([FirstName],[LastName],[MiddleName],[BirstDate],[MaritalStatusID],[WorkPlace],[ImagePath],[ResidenceAddress],[AdditionalData])
 values(@FirstName,@LastName,@MiddleName,@BirstDate,@MaritalStatusID,@WorkPlace,@ImagePath,@ResidenceAddress,@AdditionalData)

set @DetaineeID=SCOPE_IDENTITY()

insert into PhoneNumber (Number,DetaineeID)
select Number,@DetaineeID
from @PhoneTable

if((select TOP 1 DetentionID from @DetentionTable)=0)
begin

Insert into [Detention] 
([DetentionDate],[DetainedByWhomID],[DeliveryDate],[DeliveredByWhomID],[PlaceID]) 
select [DetentionDate],[DetainedByWhomID],[DeliveryDate],[DeliveredByWhomID],[PlaceID] 
from @DetentionTable

set @DetentionID=SCOPE_IDENTITY()

Insert into [DetentionsOfDetainees] (DetaineeID,DetentionID) 
values (@DetaineeID,@DetentionID)
end;

else
begin

Insert into [DetentionsOfDetainees] (DetaineeID,DetentionID) 
select @DetaineeID,DetentionID from @DetentionTable

end;

end;

go

USE [Prison]
GO

create procedure CreateEmployee(@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@PositionID as int) 
as
begin

Insert into Employee ([FirstName],[LastName],[MiddleName],[PositionID])
values(@FirstName,@LastName,@MiddleName,@PositionID)

end;
go

use Prison
go
create procedure CreateNumber(@Number nvarchar(50),@DetaineeID int) 
as

begin

insert into PhoneNumber(Number,DetaineeID)
values (@Number,@DetaineeID)

end;

go

use Prison
go
create procedure CreatePlaceOfStay(@Address as nvarchar(300))
as
begin

Insert into PlaceOfStay ([Address]) values(@Address)

end;

go

use Prison
go
create procedure CreatePosition (@PositionName nvarchar(150))
as

begin

insert into Position (PositionName) values (@PositionName)

end;
go
use Prison
go
create procedure [CreateRole](@RoleName as nvarchar(250))
as
begin

Insert into [Role] ([RoleName]) values(@RoleName)

end;
go
use Prison
go
create procedure CreateStatus (@StatusName nvarchar(50)) 
as

begin

insert into MaritalStatus(StatusName) values (@StatusName)

end;

go

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

go

use Prison
go
create procedure DeleteDetainee (@ID as int)
as
begin

Delete from Detainee where DetaineeID=@ID

end;
go
use Prison
go
create procedure DeleteEmployee (@ID as int)
as
begin

Delete from Employee where EmployeeID=@ID

end;
go
use Prison
go
create procedure DeleteNumber(@ID int) 
as

begin

delete from PhoneNumber
where NumberID=@ID

end;
go

use Prison
go
create procedure DeletePlaceOfStay(@ID as int)
as
begin

Delete from PlaceOfStay 
where PlaceID=@ID

end;
go
use Prison
go
create procedure DeletePosition (@ID int)
as

begin

delete Position 
where PositionID=@ID

end;
go
use Prison
go
create procedure DeleteRole (@ID as int)
as
begin

Delete from [Role] where RoleID=@ID

end;
go
use Prison
go
create procedure DeleteStatus (@ID int) 
as

begin

delete from MaritalStatus
where StatusID=@ID

end;
go
use Prison
go
create procedure DeleteUser (@ID as int)
as
begin

Delete from [User] where UserID=@ID

end;
go
USE prison
go
create procedure ReleaseDetainee(@ReleaseDate date,@ReleasedByWhomID int,@AmountForStaying decimal=0,@PaidAmount decimal=0,@DetentionID int,@DetaineeID int) 
as
begin

insert into Release (ReleasеDate,ReleasedByWhomID,AmountForStaying,PaidAmount,DetentionID,DetaineeID)
values (@ReleaseDate,@ReleasedByWhomID,@AmountForStaying,@PaidAmount,@DetentionID,@DetaineeID)

end;
go
use Prison
go
create procedure SelectAllDetaineeLastNames
as
begin

Select dtn.DetaineeID,dtn.LastName from Detainee dtn

end;
go

use Prison
go
create procedure SelectAllDetainees
as
begin

SELECT * FROM Detainee

select 
dof.DetaineeID as 'DetaineeID',
det.DetentionID,
det.DetentionDate,
det.DetainedByWhomID,
det.DeliveryDate,
det.DeliveredByWhomID,
det.PlaceID
from DetentionsOfDetainees as dof
left join Detention as det on dof.DetentionID=det.DetentionID

end;
go
use Prison
go
create procedure SelectAllDetentions 
as

begin

select * from Detention

end;
go
use Prison
go
create procedure SelectAllEmployees
as
begin
select * from Employee
end;
go
use Prison
go
create procedure SelectAllLogins
as
begin

select emp.LastName from Employee emp
join [User] usr on emp.EmployeeID=usr.UserID
 
end;
go
use Prison
go
create procedure SelectAllMaritalStatuses
as
begin

Select * from MaritalStatus

end;
go
use Prison
go
create procedure SelectAllNumbers 
as

begin

select * from PhoneNumber

end;
go
use Prison
go
create procedure SelectAllPlacesOfStay
as
begin

Select * from PlaceOfStay

end;
go
use Prison
go
create procedure SelectAllPositions
as
begin

Select * from Position

end;
go

use Prison
go
create procedure SelectAllRoles
as
begin

Select * from [Role]

end;
go
use Prison
go
create procedure SelectAllStatuses 
as

begin

select * from MaritalStatus

end;
go
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
go
use Prison
go
create procedure SelectAllUsers as
begin

select usr.UserID,emp.LastName 'UserName',usr.Email,usr.[Password] from [User] usr
join Employee emp on emp.EmployeeID=usr.UserID
end;
go
USE [Prison]
GO

create procedure [dbo].[SelectDetaineeByID](@ID as int)
as
begin

Select * from Detainee 
where DetaineeID=@ID

select 
Detention.[DetentionID],
[DetentionDate],
[DetainedByWhomID],
[DeliveryDate],
[DeliveredByWhomID],
[PlaceID]
from DetentionsOfDetainees 
inner join Detention 
on Detention.DetentionID = DetentionsOfDetainees.DetentionID 
where DetentionsOfDetainees.DetaineeID = @ID

select Number from PhoneNumber
where DetaineeID=@ID
end;
go
USE [Prison]
GO
create procedure [dbo].[SelectDetaineeByParams] (@FirstName nvarchar(35)='%',@LastName nvarchar(35)='%',@MiddleName nvarchar(40)='%',@DetentionDate nvarchar(40)='%',@Address nvarchar(max)='%')
as

begin

declare @StartDate date,
		@EndDate date

if @DetentionDate='%'
	begin
		set @StartDate='17000101';
		set @EndDate=getdate();
	end	
else 
	begin
		set @StartDate=@DetentionDate
		set @EndDate=@DetentionDate
	end

select distinct
dte.DetaineeID,
dte.FirstName,
dte.LastName,
dte.MiddleName,
dte.BirstDate,
dte.MaritalStatusID,
dte.ImagePath,
dte.WorkPlace,
dte.ResidenceAddress,
dte.AdditionalData
from Detainee dte
left join DetentionsOfDetainees dod
on dte.DetaineeID=dod.DetaineeID
join Detention dtn
on dod.DetentionID=dtn.DetentionID
where  
dte.FirstName like '%'+@FirstName+'%' AND 
dte.LastName like '%'+@LastName+'%' AND
dte.MiddleName like '%'+@MiddleName+'%' AND
dte.ResidenceAddress like '%'+@Address+'%' AND
dtn.DetentionDate between @StartDate and @EndDate

end;

go

USE [Prison]
GO

create procedure [dbo].[SelectDetaineesByDate](@Date as date)
as
begin

select distinct
det.DetaineeID,
det.FirstName,
det.LastName,
det.MiddleName,
det.BirstDate,
det.MaritalStatusID,
det.ImagePath,
det.WorkPlace,
det.ResidenceAddress,
det.AdditionalData 
from Detainee as det
inner join DetentionsOfDetainees as dof
on det.DetaineeID=dof.DetaineeID
where dof.DetentionID in (select dn.DetentionID from Detention as dn
where dn.DetentionDate=@Date)



end;


go
use Prison
go
create procedure SelectDetentionByID(@ID int)
as
begin

SELECT  
dtn.DetentionID,
dtn.DetentionDate,
dtn.DetainedByWhomID,
dtn.DeliveryDate,
dtn.DeliveredByWhomID,
dtn.PlaceID
FROM  Detention dtn
where dtn.DetentionID=@ID

end;
go

use Prison
go
create procedure SelectDetentionsByDetaineeID(@ID as int) as
begin
select 
Detention.[DetentionID],
[DetentionDate],
[DetainedByWhomID],
[DeliveryDate],
[DeliveredByWhomID],
[PlaceID]
from DetentionsOfDetainees 
inner join Detention 
on Detention.DetentionID = DetentionsOfDetainees.DetentionID 
where DetentionsOfDetainees.DetaineeID = @ID
end;
go
use Prison
go
create procedure SelectEmployeeByID(@ID as int)
as
begin

Select * from Employee 
where EmployeeID=@ID

end;
go

use Prison
go
create procedure SelectLastDetention(@ID int)
as
begin

SELECT TOP 1 
dtn.DetentionID,
dtn.DetentionDate,
dtn.DetainedByWhomID,
dtn.DeliveryDate,
dtn.DeliveredByWhomID,
dtn.PlaceID
FROM  Detention dtn
left join DetentionsOfDetainees dof 
on dtn.DetentionID=dof.DetentionID
where dof.DetaineeID=@ID
ORDER BY dtn.DetentionID DESC

end;
go
use Prison
go
create procedure SelectNumberByID(@ID int) 
as

begin

select * from PhoneNumber
where NumberID=@ID

end;
go

use Prison
go
create procedure SelectPasswordByLogin(@Login as nvarchar(35))as

begin
select top 1 usr.[Password] from Employee emp
join [User] usr on emp.EmployeeID=usr.UserID
where emp.LastName=@Login
end;
go

use Prison
go
create procedure SelectPlaceOfStayByID(@ID as int)
as
begin

Select * from PlaceOfStay 
where PlaceID=@ID

end;
go
use Prison
go
create procedure SelectPositionByID(@ID as int)
as
begin

Select * from Position 
where PositionID=@ID

end;
go

use Prison
go
create procedure SelectRoleByID(@ID int)
as
begin
select * from [Role] where RoleID=@ID
end;
go

use Prison
go
create procedure SelectStatusByID (@ID int) 
as

begin

select * from MaritalStatus
where StatusID=@ID

end;
go

use Prison
go
create procedure SelectUnoccupiedEmployeeNames
as
begin

select emp.EmployeeID,emp.LastName from Employee emp
left join [User] usr on emp.EmployeeID=usr.UserID
where usr.UserID is null
end;
go

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
go

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
go

USE [Prison]
GO


create procedure [dbo].[UpdateDetainee](@ID as int,@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@BirstDate as date,@MaritalStatusID as int,@WorkPlace as nvarchar(MAX),@ImagePath as nvarchar(MAX),@ResidenceAddress as nvarchar(MAX),@AdditionalData as nvarchar(MAX),@PhoneTable PhoneNumberTable readonly) as
begin

UPDATE Detainee     
SET 
[FirstName] = @FirstName,
[LastName] =@LastName,
[MiddleName] =@MiddleName,
[BirstDate] = @BirstDate,
[MaritalStatusID] = @MaritalStatusID,
[WorkPlace] =@WorkPlace,
[ImagePath] = @ImagePath,
[ResidenceAddress] = @ResidenceAddress,
[AdditionalData] = @AdditionalData 
WHERE DetaineeID =@ID

delete from PhoneNumber
where DetaineeID=@ID

insert into PhoneNumber (Number,DetaineeID)
select Number,@ID
from @PhoneTable

end;
go

USE [Prison]
GO

create procedure UpdateEmployee(@ID as int,@FirstName as nvarchar(35),@LastName as nvarchar(35),@MiddleName as nvarchar(40),@PositionID as int) 
as
begin

Update Employee 
set 

[FirstName]=@FirstName,
[LastName]=@LastName,
[MiddleName]=@MiddleName,
[PositionID]=@PositionID

where EmployeeID=@ID
end;
go
use Prison
go
create procedure UpdateNumber(@ID int,@Number nvarchar(50),@DetaineeID int) 
as

begin

update PhoneNumber
set Number=@Number, DetaineeID=@DetaineeID
where NumberID=@ID

end;
go
use Prison
go
create procedure UpdatePlaceOfStay(@ID as int,@Address as nvarchar(300))
as
begin

UPDATE PlaceOfStay 
SET [Address] = @Address 
WHERE PlaceID=@ID

end;
go
use Prison
go
create procedure UpdatePosition (@ID int,@PositionName nvarchar(150))
as

begin

update Position 
set PositionName=@PositionName
where PositionID=@ID

end;
go
USE [Prison]
GO

create procedure UpdateRole(@ID as int,@RoleName as nvarchar(250)) 
as
begin

Update [Role] 
set 

[RoleName]=@RoleName


where RoleID=@ID
end;
go
use Prison
go
create procedure UpdateStatus (@ID int,@StatusName nvarchar(50)) 
as

begin

update MaritalStatus
set StatusName=@StatusName
where StatusID=@ID

end;
go

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
/*-----------------------------------------------------------------------------------------------------------*/
USE [Prison]
GO

INSERT INTO [dbo].[Detainee]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[BirstDate]
           ,[MaritalStatusID]
           ,[WorkPlace]
           ,[ImagePath]
           ,[ResidenceAddress]
           ,[AdditionalData])
     VALUES
           ('Альберт','Эйнштейн','','21-04-1921',2,'Берлинский научный центр,Берлин, Рейн-штрассе 2','/Content/Images/ProfilePhotos/Einstein.jpg','Переулок Кельнский, 27',null),
		   ('Никола','Тесла','','18-03-1896',1,'Университет Беркли, Нью-Йорк, Колючая,33','/Content/Images/ProfilePhotos/Tesla.jpg','ул.Болгарская,29','выдающийся ученый'),
		   ('Федерико','Фелини','','01-04-1888',1,'Миланский центр кинематографа,Милан, ул.Да-Винчи,10','/Content/Images/ProfilePhotos/Felini.jpg','Рим,ул.Колизейская 22','режиссер'),
		   ('Томас','Эдисон','','21-02-1844',1,'Компания "Эдисон",Вашингтон, ул.Главная,4','/Content/Images/ProfilePhotos/Edison.jpg','Нью-Йорк, ул.Манхеттанская,25','ученый'),
		   ('Эдгар','Кодд','','06-09-1921',1,'Лондонский научный центр, Лондон, проспект Шотландский, 7','/Content/Images/ProfilePhotos/Codd.jpg','Манчестер, ул.Красная, 22','придумал реляционные бд'),
		   ('Тим','Бернес-Ли','','27-05-1955',1,'Гарвардский университет,Бруклин, ул.Ученых,66','/Content/Images/ProfilePhotos/Li.jpg','Бруклин, переулок Знатный,10','интернет'),
		   ('Илон','Маск','','03-08-1980',1,'Спасе-Икс, Сан-Паоло, ул.Космическая,22','/Content/Images/ProfilePhotos/Musk.jpg','Сан-Паоло,ул.Изобретателей,9','хороший парень')

GO


USE [Prison]
GO

INSERT INTO [dbo].[Detention]
           ([DetentionDate]
           ,[DetainedByWhomID]
           ,[DeliveryDate]
           ,[DeliveredByWhomID]
           ,[PlaceID])
     VALUES
           ('21-04-2018',5,'21-04-2018',6,1),
		   ('01-05-2018',6,'01-05-2018',7,1),
		   ('02-05-2018',7,'02-05-2018',6,1),
		   ('02-02-2018',5,'02-05-2018',6,2),
		   ('03-05-2018',5,'04-05-2018',6,2),
		   ('03-05-2018',4,'03-05-2018',4,3),
		   ('03-05-2018',4,'04-05-2018',4,4),
		   ('04-05-2018',4,'04-05-2018',4,4),
		   ('04-05-2018',4,'04-05-2018',4,3),
		   ('05-05-2018',5,'06-05-2018',7,3)
		   
GO


USE [Prison]
GO

INSERT INTO [dbo].[DetentionsOfDetainees]
           ([DetaineeID]
           ,[DetentionID])
     VALUES
           (1,1),
		   (2,1),
		   (1,2),
		   (2,3),
		   (3,4),
		   (4,5),
		   (5,6),
		   (5,7),
		   (5,8),
		   (6,9),
		   (7,10)
GO

USE [Prison]
GO

INSERT INTO [dbo].[Employee]
           ([FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[PositionID])
           
     VALUES
           ('Эркюль','Пуаро','Васильевич',1),
		   ('Шерлок','Холмс','Игнатьевич',2),
		   ('Валерий','Коломбо','Анатольевич',2),
		   ('Ниро','Вульф','Егорович',3),
		   ('Джони','Дэп','Грыгоравич',4),
		   ('Владимир','Забывающий','Николаевич',4),
		   ('Игорь','Кацапов','Васильевич',4)
GO


USE [Prison]
GO

INSERT 
INTO [dbo].[MaritalStatus]([StatusName])
VALUES ('Холост'),('Женат'),('Разведен'),('Вдовец')
GO


USE [Prison]
GO

INSERT INTO [dbo].[PhoneNumber]
           ([Number]
           ,[DetaineeID])
     VALUES
           ('+382 33 3356473',1),
		   ('+382 29 3358873',1),
		   ('+378 22 3777473',2),
		   ('+382 33 3345678',3),
		   ('+375 11 3356473',4),
		   ('+382 33 5566778',4),
		   ('+382 22 2211334',5),
		   ('+382 33 0002223',6),
		   ('+382 12 0000000',6),
		   ('+375 55 1122334',7),
		   ('+382 22 1654321',7)

GO


USE [Prison]
GO

INSERT INTO [dbo].[PlaceOfStay]([Address])
     VALUES
           ('212011, г. Могилев, ул. Крупской, 99б'),
		   ('213810, г. Бобруйск, ул. Советская, 7а'),
		   ('213320, г. Быхов, ул. Авиационная, 11'),
		   ('213160, г.п. Белыничи, ул. Мичурина, 13'),
		   ('213410, г. Горки, ул. Якубовского, 21')

GO


USE [Prison]
GO

INSERT 
INTO [dbo].[Position]([PositionName])
VALUES('Начальник'),('Дежурный'),('Водитель'),('Конвоир') 
GO


USE [Prison]
GO

INSERT 
INTO [dbo].[Role]([RoleName])
VALUES('user'),('editor'),('admin')
GO


USE [Prison]
GO

INSERT INTO [dbo].[User]
           ([UserID]
           ,[Email]
           ,[Password])
           
     VALUES
           (1,'shun@mail.com','пароль'),
           (2,'bad@mail.com','пароль'),
           (3,'sitdown@mail.com','пароль'),
           (4,'driver@mail.com','пароль'),
           (5,'shunya@mail.com','пароль'),
           (6,'stupid@mail.com','пароль')
GO

USE [Prison]
GO

INSERT INTO [dbo].[UserRoles]
           ([UserID]
           ,[RoleID])
     VALUES
           (1,3),
		   (2,2),
		   (3,2),
		   (4,1),
		   (5,1),
		   (6,1)
		   
GO

/*-----------------------------------------------------------------------------------------------------------*/

use Prison
go
ALTER TABLE [dbo].[Detainee]  
WITH CHECK ADD  CONSTRAINT [FK_Detainee_MaritalStatus] FOREIGN KEY([MaritalStatusID])
REFERENCES [dbo].[MaritalStatus] ([StatusID])
GO

use Prison
go
ALTER TABLE [dbo].[Detention]  
WITH CHECK ADD  CONSTRAINT [FK_Detention_Employee1] FOREIGN KEY([DetainedByWhomID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO


ALTER TABLE [dbo].[Detention]  
WITH CHECK ADD  CONSTRAINT [FK_Detention_PlaceOfDetention] FOREIGN KEY([PlaceID])
REFERENCES [dbo].[PlaceOfStay] ([PlaceID])
GO




use Prison
go
ALTER TABLE [dbo].[DetentionsOfDetainees]  
WITH CHECK ADD  CONSTRAINT [FK_DetentionsOfDetainee_Detainee] FOREIGN KEY([DetaineeID])
REFERENCES [dbo].[Detainee] ([DetaineeID])
on update cascade
on delete cascade

GO

ALTER TABLE [dbo].[DetentionsOfDetainees]  
WITH CHECK ADD  CONSTRAINT [FK_DetentionsOfDetainee_Detention] FOREIGN KEY([DetentionID])
REFERENCES [dbo].[Detention] ([DetentionID])

GO





use Prison
go
ALTER TABLE [dbo].[DetentionsOfDetainees]  
ADD  CONSTRAINT [PK_DetentionsOfDetainees] Primary KEY(DetaineeID,DetentionID)
GO

use Prison
go
ALTER TABLE [dbo].[Employee]  
WITH CHECK ADD  CONSTRAINT [FK_Employee_Position] FOREIGN KEY([PositionID])
REFERENCES [dbo].[Position] ([PositionID])
GO






use Prison
go
ALTER TABLE [dbo].[PhoneNumber]  
WITH CHECK ADD  CONSTRAINT [FK_PhoneNumber_Detainee] FOREIGN KEY([DetaineeID])
REFERENCES [dbo].[Detainee] ([DetaineeID])
on update cascade
on delete cascade
GO


use Prison
go
alter table [User]
add constraint [FK_Employee_User] foreign key (UserID)
references Employee(EmployeeID)
go

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













