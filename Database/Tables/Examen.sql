use VISIONMUNDIAL

exec spCreatePhysicalTable "Examen", 1

-- Columns Definition

ALTER TABLE Examen
ADD ID_Materia int not null

ALTER TABLE Examen
ADD NumeroExamen VARCHAR(5)

--Default Constraints

ALTER TABLE Examen ADD CONSTRAINT DF_Examen_NumeroExamen DEFAULT '' FOR NumeroExamen
ALTER TABLE Examen ADD CONSTRAINT DF_Examen_ID_Materia DEFAULT (0) FOR ID_Materia

-- Foreign Key Constraints

ALTER TABLE Examen
ADD CONSTRAINT FK_Examen_ID_Materia FOREIGN KEY(ID_Materia) REFERENCES Materia(ID_Materia)
ON DELETE CASCADE


