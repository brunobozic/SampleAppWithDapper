USE [ContactManager]
GO

IF OBJECT_ID('dbo.Addresses', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO

CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[AddressTypeId] [int] NULL,
	[AddressLine1] [nvarchar](60) NOT NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[HouseNr] [int] NOT NULL,
	[Dodatak] [varchar](1) NULL,
	[MjestoRHId] [int] NULL,
	[CountryId] [int] NULL,
	[StateProvinceId] [int] NULL,
	[PostalCode] [varchar](6) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedUtc] [datetimeoffset](7) NOT NULL,
	[ModifiedUtc] [datetimeoffset](7) NULL,
	[DeletedUtc] [datetimeoffset](7) NULL,
	[CreatedBy] [int] NULL,
	[ModifiedBy] [int] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [C_PK_address_id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Addresses] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Addresses] ADD  DEFAULT (sysdatetimeoffset()) FOR [CreatedUtc]
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_to_address_types] FOREIGN KEY([AddressTypeId])
REFERENCES [dbo].[AddressTypes] ([Id])
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_to_address_types]
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_addresses_to_countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_addresses_to_countries]
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_addresses_to_statesOrProvinces] FOREIGN KEY([StateProvinceId])
REFERENCES [dbo].[StatesOrProvinces] ([Id])
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_addresses_to_statesOrProvinces]
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [CHK_no_numeric_addr_line1] CHECK  ((NOT [AddressLine1] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [CHK_no_numeric_addr_line1]
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [CHK_no_numeric_addr_line2] CHECK  ((NOT [AddressLine2] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [CHK_no_numeric_addr_line2]
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [CHK_no_numeric_housenr] CHECK  (([HouseNr] like '%[^A-Z]%'))
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [CHK_no_numeric_housenr]
GO