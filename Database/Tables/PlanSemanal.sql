use VISIONMUNDIAL

exec spCreatePhysicalTable "PlanSemanal", 1

--Columns Definition

ALTER TABLE PlanSemanal
ADD ID_Persona int not null

ALTER TABLE PlanSemanal
ADD FechaInicio DATETIME not null

ALTER TABLE PlanSemanal
ADD FechaFinal DATETIME not null


--Default Constraints

ALTER TABLE PlanSemanal ADD CONSTRAINT DF_PlanSemanal_ID_Persona DEFAULT 0 FOR ID_Persona;
ALTER TABLE PlanSemanal ADD CONSTRAINT DF_PlanSemanal_FechaInicio DEFAULT GETDATE() FOR FechaInicio;
ALTER TABLE PlanSemanal ADD CONSTRAINT DF_PlanSemanal_FechaFinal DEFAULT GETDATE() FOR FechaFinal;


-- Unique Constraint


-- Foreign Keys

ALTER TABLE PlanSemanal
ADD CONSTRAINT FK_PlanSemanal_ID_Persona FOREIGN KEY(ID_Persona) REFERENCES Persona(ID_Persona)
ON DELETE CASCADE