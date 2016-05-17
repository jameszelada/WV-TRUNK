use VISIONMUNDIAL

exec spCreatePhysicalTable "Objetivo", 1

-- Columns Definition

ALTER TABLE Objetivo
ADD Objetivo VARCHAR(1000) not null

ALTER TABLE Objetivo
ADD ObjetivoDescripcion VARCHAR(1000) not null

ALTER TABLE Objetivo
ADD TipoObjetivo VARCHAR(1) not null


ALTER TABLE Objetivo
ADD ID_Programa int not null

-- Default Constraints

ALTER TABLE Objetivo ADD CONSTRAINT DF_Objetivo_Objetivo DEFAULT '' FOR Objetivo;
ALTER TABLE Objetivo ADD CONSTRAINT DF_Objetivo_ObjetivoDescripcion DEFAULT '' FOR ObjetivoDescripcion;
ALTER TABLE Objetivo ADD CONSTRAINT DF_Objetivo_TipoObjetivo DEFAULT '' FOR TipoObjetivo;
ALTER TABLE Objetivo ADD CONSTRAINT DF_Objetivo_ID_Programa DEFAULT (0) FOR ID_Programa


-- Foreign Key Constraints


ALTER TABLE Objetivo
ADD CONSTRAINT FK_Objetivo_ID_Programa FOREIGN KEY(ID_Programa) REFERENCES Programa(ID_Programa)
ON DELETE CASCADE