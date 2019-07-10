CREATE TABLE Airport 
   (
         Id int not null IDENTITY(1,1) CONSTRAINT pk_Airport_Id PRIMARY KEY,
         Code nvarchar(5) not null,
         City nvarchar(100) not null,
         ProvinceState nvarchar(100) not null,
         Country nvarchar(100) not NULL,
         CreatedUtc DATETIMEOFFSET not null DEFAULT SYSDATETIMEOFFSET(),
         CreatedBy nvarchar(40) not null ,
         ModifiedUtc DATETIMEOFFSET null ,
         ModifiedBy nvarchar(40) null ,
         IsDeleted bit DEFAULT 0, 
         DeletedUtc DATETIMEOFFSET,
         DeletedBy nvarchar(40),
         RowVer rowversion
   )