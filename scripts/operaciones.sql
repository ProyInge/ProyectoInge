use g4inge;

SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido) FROM Usuario WHERE nomUsuario = 'emma';

update usuario set sesionActiva  = 0;

SELECT CONCAT(pNombre, ' ', pApellido, ' ', sApellido, ' - ', rol) from Usuario 
WHERE not rol = 'Lider'
AND not perfil = 'A';

select * from usuario 

INSERT INTO Usuario VALUES('207360523', 'Emmanuel', 'Arias', 'Soto', 'emma@gmail.com', 'emma', '123', 1, 'Desarrollador', 0, null);

INSERT INTO Usuario VALUES('109860554', 'María', 'Salas', 'Solís', 'maria@gmail.com', 'msalas', '123', 1, 'Tester', 0, null);
INSERT INTO Usuario VALUES('102430348', 'Pedro', 'Rojas', 'Calvo', 'peter@gmail.com', 'projas', '123', 1, 'Analista', 0, null);
INSERT INTO Usuario VALUES('405980121', 'Jose Manuel', 'Martínez', 'Agüero', 'josema@gmail.com', 'jmartinez', '123', 1, 'Desarrolador', 0, null);
INSERT INTO Usuario VALUES('207450989', 'Carlos', 'Fernández', 'Mata', 'carlos@gmail.com', 'cfernandez', '123', 1, 'Consultor BD', 0, null);

UPDATE usuario set pnombre = 'Jose' WHERE pnombre = 'Jose Manuel'