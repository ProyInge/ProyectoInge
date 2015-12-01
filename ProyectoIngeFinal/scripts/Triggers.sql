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

-- fin QuitarRecursosP

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

--fin QuitarRecursosRH

GO
CREATE TRIGGER EliminarReq ON Requerimiento Instead of DELETE
AS
BEGIN
	DECLARE @ID_Req varchar
	SELECT @ID_Req = id FROM deleted
	
	IF EXISTS (select idReq FROM DisenoRequerimiento WHERE idReq = @ID_Req)
	BEGIN
		DELETE FROM DisenoRequerimiento WHERE idReq = @ID_Req
	END
	
	DELETE FROM Requerimiento WHERE id = @ID_Req
END
--fin eliminarReq

/*GO
CREATE TRIGGER DesasociarReq ON Diseno Instead of DELETE
AS
BEGIN
	DECLARE @ID_Dise int
	SELECT @ID_Dise = id FROM deleted

	IF EXISTS (select idDise FROM DisenoRequerimiento WHERE idDise = @ID_Dise)
	BEGIN
		DELETE FROM DisenoRequerimiento WHERE idDise = @ID_Dise
	END
	Delete From Diseno where id = @ID_Dise
END
--fin DesasociarReq
*/