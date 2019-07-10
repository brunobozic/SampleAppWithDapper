CREATE TABLE [dbo].[Contacts]
(
        Id int not null IDENTITY(1,1),
        FirstName nvarchar(255) not null ,
        LastName nvarchar(255) not null ,
        TelephoneNumber nvarchar(14) ,
        AccessCode INTEGER,
        AreaCode INTEGER,
        TelephoneNumber_Entry nvarchar(14) not null ,
        EMail nvarchar(50) null ,
        IsDeleted bit DEFAULT 0, 
        CreatedUtc DATETIMEOFFSET not null DEFAULT SYSDATETIMEOFFSET(),
        CreatedBy nvarchar(40) not null ,
        ModifiedUtc DATETIMEOFFSET null,
        ModifiedBy nvarchar(40) null ,
        DeletedUtc DATETIMEOFFSET,
        DeletedBy nvarchar(40),
        RowVer ROWVERSION,
        CONSTRAINT [C_PK_Contacts_I] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_area_code CHECK (AreaCode > 0);
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_area_code2 CHECK (AreaCode <= 99);
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_acceess_code CHECK (AccessCode > 0);
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_acceess_code2 CHECK (AccessCode < 999);
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_UserRegistrationUserEmail CHECK (EMail LIKE '[a-z,0-9,_,-]%@[a-z,0-9,_,-]%.[a-z][a-z]%');
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_name_len_min CHECK (DATALENGTH(FirstName) > 1)
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_lastname_len_min CHECK (DATALENGTH(LastName) > 1)
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_telephone CHECK (TelephoneNumber_Entry NOT LIKE '%[^0-9+-.]%')
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_name_no_numeric CHECK (FirstName NOT LIKE '%[^A-Z]%') 
ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_lastname_no_numeric CHECK (LastName NOT LIKE '%[^A-Z]%') 


GO

-- ALTER TABLE [dbo].[Contacts] ADD CONSTRAINT chk_email 
-- CHECK ( PATINDEX('%[^A-z0-9._-]%@%.%',[EMail]) > 0)

-- filtered index, provided we are expecting lots of nulls in email, so the searches for "is not null" would be a bit faster
CREATE NONCLUSTERED INDEX [C_NonDeleted_Contacts_I] 
  ON [dbo].[Contacts]
  ([FirstName] ASC, [LastName] ASC, [EMail])
  WHERE ([EMail] IS NOT NULL);
GO


-- covering index, columns (key - where part) nvarchars do
CREATE NONCLUSTERED INDEX [C_Name_Contacts_I] 
  ON [dbo].[Contacts]
  ([FirstName] ASC, [LastName] ASC, [IsDeleted]) -- where part (key)
  INCLUDE ([EMail], [TelephoneNumber_Entry], [CreatedUtc], [ModifiedUtc]); -- non key  varchar(max), nvarchar(max), varbinary(max)
GO  