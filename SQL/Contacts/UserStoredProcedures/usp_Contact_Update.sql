USE [Dapper]
GO

IF OBJECT_ID('usp_Contact_Update', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[usp_Contact_Update];
GO

CREATE PROCEDURE [dbo].[usp_Contact_Update](@Id BIGINT,
                                            @FirstName varchar(50), -- start with 50, then expand if needed
                                            @LastName varchar(50), -- start with 50, then expand if needed
                                            @TelephoneNumber_Entry varchar(50), -- start with 50, then expand if needed
                                            @EMail varchar(50), -- start with 50, then expand if needed
                                            @Updater varchar(50) -- start with 50, then expand if needed
)
As
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF (ISNULL(@Id, -1) = -1)
            BEGIN
                RAISERROR ('Invalid parameter: Id be NULL or empty', 18, 0)
                RETURN
            END

        -- validate name, then surname, min length 1 char, no numerics
        IF (ISNULL(@FirstName, '') = '')
            BEGIN
                RAISERROR ('Invalid parameter: First name cannot be NULL or empty', 18, 0)
                RETURN
            END

        IF (ISNULL(@LastName, '') = '')
            BEGIN
                RAISERROR ('Invalid parameter: Last name cannot be NULL or empty', 18, 0)
                RETURN
            END
        -- validate email, use UDF for this

        IF LEN(ISNULL(@EMail, '')) > 0
            -- Is not empty and is not NULL
            BEGIN
                DECLARE
                    @EmailIsValid BIT
                SET @EmailIsValid = [dbo].[udf_IsValidEmail](@EMail)

                IF @EmailIsValid = 0
                    BEGIN
                        RAISERROR ('Invalid parameter: the provided e-mail address %s is not a valid e-mail address', 18, 0, @EMail)
                        RETURN
                    END
            END


        -- validate phone number (perhaps? maybe leave it be "as entered" by the user?)
        IF (ISNULL(@TelephoneNumber_Entry, '') = '')
            BEGIN
                RAISERROR ('Invalid parameter: Telephone Number cannot be NULL or empty', 18, 0)
                RETURN
            END

        -- check if the contact to be updated exists
        IF EXISTS(
                SELECT TOP 1 Id
                FROM [dbo].[Contacts]
                Where Id = @Id
            )
            BEGIN
                UPDATE [dbo].[Contacts]
                SET FirstName             = @FirstName,
                    LastName              = @LastName,
                    TelephoneNumber_Entry = @TelephoneNumber_Entry,
                    EMail                 = @EMail,
                    ModifiedBy            = @Updater,
                    ModifiedUtc           = SYSDATETIMEOFFSET()
                Where Id = @Id
                  AND IsDeleted = 0
            END
        ELSE
            BEGIN
                RAISERROR ('Invalid operation: Contact not found, nothing to update', 18, 0)
                RETURN
            END

    END TRY
    BEGIN CATCH
        DECLARE
            @ErrorMessage NVARCHAR(4000);
        DECLARE
            @ErrorSeverity INT;
        DECLARE
            @ErrorState INT;

        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();

        -- Use RAISERROR inside the CATCH block to return error  
        -- information about the original error that caused  
        -- execution to jump to the CATCH block.  
        RAISERROR (@ErrorMessage, -- Message text.  
            @ErrorSeverity, -- Severity.
            @ErrorState -- State.
            );
    END CATCH
END
