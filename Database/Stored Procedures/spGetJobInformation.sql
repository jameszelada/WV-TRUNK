USE [VISIONMUNDIAL]
GO
/****** Object:  StoredProcedure [dbo].[spGetJobInformation]    Script Date: 8/23/2016 4:09:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetJobInformation]			
AS
SELECT TP.TipoPuesto,TP.TipoPuestoDescripcion FROM TipoPuesto TP;

SELECT PT.Puesto,PT.PuestoDescripcion,TP.TipoPuesto FROM TipoPuesto TP
INNER JOIN Puesto PT
ON PT.ID_TipoPuesto = TP.ID_TipoPuesto 