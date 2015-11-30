use g4inge;
update usuario set sesionActiva  = 0;
SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = 'emma';

select * from Usuario;

select * from proyecto;

select * from diseno;

select * from diseno where idproy = 2;

EXEC cerrarSesion @nombre = 'emma';

EXEC iniciarSesion @nombre='emma', @contra='123';

SELECT count(*) cantidad, nc.estado, nc.idEjecucion, e.fecha
FROM NoConformidad nc, Ejecuciones e 
WHERE e.id=nc.idEjecucion AND e.idProy='Proyecto X' 
GROUP BY nc.estado, nc.idEjecucion , e.fecha
ORDER BY e.fecha DESC

update CasoPrueba set entrada = 'entrada1 - whatev, entrada2- whatev';

select * from Requerimiento;

select * from noconformidad;

SELECT r.id, r.nombre FROM Requerimiento r, CasoRequerimiento cr WHERE cr.idCaso = 1 AND cr.idDise = 4 AND r.id = cr.idReq;

SELECT r.id, r.nombre FROM Requerimiento r, CasoRequerimiento cr WHERE cr.idCaso = 1 AND cr.idDise = 4 AND r.id = cr.idReq;
