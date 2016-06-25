use VISIONMUNDIAL

exec spCreatePhysicalTable "BeneficiarioEducacion", 1

--Columns Definition

ALTER TABLE BeneficiarioEducacion
ADD Estudia BIT null

ALTER TABLE BeneficiarioEducacion
ADD GradoEducacion VARCHAR(30) null

ALTER TABLE BeneficiarioEducacion
ADD Motivo VARCHAR(30) null

ALTER TABLE BeneficiarioEducacion
ADD UltimoGrado VARCHAR(30) null

ALTER TABLE BeneficiarioEducacion
ADD UltimoAño VARCHAR(30) null

ALTER TABLE BeneficiarioEducacion
ADD NombreCentroEscolar VARCHAR(100) null

ALTER TABLE BeneficiarioEducacion
ADD Turno VARCHAR(30) null

ALTER TABLE BeneficiarioEducacion
ADD ID_Beneficiario int not null

--Default Constraints

ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_Estudia DEFAULT 0 FOR Estudia;
ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_GradoEducacion DEFAULT '' FOR GradoEducacion;
ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_Motivo DEFAULT '' FOR Motivo;
ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_UltimoGrado DEFAULT '' FOR UltimoGrado;
ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_UltimoAnio DEFAULT '' FOR UltimoAño;
ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_NombreCentroEscolar DEFAULT '' FOR NombreCentroEscolar;
ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_Turno DEFAULT '' FOR Turno;
ALTER TABLE BeneficiarioEducacion ADD CONSTRAINT DF_BeneficiarioEducacion_ID_Beneficiario DEFAULT (0) FOR ID_Beneficiario;

-- Unique Constraint

--ALTER TABLE Persona ADD CONSTRAINT UQ_Dui UNIQUE (Dui)

-- Foreign Key Constraints

ALTER TABLE BeneficiarioEducacion
ADD CONSTRAINT FK_BeneficiarioEducacion_ID_Beneficiario FOREIGN KEY(ID_Beneficiario) REFERENCES Beneficiario(ID_Beneficiario)
ON DELETE CASCADE
