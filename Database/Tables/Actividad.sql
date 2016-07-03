use VISIONMUNDIAL

exec spCreatePhysicalTable "Actividad", 1

--Columns Definition

ALTER TABLE Actividad
ADD Codigo VARCHAR(20) not null

ALTER TABLE Actividad
ADD ActividadDescripcion VARCHAR(1000) not null

ALTER TABLE Actividad
ADD Estado VARCHAR(1) not null 

ALTER TABLE Actividad
ADD Fecha DATE not null


ALTER TABLE Actividad
ADD ID_Programa int not null

ALTER TABLE Actividad
ADD Observacion VARCHAR(1000) not null

--Default Constraints

ALTER TABLE Actividad ADD CONSTRAINT DF_Actividad_Codigo DEFAULT '' FOR Codigo;
ALTER TABLE Actividad ADD CONSTRAINT DF_Actividad_ProgramaDescripcion DEFAULT '' FOR ActividadDescripcion;
ALTER TABLE Actividad ADD CONSTRAINT DF_Actividad_Estado DEFAULT '' FOR Estado;
ALTER TABLE Actividad ADD CONSTRAINT DF_Actividad_Fecha DEFAULT ('1900-01-01') FOR Fecha;
ALTER TABLE Actividad ADD CONSTRAINT DF_Actividad_ID_Programa DEFAULT (0) FOR ID_Programa;
ALTER TABLE Actividad ADD CONSTRAINT DF_Actividad_Observacion DEFAULT '' FOR Observacion;


--Table Structure modification
--ALTER TABLE Actividad DROP CONSTRAINT UQ_CodigoActividad
--ALTER TABLE Actividad DROP CONSTRAINT DF_Actividad_FechaInicio
--ALTER TABLE Actividad DROP CONSTRAINT DF_Actividad_FechaFinal

--ALTER TABLE Actividad DROP COLUMN FechaInicio
--ALTER TABLE Actividad DROP COLUMN FechaFinal






-- Unique Constraint

ALTER TABLE Actividad ADD CONSTRAINT UQ_ProgramaFechaActividad UNIQUE (ID_Programa,Fecha)

-- Foreign Keys

ALTER TABLE Actividad
ADD CONSTRAINT FK_Actividad_ID_Programa FOREIGN KEY(ID_Programa) REFERENCES Programa(ID_Programa)
ON DELETE CASCADE

