USE [ContactManager]
GO

IF OBJECT_ID('dbo.Contacts', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO

CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactTypeId] [int] NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[TelephoneNumber] [nvarchar](14) NULL,
	[AccessCode] [int] NULL,
	[AreaCode] [int] NULL,
	[TelephoneNumber_Entry] [nvarchar](14) NOT NULL,
	[EMail] [nvarchar](50) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedUtc] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](40) NOT NULL,
	[ModifiedUtc] [datetimeoffset](7) NULL,
	[ModifiedBy] [nvarchar](40) NULL,
	[DeletedUtc] [datetimeoffset](7) NULL,
	[DeletedBy] [nvarchar](40) NULL,
	[RowVer] [timestamp] NOT NULL,
	[ContactAddressId] [int] NULL,
 CONSTRAINT [C_PK_Contacts_I] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contacts] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Contacts] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedUtc]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_access_code] CHECK  (([AccessCode]>(0)))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_access_code]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_access_code2] CHECK  (([AccessCode]<(999)))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_access_code2]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_area_code] CHECK  (([AreaCode]>(0)))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_area_code]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_area_code2] CHECK  (([AreaCode]<=(99)))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_area_code2]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_lastname_len_min] CHECK  ((datalength([LastName])>(1)))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_lastname_len_min]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_lastname_no_numeric] CHECK  ((NOT [LastName] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_lastname_no_numeric]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_name_len_min] CHECK  ((datalength([FirstName])>(1)))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_name_len_min]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_name_no_numeric] CHECK  ((NOT [FirstName] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_name_no_numeric]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [chk_UserRegistrationUserEmail] CHECK  (([EMail] like '[a-z,0-9,_,-]%@[a-z,0-9,_,-]%.[a-z][a-z]%'))
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [chk_UserRegistrationUserEmail]
GO


