use VISIONMUNDIAL

exec spCreatePhysicalTable "Indicador", 1

-- Columns Definition

ALTER TABLE Indicador
ADD Indicador VARCHAR(30) not null

ALTER TABLE Indicador
ADD IndicadorDescripcion VARCHAR(30) not null

ALTER TABLE Indicador
ADD IndicadorValor VARCHAR(50) not null

ALTER TABLE Indicador
ADD ID_Programa int not null


-- Default Constraints

ALTER TABLE Indicador ADD CONSTRAINT DF_Indicador_Indicador DEFAULT '' FOR Indicador;
ALTER TABLE Indicador ADD CONSTRAINT DF_Indicador_IndicadorDescripcion DEFAULT '' FOR IndicadorDescripcion;
ALTER TABLE Indicador ADD CONSTRAINT DF_Indicador_IndicadorValor DEFAULT '' FOR IndicadorValor;
ALTER TABLE Indicador ADD CONSTRAINT DF_Indicador_ID_Programa DEFAULT (0) FOR ID_Programa;


-- Foreign Key Constraints

ALTER TABLE Indicador
ADD CONSTRAINT FK_Indicador_ID_Programa FOREIGN KEY(ID_Programa) REFERENCES Programa(ID_Programa)
ON DELETE CASCADE