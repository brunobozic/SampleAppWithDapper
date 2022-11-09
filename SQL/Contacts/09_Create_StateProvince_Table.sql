USE [ContactManager]
GO

IF OBJECT_ID('dbo.StatesOrProvinces', 'U') IS NOT NULL
    DROP TABLE [dbo].[StatesOrProvinces];
GO

CREATE TABLE [dbo].[StatesOrProvinces](
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
 CONSTRAINT [C_PK_state_prov_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StatesOrProvinces] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[StatesOrProvinces] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedUtc]
GO

ALTER TABLE [dbo].[StatesOrProvinces]  WITH CHECK ADD  CONSTRAINT [CHK_no_numeric_stateprov_desc] CHECK  ((NOT [Description] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[StatesOrProvinces] CHECK CONSTRAINT [CHK_no_numeric_stateprov_desc]
GO

ALTER TABLE [dbo].[StatesOrProvinces]  WITH CHECK ADD  CONSTRAINT [CHK_no_numeric_stateprov_name] CHECK  ((NOT [Name] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[StatesOrProvinces] CHECK CONSTRAINT [CHK_no_numeric_stateprov_name]
GO

