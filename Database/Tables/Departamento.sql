use VISIONMUNDIAL

exec spCreatePhysicalTable "Departamento", 1

-- Columns Definition

ALTER TABLE Departamento
ADD Departamento VARCHAR(30) not null


-- Default Constraints

ALTER TABLE Departamento ADD CONSTRAINT DF_Departamento_Departamento DEFAULT '' FOR Departamento;