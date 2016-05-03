use VISIONMUNDIAL

exec spCreatePhysicalTable "Indicador", 1

-- Columns Definition

ALTER TABLE Indicador
ADD Indicador VARCHAR(30) not null

ALTER TABLE Indicador
ADD IndicadorDescripcion VARCHAR(30) not null

ALTER TABLE Indicador
ADD IndicadorValor VARCHAR(50) not null


-- Default Constraints

ALTER TABLE Indicador ADD CONSTRAINT DF_Indicador_Indicador DEFAULT '' FOR Indicador;
ALTER TABLE Indicador ADD CONSTRAINT DF_Indicador_IndicadorDescripcion DEFAULT '' FOR IndicadorDescripcion;
ALTER TABLE Indicador ADD CONSTRAINT DF_Indicador_IndicadorValor DEFAULT '' FOR IndicadorValor;


-- Foreign Key Constraints

--ALTER TABLE Comunidad
--ADD CONSTRAINT FK_Comunidad_ID_Municipio FOREIGN KEY(ID_Municipio) REFERENCES Municipio(ID_Municipio)
--ON DELETE CASCADE