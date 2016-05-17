use VISIONMUNDIAL

exec spCreatePhysicalTable "BeneficiarioCompromiso", 1

--Columns Definition

ALTER TABLE BeneficiarioCompromiso
ADD AceptaCompromiso BIT not null

ALTER TABLE BeneficiarioCompromiso
ADD ExistioProblema BIT not null

ALTER TABLE BeneficiarioCompromiso
ADD TieneRegistroNacimiento BIT not null

ALTER TABLE BeneficiarioCompromiso
ADD SeCongrega BIT not null

ALTER TABLE BeneficiarioCompromiso
ADD NombreIglesia VARCHAR(30) not null

ALTER TABLE BeneficiarioCompromiso
ADD Comentario VARCHAR(2000) not null

ALTER TABLE BeneficiarioCompromiso
ADD ID_Beneficiario int not null

--Default Constraints

ALTER TABLE BeneficiarioCompromiso ADD CONSTRAINT DF_BeneficiarioCompromiso_AceptaCompromiso DEFAULT 1 FOR AceptaCompromiso;
ALTER TABLE BeneficiarioCompromiso ADD CONSTRAINT DF_BeneficiarioCompromiso_ExistioProblema DEFAULT 0 FOR ExistioProblema;
ALTER TABLE BeneficiarioCompromiso ADD CONSTRAINT DF_BeneficiarioCompromiso_SeCongrega DEFAULT 0 FOR SeCongrega;
ALTER TABLE BeneficiarioCompromiso ADD CONSTRAINT DF_BeneficiarioCompromiso_NombreIglesia DEFAULT '' FOR NombreIglesia;
ALTER TABLE BeneficiarioCompromiso ADD CONSTRAINT DF_BeneficiarioCompromiso_Comentario DEFAULT '' FOR Comentario;
ALTER TABLE BeneficiarioCompromiso ADD CONSTRAINT DF_BeneficiarioCompromiso_ID_Beneficiario DEFAULT (0) FOR ID_Beneficiario;

-- Unique Constraint

--ALTER TABLE Persona ADD CONSTRAINT UQ_Dui UNIQUE (Dui)

-- Foreign Key Constraints

ALTER TABLE BeneficiarioCompromiso
ADD CONSTRAINT FK_BeneficiarioCompromiso_ID_Beneficiario FOREIGN KEY(ID_Beneficiario) REFERENCES Beneficiario(ID_Beneficiario)
ON DELETE CASCADE
