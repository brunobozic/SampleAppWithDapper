IF OBJECT_ID('dbo.Email', 'U') IS NOT NULL
    DROP TABLE [dbo].[Email];
GO

create table Person.Email
(
    EmailID int identity
        constraint PK_Email_EmailID
            primary key,
    Name          Name                                           not null,
    ModifiedDate  datetime
        constraint DF_Email_ModifiedDate default getdate() not null
)
go

create unique index AK_Email_Name
    on Person.Email (Name)
go

