USE [master]
GO
/****** Object:  Database [Incidentes]    Script Date: 18/11/2021 19:12:35 ******/
CREATE DATABASE [Incidentes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Incidentes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Incidentes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Incidentes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Incidentes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Incidentes] SET COMPATIBILITY_LEVEL = 140
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
ALTER DATABASE [Incidentes] SET QUERY_STORE = OFF
GO
USE [Incidentes]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 18/11/2021 19:12:35 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Incidentes]    Script Date: 18/11/2021 19:12:35 ******/
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
	[Duracion] [int] NOT NULL,
 CONSTRAINT [PK_Incidentes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proyectos]    Script Date: 18/11/2021 19:12:35 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProyectoUsuario]    Script Date: 18/11/2021 19:12:35 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tareas]    Script Date: 18/11/2021 19:12:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tareas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Costo] [float] NOT NULL,
	[Duracion] [int] NOT NULL,
	[ProyectoId] [int] NOT NULL,
 CONSTRAINT [PK_Tareas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 18/11/2021 19:12:35 ******/
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
	[ValorHora] [float] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211023160700_duracion_incidente_valorhora_usuario', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211024032346_tareas_y_lista_de_tareas_a_proyecto', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211031220651_proyecto_id_en_tarea', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211111024633_se_quita_usuarioid_de_incidente', N'5.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211117030613_costos_con_double', N'5.0.10')
GO
SET IDENTITY_INSERT [dbo].[Incidentes] ON 

INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [Duracion]) VALUES (39, N'tester', N'tester', N'tester', 1, 0, 12, 20)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [Duracion]) VALUES (40, N'tester2', N'tttttttttttttttt', N'1.2', 2, 29, 12, 10)
INSERT [dbo].[Incidentes] ([Id], [Nombre], [Descripcion], [Version], [EstadoIncidente], [DesarrolladorId], [ProyectoId], [Duracion]) VALUES (41, N'Nuevo Incidenteee', N'Descripcion de un incidente', N'1.2', 1, 0, 12, 0)
SET IDENTITY_INSERT [dbo].[Incidentes] OFF
GO
SET IDENTITY_INSERT [dbo].[Proyectos] ON 

INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (12, N'Antel Arena')
INSERT [dbo].[Proyectos] ([Id], [Nombre]) VALUES (15, N'Proyecto 16')
SET IDENTITY_INSERT [dbo].[Proyectos] OFF
GO
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (29, 12)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (30, 12)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (29, 15)
INSERT [dbo].[ProyectoUsuario] ([AsignadosId], [proyectosId]) VALUES (30, 15)
GO
SET IDENTITY_INSERT [dbo].[Tareas] ON 

INSERT [dbo].[Tareas] ([Id], [Nombre], [Costo], [Duracion], [ProyectoId]) VALUES (1, N'tarea1', 100, 1, 12)
INSERT [dbo].[Tareas] ([Id], [Nombre], [Costo], [Duracion], [ProyectoId]) VALUES (2, N'tarea2', 150, 1, 15)
INSERT [dbo].[Tareas] ([Id], [Nombre], [Costo], [Duracion], [ProyectoId]) VALUES (4, N'juciane', 100, 5, 12)
INSERT [dbo].[Tareas] ([Id], [Nombre], [Costo], [Duracion], [ProyectoId]) VALUES (5, N'asdasasd', 100, 2, 15)
INSERT [dbo].[Tareas] ([Id], [Nombre], [Costo], [Duracion], [ProyectoId]) VALUES (7, N'eeeeeee', 20, 5, 12)
SET IDENTITY_INSERT [dbo].[Tareas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario], [ValorHora]) VALUES (1, N'admin', N'admin', N'admin', N'admin', N'falonso@armada.mil.uy', N'k6Aatu@#rFzPijn$5XyVrhg$n5*67KHl', 0, 0)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario], [ValorHora]) VALUES (29, N'desarrollador', N'desarrollador', N'desarrollador', N'desarrollador', N'desarrollador@desarrollador.com', N'aaaaaaaaaaa', 1, 20)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario], [ValorHora]) VALUES (30, N'tester', N'tester', N'tester', N'testertester', N'tester@gmail.com', N'k6Aatu1#rFzPijn$5XyVrhg$n5*67KHl', 2, 2)
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [Nombre], [Apellido], [Contrasenia], [Email], [Token], [RolUsuario], [ValorHora]) VALUES (31, N'usuario1', N'usuario1', N'usuario1', N'usuario1', N'usuario1@gmail.com', NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [IX_Incidentes_Id]    Script Date: 18/11/2021 19:12:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Incidentes_Id] ON [dbo].[Incidentes]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Incidentes_ProyectoId]    Script Date: 18/11/2021 19:12:35 ******/
CREATE NONCLUSTERED INDEX [IX_Incidentes_ProyectoId] ON [dbo].[Incidentes]
(
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Proyectos_Id]    Script Date: 18/11/2021 19:12:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Proyectos_Id] ON [dbo].[Proyectos]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProyectoUsuario_proyectosId]    Script Date: 18/11/2021 19:12:35 ******/
CREATE NONCLUSTERED INDEX [IX_ProyectoUsuario_proyectosId] ON [dbo].[ProyectoUsuario]
(
	[proyectosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tareas_Id]    Script Date: 18/11/2021 19:12:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tareas_Id] ON [dbo].[Tareas]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tareas_ProyectoId]    Script Date: 18/11/2021 19:12:35 ******/
CREATE NONCLUSTERED INDEX [IX_Tareas_ProyectoId] ON [dbo].[Tareas]
(
	[ProyectoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Usuarios_Id]    Script Date: 18/11/2021 19:12:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_Id] ON [dbo].[Usuarios]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Usuarios_NombreUsuario]    Script Date: 18/11/2021 19:12:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuarios_NombreUsuario] ON [dbo].[Usuarios]
(
	[NombreUsuario] ASC
)
WHERE ([NombreUsuario] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [ProyectoId]
GO
ALTER TABLE [dbo].[Incidentes] ADD  DEFAULT ((0)) FOR [Duracion]
GO
ALTER TABLE [dbo].[Tareas] ADD  DEFAULT ((0)) FOR [ProyectoId]
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
ALTER TABLE [dbo].[Tareas]  WITH CHECK ADD  CONSTRAINT [FK_Tareas_Proyectos_ProyectoId] FOREIGN KEY([ProyectoId])
REFERENCES [dbo].[Proyectos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tareas] CHECK CONSTRAINT [FK_Tareas_Proyectos_ProyectoId]
GO
USE [master]
GO
ALTER DATABASE [Incidentes] SET  READ_WRITE 
GO
