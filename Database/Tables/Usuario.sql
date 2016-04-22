use VISIONMUNDIAL

exec spCreatePhysicalTable "Usuario", 1

--Columns Definition

ALTER TABLE Usuario
ADD NombreUsuario VARCHAR(30) not null

ALTER TABLE Usuario
ADD Contrasenia VARCHAR(50) not null

ALTER TABLE Usuario
ADD Nombre VARCHAR(50) not null 

ALTER TABLE Usuario
ADD Apellido VARCHAR(50) not null 

ALTER TABLE Usuario
ADD Email VARCHAR(50) not null

--Default Constraints

ALTER TABLE Usuario ADD CONSTRAINT DF_Usuario_NombreUsuario DEFAULT '' FOR NombreUsuario;
ALTER TABLE Usuario ADD CONSTRAINT DF_Usuario_Contrasenia DEFAULT '' FOR Contrasenia;
ALTER TABLE Usuario ADD CONSTRAINT DF_Usuario_Nombre DEFAULT '' FOR Nombre;
ALTER TABLE Usuario ADD CONSTRAINT DF_Usuario_Apellido DEFAULT '' FOR Apellido;
ALTER TABLE Usuario ADD CONSTRAINT DF_Usuario_Email DEFAULT '' FOR EMAIL;


-- Unique Constraint

ALTER TABLE Usuario ADD CONSTRAINT UQ_NombreUsuario_Contrasenia UNIQUE (NombreUsuario,Contrasenia)

-- Foreign Keys