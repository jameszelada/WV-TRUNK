use VISIONMUNDIAL

exec spCreatePhysicalTable "RolRecurso", 1

-- Columns Definition

ALTER TABLE RolRecurso
ADD ID_Rol int not null

ALTER TABLE RolRecurso
ADD ID_Recurso int not null

ALTER TABLE RolRecurso
ADD Agregar BIT  null

ALTER TABLE RolRecurso
ADD Modificar BIT  null

ALTER TABLE RolRecurso
ADD Eliminar BIT  null

--Default Constraints

ALTER TABLE RolRecurso ADD CONSTRAINT DF_RolRecurso_ID_Rol DEFAULT (0) FOR ID_Rol
ALTER TABLE RolRecurso ADD CONSTRAINT DF_RolRecurso_ID_Recurso DEFAULT (0) FOR ID_Recurso
ALTER TABLE RolRecurso ADD CONSTRAINT DF_RolRecurso_Agregar DEFAULT 1 FOR Agregar;
ALTER TABLE RolRecurso ADD CONSTRAINT DF_RolRecurso_Modificar DEFAULT 1 FOR Modificar;
ALTER TABLE RolRecurso ADD CONSTRAINT DF_RolRecurso_Eliminar DEFAULT 1 FOR Eliminar;

-- Foreign Key Constraints

ALTER TABLE RolRecurso
ADD CONSTRAINT FK_RolRecurso_ID_Rol FOREIGN KEY(ID_Rol) REFERENCES Rol(ID_Rol)
ON DELETE CASCADE

ALTER TABLE RolRecurso
ADD CONSTRAINT FK_RolRecurso_ID_Recurso FOREIGN KEY(ID_Recurso) REFERENCES Recurso(ID_Recurso)
ON DELETE CASCADE

