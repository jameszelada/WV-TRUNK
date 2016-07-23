use VISIONMUNDIAL

exec spCreatePhysicalTable "ExamenResultado", 1

-- Columns Definition

ALTER TABLE ExamenResultado
ADD ID_Examen int not null

ALTER TABLE ExamenResultado
ADD ID_Beneficiario int not null

ALTER TABLE ExamenResultado
ADD Nota VARCHAR(10) not null

--Default Constraints

ALTER TABLE ExamenResultado ADD CONSTRAINT DF_ExamenResultado_Nota DEFAULT '' FOR Nota
ALTER TABLE ExamenResultado ADD CONSTRAINT DF_ExamenResultado_ID_Examen DEFAULT (0) FOR ID_Examen
ALTER TABLE ExamenResultado ADD CONSTRAINT DF_ExamenResultado_ID_Beneficiario DEFAULT (0) FOR ID_Beneficiario

-- Foreign Key Constraints

ALTER TABLE ExamenResultado
ADD CONSTRAINT FK_ExamenResultado_ID_Examen FOREIGN KEY(ID_Examen) REFERENCES Examen(ID_Examen)
ON DELETE CASCADE


