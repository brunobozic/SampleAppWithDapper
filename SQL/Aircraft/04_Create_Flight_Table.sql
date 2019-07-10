CREATE TABLE Flight
   (
       Id int not null IDENTITY(1,1) CONSTRAINT pk_Flight_Id PRIMARY KEY,
       ScheduledFlightId int not null,
       Day DATE not null,
       ScheduledDeparture DATETIME2 not null,
       ActualDeparture DATETIME2,
       ScheduledArrival DATETIME2 not null,
       ActualArrival DATETIME2,
       CreatedUtc DATETIMEOFFSET not null DEFAULT SYSDATETIMEOFFSET(),
       CreatedBy nvarchar(40) not null ,
       ModifiedUtc DATETIMEOFFSET null,
       ModifiedBy nvarchar(40) null ,
       IsDeleted bit DEFAULT 0, 
       DeletedUtc DATETIMEOFFSET,
       DeletedBy nvarchar(40),
       RowVer ROWVERSION,
       CONSTRAINT FK_Flight_ScheduledFlight FOREIGN KEY (ScheduledFlightId)     
            REFERENCES ScheduledFlight (Id)
   )