USE [VISIONMUNDIAL]
GO
/****** Object:  StoredProcedure [dbo].[spGetRoleInformation]    Script Date: 8/22/2016 11:55:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetRoleInformation]
	 @roleIdentity INT			/* role ID */				
AS

SELECT RL.Rol,RL.Descripcion,REC.Pagina,REC.Recurso,ROLREC.Agregar,ROLREC.Modificar,ROLREC.Eliminar FROM Rol RL
INNER JOIN RolRecurso ROLREC ON
RL.ID_Rol= ROLREC.ID_Rol
INNER JOIN Recurso REC ON
ROLREC.ID_Recurso = REC.ID_Recurso
WHERE RL.ID_Rol = @roleIdentity
	
