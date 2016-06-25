use VISIONMUNDIAL

exec spCreatePhysicalTable "BeneficiarioSalud", 1

--Columns Definition

ALTER TABLE BeneficiarioSalud
ADD EstadoSalud VARCHAR(30) not null

ALTER TABLE BeneficiarioSalud
ADD TieneTarjeta BIT null

ALTER TABLE BeneficiarioSalud
ADD FechaCurvaCrecimiento DATETIME null

ALTER TABLE BeneficiarioSalud
ADD FechaInmunizacion DATETIME null

ALTER TABLE BeneficiarioSalud
ADD Enfermedad VARCHAR(30) null

ALTER TABLE BeneficiarioSalud
ADD Discapacidad VARCHAR(30) null

ALTER TABLE BeneficiarioSalud
ADD ID_Beneficiario int not null

--Default Constraints

ALTER TABLE BeneficiarioSalud ADD CONSTRAINT DF_BeneficiarioSalud_EstadoSalud DEFAULT '' FOR EstadoSalud;
ALTER TABLE BeneficiarioSalud ADD CONSTRAINT DF_BeneficiarioSalud_TieneTarjeta DEFAULT 0 FOR TieneTarjeta;
ALTER TABLE BeneficiarioSalud ADD CONSTRAINT DF_BeneficiarioSalud_FechaCurvaCrecimiento  DEFAULT ('1900-01-01 00:00:00') FOR FechaCurvaCrecimiento;
ALTER TABLE BeneficiarioSalud ADD CONSTRAINT DF_BeneficiarioSalud_FechaInmunizacion  DEFAULT ('1900-01-01 00:00:00') FOR FechaInmunizacion;
ALTER TABLE BeneficiarioSalud ADD CONSTRAINT DF_BeneficiarioSalud_Enfermedad DEFAULT '' FOR Enfermedad;
ALTER TABLE BeneficiarioSalud ADD CONSTRAINT DF_BeneficiarioSalud_Discapacidad DEFAULT '' FOR Discapacidad;
ALTER TABLE BeneficiarioSalud ADD CONSTRAINT DF_BeneficiarioSalud_ID_Beneficiario DEFAULT (0) FOR ID_Beneficiario;



-- Unique Constraint

--ALTER TABLE Persona ADD CONSTRAINT UQ_Dui UNIQUE (Dui)

-- Foreign Key Constraints

ALTER TABLE BeneficiarioSalud
ADD CONSTRAINT FK_BeneficiarioSalud_ID_Beneficiario FOREIGN KEY(ID_Beneficiario) REFERENCES Beneficiario(ID_Beneficiario)
ON DELETE CASCADE
