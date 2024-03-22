IF NOT EXISTS 
	(SELECT name  
		FROM master.sys.sql_logins
		WHERE name = 'testuser')
BEGIN
	CREATE LOGIN [testuser] WITH PASSWORD = N'rglna{adQP123456';
END  
-- check our db
IF NOT EXISTS
    (SELECT name
     FROM sys.database_principals
     WHERE name = 'testuser')
BEGIN
    CREATE USER [testuser] FOR LOGIN [testuser] WITH DEFAULT_SCHEMA = dbo
END
exec sp_addrolemember db_datareader, 'testuser'
exec sp_addrolemember db_datawriter, 'testuser'
