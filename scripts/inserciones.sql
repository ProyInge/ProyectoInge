use g4inge;

INSERT INTO Usuario VALUES(
'123456789','admin',null,null,null,'admin','admin','A','Administrador','0',null,CURRENT_TIMESTAMP
);

INSERT INTO Usuario VALUES(
'111222333','Angelica','Fallas','Blanco','ange@ucr.ac.cr','ange','ange','A','Lider','0',null,CURRENT_TIMESTAMP
);

INSERT INTO Usuario VALUES(
'115900358','Daniel','Muñoz','Rojas','daniel@gmail.com','daniel','daniel','A','Lider','0',null,CURRENT_TIMESTAMP
);

INSERT INTO Usuario VALUES(
'304770347','David','Solano','Mora','david.solanomora@ucr.ac.cr','Davesmacer','Davesmacer','A','Tester','0',null, CURRENT_TIMESTAMP
);

INSERT INTO Usuario VALUES(
'207400774','Jeffry','Venegas','Montoya','jeffvene@gmail.com','jeffvene','jeffvene','M','Tester','0',null, CURRENT_TIMESTAMP
);

INSERT INTO Usuario VALUES(
'207360523','Emmanuel','Arias','Soto','ariassotoemmanuel@gmail.com','emma','123','A','Tester','0',null, CURRENT_TIMESTAMP
);

select * from Usuario

INSERT INTO PROYECTO VALUES('proy1', 'objetivo', CURRENT_TIMESTAMP, 'estado');
INSERT INTO PROYECTO VALUES('proy2', 'objetivo', CURRENT_TIMESTAMP, 'estado');
INSERT INTO PROYECTO VALUES('proy3', 'objetivo', CURRENT_TIMESTAMP, 'estado');

SELECT * FROM Proyecto;

INSERT INTO Requerimiento VALUES(1, 'RF(1)');
INSERT INTO Requerimiento VALUES(2, 'RNF(1)');
INSERT INTO Requerimiento VALUES(3, 'RF(2)');
INSERT INTO Requerimiento VALUES(4, 'RF(3)');
INSERT INTO Requerimiento VALUES(5, 'RNF(2)');
INSERT INTO Requerimiento VALUES(6, 'RNF(3)');

INSERT INTO Diseno VALUES('criterios', 'Unitaria', 'Caja Blanca', 'ambiente', 'procedimiento', CURRENT_TIMESTAMP, 'proposito', '207360523', 1);
INSERT INTO Diseno VALUES('criterios', 'Unitaria', 'Caja Blanca', 'ambiente', 'procedimiento', CURRENT_TIMESTAMP, 'proposito', '207360523', 2);
INSERT INTO Diseno VALUES('criterios', 'Unitaria', 'Caja Blanca', 'ambiente', 'procedimiento', CURRENT_TIMESTAMP, 'proposito', '207360523', 2);
INSERT INTO Diseno VALUES('criterios', 'Unitaria', 'Caja Blanca', 'ambiente', 'procedimiento', CURRENT_TIMESTAMP, 'proposito', '207360523', 1);

INSERT INTO CasoPrueba VALUES(1, 'proposito', 'entrada1 - whatev, entrada2- whatev', 'resultado esperado', 'flujocentral', 1);
INSERT INTO CasoPrueba VALUES(2, 'proposito', 'entrada1 - whatev, entrada2- whatev', 'resultado esperado', 'flujocentral', 1);

SELECT * FROM CasoPrueba;

INSERT INTO Ejecuciones VALUES(1, CURRENT_TIMESTAMP, 'incidencias', '207360523', 1, 1);
INSERT INTO Ejecuciones VALUES(2, CURRENT_TIMESTAMP, 'incidencias', '207360523', 4, 1);
INSERT INTO Ejecuciones VALUES(3, CURRENT_TIMESTAMP, 'incidencias', '207360523', 2, 2);
INSERT INTO Ejecuciones VALUES(4, CURRENT_TIMESTAMP, 'incidencias', '207360523', 3, 2);