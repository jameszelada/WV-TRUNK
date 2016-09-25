USE [VISIONMUNDIAL]
GO
/****** Object:  StoredProcedure [dbo].[spGetUserInformation]    Script Date: 8/22/2016 9:44:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].spGetUserInformation
	 @userIdentity INT			/* User ID */				
AS

SELECT US.NombreUsuario,US.Nombre,US.Apellido,US.Email,RL.Rol,REC.Pagina,REC.Recurso ,ROLREC.Agregar,ROLREC.Modificar,ROLREC.Eliminar FROM USUARIO US
INNER JOIN UsuarioRol USROL ON
US.ID_Usuario= USROL.ID_Usuario
INNER JOIN Rol RL ON
USROL.ID_Rol= RL.ID_Rol
INNER JOIN RolRecurso ROLREC ON
RL.ID_Rol= ROLREC.ID_Rol
INNER JOIN Recurso REC ON
ROLREC.ID_Recurso = REC.ID_Recurso
WHERE US.ID_Usuario = @userIdentity
	
