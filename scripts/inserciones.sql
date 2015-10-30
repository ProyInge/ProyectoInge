use g4inge;

INSERT INTO Usuario VALUES(
'207360523', 'Emmanuel', 'Arias', 'Soto', 'emma@gmail.com', 'emma', '123', 'A', 'Lider', 0, null, CURRENT_TIMESTAMP
);


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

INSERT INTO Requerimiento VALUES(
'RF-001','Requerimiento 1'
);

INSERT INTO Requerimiento VALUES(
'RF-002','Requerimiento 2'
);

INSERT INTO Requerimiento VALUES(
'RF-003','Requerimiento 3'
);


INSERT INTO Diseno VALUES(
'criterio', 'Del Sistema', 'Caja Blanca', 'ambiente', 'procedimiento', CURRENT_TIMESTAMP, 'proposito', 207400774, 1
);

INSERT INTO DisenoRequerimiento VALUES(
1, 'RF-002'
);