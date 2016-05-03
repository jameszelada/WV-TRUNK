use VISIONMUNDIAL

exec spCreatePhysicalTable "Persona", 1

--Columns Definition

ALTER TABLE Persona
ADD Nombre VARCHAR(30) not null

ALTER TABLE Persona
ADD Apellido VARCHAR(50) not null

ALTER TABLE Persona
ADD Dui VARCHAR(10) not null

ALTER TABLE Persona
ADD Sexo VARCHAR(1) not null 

ALTER TABLE Persona
ADD FechaNacimiento Datetime not null 

ALTER TABLE Persona
ADD Email VARCHAR(50) not null

ALTER TABLE Persona
ADD Direccion VARCHAR(100) not null

ALTER TABLE Persona
ADD Telefono VARCHAR(15) not null


--Default Constraints

ALTER TABLE Persona ADD CONSTRAINT DF_Persona_Nombre DEFAULT '' FOR Nombre;
ALTER TABLE Persona ADD CONSTRAINT DF_Persona_Apellido DEFAULT '' FOR Apellido;
ALTER TABLE Persona ADD CONSTRAINT DF_Persona_Dui DEFAULT '' FOR Dui;
ALTER TABLE Persona ADD CONSTRAINT DF_Persona_Sexo DEFAULT '' FOR Sexo;
ALTER TABLE Persona ADD CONSTRAINT DF_Persona_FechaNacimiento DEFAULT GETDATE() FOR FechaNacimiento;
ALTER TABLE Persona ADD CONSTRAINT DF_Persona_Email DEFAULT '' FOR Email;
ALTER TABLE Persona ADD CONSTRAINT DF_Persona_Direccion DEFAULT '' FOR Direccion;
ALTER TABLE Persona ADD CONSTRAINT DF_Persona_Telefono DEFAULT '' FOR Telefono;


-- Unique Constraint

ALTER TABLE Persona ADD CONSTRAINT UQ_Dui UNIQUE (Dui)

-- Foreign Keys