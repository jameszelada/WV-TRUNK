use VISIONMUNDIAL

exec spCreatePhysicalTable "TipoPrograma", 1

-- Columns Definition

ALTER TABLE TipoPrograma
ADD TipoPrograma VARCHAR(100) not null

ALTER TABLE TipoPrograma
ADD TipoProgramaDescripcion VARCHAR(500) not null


-- Default Constraints

ALTER TABLE TipoPrograma ADD CONSTRAINT DF_TipoPrograma_TipoPrograma DEFAULT '' FOR TipoPrograma;
ALTER TABLE TipoPrograma ADD CONSTRAINT DF_TipoPrograma_TipoProgramaDescripcion DEFAULT '' FOR TipoProgramaDescripcion;


-- Foreign Key Constraints

--ALTER TABLE Comunidad
--ADD CONSTRAINT FK_Comunidad_ID_Municipio FOREIGN KEY(ID_Municipio) REFERENCES Municipio(ID_Municipio)
--ON DELETE CASCADE