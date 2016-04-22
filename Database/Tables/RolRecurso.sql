use VISIONMUNDIAL

exec spCreatePhysicalTable "RolRecurso", 1

-- Columns Definition

ALTER TABLE RolRecurso
ADD ID_Rol int not null

ALTER TABLE RolRecurso
ADD ID_Recurso int not null

--Default Constraints

ALTER TABLE RolRecurso ADD CONSTRAINT DF_RolRecurso_ID_Rol DEFAULT (0) FOR ID_Rol
ALTER TABLE RolRecurso ADD CONSTRAINT DF_RolRecurso_ID_Recurso DEFAULT (0) FOR ID_Recurso

-- Foreign Key Constraints

ALTER TABLE RolRecurso
ADD CONSTRAINT FK_RolRecurso_ID_Rol FOREIGN KEY(ID_Rol) REFERENCES Rol(ID_Rol)
ON DELETE CASCADE

ALTER TABLE RolRecurso
ADD CONSTRAINT FK_RolRecurso_ID_Recurso FOREIGN KEY(ID_Recurso) REFERENCES Recurso(ID_Recurso)
ON DELETE CASCADE

