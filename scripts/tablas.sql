 create table Proyecto(
	id int, 
	nombre varchar (150),
	objetivo varchar (200), 
	fechaAsignacion date, 
	estado char,
	
	constraint PK_Proyecto  primary key (id)
);

create table OficinaUsuaria(
	id int primary key,
	representante varchar(150),
	nombre varchar (100),
	correo varchar(100)
);

create table TelefonoOficina(
	numero int,
	idCliente int ,

	constraint PK_TelefonoOficina  primary key (numero, idCliente),
	constraint FK_TelefonoOficina foreign key (idCliente) references OficinaUsuaria(id)
};

create table Usuario
(
cedula int, 
pNombre varchar(50),
pApellido varchar(50),
sApellido varchar(50),
correo varchar(100),
nomUsuario varchar(20),
contrasena varchar(30),
perfil char,
rol varchar(30),
sesionActiva bit,
idProy int,

constraint PK_Usuario primary key (cedula),
constraint FK_UsuarioProyecto foreign key (idProy) references Proyecto(id)
);

create table TelefonoUsuario
(
numero int,
cedula int,

constraint PK_TelefonoUsuario primary key(numero),
constraint FK_CedulaTelefono foreign key (cedula) references Usuario(cedula)  
);


