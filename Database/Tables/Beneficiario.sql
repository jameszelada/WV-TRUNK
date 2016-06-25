use VISIONMUNDIAL

exec spCreatePhysicalTable "Beneficiario", 1

--Columns Definition

ALTER TABLE Beneficiario
ADD Nombre VARCHAR(30) not null

ALTER TABLE Beneficiario
ADD Apellido VARCHAR(50) not null

ALTER TABLE Beneficiario
ADD Dui VARCHAR(10) null

ALTER TABLE Beneficiario
ADD Codigo VARCHAR(10) null

ALTER TABLE Beneficiario
ADD Edad VARCHAR(10) null

ALTER TABLE Beneficiario
ADD Sexo VARCHAR(1) not null 
 
ALTER TABLE Beneficiario
ADD Direccion VARCHAR(100) not null

ALTER TABLE Beneficiario
ADD ID_Programa int not null


--Default Constraints

ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_Nombre DEFAULT '' FOR Nombre;
ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_Apellido DEFAULT '' FOR Apellido;
ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_Dui DEFAULT '' FOR Dui;
ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_Codigo DEFAULT '' FOR Codigo;
ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_Sexo DEFAULT '' FOR Sexo;
ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_Edad DEFAULT '' FOR Edad;
ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_Direccion DEFAULT '' FOR Direccion;
ALTER TABLE Beneficiario ADD CONSTRAINT DF_Beneficiario_ID_Programa DEFAULT (0) FOR ID_Programa;




-- Unique Constraint

--ALTER TABLE Persona ADD CONSTRAINT UQ_Dui UNIQUE (Dui)

-- Foreign Key Constraints

ALTER TABLE Beneficiario
ADD CONSTRAINT FK_Beneficiario_ID_Programa FOREIGN KEY(ID_Programa) REFERENCES Programa(ID_Programa)
ON DELETE CASCADE


ALTER TABLE Beneficiario
DROP COLUMN Edad