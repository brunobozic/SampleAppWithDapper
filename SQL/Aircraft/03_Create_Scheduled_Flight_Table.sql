CREATE TABLE ScheduledFlight
   (
       Id int not null IDENTITY(1,1) CONSTRAINT pk_ScheduledFlight_Id PRIMARY KEY,
       FlightNumber nvarchar(10) not null,
       DepartureAirportId int not null,
       DepartureHour int not null,
       DepartureMinute int not null,
       ArrivalAirportId int not null,
       ArrivalHour int not null,
       ArrivalMinute int not null,
       IsSundayFlight bit not null,
       IsMondayFlight bit not null,
       IsTuesdayFlight bit not null,
       IsWednesdayFlight bit not null,
       IsThursdayFlight bit not null,
       IsFridayFlight bit not null,
       IsSaturdayFlight bit not null,
       CreatedUtc DATETIMEOFFSET not null DEFAULT SYSDATETIMEOFFSET(),
       CreatedBy nvarchar(40) not null ,
       ModifiedUtc DATETIMEOFFSET null,
       ModifiedBy nvarchar(40) null ,
       IsDeleted bit DEFAULT 0, 
       DeletedUtc DATETIMEOFFSET,
       DeletedBy nvarchar(40),
       RowVer ROWVERSION,
       CONSTRAINT FK_ScheduledFlight_DepartureAirport FOREIGN KEY (DepartureAirportId)     
            REFERENCES Airport (Id),
        CONSTRAINT FK_ScheduledFlight_ArrivalAirport FOREIGN KEY (ArrivalAirportId)     
            REFERENCES Airport (Id)
   )