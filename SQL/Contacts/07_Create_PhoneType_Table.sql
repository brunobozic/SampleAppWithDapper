IF OBJECT_ID('dbo.PhoneType', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhoneType];
GO

create table Person.PhoneType
(
    PhoneTypeID int identity
        constraint PK_PhoneType_PhoneTypeID
            primary key,
    Name          Name                                           not null,
    ModifiedDate  datetime
        constraint DF_PhoneType_ModifiedDate default getdate() not null
)
go

create unique index AK_PhoneType_Name
    on Person.PhoneType (Name)
go

