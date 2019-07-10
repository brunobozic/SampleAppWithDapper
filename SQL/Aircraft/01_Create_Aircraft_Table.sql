CREATE TABLE Aircraft
    (
        Id int not null IDENTITY(1,1) CONSTRAINT pk_Aircraft_Id PRIMARY KEY,
        Manufacturer nvarchar(255),
        Model nvarchar(255),
        RegistrationNumber nvarchar(50),
        FirstClassCapacity int,
        RegularClassCapacity int,
        CrewCapacity int,
        ManufactureDate date,
        NumberOfEngines int,
        EmptyWeight int,
        MaxTakeoffWeight int,
        IsDeleted bit DEFAULT 0, 
        CreatedUtc DATETIMEOFFSET not null DEFAULT SYSDATETIMEOFFSET(),
        CreatedBy nvarchar(40) not null ,
        ModifiedUtc DATETIMEOFFSET null,
        ModifiedBy nvarchar(40) null ,
        DeletedUtc DATETIMEOFFSET,
        DeletedBy nvarchar(40),
        RowVer rowversion
    )