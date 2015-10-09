use g4inge;

select * from Usuario;
update usuario set sesionActiva  = 0;


INSERT INTO Usuario VALUES('207360523', 'Emmanuel', 'Arias', 'Soto', 'emma@gmail.com', 'emma', '123', 1, null, 0, null);

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


GO
CREATE PROCEDURE cerrarSesion
	@nombre varchar(20)
AS
	BEGIN
		UPDATE Usuario SET sesionActiva = 0 WHERE nomUsuario = @nombre;
	END

EXEC cerrarSesion @nombre = 'emma';

EXEC iniciarSesion @nombre='emma', @contra='123';