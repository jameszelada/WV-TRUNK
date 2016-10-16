DECLARE @Fecha as Date;
Declare @ID_Actividad int

DECLARE MY_CURSOR CURSOR 
  LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR 
SELECT act.ID_Actividad, act.Fecha from Actividad act
inner join Programa prg
on act.ID_Programa = prg.ID_Programa
inner join Comunidad com
on com.ID_Comunidad = prg.ID_Comunidad
inner join TipoPrograma tip
on tip.ID_TipoPrograma= prg.ID_TipoPrograma
--where prg.ID_Programa = 37
where tip.TipoPrograma = 'Primera Infancia'

OPEN MY_CURSOR
FETCH NEXT FROM MY_CURSOR INTO @ID_Actividad,@Fecha

WHILE @@FETCH_STATUS = 0
BEGIN 
	update Actividad set Fecha = DATEADD(DAY,4,@Fecha) where ID_Actividad = @ID_Actividad
	

    FETCH NEXT FROM MY_CURSOR INTO @ID_Actividad,@Fecha
END
CLOSE MY_CURSOR
DEALLOCATE MY_CURSOR