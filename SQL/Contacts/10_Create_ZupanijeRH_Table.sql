USE [ContactManager]
GO

IF OBJECT_ID('dbo.ZupanijeRH', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZupanijeRH];
GO

CREATE TABLE [dbo].[ZupanijeRH](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedUtc] [datetimeoffset](7) NOT NULL,
	[ModifiedUtc] [datetimeoffset](7) NULL,
	[DeletedUtc] [datetimeoffset](7) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [C_PK_zupanija_id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ZupanijeRH] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[ZupanijeRH] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedUtc]
GO

ALTER TABLE [dbo].[ZupanijeRH]  WITH CHECK ADD  CONSTRAINT [CHK_no_numeric_zupa_desc] CHECK  ((NOT [Description] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[ZupanijeRH] CHECK CONSTRAINT [CHK_no_numeric_zupa_desc]
GO


