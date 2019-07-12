﻿USE [master]
GO
CREATE DATABASE [DapperIdentityDb]
GO
ALTER DATABASE [DapperIdentityDb] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    begin
        EXEC [DapperIdentityDb].[dbo].[sp_fulltext_database] @action = 'enable'
    end
GO
ALTER DATABASE [DapperIdentityDb] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DapperIdentityDb] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DapperIdentityDb] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DapperIdentityDb] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DapperIdentityDb] SET ARITHABORT OFF
GO
ALTER DATABASE [DapperIdentityDb] SET AUTO_CLOSE ON
GO
ALTER DATABASE [DapperIdentityDb] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DapperIdentityDb] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DapperIdentityDb] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DapperIdentityDb] SET CURSOR_DEFAULT GLOBAL
GO
ALTER DATABASE [DapperIdentityDb] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DapperIdentityDb] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DapperIdentityDb] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DapperIdentityDb] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DapperIdentityDb] SET ENABLE_BROKER
GO
ALTER DATABASE [DapperIdentityDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DapperIdentityDb] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DapperIdentityDb] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DapperIdentityDb] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DapperIdentityDb] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DapperIdentityDb] SET READ_COMMITTED_SNAPSHOT ON
GO
ALTER DATABASE [DapperIdentityDb] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DapperIdentityDb] SET RECOVERY SIMPLE
GO
ALTER DATABASE [DapperIdentityDb] SET MULTI_USER
GO
ALTER DATABASE [DapperIdentityDb] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DapperIdentityDb] SET DB_CHAINING OFF
GO
ALTER DATABASE [DapperIdentityDb] SET FILESTREAM ( NON_TRANSACTED_ACCESS = OFF )
GO
ALTER DATABASE [DapperIdentityDb] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO
ALTER DATABASE [DapperIdentityDb] SET DELAYED_DURABILITY = DISABLED
GO
ALTER DATABASE [DapperIdentityDb] SET QUERY_STORE = OFF
GO
USE [DapperIdentityDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [DapperIdentityDb]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 22-Sep-18 10:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims]
(
    [Id]         [int] IDENTITY (1,1) NOT NULL,
    [RoleId]     [nvarchar](450)      NOT NULL,
    [ClaimType]  [nvarchar](max)      NULL,
    [ClaimValue] [nvarchar](max)      NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 22-Sep-18 10:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles]
(
    [Id]               [nvarchar](450) NOT NULL,
    [Name]             [nvarchar](256) NULL,
    [NormalizedName]   [nvarchar](256) NULL,
    [ConcurrencyStamp] [nvarchar](max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 22-Sep-18 10:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims]
(
    [Id]         [int] IDENTITY (1,1) NOT NULL,
    [UserId]     [nvarchar](450)      NOT NULL,
    [ClaimType]  [nvarchar](max)      NULL,
    [ClaimValue] [nvarchar](max)      NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 22-Sep-18 10:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins]
(
    [LoginProvider]       [nvarchar](128) NOT NULL,
    [ProviderKey]         [nvarchar](128) NOT NULL,
    [ProviderDisplayName] [nvarchar](max) NULL,
    [UserId]              [nvarchar](450) NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED
        (
         [LoginProvider] ASC,
         [ProviderKey] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 22-Sep-18 10:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles]
(
    [UserId] [nvarchar](450) NOT NULL,
    [RoleId] [nvarchar](450) NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED
        (
         [UserId] ASC,
         [RoleId] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22-Sep-18 10:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users]
(
    [Id]                   [nvarchar](450)     NOT NULL,
    [UserName]             [nvarchar](256)     NULL,
    [NormalizedUserName]   [nvarchar](256)     NULL,
    [Email]                [nvarchar](256)     NULL,
    [NormalizedEmail]      [nvarchar](256)     NULL,
    [EmailConfirmed]       [bit]               NOT NULL,
    [PasswordHash]         [nvarchar](max)     NULL,
    [SecurityStamp]        [nvarchar](max)     NULL,
    [ConcurrencyStamp]     [nvarchar](max)     NULL,
    [PhoneNumber]          [nvarchar](max)     NULL,
    [PhoneNumberConfirmed] [bit]               NOT NULL,
    [TwoFactorEnabled]     [bit]               NOT NULL,
    [LockoutEnd]           [datetimeoffset](7) NULL,
    [LockoutEnabled]       [bit]               NOT NULL,
    [AccessFailedCount]    [int]               NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 22-Sep-18 10:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens]
(
    [UserId]        [nvarchar](450) NOT NULL,
    [LoginProvider] [nvarchar](128) NOT NULL,
    [Name]          [nvarchar](128) NOT NULL,
    [Value]         [nvarchar](max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED
        (
         [UserId] ASC,
         [LoginProvider] ASC,
         [Name] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleClaims_RoleId]    Script Date: 22-Sep-18 10:08:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId] ON [dbo].[RoleClaims]
    (
     [RoleId] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 22-Sep-18 10:08:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
    (
     [NormalizedName] ASC
        )
    WHERE ([NormalizedName] IS NOT NULL)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserClaims_UserId]    Script Date: 22-Sep-18 10:08:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId] ON [dbo].[UserClaims]
    (
     [UserId] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserLogins_UserId]    Script Date: 22-Sep-18 10:08:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId] ON [dbo].[UserLogins]
    (
     [UserId] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 22-Sep-18 10:08:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
    (
     [RoleId] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 22-Sep-18 10:08:09 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Users]
    (
     [NormalizedEmail] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 22-Sep-18 10:08:09 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Users]
    (
     [NormalizedUserName] ASC
        )
    WHERE ([NormalizedUserName] IS NOT NULL)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RoleClaims]
    WITH CHECK ADD CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId])
        REFERENCES [dbo].[Roles] ([Id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims]
    CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserClaims]
    WITH CHECK ADD CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users] ([Id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims]
    CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]
    WITH CHECK ADD CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users] ([Id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins]
    CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]
    WITH CHECK ADD CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId])
        REFERENCES [dbo].[Roles] ([Id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles]
    CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]
    WITH CHECK ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users] ([Id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles]
    CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserTokens]
    WITH CHECK ADD CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users] ([Id])
        ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens]
    CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [DapperIdentityDb] SET READ_WRITE
GO