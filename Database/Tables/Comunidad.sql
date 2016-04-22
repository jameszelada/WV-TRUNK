use VISIONMUNDIAL

exec spCreatePhysicalTable "Comunidad", 1

-- Columns Definition

ALTER TABLE Comunidad
ADD Comunidad VARCHAR(30) not null

ALTER TABLE Comunidad
ADD ID_Municipio int not null


-- Default Constraints

ALTER TABLE Comunidad ADD CONSTRAINT DF_Comunidad_Comunidad DEFAULT '' FOR Comunidad;
ALTER TABLE Comunidad ADD CONSTRAINT DF_Comunidad_ID_Municipio DEFAULT (0) FOR ID_Municipio;

-- Foreign Key Constraints

ALTER TABLE Comunidad
ADD CONSTRAINT FK_Comunidad_ID_Municipio FOREIGN KEY(ID_Municipio) REFERENCES Municipio(ID_Municipio)
ON DELETE CASCADE