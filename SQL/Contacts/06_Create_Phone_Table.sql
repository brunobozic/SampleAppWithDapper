IF OBJECT_ID('dbo.Phone', 'U') IS NOT NULL
    DROP TABLE [dbo].[Phone];
GO

create table Person.Phone
(
    PhoneID int identity
        constraint PK_Phone_PhoneID
            primary key,
    Name          Name                                           not null,
    ModifiedDate  datetime
        constraint DF_Phone_ModifiedDate default getdate() not null
)
go

create unique index AK_Phone_Name
    on Person.Phone (Name)
go

