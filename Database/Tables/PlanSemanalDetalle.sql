use VISIONMUNDIAL

exec spCreatePhysicalTable "PlanSemanalDetalle", 1

--Columns Definition

ALTER TABLE PlanSemanalDetalle
ADD ID_PlanSemanal int not null

ALTER TABLE PlanSemanalDetalle
ADD Actividad NVARCHAR(500) not null

ALTER TABLE PlanSemanalDetalle
ADD Observaciones NVARCHAR(1000) not null

ALTER TABLE PlanSemanalDetalle
ADD Recurso NVARCHAR(1000) not null


--Default Constraints

ALTER TABLE PlanSemanalDetalle ADD CONSTRAINT DF_PlanSemanalDetalle_ID_PlanSemanal DEFAULT 0 FOR ID_PlanSemanal;
ALTER TABLE PlanSemanalDetalle ADD CONSTRAINT DF_PlanSemanalDetalle_Actividad DEFAULT '' FOR Actividad;
ALTER TABLE PlanSemanalDetalle ADD CONSTRAINT DF_PlanSemanalDetalle_Observaciones DEFAULT '' FOR Observaciones;
ALTER TABLE PlanSemanalDetalle ADD CONSTRAINT DF_PlanSemanalDetalle_Recurso DEFAULT '' FOR Recurso;


-- Unique Constraint


-- Foreign Keys

ALTER TABLE PlanSemanalDetalle
ADD CONSTRAINT FK_PlanSemanalDetalle_ID_PlanSemanal FOREIGN KEY(ID_PlanSemanal) REFERENCES PlanSemanal(ID_PlanSemanal)
ON DELETE CASCADE