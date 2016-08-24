USE [VISIONMUNDIAL]
GO
/****** Object:  StoredProcedure [dbo].[spGetWeeklyPlan]    Script Date: 8/23/2016 9:18:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetWeeklyPlan]
	 @personIdentity INT,			/* person ID */	
	 @weeklyplanIdentity INT			/* weeklyplan ID */				
AS

SELECT PER.Nombre +' '+PER.Apellido AS NombreCompleto,PER.Dui,PER.Email,PER.Telefono,
PLANS.FechaInicio,PLANS.FechaFinal,PLANSDET.Actividad,PLANSDET.Recurso,PLANSDET.Observaciones FROM Persona AS PER
INNER JOIN PlanSemanal AS PLANS
ON PER.ID_Persona = PLANS.ID_Persona
INNER JOIN PlanSemanalDetalle AS PLANSDET
ON PLANS.ID_PlanSemanal = PLANSDET.ID_PlanSemanal
WHERE PER.ID_Persona = @personIdentity AND PLANS.ID_PlanSemanal=@weeklyplanIdentity