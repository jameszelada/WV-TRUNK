USE [VISIONMUNDIAL]
GO
/****** Object:  StoredProcedure [dbo].[spGetAsignation]    Script Date: 8/23/2016 6:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetAsignation]
	 @projectIdentity INT			/* Project ID */				
AS

SELECT PROY.Codigo,PROY.ProyectoDescripcion, 
CASE WHEN PROY.Estado = 'A' THEN 'ACTIVO' 
	 WHEN PROY.Estado = 'i' THEN 'INACTIVO'
	 WHEN PROY.Estado = 'S' THEN 'SUSPENDIDO'
	 END AS Estado,
PER.Nombre +' '+PER.Apellido AS NombreCompleto,PER.Dui,PER.Email,
PUE.Puesto,TIP.TipoPuesto FROM AsignacionRecursoHumano AS ASIG
INNER JOIN Persona AS PER
ON PER.ID_Persona= ASIG.ID_Persona
INNER JOIN Proyecto AS PROY
ON PROY.ID_Proyecto = ASIG.ID_Proyecto
INNER JOIN Puesto AS PUE
ON PUE.ID_Puesto = ASIG.ID_Puesto
INNER JOIN TipoPuesto AS TIP
ON TIP.ID_TipoPuesto = PUE.ID_TipoPuesto
WHERE ASIG.ID_Proyecto = @projectIdentity
	
