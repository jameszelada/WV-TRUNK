use VISIONMUNDIAL

exec spCreatePhysicalTable "Objetivo", 1

-- Columns Definition

ALTER TABLE Objetivo
ADD Objetivo VARCHAR(1000) not null

ALTER TABLE Objetivo
ADD ObjetivoDescripcion VARCHAR(1000) not null


-- Default Constraints

ALTER TABLE Objetivo ADD CONSTRAINT DF_Objetivo_Objetivo DEFAULT '' FOR Objetivo;
ALTER TABLE Objetivo ADD CONSTRAINT DF_Objetivo_ObjetivoDescripcion DEFAULT '' FOR ObjetivoDescripcion;


-- Foreign Key Constraints

--ALTER TABLE Comunidad
--ADD CONSTRAINT FK_Comunidad_ID_Municipio FOREIGN KEY(ID_Municipio) REFERENCES Municipio(ID_Municipio)
--ON DELETE CASCADE