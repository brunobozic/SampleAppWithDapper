USE [ContactManager]
GO

IF OBJECT_ID('dbo.Countries', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO

CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ISO] [char](3) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[NiceName] [nvarchar](50) NULL,
	[ISO3] [char](3) NULL,
	[Numcode] [int] NULL,
	[Phonecode] [int] NULL,
	[Description] [nvarchar](50) NULL,
	[CountryCode] [varchar](4) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedUtc] [datetimeoffset](7) NOT NULL,
	[ModifiedUtc] [datetimeoffset](7) NULL,
	[DeletedUtc] [datetimeoffset](7) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [C_PK_country_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Countries] ADD  DEFAULT (NULL) FOR [ISO]
GO

ALTER TABLE [dbo].[Countries] ADD  DEFAULT (NULL) FOR [ISO3]
GO

ALTER TABLE [dbo].[Countries] ADD  DEFAULT (NULL) FOR [Numcode]
GO

ALTER TABLE [dbo].[Countries] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Countries] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedUtc]
GO

ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [CHK_no_numeric_country_desc] CHECK  ((NOT [Description] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [CHK_no_numeric_country_desc]
GO


