use VISIONMUNDIAL

exec spCreatePhysicalTable "Materia", 1

-- Columns Definition

ALTER TABLE Materia
ADD Nombre VARCHAR(50) not null

ALTER TABLE Materia
ADD Anio VARCHAR(4) not null

ALTER TABLE Materia
ADD Grado VARCHAR(20) not null



-- Default Constraints

ALTER TABLE Materia ADD CONSTRAINT DF_Materia_Nombre DEFAULT '' FOR Nombre;
ALTER TABLE Materia ADD CONSTRAINT DF_Materia_Anio DEFAULT '' FOR Anio;
ALTER TABLE Materia ADD CONSTRAINT DF_Materia_Grado DEFAULT '' FOR Grado;


-- Foreign Key Constraints
