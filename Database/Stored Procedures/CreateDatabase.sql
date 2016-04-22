DECLARE @DBName varchar(50)='VISIONMUNDIAL' 
DECLARE @Path NVARCHAR(MAX) ='E:\Database' 
/* If Exists Drop Sys DB */
EXEC('IF EXISTS (SELECT Sys.Databases.name
	FROM Sys.Databases
	WHERE Sys.Databases.name = ''' + @DBName + ''')
BEGIN
	EXEC spDisconnectUsers ''' + @DBName + '''

	DROP DATABASE ' + @DBName + '
END') 

/* Create DB */
EXEC('CREATE DATABASE ' + @DBName + '
ON 
( NAME = ' + @DBName + '_dat,
   FILENAME = ''' + @Path + '\' + @DBName + '.mdf'')
LOG ON
( NAME = ' + @DBName + '_log,
   FILENAME = ''' + @Path + '\' + @DBName + '.ldf'')
	COLLATE SQL_Latin1_General_CP1_CI_AS')
	
GO

USE VISIONMUNDIAL

GO
