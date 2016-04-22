use VISIONMUNDIAL

exec spCreatePhysicalTable "Municipio", 1

-- Columns Definition

ALTER TABLE Municipio
ADD Municipio VARCHAR(30) not null

ALTER TABLE Municipio
ADD ID_Departamento int not null


-- Default Constraints

ALTER TABLE Municipio ADD CONSTRAINT DF_Municipio_Municipio DEFAULT '' FOR Municipio;
ALTER TABLE Municipio ADD CONSTRAINT DF_Municipio_ID_Departamento DEFAULT (0) FOR ID_Departamento;

-- Foreign Key Constraints

ALTER TABLE Municipio
ADD CONSTRAINT FK_Municipio_ID_Departamento FOREIGN KEY(ID_Departamento) REFERENCES Departamento(ID_Departamento)
ON DELETE CASCADE