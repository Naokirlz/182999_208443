
/****** Object:  Database [Incidentes]    Script Date: 7/10/2021 22:31:33 ******/
CREATE DATABASE [Incidentes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Incidentes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Incidentes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Incidentes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Incidentes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Incidentes] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Incidentes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Incidentes] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Incidentes] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Incidentes] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Incidentes] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Incidentes] SET ARITHABORT OFF 
GO
ALTER DATABASE [Incidentes] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Incidentes] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Incidentes] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Incidentes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Incidentes] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Incidentes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Incidentes] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Incidentes] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Incidentes] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Incidentes] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Incidentes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Incidentes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Incidentes] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Incidentes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Incidentes] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Incidentes] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Incidentes] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Incidentes] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Incidentes] SET  MULTI_USER 
GO
ALTER DATABASE [Incidentes] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Incidentes] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Incidentes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Incidentes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Incidentes] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Incidentes] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Incidentes] SET QUERY_STORE = OFF
GO
USE [Incidentes]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/10/2021 22:31:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incidentes]    Script Date: 7/10/2021 22:31:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Incidentes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Version] [nvarchar](max) NULL,
	[EstadoIncidente] [int] NOT NULL,
	[DesarrolladorId] [int] NOT NULL,
	[ProyectoId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
 CONSTRAINT [PK_Incidentes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proyectos]    Script Date: 7/10/2021 22:31:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proyectos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
 CONSTRAINT [PK_Proyectos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectoUsuario]    Script Date: 7/10/2021 22:31:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoUsuario](
	[AsignadosId] [int] NOT NULL,
	[proyectosId] [int] NOT NULL,
 CONSTRAINT [PK_ProyectoUsuario] PRIMARY KEY CLUSTERED 
(
	[AsignadosId] ASC,
	[proyectosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 7/10/2021 22:31:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [nvarchar](450) NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[Contrasenia] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
	[RolUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210927004257_carga_inicial', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210927004602_prueba_adm', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210927004756_prueba_adm_back', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211002135939_cambio_en_clase_usuario', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211002140647_cambio_version_incidente_a_string', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211006002231_virtualProyectos', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211006002946_virtualProyectos2', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211006201746_referencia_incidente_usuario_creador', N'5.0.10')
GO
SET IDENTITY_INSERT [dbo].[Incidentes] ON 

INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (4, N'Nuevo Incidente 1', N'Descripcion de un incidente', N'1.2', 1, 0, 3, 0)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (10, N'Nuevo Incidente 19', N'Descripcion de un incidente', N'1.2', 2, 16, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (11, N'Nuevo Incidente 20', N'Descripcion de un incidente', N'1.2', 1, 0, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (12, N'Nuevo Incidente 21', N'Descripcion de un incidente', N'1.2', 1, 0, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (13, N'Nuevo Incidente 25', N'Descripcion de un incidente', N'1.2', 1, 0, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (14, N'Error en el envío de correo', N'El error se produce cuando el usuario no tiene un correo asignado', N'1.0', 1, 0, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (15, N'Error en el envío de correo 2', N'El error se produce cuando el usuario no tiene un correo asignado 2', N'1.0', 2, 0, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (16, N'Error en el envío de correo 9', N'El error se produce cuando el usuario no tiene un correo asignado.', N'1.0', 1, 0, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (17, N'Error en el envío de correo2 10', N'El error se produce cuando el usuario no tiene un correo asignado 2.', N'1.0', 1, 0, 4, 17)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [UsuarioId]) VALUES (18, N'Error en el envío de correo3 11', N'El error se produce cuando el usuario no tiene un correo asignado 2.', N'1.0', 2, 0, 4, 17)
SET IDENTITY_INSERT [dbo].[Incidentes] OFF
GO
SET IDENTITY_INSERT [dbo].[Proyectos] ON 

INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (2, N'Nuevo Proyecto 1')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (3, N'Nuevo Proyecto 2')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (4, N'Nuevo Proyecto 3')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (5, N'Nuevo Proyecto 9')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (6, N'Nuevo Proyecto 66')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (7, N'Nuevo Proyecto 86')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (8, N'Nuevo Proyecto 56')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (9, N'Nuevo Proyecto 76')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (10, N'Nuevo Proyecto 78')
SET IDENTITY_INSERT [dbo].[Proyectos] OFF
GO
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (3, 3)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (11, 3)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (5, 4)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (8, 4)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (16, 4)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (17, 4)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (1, N'federico', N'federico', N'alonso', N'password', N'falonso@armada.mil.uy', N'yfnZSsVKkaaaaaao5nYJWnBuM5$012WR', 0)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (2, N'martincosad1', N'Martin', N'Cosas', N'Casa#Blanca', N'martind1@gmail.com', NULL, 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (3, N'martincosat1', N'Martin', N'Cosas', N'Casa#Blanca', N'martint1@gmail.com', NULL, 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (4, N'martincosat2', N'Martin', N'Cosas', N'Casa#Blanca', N'martint2@gmail.com', NULL, 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (5, N'martincosat3', N'Martin', N'Cosas', N'Casa#Blanca', N'martint3@gmail.com', NULL, 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (6, N'martincosat4', N'Martin', N'Cosas', N'Casa#Blanca', N'martint4@gmail.com', NULL, 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (7, N'martincosat5', N'Martin', N'Cosas', N'Casa#Blanca', N'martint5@gmail.com', NULL, 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (8, N'martincosat6', N'Martin', N'Cosas', N'Casa#Blanca', N'martint6@gmail.com', N'm*153QLJM0yL4zmWK$a@TJ1FLha27Yz2', 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (9, N'martincosad2', N'Martin', N'Cosas', N'Casa#Blanca', N'martind2@gmail.com', NULL, 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (10, N'martincosad3', N'Martin', N'Cosas', N'Casa#Blanca', N'martind3@gmail.com', N'', 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (11, N'martincosad4', N'Martin', N'Cosas', N'Casa#Blanca', N'martind4@gmail.com', NULL, 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (12, N'martincosad5', N'Martin', N'Cosas', N'Casa#Blanca', N'martind5@gmail.com', NULL, 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (13, N'martincosad6', N'Martin', N'Cosas', N'Casa#Blanca', N'martind6@gmail.com', N'e%7z%*GCtLH8CWOs*2zcg2K1Ypo!bd@f', 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (14, N'martincosad7', N'Martin', N'Cosas', N'Casa#Blanca', N'martind7@gmail.com', NULL, 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (15, N'martincosat7', N'Martin', N'Cosas', N'Casa#Blanca', N'martint7@gmail.com', NULL, 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (16, N'martincosat55', N'Martin', N'Cosas', N'Casa#Blanca', N'martint55@gmail.com', N'iIPw*3heyM1Q&hn4^TTcWqjDWf7DCuow', 1)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario]) VALUES (17, N'martincosat66', N'Martin', N'Cosas', N'Casa#Blanca', N'martint66@gmail.com', N'Do*uEDc&lD1!3Sz*DrZOLX*sd*HURCC!', 2)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [IX_Incidentes_Id]    Script Date: 7/10/2021 22:31:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Incidentes_Id] ON [dbo].[Incidentes]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Incidentes_ProyectoId]    Script Date: 7/10/2021 22:31:34 ******/
CREATE NONCLUSTERED INDEX [IX_Incidentes_ProyectoId] ON [dbo].[Incidentes]
(
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Proyectos_Id]    Script Date: 7/10/2021 22:31:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Proyectos_Id] ON [dbo].[Proyectos]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProyectoUsuario_proyectosId]    Script Date: 7/10/2021 22:31:34 ******/
CREATE NONCLUSTERED INDEX [IX_ProyectoUsuario_proyectosId] ON [dbo].[ProyectoUsuario]
(
	[proyectosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Usuarios_Id]    Script Date: 7/10/2021 22:31:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_Id] ON [dbo].[Usuarios]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Usuarios_NombreUsuario]    Script Date: 7/10/2021 22:31:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_NombreUsuario] ON [dbo].[Usuarios]
(
	[NombreUsuario] ASC
)
WHERE ([NombreUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [ProyectoId]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [UsuarioId]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [RolUsuario]
GO
ALTER TABLE [dbo].[Incidentes]  WITH CHECK ADD  CONSTRAINT [FK_Incidentes_Proyectos_ProyectoId] FOREIGN KEY([ProyectoId])
REFERENCES [dbo].[Proyectos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Incidentes] CHECK CONSTRAINT [FK_Incidentes_Proyectos_ProyectoId]
GO
ALTER TABLE [dbo].[ProyectoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoUsuario_Proyectos_proyectosId] FOREIGN KEY([proyectosId])
REFERENCES [dbo].[Proyectos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProyectoUsuario] CHECK CONSTRAINT [FK_ProyectoUsuario_Proyectos_proyectosId]
GO
ALTER TABLE [dbo].[ProyectoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoUsuario_Usuarios_AsignadosId] FOREIGN KEY([AsignadosId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProyectoUsuario] CHECK CONSTRAINT [FK_ProyectoUsuario_Usuarios_AsignadosId]
GO
USE [master]
GO
ALTER DATABASE [Incidentes] SET  READ_WRITE 
GO
