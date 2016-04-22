IF OBJECT_ID(N'[dbo].[[spCreatePhysicalTable]]') IS NOT NULL
	DROP PROCEDURE [dbo].[spCreatePhysicalTable]
GO

CREATE PROCEDURE [dbo].[spCreatePhysicalTable]
	 @tableName NVARCHAR(50)				/* Name of the table */
	,@isIdentity BIT = 1					/* Define if the PK is Identity */
AS
	IF OBJECT_ID('[' + @TableName + ']') IS NULL
	BEGIN
		IF @isIdentity = 1
		BEGIN
			EXEC('CREATE TABLE [dbo].[' + @tableName + '] ' +
				'(ID_' + @tableName + ' INT IDENTITY NOT NULL, ' +
				'CONSTRAINT [PK_' + @tableName + '] PRIMARY KEY CLUSTERED(' +
				'[ID_' + @tableName + ']) WITH FILLFACTOR = 90 ON [PRIMARY])')	
		END
		ELSE
		BEGIN
			EXEC('CREATE TABLE [dbo].[' + @tableName + '] ' +
				'(ID_' + @tableName + ' INT NOT NULL, ' +
				'CONSTRAINT [PK_' + @tableName + '] PRIMARY KEY CLUSTERED(' +
				'[ID_' + @tableName + ']) WITH FILLFACTOR = 90 ON [PRIMARY])')
		END
	END
	EXEC('ALTER INDEX ALL ON dbo.' + @tableName + ' REBUILD')
GO
--#endregion