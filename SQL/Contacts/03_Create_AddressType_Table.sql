create table Person.AddressType
(
    AddressTypeID int identity
        constraint PK_AddressType_AddressTypeID
            primary key,
    Name          Name                                           not null,
    rowguid       uniqueidentifier
        constraint DF_AddressType_rowguid default newid()        not null,
    ModifiedDate  datetime
        constraint DF_AddressType_ModifiedDate default getdate() not null
)
go

create unique index AK_AddressType_rowguid
    on Person.AddressType (rowguid)
go

create unique index AK_AddressType_Name
    on Person.AddressType (Name)
go

