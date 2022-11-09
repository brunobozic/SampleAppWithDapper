USE [ContactManager]
GO

IF OBJECT_ID('dbo.ContactType', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactType];
GO

create table Person.ContactType
(
    ContactTypeID int identity
        constraint PK_ContactType_ContactTypeID
            primary key,
    Name          Name                                           not null,
    ModifiedDate  datetime
        constraint DF_ContactType_ModifiedDate default getdate() not null
)
go

create unique index AK_ContactType_Name
    on Person.ContactType (Name)
go

