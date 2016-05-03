use VISIONMUNDIAL

exec spCreatePhysicalTable "Bitacora", 1

--Columns Definition

ALTER TABLE Bitacora
ADD ID_Persona int not null

ALTER TABLE Bitacora
ADD FechaBitacora DATETIME not null



--Default Constraints

ALTER TABLE Bitacora ADD CONSTRAINT DF_Bitacora_ID_Persona DEFAULT 0 FOR ID_Persona;
ALTER TABLE Bitacora ADD CONSTRAINT DF_Bitacora_FechaBitacora DEFAULT GETDATE() FOR FechaBitacora;


-- Unique Constraint


-- Foreign Keys

ALTER TABLE Bitacora
ADD CONSTRAINT FK_Bitacora_ID_Persona FOREIGN KEY(ID_Persona) REFERENCES Persona(ID_Persona)
ON DELETE CASCADE