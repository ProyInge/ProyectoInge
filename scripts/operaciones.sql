use g4inge;
update usuario set sesionActiva  = 0;
SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = 'emma';

select * from Usuario;

EXEC cerrarSesion @nombre = 'emma';

EXEC iniciarSesion @nombre='emma', @contra='123';


update CasoPrueba set entrada = 'entrada1 - whatev, entrada2- whatev';

select * from Requerimiento;

SELECT r.id, r.nombre FROM Requerimiento r, CasoRequerimiento cr WHERE cr.idCaso = 1 AND cr.idDise = 4 AND r.id = cr.idReq;

SELECT r.id, r.nombre FROM Requerimiento r, CasoRequerimiento cr WHERE cr.idCaso = 1 AND cr.idDise = 4 AND r.id = cr.idReq;
