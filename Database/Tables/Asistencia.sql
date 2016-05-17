use VISIONMUNDIAL

exec spCreatePhysicalTable "Asistencia", 1

-- Columns Definition

ALTER TABLE Asistencia
ADD ID_Actividad int not null

ALTER TABLE Asistencia
ADD ID_Beneficiario int not null


--Default Constraints

ALTER TABLE Asistencia ADD CONSTRAINT DF_Asistencia_ID_Actividad DEFAULT (0) FOR ID_Actividad
ALTER TABLE Asistencia ADD CONSTRAINT DF_Asistencia_ID_Beneficiario DEFAULT (0) FOR ID_Beneficiario


-- Foreign Key Constraints

ALTER TABLE Asistencia
ADD CONSTRAINT FK_Asistencia_ID_Actividad FOREIGN KEY(ID_Actividad) REFERENCES Actividad(ID_Actividad)
ON DELETE CASCADE

ALTER TABLE Asistencia
ADD CONSTRAINT FK_Asistencia_ID_Beneficiario FOREIGN KEY(ID_Beneficiario) REFERENCES Beneficiario(ID_Beneficiario)
ON DELETE NO ACTION



