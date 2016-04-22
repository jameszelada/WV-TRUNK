use VISIONMUNDIAL

exec spCreatePhysicalTable "Rol", 1

-- Columns Definition

ALTER TABLE Rol
ADD Rol VARCHAR(30) not null

ALTER TABLE Rol
ADD Descripcion VARCHAR(30) not null

--Default Constraints

ALTER TABLE Rol ADD CONSTRAINT DF_Rol_Rol DEFAULT '' FOR Rol;
ALTER TABLE Rol ADD CONSTRAINT DF_Rol_Descripcion DEFAULT '' FOR Descripcion;