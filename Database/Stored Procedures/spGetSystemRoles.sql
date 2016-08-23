USE [VISIONMUNDIAL]
GO
/****** Object:  StoredProcedure [dbo].[spGetSystemRoles]    Script Date: 8/22/2016 8:39:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSystemRoles]			
AS
SELECT Rol,Descripcion FROM Rol  