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
