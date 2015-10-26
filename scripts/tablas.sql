use g4inge
 create table Proyecto(
	id int IDENTITY(1,1), 
	nombre varchar (150) unique,
	objetivo varchar (200), 
	fechaAsignacion date, 
	estado varchar(30),
	
	constraint PK_Proyecto  primary key (id)
);

create table OficinaUsuaria(
	id int IDENTITY(1,1),
	representante varchar(150),
	nombre varchar (100) unique,
	correo varchar(100),
	idProyecto int,

	constraint PK_OficinaUsuaria primary key (id),
	constraint FK_ProyectoOficina foreign key (idProyecto) references Proyecto(id) on delete cascade on update cascade
);

create table TelefonoOficina(
	numero int,
	idCliente int ,

	constraint PK_TelefonoOficina  primary key (numero, idCliente),
	constraint FK_TelefonoOficina foreign key (idCliente) references OficinaUsuaria(id) on delete cascade on update cascade
);

create table Usuario
(
idRH int IDENTITY(1,1),
cedula int unique, 
pNombre varchar(50),
pApellido varchar(50),
sApellido varchar(50),
correo varchar(100),
nomUsuario varchar(20) unique,
contrasena varchar(30),
perfil char,
rol varchar(30),
sesionActiva bit default 0,
idProy int default null,
fechaModif datetime,

constraint PK_Usuario primary key (idRH),
constraint FK_UsuarioProyecto foreign key (idProy) references Proyecto(id)
);

create table TelefonoUsuario
(
numero int,
cedula int,

constraint PK_TelefonoUsuario primary key(numero,cedula),
constraint FK_CedulaTelefono foreign key (cedula) references Usuario(cedula) on delete cascade on update cascade
);


create table Requerimiento(
id VARCHAR (20),
nombre VARCHAR (100),

constraint PK_Requerimiento primary key(id)
);


create table Diseno(
id int IDENTITY(1,1),
criterios VARCHAR (400),
nivel VARCHAR (20),
tipoPrueba VARCHAR (20),
tecnica VARCHAR (20),
ambiente VARCHAR (50),
procedimiento VARCHAR (200),
fecha date,
proposito VARCHAR (100),
responsable int,
idProy int,

constraint PK_Diseno primary key(id),
constraint FK_Responsable foreign key (responsable) references Usuario(cedula) on delete cascade on update cascade,
constraint FK_Proyecto foreign key (idProy) references Proyecto(id) on delete cascade on update cascade
);


create table Referencia(
idDise INT,
idReq VARCHAR (20),

constraint PK_Referecia primary key(idDise,idReq),
constraint FK_idDiseno foreign key (idDise) references Diseno(id) on delete cascade on update cascade,
constraint FK_idRequerimiento foreign key (idReq) references Requerimiento(id) on delete cascade on update cascade
);



create table CasoPrueba (
id int,
proposito VARCHAR (200),
tipoEntrada VARCHAR (50),
nombreEntrada VARCHAR (100),
resultadoEsperado VARCHAR (100),
flujoCentral VARCHAR (200),
idDise int,

constraint PK_CasoPrueba primary key(id, idDise),
constraint FK_Diseno foreign key (idDise) references Diseno(id) on delete cascade on update cascade,
);

create table Asociado(
idCaso INT,
idReq VARCHAR (20),
idDise int,

constraint PK_Asociado primary key(idCaso,idReq),
constraint FK_idCaso foreign key (idCaso, idDise) references CasoPrueba(id,idDise) on delete cascade on update cascade,
constraint FK_idRequer foreign key (idReq) references Requerimiento(id) on delete cascade on update cascade
);

insert into Usuario values(
'123456789','admin',null,null,null,'admin','admin','A','Administrador','0',null,CURRENT_TIMESTAMP
);

insert into Usuario values(
'111222333','Angelica','Fallas','Blanco','ange@ucr.ac.cr','ange','ange','A','Lider','0',null,CURRENT_TIMESTAMP
);

insert into Usuario values(
'115900358','Daniel','Muñoz','Rojas','daniel@gmail.com','daniel','daniel','A','Lider','0',null,CURRENT_TIMESTAMP
);

insert into Usuario values(
'304770347','David','Solano','Mora','david.solanomora@ucr.ac.cr','Davesmacer','Davesmacer','A','Tester','0',null, CURRENT_TIMESTAMP
);

insert into Usuario values(
'207400774','Jeffry','Venegas','Montoya','jeffvene@gmail.com','jeffvene','jeffvene','M','Tester','0',null, CURRENT_TIMESTAMP
);

select * from Usuario

drop table TelefonoOficina;
drop table TelefonoUsuario;
drop table OficinaUsuaria;
drop table Asociado
drop table Referencia
drop table CasoPrueba
drop table Requerimiento 
drop table Diseno
drop table Usuario;
drop table Proyecto;

