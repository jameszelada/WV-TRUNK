use VISIONMUNDIAL

exec spCreatePhysicalTable "UsuarioRol", 1

-- Columns Definition

ALTER TABLE UsuarioRol
ADD ID_Usuario int not null

ALTER TABLE UsuarioRol
ADD ID_Rol int not null

--Default Constraints

ALTER TABLE UsuarioRol ADD CONSTRAINT DF_UsuarioRol_ID_Usuario DEFAULT (0) FOR ID_Usuario
ALTER TABLE UsuarioRol ADD CONSTRAINT DF_UsuarioRol_ID_Rol DEFAULT (0) FOR ID_Rol

-- Foreign Key Constraints

ALTER TABLE UsuarioRol
ADD CONSTRAINT FK_UsuarioRol_ID_Usuario FOREIGN KEY(ID_Usuario) REFERENCES Usuario(ID_Usuario)
ON DELETE CASCADE

ALTER TABLE UsuarioRol
ADD CONSTRAINT FK_UsuarioRol_ID_Rol FOREIGN KEY(ID_Rol) REFERENCES Rol(ID_Rol) 
ON DELETE CASCADE