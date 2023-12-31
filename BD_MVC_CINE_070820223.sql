USE [master]
GO
/****** Object:  Database [cines_mvc]    Script Date: 07/08/2023 21:19:38 ******/
CREATE DATABASE [cines_mvc]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cines_mvc', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\cines_mvc.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'cines_mvc_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\cines_mvc_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [cines_mvc] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cines_mvc].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cines_mvc] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [cines_mvc] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [cines_mvc] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [cines_mvc] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [cines_mvc] SET ARITHABORT OFF 
GO
ALTER DATABASE [cines_mvc] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [cines_mvc] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [cines_mvc] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [cines_mvc] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [cines_mvc] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [cines_mvc] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [cines_mvc] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [cines_mvc] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [cines_mvc] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [cines_mvc] SET  DISABLE_BROKER 
GO
ALTER DATABASE [cines_mvc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [cines_mvc] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [cines_mvc] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [cines_mvc] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [cines_mvc] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [cines_mvc] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [cines_mvc] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [cines_mvc] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [cines_mvc] SET  MULTI_USER 
GO
ALTER DATABASE [cines_mvc] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [cines_mvc] SET DB_CHAINING OFF 
GO
ALTER DATABASE [cines_mvc] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [cines_mvc] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [cines_mvc] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [cines_mvc] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [cines_mvc] SET QUERY_STORE = ON
GO
ALTER DATABASE [cines_mvc] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [cines_mvc]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 07/08/2023 21:19:38 ******/
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
/****** Object:  Table [dbo].[Funciones]    Script Date: 07/08/2023 21:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funciones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[idSala] [int] NOT NULL,
	[idPelicula] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[CantClientes] [int] NOT NULL,
	[Costo] [float] NOT NULL,
 CONSTRAINT [PK_Funciones] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peliculas]    Script Date: 07/08/2023 21:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peliculas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Sinopsis] [varchar](200) NULL,
	[Duracion] [int] NOT NULL,
	[Poster] [varchar](max) NULL,
 CONSTRAINT [PK_Peliculas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sala]    Script Date: 07/08/2023 21:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sala](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Ubicacion] [varchar](100) NULL,
	[Capacidad] [int] NOT NULL,
 CONSTRAINT [PK_Sala] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UF]    Script Date: 07/08/2023 21:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UF](
	[idUsuario] [int] NOT NULL,
	[idFuncion] [int] NOT NULL,
	[cantidadCompra] [int] NOT NULL,
 CONSTRAINT [PK_UF] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC,
	[idFuncion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 07/08/2023 21:19:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[DNI] [varchar](50) NOT NULL,
	[Mail] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IntentosFallidos] [int] NULL,
	[Bloqueado] [bit] NULL,
	[Credito] [float] NULL,
	[FechaNacimiento] [date] NOT NULL,
	[EsAdmin] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230713002456_PrimeraMig', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230714232540_Segunda', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230714234835_Nueva', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230714235336_Nueva2', N'7.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230714235454_Nueva3', N'7.0.9')
GO
SET IDENTITY_INSERT [dbo].[Funciones] ON 

INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (1, 1, 1, CAST(N'2023-08-07' AS Date), 1, 1500)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (3, 2, 2, CAST(N'2023-07-17' AS Date), 1, 1650)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (7, 3, 3, CAST(N'2023-07-19' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (8, 4, 4, CAST(N'2023-07-19' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (9, 5, 5, CAST(N'2023-07-19' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (10, 10, 10, CAST(N'2023-07-21' AS Date), 0, 1600)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (11, 10, 7, CAST(N'2023-07-27' AS Date), 0, 1500)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (12, 4, 17, CAST(N'2023-07-30' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (13, 2, 1, CAST(N'2023-07-28' AS Date), 4, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (14, 3, 1, CAST(N'2023-07-28' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (15, 4, 1, CAST(N'2023-07-28' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (16, 5, 1, CAST(N'2023-07-28' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (17, 10, 2, CAST(N'2023-07-28' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (18, 5, 1, CAST(N'2023-07-29' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (19, 5, 1, CAST(N'2023-07-30' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (20, 1, 1, CAST(N'2023-09-24' AS Date), 3, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (21, 1, 1, CAST(N'2023-08-30' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (22, 2, 1, CAST(N'2023-08-30' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (23, 3, 1, CAST(N'2023-08-30' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (24, 4, 1, CAST(N'2023-08-30' AS Date), 1, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (25, 1, 2, CAST(N'2023-08-30' AS Date), 1, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (26, 2, 2, CAST(N'2023-08-30' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (27, 3, 2, CAST(N'2023-08-30' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (28, 4, 2, CAST(N'2023-08-30' AS Date), 0, 1400)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (29, 5, 1, CAST(N'2023-08-30' AS Date), 0, 1400)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (30, 10, 1, CAST(N'2023-08-30' AS Date), 0, 1400)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (31, 1, 1, CAST(N'2023-08-31' AS Date), 0, 1500)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (32, 10, 2, CAST(N'2023-08-30' AS Date), 0, 1500)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (33, 1, 1, CAST(N'2023-08-12' AS Date), 0, 1500)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (34, 2, 1, CAST(N'2023-08-12' AS Date), 0, 1500)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (35, 3, 1, CAST(N'2023-08-12' AS Date), 0, 1500)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (36, 4, 1, CAST(N'2023-08-12' AS Date), 0, 1550)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (37, 5, 1, CAST(N'2023-08-12' AS Date), 0, 1550)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (38, 10, 1, CAST(N'2023-08-12' AS Date), 0, 1550)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (39, 1, 3, CAST(N'2023-08-12' AS Date), 0, 1600)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (40, 2, 3, CAST(N'2023-08-12' AS Date), 0, 1650)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (41, 3, 3, CAST(N'2023-08-12' AS Date), 0, 1650)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (42, 4, 3, CAST(N'2023-08-12' AS Date), 0, 1650)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (43, 5, 3, CAST(N'2023-08-12' AS Date), 0, 1650)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (44, 10, 3, CAST(N'2023-08-12' AS Date), 0, 1650)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (45, 1, 9, CAST(N'2023-08-12' AS Date), 1, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (46, 2, 9, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (47, 3, 9, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (48, 4, 9, CAST(N'2023-08-12' AS Date), 0, 1330)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (49, 5, 9, CAST(N'2023-08-12' AS Date), 0, 1330)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (50, 10, 9, CAST(N'2023-08-12' AS Date), 0, 1330)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (53, 1, 2, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (54, 2, 2, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (55, 3, 2, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (56, 4, 2, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (57, 5, 2, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (58, 10, 2, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (59, 1, 4, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (60, 2, 4, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (61, 3, 4, CAST(N'2023-08-12' AS Date), 0, 1350)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (62, 4, 4, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (63, 5, 4, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (64, 10, 4, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (65, 1, 5, CAST(N'2023-08-12' AS Date), 0, 1350)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (66, 2, 5, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (67, 3, 5, CAST(N'2023-08-12' AS Date), 0, 1350)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (68, 4, 5, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (69, 5, 5, CAST(N'2023-08-12' AS Date), 0, 1350)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (70, 10, 5, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (71, 1, 6, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (72, 2, 6, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (73, 3, 6, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (74, 4, 6, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (75, 5, 6, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (76, 10, 6, CAST(N'2023-08-12' AS Date), 0, 1250)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (77, 1, 7, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (78, 2, 7, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (79, 3, 7, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (80, 4, 7, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (81, 5, 7, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (82, 10, 7, CAST(N'2023-08-12' AS Date), 0, 1300)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (83, 1, 8, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (84, 2, 8, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (85, 3, 8, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (86, 4, 8, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (87, 5, 8, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (88, 10, 8, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (89, 1, 10, CAST(N'2023-08-12' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (90, 2, 10, CAST(N'2023-08-12' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (91, 3, 10, CAST(N'2023-08-12' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (92, 4, 10, CAST(N'2023-08-12' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (93, 5, 10, CAST(N'2023-08-12' AS Date), 0, 900)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (94, 10, 10, CAST(N'2023-08-12' AS Date), 0, 950)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (95, 1, 11, CAST(N'2023-08-12' AS Date), 0, 800)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (96, 1, 11, CAST(N'2023-08-12' AS Date), 0, 800)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (99, 3, 11, CAST(N'2023-08-12' AS Date), 0, 800)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (100, 2, 11, CAST(N'2023-08-12' AS Date), 0, 800)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (101, 4, 11, CAST(N'2023-08-12' AS Date), 0, 800)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (102, 5, 11, CAST(N'2023-08-12' AS Date), 0, 800)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (103, 10, 11, CAST(N'2023-08-12' AS Date), 0, 800)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (104, 1, 12, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (105, 2, 12, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (106, 3, 12, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (107, 4, 12, CAST(N'2023-08-12' AS Date), 0, 1200)
GO
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (108, 5, 12, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (109, 10, 12, CAST(N'2023-08-12' AS Date), 0, 1200)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (110, 1, 17, CAST(N'2023-08-12' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (111, 2, 17, CAST(N'2023-08-12' AS Date), 0, 1100)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (112, 3, 17, CAST(N'2023-08-12' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (113, 4, 17, CAST(N'2023-08-12' AS Date), 0, 1000)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (114, 5, 17, CAST(N'2023-08-12' AS Date), 0, 1100)
INSERT [dbo].[Funciones] ([ID], [idSala], [idPelicula], [Fecha], [CantClientes], [Costo]) VALUES (115, 10, 17, CAST(N'2023-08-12' AS Date), 0, 1000)
SET IDENTITY_INSERT [dbo].[Funciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Peliculas] ON 

INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (1, N'Guardianes de la Galaxia 3', N'Aunque sigue afectado por la pérdida de Gamora, Peter Quill debe reunir a su equipo para defender el universo de una nueva amenaza o, en caso de fracasar, será el final de los Guardianes.', 120, N'https://static.voyalcine.net/Uploads/i2074.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (2, N'Super Mario Bros La Pelicula', N'Dos hermanos plomeros, Mario y Luigi, caen por las alcantarillas y llegan a un mundo subterráneo mágico', 130, N'https://static.voyalcine.net/Uploads/i1938.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (3, N'Oppenheimer', N'El filme cuenta la historia de Julius Robert Oppenheimer, el físico teórico de Nueva York que se convirtió en hombre clave durante el Proyecto Manhattan en la Segunda Guerra Mundial', 120, N'https://static.voyalcine.net/Uploads/i2316.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (4, N'Misión imposible: sentencia mortal-parte1', N'Ethan debe detener a una inteligencia artificial que todas las potencias mundiales codician, la cual se ha vuelto tan poderosa que se rebeló contra sus creadores y ahora es una amenaza en sí misma.', 120, N'https://static.voyalcine.net/Uploads/i2277.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (5, N'Barbie', N'Después de ser expulsada de Barbieland por no ser una muñeca de aspecto perfecto, Barbie parte hacia el mundo humano para encontrar la verdadera felicidad.', 90, N'https://static.voyalcine.net/Uploads/i2278.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (6, N'La noche del demonio: la puerta roja', N'Nueve años después, Josh y Renai se han divorciado, Lorraine ha fallecido y Dalton ha ingresado en la universidad.', 90, N'https://static.voyalcine.net/Uploads/i2294.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (7, N'Indiana Jones y el dial del destino', N'En 1969, un Indiana Jones envejecido y derrotado por la vida regresa a la acción para evitar que la hija de uno de sus amigos venda un artilugio que permite viajar en el tiempo', 120, N'https://static.voyalcine.net/Uploads/i2262.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (8, N'Flash', N'Flash viaja a través del tiempo para evitar el asesinato de su madre, pero, sin saberlo, provoca una serie de cambios que originan la creación de un multiverso.', 140, N'https://static.voyalcine.net/Uploads/i2199.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (9, N'Elementos', N' una chica de fuego y un chico de agua descubren que, aunque la sociedad les diga lo contrario, tienen muchas cosas en común.', 90, N'https://static.voyalcine.net/Uploads/i2240.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (10, N'Transformers: el despertar de las bestias', N'Durante la década de 1990, los Maximals, Predacons y Terrorcons se unen a la batalla existente en la Tierra entre Autobots y Decepticons.', 120, N'https://static.voyalcine.net/Uploads/i2198.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (11, N'Mi querido monstruo', N'Bai Ze, el curandero de Kunlun, destruye una isla accidentalmente mientras trataba de encontrar la cura para una enfermedad terminal. ', 120, N'https://static.voyalcine.net/Uploads/i2296.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (12, N'Spiderman: a través del spider-verso', N'Después de reunirse con Gwen Stacy, el amigable vecino de tiempo completo de Brooklyn Spiderman, es lanzado a través del multiverso.', 150, N'https://static.voyalcine.net/Uploads/i2197.jpg')
INSERT [dbo].[Peliculas] ([ID], [Nombre], [Sinopsis], [Duracion], [Poster]) VALUES (17, N'Los Pitufos 2', N'Papá y el resto de los pitufos se reúnen con sus amigos humanos para rescatar a Pitufina de las garras del malvado mago Gargamel.', 100, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRe8-gC3WQjBlcj2EfQt2SfJy9fUVbgMd4ThIk15ioIono5uuYr')
SET IDENTITY_INSERT [dbo].[Peliculas] OFF
GO
SET IDENTITY_INSERT [dbo].[Sala] ON 

INSERT [dbo].[Sala] ([ID], [Ubicacion], [Capacidad]) VALUES (1, N'Flores', 100)
INSERT [dbo].[Sala] ([ID], [Ubicacion], [Capacidad]) VALUES (2, N'Palermo', 50)
INSERT [dbo].[Sala] ([ID], [Ubicacion], [Capacidad]) VALUES (3, N'Recoleta', 75)
INSERT [dbo].[Sala] ([ID], [Ubicacion], [Capacidad]) VALUES (4, N'Unicenter', 120)
INSERT [dbo].[Sala] ([ID], [Ubicacion], [Capacidad]) VALUES (5, N'Abasto', 300)
INSERT [dbo].[Sala] ([ID], [Ubicacion], [Capacidad]) VALUES (10, N'Rosario', 100)
SET IDENTITY_INSERT [dbo].[Sala] OFF
GO
INSERT [dbo].[UF] ([idUsuario], [idFuncion], [cantidadCompra]) VALUES (1, 1, 2)
INSERT [dbo].[UF] ([idUsuario], [idFuncion], [cantidadCompra]) VALUES (1, 3, 1)
INSERT [dbo].[UF] ([idUsuario], [idFuncion], [cantidadCompra]) VALUES (1, 13, 4)
INSERT [dbo].[UF] ([idUsuario], [idFuncion], [cantidadCompra]) VALUES (1, 20, 3)
INSERT [dbo].[UF] ([idUsuario], [idFuncion], [cantidadCompra]) VALUES (1, 24, 1)
INSERT [dbo].[UF] ([idUsuario], [idFuncion], [cantidadCompra]) VALUES (1, 25, 1)
INSERT [dbo].[UF] ([idUsuario], [idFuncion], [cantidadCompra]) VALUES (1, 45, 1)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (1, N'Obdulio R.', N'Gomez Bolaños J', N'11111111', N'm@mail', N'123456', 0, 0, 44351, CAST(N'1973-05-07' AS Date), 1)
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (2, N'ALe', N'Solo', N'12345678', N'a@a.com', N'123', 4, 1, 50, CAST(N'2023-07-31' AS Date), 0)
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (12, N'Prueba 123', N'Apellido', N'22222222', N'a@a.com.ar', N'123', 0, 1, 0, CAST(N'2023-07-31' AS Date), 1)
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (14, N'Ale', N'123', N'35156726', N'solo@gmail.com', N'123', 0, 0, 0, CAST(N'2023-07-26' AS Date), 0)
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (15, N'asasd', N'cdfgfdg', N'88888888', N'solo2@gmail.com', N'123', 0, 0, 0, CAST(N'2023-07-26' AS Date), 0)
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (17, N'egfdgd', N'fdgdgd', N'78788788', N'dsfsdf', N'123', 0, 1, 0, CAST(N'2023-07-26' AS Date), 0)
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (18, N'aasda', N'asdad', N'23234234', N'qqdqqd', N'rewqrew', 0, 0, 0, CAST(N'2023-07-26' AS Date), 0)
INSERT [dbo].[Usuarios] ([ID], [Nombre], [Apellido], [DNI], [Mail], [Password], [IntentosFallidos], [Bloqueado], [Credito], [FechaNacimiento], [EsAdmin]) VALUES (19, N'dfjsdfisdijnij', N'isdnf ijsdnfijsdnfij', N'33333333', N'msdmsdm', N'23232', 0, 0, 1111, CAST(N'2023-08-07' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [IX_Funciones_idPelicula]    Script Date: 07/08/2023 21:19:38 ******/
CREATE NONCLUSTERED INDEX [IX_Funciones_idPelicula] ON [dbo].[Funciones]
(
	[idPelicula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Funciones_idSala]    Script Date: 07/08/2023 21:19:38 ******/
CREATE NONCLUSTERED INDEX [IX_Funciones_idSala] ON [dbo].[Funciones]
(
	[idSala] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UF_idFuncion]    Script Date: 07/08/2023 21:19:38 ******/
CREATE NONCLUSTERED INDEX [IX_UF_idFuncion] ON [dbo].[UF]
(
	[idFuncion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [IntentosFallidos]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [Bloqueado]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [Credito]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [EsAdmin]
GO
ALTER TABLE [dbo].[Funciones]  WITH CHECK ADD  CONSTRAINT [FK_Funciones_Peliculas_idPelicula] FOREIGN KEY([idPelicula])
REFERENCES [dbo].[Peliculas] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Funciones] CHECK CONSTRAINT [FK_Funciones_Peliculas_idPelicula]
GO
ALTER TABLE [dbo].[Funciones]  WITH CHECK ADD  CONSTRAINT [FK_Funciones_Sala_idSala] FOREIGN KEY([idSala])
REFERENCES [dbo].[Sala] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Funciones] CHECK CONSTRAINT [FK_Funciones_Sala_idSala]
GO
ALTER TABLE [dbo].[UF]  WITH CHECK ADD  CONSTRAINT [FK_UF_Funciones_idFuncion] FOREIGN KEY([idFuncion])
REFERENCES [dbo].[Funciones] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UF] CHECK CONSTRAINT [FK_UF_Funciones_idFuncion]
GO
ALTER TABLE [dbo].[UF]  WITH CHECK ADD  CONSTRAINT [FK_UF_Usuarios_idUsuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuarios] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UF] CHECK CONSTRAINT [FK_UF_Usuarios_idUsuario]
GO
USE [master]
GO
ALTER DATABASE [cines_mvc] SET  READ_WRITE 
GO
