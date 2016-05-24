use VISIONMUNDIAL

exec spCreatePhysicalTable "AsignacionRecursoHumano", 1

-- Columns Definition

ALTER TABLE AsignacionRecursoHumano
ADD ID_Puesto int not null

ALTER TABLE AsignacionRecursoHumano
ADD ID_Persona int not null

ALTER TABLE AsignacionRecursoHumano
ADD ID_Proyecto int not null


-- Default Constraints

ALTER TABLE AsignacionRecursoHumano ADD CONSTRAINT DF_AsignacionRecursoHumano_ID_Puesto DEFAULT (0) FOR ID_Puesto
ALTER TABLE AsignacionRecursoHumano ADD CONSTRAINT DF_AsignacionRecursoHumano_ID_Persona DEFAULT (0) FOR ID_Persona
ALTER TABLE AsignacionRecursoHumano ADD CONSTRAINT DF_AsignacionRecursoHumano_ID_Proyecto DEFAULT (0) FOR ID_Proyecto


-- Foreign Key Constraints

ALTER TABLE AsignacionRecursoHumano
ADD CONSTRAINT FK_AsignacionRecursoHumano_ID_Puesto FOREIGN KEY(ID_Puesto) REFERENCES Puesto(ID_Puesto)
ON DELETE CASCADE

ALTER TABLE AsignacionRecursoHumano
ADD CONSTRAINT FK_AsignacionRecursoHumano_ID_Persona FOREIGN KEY(ID_Persona) REFERENCES Persona(ID_Persona)
ON DELETE CASCADE

ALTER TABLE AsignacionRecursoHumano
ADD CONSTRAINT FK_AsignacionRecursoHumano_ID_Proyecto FOREIGN KEY(ID_Proyecto) REFERENCES Proyecto(ID_Proyecto)
ON DELETE CASCADE