use VISIONMUNDIAL

exec spCreatePhysicalTable "TipoPuesto", 1

-- Columns Definition

ALTER TABLE TipoPuesto
ADD TipoPuesto VARCHAR(100) not null

ALTER TABLE TipoPuesto
ADD TipoPuestoDescripcion VARCHAR(500) not null


-- Default Constraints

ALTER TABLE TipoPuesto ADD CONSTRAINT DF_TipoPuesto_TipoPuesto DEFAULT '' FOR TipoPuesto;
ALTER TABLE TipoPuesto ADD CONSTRAINT DF_TipoPuesto_TipoPuestoDescripcion DEFAULT '' FOR TipoPuestoDescripcion;


-- Foreign Key Constraints

--ALTER TABLE Comunidad
--ADD CONSTRAINT FK_Comunidad_ID_Municipio FOREIGN KEY(ID_Municipio) REFERENCES Municipio(ID_Municipio)
--ON DELETE CASCADE