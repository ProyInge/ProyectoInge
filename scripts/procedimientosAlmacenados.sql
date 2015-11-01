use g4inge;

DROP PROCEDURE iniciarSesion

GO
CREATE PROCEDURE iniciarSesion
    @nombre varchar(20), 
    @contra varchar(30) 
AS
	IF( SELECT COUNT(*) FROM Usuario WHERE contrasena = @contra AND nomUsuario = @nombre ) = 1
	BEGIN
		IF(SELECT sesionActiva FROM Usuario WHERE nomUsuario = @nombre) = 0
		BEGIN
			UPDATE Usuario SET sesionActiva = 1 WHERE nomUsuario = @nombre;
			SELECT 0
		END
		ELSE
		BEGIN
			-- else sesion activa
			SELECT 1
		END
	END
	ELSE
	BEGIN
	--else datos incorrectos
		SELECT -1
	END
-- fin iniciarSesion

GO
CREATE PROCEDURE cerrarSesion
	@nombre varchar(20)
AS
	BEGIN
		UPDATE Usuario SET sesionActiva = 0 WHERE nomUsuario = @nombre;
	END
-- fin cerrarSesion