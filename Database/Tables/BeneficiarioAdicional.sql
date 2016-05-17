use VISIONMUNDIAL

exec spCreatePhysicalTable "BeneficiarioAdicional", 1

--Columns Definition

ALTER TABLE BeneficiarioAdicional
ADD NombreEmergencia VARCHAR(30) not null

ALTER TABLE BeneficiarioAdicional
ADD NumeroEmergencia VARCHAR(30) not null

ALTER TABLE BeneficiarioAdicional
ADD TieneRegistroNacimiento BIT not null

ALTER TABLE BeneficiarioAdicional
ADD ID_Beneficiario int not null

--Default Constraints

ALTER TABLE BeneficiarioAdicional ADD CONSTRAINT DF_BeneficiarioAdicional_NombreEmergencia DEFAULT '' FOR NombreEmergencia;
ALTER TABLE BeneficiarioAdicional ADD CONSTRAINT DF_BeneficiarioAdicional_NumeroEmergencia DEFAULT '' FOR NumeroEmergencia;
ALTER TABLE BeneficiarioAdicional ADD CONSTRAINT DF_BeneficiarioAdicional_TieneRegistroNacimiento DEFAULT 0 FOR TieneRegistroNacimiento;
ALTER TABLE BeneficiarioAdicional ADD CONSTRAINT DF_BeneficiarioAdicional_ID_Beneficiario DEFAULT (0) FOR ID_Beneficiario;

-- Unique Constraint

--ALTER TABLE Persona ADD CONSTRAINT UQ_Dui UNIQUE (Dui)

-- Foreign Key Constraints

ALTER TABLE BeneficiarioAdicional
ADD CONSTRAINT FK_BeneficiarioAdicional_ID_Beneficiario FOREIGN KEY(ID_Beneficiario) REFERENCES Beneficiario(ID_Beneficiario)
ON DELETE CASCADE
