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
cedula int, 
pNombre varchar(50),
pApellido varchar(50),
sApellido varchar(50),
correo varchar(100) unique,
nomUsuario varchar(20) unique,
contrasena varchar(30),
perfil char,
rol varchar(30),
sesionActiva bit default 0,
idProy int default null,

constraint PK_Usuario primary key (cedula),
constraint FK_UsuarioProyecto foreign key (idProy) references Proyecto(id)
);

create table TelefonoUsuario
(
numero int,
cedula int,

constraint PK_TelefonoUsuario primary key(numero),
constraint FK_CedulaTelefono foreign key (cedula) references Usuario(cedula) on delete cascade on update cascade
);


insert into Usuario values(
'123456789','admin',null,null,null,'admin','admin','A','Administrador','0',null
);

insert into Usuario values(
'111222333','Angelica','Fallas','Blanco','ange@ucr.ac.cr','ange','ange','A','Lider','0',null
);

insert into Usuario values(
'115900358','Daniel','Muñoz','Rojas','daniel@gmail.com','daniel','daniel','A','Lider','0',null
);

insert into Usuario values(
'304770347','David','Solano','Mora','david.solanomora@ucr.ac.cr','Davesmacer','Davesmacer','A','Lider','0',null
);

select * from Usuario

drop table TelefonoOficina;
drop table TelefonoUsuario;
drop table OficinaUsuaria;
drop table Usuario;
drop table Proyecto;