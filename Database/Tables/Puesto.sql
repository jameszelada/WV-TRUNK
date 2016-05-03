use VISIONMUNDIAL

exec spCreatePhysicalTable "Puesto", 1

-- Columns Definition

ALTER TABLE Puesto
ADD Puesto VARCHAR(100) not null

ALTER TABLE Puesto
ADD PuestoDescripcion VARCHAR(500) not null

ALTER TABLE Puesto
ADD ID_TipoPuesto int not null


-- Default Constraints

ALTER TABLE Puesto ADD CONSTRAINT DF_Puesto_Puesto DEFAULT '' FOR Puesto;
ALTER TABLE Puesto ADD CONSTRAINT DF_Puesto_PuestoDescripcion DEFAULT '' FOR PuestoDescripcion;
ALTER TABLE Puesto ADD CONSTRAINT DF_Puesto_ID_TipoPuesto DEFAULT (0) FOR ID_TipoPuesto


-- Foreign Key Constraints

ALTER TABLE Puesto
ADD CONSTRAINT FK_Puesto_ID_TipoPuesto FOREIGN KEY(ID_TipoPuesto) REFERENCES TipoPuesto(ID_TipoPuesto)
ON DELETE CASCADE