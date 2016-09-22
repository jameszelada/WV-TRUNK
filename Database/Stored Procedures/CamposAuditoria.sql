


DECLARE @Table_Name NVARCHAR(100)

DECLARE MY_CURSOR CURSOR 
  LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR 
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' 
AND TABLE_CATALOG='VISIONMUNDIAL' AND TABLE_NAME NOT IN ('sysdiagrams')

OPEN MY_CURSOR
FETCH NEXT FROM MY_CURSOR INTO @Table_Name
WHILE @@FETCH_STATUS = 0
BEGIN 
    
	DECLARE @query NVARCHAR(MAX) 
	set @query = N'ALTER TABLE ' + @Table_Name +
				 N' ADD CreadoPor NVARCHAR(30) NULL;' +
				 N' ALTER TABLE ' + @Table_Name +
				 N' ADD CONSTRAINT DF_'+@Table_Name+'_CreadoPor  DEFAULT ('''') FOR CreadoPor;'+
				 N' ALTER TABLE ' + @Table_Name +
				 N' ADD FechaCreacion DATETIME NULL;' +
				 N' ALTER TABLE ' + @Table_Name +
				 N' ADD CONSTRAINT DF_'+@Table_Name+'_FechaCreacion  DEFAULT (''1900-01-01 00:00:00'') FOR FechaCreacion;'+
				 N' ALTER TABLE ' + @Table_Name +
				 N' ADD ModificadoPor NVARCHAR(30) NULL;' +
				 N' ALTER TABLE ' + @Table_Name +
				 N' ADD CONSTRAINT DF_'+@Table_Name+'_ModificadoPor  DEFAULT ('''') FOR ModificadoPor;'+
				 N' ALTER TABLE ' + @Table_Name +  
				 N' ADD FechaModificacion DATETIME NULL;' +
				 N' ALTER TABLE ' + @Table_Name + 
				 N' ADD CONSTRAINT DF_'+@Table_Name+'_FechaModificacion  DEFAULT (''1900-01-01 00:00:00'') FOR FechaModificacion;'

	--print @query;
	EXEC sp_executesql @query

	 
    FETCH NEXT FROM MY_CURSOR INTO @Table_Name
END
CLOSE MY_CURSOR
DEALLOCATE MY_CURSOR