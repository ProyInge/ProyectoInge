use g4inge

GO
CREATE TRIGGER QuitarRecursosP ON Proyecto Instead of Delete	
AS
BEGIN
	DECLARE @ID_Proyecto int
	SELECT @ID_Proyecto = id FROM deleted
	
	IF EXISTS ( SELECT id FROM Proyecto WHERE id = @ID_Proyecto )
	BEGIN
		Update Usuario Set idProy = null where idProy = @ID_Proyecto
	END
	
	Delete from Proyecto where id = @ID_Proyecto;
END

GO

CREATE TRIGGER QuitarRecursosRH ON Usuario Instead of Delete	
AS
BEGIN
	DECLARE @ID_Proyecto int
	DECLARE @Cedula int
	SELECT @ID_Proyecto = idProy FROM deleted
	Select @Cedula = cedula from deleted
	
	IF EXISTS ( SELECT idProy FROM Usuario WHERE idProy = @ID_Proyecto And cedula = @Cedula)
	BEGIN
		Update Usuario Set idProy = null where idProy = @ID_Proyecto And cedula = @Cedula
	END
	
	Delete from Usuario where cedula = @Cedula;
END