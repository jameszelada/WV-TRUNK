use VISIONMUNDIAL

exec spCreatePhysicalTable "Programa", 1

--Columns Definition

ALTER TABLE Programa
ADD Codigo VARCHAR(20) not null

ALTER TABLE Programa
ADD ProgramaDescripcion VARCHAR(1000) not null

ALTER TABLE Programa
ADD Estado VARCHAR(1) not null 

ALTER TABLE Programa
ADD FechaInicio DATETIME not null

ALTER TABLE Programa
ADD FechaFinal DATETIME not null

ALTER TABLE Programa
ADD ID_Proyecto int not null

ALTER TABLE Programa
ADD ID_TipoPrograma int not null

ALTER TABLE Programa
ADD ID_Comunidad int not null



--Default Constraints

ALTER TABLE Programa ADD CONSTRAINT DF_Programa_Codigo DEFAULT '' FOR Codigo;
ALTER TABLE Programa ADD CONSTRAINT DF_Programa_ProgramaDescripcion DEFAULT '' FOR ProgramaDescripcion;
ALTER TABLE Programa ADD CONSTRAINT DF_Programa_Estado DEFAULT '' FOR Estado;
ALTER TABLE Programa ADD CONSTRAINT DF_Programa_FechaInicio DEFAULT GETDATE() FOR FechaInicio;
ALTER TABLE Programa ADD CONSTRAINT DF_Programa_FechaFinal DEFAULT GETDATE() FOR FechaFinal;
ALTER TABLE Programa ADD CONSTRAINT DF_Programa_ID_Proyecto DEFAULT (0) FOR ID_Proyecto;
ALTER TABLE Programa ADD CONSTRAINT DF_Programa_ID_TipoPrograma DEFAULT (0) FOR ID_TipoPrograma;
ALTER TABLE Programa ADD CONSTRAINT DF_Programa_ID_Comunidad DEFAULT (0) FOR ID_Comunidad;

-- Unique Constraint

ALTER TABLE Programa ADD CONSTRAINT UQ_CodigoPrograma UNIQUE (Codigo)

-- Foreign Keys

ALTER TABLE Programa
ADD CONSTRAINT FK_Programa_ID_Proyecto FOREIGN KEY(ID_Proyecto) REFERENCES Proyecto(ID_Proyecto)
ON DELETE CASCADE

ALTER TABLE Programa
ADD CONSTRAINT FK_Programa_ID_TipoPrograma FOREIGN KEY(ID_TipoPrograma) REFERENCES TipoPrograma(ID_TipoPrograma)
ON DELETE CASCADE

ALTER TABLE Programa
ADD CONSTRAINT FK_Programa_ID_Comunidad FOREIGN KEY(ID_Comunidad) REFERENCES Comunidad(ID_Comunidad)
ON DELETE CASCADE