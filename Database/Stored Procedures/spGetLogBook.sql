USE [VISIONMUNDIAL]
GO
/****** Object:  StoredProcedure [dbo].[spGetLogBook]    Script Date: 8/23/2016 8:48:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetLogBook]
	 @personIdentity INT,			/* person ID */	
	 @logbookIdentity INT			/* logbook ID */				
AS

SELECT PER.Nombre +' '+PER.Apellido AS NombreCompleto,PER.Dui,PER.Email,PER.Telefono,
BITA.FechaBitacora,BITDETA.Actividad,BITDETA.Observaciones FROM Persona AS PER
INNER JOIN Bitacora AS BITA
ON PER.ID_Persona = BITA.ID_Persona
INNER JOIN BitacoraDetalle AS BITDETA
ON BITA.ID_Bitacora = BITDETA.ID_Bitacora
WHERE PER.ID_Persona = @personIdentity AND BITA.ID_Bitacora=@logbookIdentity