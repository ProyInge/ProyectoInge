USE [master]
GO
/****** Object:  Database [g4inge]    Script Date: 06/11/2015 10:57:31 ******/
CREATE DATABASE [g4inge]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'g4inge', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.ECCIBDISW\MSSQL\DATA\g4inge.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'g4inge_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.ECCIBDISW\MSSQL\DATA\g4inge_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [g4inge] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [g4inge].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [g4inge] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [g4inge] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [g4inge] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [g4inge] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [g4inge] SET ARITHABORT OFF 
GO
ALTER DATABASE [g4inge] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [g4inge] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [g4inge] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [g4inge] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [g4inge] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [g4inge] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [g4inge] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [g4inge] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [g4inge] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [g4inge] SET  DISABLE_BROKER 
GO
ALTER DATABASE [g4inge] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [g4inge] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [g4inge] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [g4inge] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [g4inge] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [g4inge] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [g4inge] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [g4inge] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [g4inge] SET  MULTI_USER 
GO
ALTER DATABASE [g4inge] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [g4inge] SET DB_CHAINING OFF 
GO
ALTER DATABASE [g4inge] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [g4inge] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [g4inge] SET DELAYED_DURABILITY = DISABLED 
GO
USE [g4inge]
GO
/****** Object:  Table [dbo].[CasoPrueba]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CasoPrueba](
	[id] [varchar](50) NOT NULL,
	[proposito] [varchar](200) NULL,
	[entrada] [varchar](200) NULL,
	[resultadoEsperado] [varchar](100) NULL,
	[flujoCentral] [varchar](200) NULL,
	[idDise] [int] NOT NULL,
 CONSTRAINT [PK_CasoPrueba] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[idDise] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CasoRequerimiento]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CasoRequerimiento](
	[idCaso] [varchar](50) NOT NULL,
	[idReq] [varchar](20) NOT NULL,
	[idDise] [int] NULL,
 CONSTRAINT [PK_Asociado] PRIMARY KEY CLUSTERED 
(
	[idCaso] ASC,
	[idReq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Diseno]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Diseno](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[criterios] [varchar](400) NULL,
	[nivel] [varchar](20) NULL,
	[tecnica] [varchar](20) NULL,
	[ambiente] [varchar](50) NULL,
	[procedimiento] [varchar](200) NULL,
	[fecha] [date] NULL,
	[proposito] [varchar](100) NULL,
	[responsable] [int] NULL,
	[idProy] [int] NULL,
 CONSTRAINT [PK_Diseno] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DisenoRequerimiento]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DisenoRequerimiento](
	[idDise] [int] NOT NULL,
	[idReq] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Referecia] PRIMARY KEY CLUSTERED 
(
	[idDise] ASC,
	[idReq] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OficinaUsuaria]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OficinaUsuaria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[representante] [varchar](150) NULL,
	[nombre] [varchar](100) NULL,
	[correo] [varchar](100) NULL,
	[idProyecto] [int] NULL,
 CONSTRAINT [PK_OficinaUsuaria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proyecto]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proyecto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](150) NULL,
	[objetivo] [varchar](200) NULL,
	[fechaAsignacion] [date] NULL,
	[estado] [varchar](30) NULL,
 CONSTRAINT [PK_Proyecto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Requerimiento]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Requerimiento](
	[id] [varchar](20) NOT NULL,
	[nombre] [varchar](100) NULL,
 CONSTRAINT [PK_Requerimiento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TelefonoOficina]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TelefonoOficina](
	[numero] [int] NOT NULL,
	[idCliente] [int] NOT NULL,
 CONSTRAINT [PK_TelefonoOficina] PRIMARY KEY CLUSTERED 
(
	[numero] ASC,
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TelefonoUsuario]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TelefonoUsuario](
	[numero] [int] NOT NULL,
	[cedula] [int] NOT NULL,
 CONSTRAINT [PK_TelefonoUsuario] PRIMARY KEY CLUSTERED 
(
	[numero] ASC,
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[idRH] [int] IDENTITY(1,1) NOT NULL,
	[cedula] [int] NULL,
	[pNombre] [varchar](50) NULL,
	[pApellido] [varchar](50) NULL,
	[sApellido] [varchar](50) NULL,
	[correo] [varchar](100) NULL,
	[nomUsuario] [varchar](20) NULL,
	[contrasena] [varchar](30) NULL,
	[perfil] [char](1) NULL,
	[rol] [varchar](30) NULL,
	[sesionActiva] [bit] NULL DEFAULT ((0)),
	[idProy] [int] NULL DEFAULT (NULL),
	[fechaModif] [date] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[idRH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[nomUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CasoPrueba]  WITH CHECK ADD  CONSTRAINT [FK_Diseno] FOREIGN KEY([idDise])
REFERENCES [dbo].[Diseno] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CasoPrueba] CHECK CONSTRAINT [FK_Diseno]
GO
ALTER TABLE [dbo].[CasoRequerimiento]  WITH CHECK ADD  CONSTRAINT [FK_idCaso] FOREIGN KEY([idCaso], [idDise])
REFERENCES [dbo].[CasoPrueba] ([id], [idDise])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CasoRequerimiento] CHECK CONSTRAINT [FK_idCaso]
GO
ALTER TABLE [dbo].[CasoRequerimiento]  WITH CHECK ADD  CONSTRAINT [FK_idRequer] FOREIGN KEY([idReq])
REFERENCES [dbo].[Requerimiento] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CasoRequerimiento] CHECK CONSTRAINT [FK_idRequer]
GO
ALTER TABLE [dbo].[Diseno]  WITH CHECK ADD  CONSTRAINT [FK_Proyecto] FOREIGN KEY([idProy])
REFERENCES [dbo].[Proyecto] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Diseno] CHECK CONSTRAINT [FK_Proyecto]
GO
ALTER TABLE [dbo].[Diseno]  WITH CHECK ADD  CONSTRAINT [FK_Responsable] FOREIGN KEY([responsable])
REFERENCES [dbo].[Usuario] ([cedula])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Diseno] CHECK CONSTRAINT [FK_Responsable]
GO
ALTER TABLE [dbo].[DisenoRequerimiento]  WITH CHECK ADD  CONSTRAINT [FK_idDiseno] FOREIGN KEY([idDise])
REFERENCES [dbo].[Diseno] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DisenoRequerimiento] CHECK CONSTRAINT [FK_idDiseno]
GO
ALTER TABLE [dbo].[DisenoRequerimiento]  WITH CHECK ADD  CONSTRAINT [FK_idRequerimiento] FOREIGN KEY([idReq])
REFERENCES [dbo].[Requerimiento] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DisenoRequerimiento] CHECK CONSTRAINT [FK_idRequerimiento]
GO
ALTER TABLE [dbo].[OficinaUsuaria]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoOficina] FOREIGN KEY([idProyecto])
REFERENCES [dbo].[Proyecto] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OficinaUsuaria] CHECK CONSTRAINT [FK_ProyectoOficina]
GO
ALTER TABLE [dbo].[TelefonoOficina]  WITH CHECK ADD  CONSTRAINT [FK_TelefonoOficina] FOREIGN KEY([idCliente])
REFERENCES [dbo].[OficinaUsuaria] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TelefonoOficina] CHECK CONSTRAINT [FK_TelefonoOficina]
GO
ALTER TABLE [dbo].[TelefonoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_CedulaTelefono] FOREIGN KEY([cedula])
REFERENCES [dbo].[Usuario] ([cedula])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TelefonoUsuario] CHECK CONSTRAINT [FK_CedulaTelefono]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioProyecto] FOREIGN KEY([idProy])
REFERENCES [dbo].[Proyecto] ([id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_UsuarioProyecto]
GO
/****** Object:  StoredProcedure [dbo].[cerrarSesion]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cerrarSesion]
	@nombre varchar(20)
AS
	BEGIN
		UPDATE Usuario SET sesionActiva = 0 WHERE nomUsuario = @nombre;
	END
GO
/****** Object:  StoredProcedure [dbo].[iniciarSesion]    Script Date: 06/11/2015 10:57:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[iniciarSesion]
    @nombre varchar(20), 
    @contra varchar(30) 
AS
	IF( SELECT COUNT(*) FROM Usuario WHERE contrasena = @contra AND nomUsuario = @nombre ) = 1
	BEGIN
		IF(SELECT sesionActiva FROM Usuario WHERE nomUsuario = @nombre) = 0
		BEGIN
			UPDATE Usuario SET sesionActiva = 1 WHERE nomUsuario = @nombre;
			SELECT 0
		END
		ELSE
		BEGIN
			-- else sesion activa
			SELECT 1
		END
	END
	ELSE
	BEGIN
	--else datos incorrectos
		SELECT -1
	END



GO
USE [master]
GO
ALTER DATABASE [g4inge] SET  READ_WRITE 
GO
