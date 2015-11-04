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

INSERT INTO Requerimiento VALUES(1, 'req 1');
INSERT INTO Requerimiento VALUES(2, 'req 2');
INSERT INTO Requerimiento VALUES(3, 'req 3');

INSERT INTO Diseno VALUES('criterios', 'Unitaria', 'Caja Blanca', 'ambiente', 'procedimiento', CURRENT_TIMESTAMP, 'proposito', '207360523', 1);

INSERT INTO CasoPrueba VALUES(1, 'proposito', 'entrada1 - whatev, entrada2- whatev', 'resultado esperado', 'flujocentral', 4);
INSERT INTO CasoPrueba VALUES(2, 'proposito', 'entrada1 - whatev, entrada2- whatev', 'resultado esperado', 'flujocentral', 4);

select * from CasoPrueba
SELECT * FROM CasoPrueba;


update CasoPrueba set entrada = 'entrada1 - whatev , entrada2- whatev'