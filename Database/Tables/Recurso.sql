use VISIONMUNDIAL

exec spCreatePhysicalTable "Recurso", 1

--Columns Definition

ALTER TABLE Recurso
ADD Recurso VARCHAR(30) not null

ALTER TABLE Recurso
ADD Pagina VARCHAR(30) not null

--Default Constraints
ALTER TABLE Recurso ADD CONSTRAINT DF_Recurso_Recurso DEFAULT '' FOR Recurso;
ALTER TABLE Recurso ADD CONSTRAINT DF_Recurso_Pagina DEFAULT '' FOR Pagina;