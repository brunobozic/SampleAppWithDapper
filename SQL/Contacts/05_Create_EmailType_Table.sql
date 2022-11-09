IF OBJECT_ID('dbo.EmailType', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailType];
GO

create table Person.EmailType
(
    EmailTypeID int identity
        constraint PK_EmailType_EmailTypeID
            primary key,
    Name          Name                                           not null,
    ModifiedDate  datetime
        constraint DF_EmailType_ModifiedDate default getdate() not null
)
go

create unique index AK_EmailType_Name
    on Person.EmailType (Name)
go

