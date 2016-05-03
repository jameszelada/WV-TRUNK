use VISIONMUNDIAL

exec spCreatePhysicalTable "BitacoraDetalle", 1

--Columns Definition

ALTER TABLE BitacoraDetalle
ADD ID_Bitacora int not null

ALTER TABLE BitacoraDetalle
ADD Actividad NVARCHAR(500) not null

ALTER TABLE BitacoraDetalle
ADD Observaciones NVARCHAR(1000) not null


--Default Constraints

ALTER TABLE BitacoraDetalle ADD CONSTRAINT DF_BitacoraDetalle_ID_Bitacora DEFAULT 0 FOR ID_Bitacora;
ALTER TABLE BitacoraDetalle ADD CONSTRAINT DF_BitacoraDetalle_Actividad DEFAULT '' FOR Actividad;
ALTER TABLE BitacoraDetalle ADD CONSTRAINT DF_BitacoraDetalle_Observaciones DEFAULT '' FOR Observaciones;


-- Unique Constraint


-- Foreign Keys

ALTER TABLE BitacoraDetalle
ADD CONSTRAINT FK_BitacoraDetalle_ID_Bitacora FOREIGN KEY(ID_Bitacora) REFERENCES Bitacora(ID_Bitacora)
ON DELETE CASCADE