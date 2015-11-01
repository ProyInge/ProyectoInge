use g4inge;

SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = 'emma';

select * from Usuario;
update usuario set sesionActiva  = 0;


EXEC cerrarSesion @nombre = 'emma';

EXEC iniciarSesion @nombre='emma', @contra='123';