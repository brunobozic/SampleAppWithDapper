USE [ContactManager]
GO

IF OBJECT_ID('dbo.SifrarnikZupanija', 'U') IS NOT NULL
    DROP TABLE [dbo].[SifrarnikZupanija];
GO

CREATE TABLE [dbo].[SifrarnikZupanija] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [ZUPANIJA]     NVARCHAR (50) NULL,
    [PB]           INT           NULL,
    [POSTA]        INT           NULL,
    [GRAD]         NVARCHAR (50) NULL,
    [MJESTOOPCINA] NVARCHAR (50) NULL,
    [NASELJE]      NVARCHAR (50) NULL,
    [PostBroj]     INT           NULL
);
