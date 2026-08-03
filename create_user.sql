USE [master]
GO
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'indianschool_user')
BEGIN
    CREATE LOGIN [indianschool_user] WITH PASSWORD=N'Indianschool123!', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
END
GO
USE [indianschooloman]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'indianschool_user')
BEGIN
    CREATE USER [indianschool_user] FOR LOGIN [indianschool_user]
END
GO
ALTER ROLE [db_owner] ADD MEMBER [indianschool_user]
GO
