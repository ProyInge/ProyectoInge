use g4inge

CREATE TABLE Proyecto(
	id INT IDENTITY(1,1), 
	nombre VARCHAR (150) UNIQUE,
	objetivo VARCHAR (200), 
	fechaAsignacion DATE, 
	estado VARCHAR(30),
	
	CONSTRAINT PK_Proyecto PRIMARY KEY (id)
);

CREATE TABLE OficinaUsuaria(
	id INT IDENTITY(1,1),
	representante VARCHAR(150),
	nombre VARCHAR (100) UNIQUE,
	correo VARCHAR(100),
	idProyecto INT,

	CONSTRAINT PK_OficinaUsuaria PRIMARY KEY (id),
	CONSTRAINT FK_ProyectoOficina FOREIGN KEY (idProyecto)
	REFERENCES Proyecto(id) 
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE TelefonoOficina(
	numero INT,
	idCliente INT,

	CONSTRAINT PK_TelefonoOficina  PRIMARY KEY (numero, idCliente),
	CONSTRAINT FK_TelefonoOficina FOREIGN KEY (idCliente)
	REFERENCES OficinaUsuaria(id)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

CREATE TABLE Usuario(
	idRH INT IDENTITY(1,1),
	cedula INT UNIQUE, 
	pNombre VARCHAR(50),
	pApellido VARCHAR(50),
	sApellido VARCHAR(50),
	correo VARCHAR(100),
	nomUsuario VARCHAR(20) UNIQUE,
	contrasena VARCHAR(30),
	perfil CHAR,
	rol VARCHAR(30),
	sesionActiva BIT
	DEFAULT 0,
	idProy INT 
	DEFAULT null,
	fechaModif DATE,

	CONSTRAINT PK_Usuario PRIMARY KEY (idRH),
	CONSTRAINT FK_UsuarioProyecto FOREIGN KEY (idProy) 
	REFERENCES Proyecto(id)
);

CREATE TABLE TelefonoUsuario(
	numero INT,
	cedula INT,

	CONSTRAINT PK_TelefonoUsuario PRIMARY KEY(numero,cedula),
	CONSTRAINT FK_CedulaTelefono FOREIGN KEY (cedula) 
	REFERENCES Usuario(cedula) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE
);


CREATE TABLE Requerimiento(
	id VARCHAR (20),
	nombre VARCHAR (100),

	CONSTRAINT PK_Requerimiento PRIMARY KEY(id)
);


CREATE TABLE Diseno(
	id INT IDENTITY(1,1),
	criterios VARCHAR (400),
	nivel VARCHAR (20),
	tipoPrueba VARCHAR (20),
	tecnica VARCHAR (20),
	ambiente VARCHAR (50),
	procedimiento VARCHAR (200),
	fecha date,
	proposito VARCHAR (100),
	responsable INT,
	idProy INT,

	CONSTRAINT PK_Diseno PRIMARY KEY(id),
	CONSTRAINT FK_Proyecto FOREIGN KEY (idProy) 
	REFERENCES Proyecto(id) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE,
	CONSTRAINT FK_Responsable FOREIGN KEY (responsable) 
	REFERENCES Usuario(cedula) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE
	
);


CREATE TABLE Referencia(
	idDise INT,
	idReq VARCHAR (20),

	CONSTRAINT PK_Referecia PRIMARY KEY(idDise,idReq),
	CONSTRAINT FK_idDiseno FOREIGN KEY (idDise) 
	REFERENCES Diseno(id) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE,
	CONSTRAINT FK_idRequerimiento FOREIGN KEY (idReq) 
	REFERENCES Requerimiento(id) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE
);



CREATE TABLE CasoPrueba (
	id INT,
	proposito VARCHAR (200),
	tipoEntrada VARCHAR (50),
	nombreEntrada VARCHAR (100),
	resultadoEsperado VARCHAR (100),
	flujoCentral VARCHAR (200),
	idDise INT,

	CONSTRAINT PK_CasoPrueba PRIMARY KEY(id, idDise),
	CONSTRAINT FK_Diseno FOREIGN KEY (idDise) 
	REFERENCES Diseno(id) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE,
);

CREATE TABLE Asociado(
	idCaso INT,
	idReq VARCHAR (20),
	idDise INT,

	CONSTRAINT PK_Asociado PRIMARY KEY(idCaso,idReq),
	CONSTRAINT FK_idCaso FOREIGN KEY (idCaso, idDise) 
	REFERENCES CasoPrueba(id,idDise) 
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	CONSTRAINT FK_idRequer FOREIGN KEY (idReq) 
	REFERENCES Requerimiento(id) 
	ON DELETE CASCADE 
	ON UPDATE CASCADE
);

SELECT * FROM Usuario
SELECT * FROM Proyecto
SELECT * FROM OficinaUsuaria
SELECT * FROM TelefonoOficina

DROP TABLE TelefonoOficina;
DROP TABLE TelefonoUsuario;
DROP TABLE OficinaUsuaria;
DROP TABLE Asociado
DROP TABLE Referencia
DROP TABLE CasoPrueba
DROP TABLE Requerimiento 
DROP TABLE Diseno
DROP TABLE Usuario;
DROP TABLE Proyecto;