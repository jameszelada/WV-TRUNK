use VISIONMUNDIAL

exec spCreatePhysicalTable "AsignacionMateria", 1

-- Columns Definition

ALTER TABLE AsignacionMateria
ADD ID_Beneficiario int not null

ALTER TABLE AsignacionMateria
ADD ID_Materia int not null

--Default Constraints

ALTER TABLE AsignacionMateria ADD CONSTRAINT DF_AsignacionMateria_ID_Beneficiario DEFAULT (0) FOR ID_Beneficiario
ALTER TABLE AsignacionMateria ADD CONSTRAINT DF_AsignacionMateria_ID_Materia DEFAULT (0) FOR ID_Materia

-- Foreign Key Constraints

ALTER TABLE AsignacionMateria
ADD CONSTRAINT FK_AsignacionMateria_ID_Beneficiario FOREIGN KEY(ID_Beneficiario) REFERENCES Beneficiario(ID_Beneficiario)
ON DELETE CASCADE

ALTER TABLE AsignacionMateria
ADD CONSTRAINT FK_AsignacionMateria_ID_Materia FOREIGN KEY(ID_Materia) REFERENCES Materia(ID_Materia)
ON DELETE CASCADE

