create table Person.Address
(
    AddressID       int identity
        constraint PK_Address_AddressID
            primary key,
    AddressLine1    nvarchar(60)                             not null,
    AddressLine2    nvarchar(60),
    City            nvarchar(30)                             not null,
    StateProvinceID int                                      not null
        constraint FK_Address_StateProvince_StateProvinceID
            references Person.StateProvince,
    PostalCode      nvarchar(15)                             not null,
    SpatialLocation geography,
    rowguid         uniqueidentifier
        constraint DF_Address_rowguid default newid()        not null,
    ModifiedDate    datetime
        constraint DF_Address_ModifiedDate default getdate() not null
)
go

create unique index AK_Address_rowguid
    on Person.Address (rowguid)
go

create unique index IX_Address_AddressLine1_AddressLine2_City_StateProvinceID_PostalCode
    on Person.Address (AddressLine1, AddressLine2, City, StateProvinceID, PostalCode)
go

create index IX_Address_StateProvinceID
    on Person.Address (StateProvinceID)
go


