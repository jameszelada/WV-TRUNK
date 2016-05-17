use VISIONMUNDIAL

exec spCreatePhysicalTable "Proyecto", 1

--Columns Definition

ALTER TABLE Proyecto
ADD Codigo VARCHAR(20) not null

ALTER TABLE Proyecto
ADD ProyectoDescripcion VARCHAR(1000) not null

ALTER TABLE Proyecto
ADD Estado VARCHAR(1) not null 

--Default Constraints

ALTER TABLE Proyecto ADD CONSTRAINT DF_Proyecto_Codigo DEFAULT '' FOR Codigo;
ALTER TABLE Proyecto ADD CONSTRAINT DF_Proyecto_ProyectoDescripcion DEFAULT '' FOR ProyectoDescripcion;
ALTER TABLE Proyecto ADD CONSTRAINT DF_Proyecto_Estado DEFAULT '' FOR Estado;

-- Unique Constraint

ALTER TABLE Proyecto ADD CONSTRAINT UQ_Codigo UNIQUE (Codigo)

-- Foreign Keys



