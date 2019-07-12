CREATE
    OR
    ALTER PROCEDURE [dbo].[usp_Contact_Insert](@FirstName varchar(50), -- start with 50, then expand if needed
                                               @LastName varchar(50), -- start with 50, then expand if needed
                                               @TelephoneNumber_Entry varchar(50), -- start with 50, then expand if needed
                                               @EMail varchar(50), -- start with 50, then expand if needed
                                               @Creator varchar(50) -- start with 50, then expand if needed
)
As
    SET NOCOUNT ON

BEGIN
    BEGIN TRY

        IF (ISNULL(@FirstName, '') = '')
            BEGIN
                RAISERROR
                    ('Invalid parameter: First name cannot be NULL or empty', 18, 0)

            END
        IF (ISNULL(@LastName, '') = '')
            BEGIN
                RAISERROR
                    ('Invalid parameter: Last name cannot be NULL or empty', 18, 0)

            END


        IF LEN(ISNULL(@EMail, '')) > 0
            BEGIN
                DECLARE
                    @EmailIsValid BIT
                SET @EmailIsValid = [dbo].[udf_IsValidEmail](@EMail)
                IF @EmailIsValid = 0
                    BEGIN
                        RAISERROR
                            ('Invalid parameter: the provided e-mail address %s is not a valid e-mail address', 18, 0, @EMail)

                    END
            END


        IF
            (ISNULL(@TelephoneNumber_Entry, '') = '')
            BEGIN
                RAISERROR
                    ('Invalid parameter: Telephone Number cannot be NULL or empty', 18, 0)

            END

        DECLARE
            @INSERTEDID TABLE
                        (
                            ID INT
                        )

        INSERT INTO dbo.Contacts(FirstName, LastName, TelephoneNumber_Entry, EMail, CreatedUtc, CreatedBy)
            OUTPUT inserted.ID INTO @INSERTEDID (ID)
        VALUES (@FirstName, @LastName, @TelephoneNumber_Entry, @EMail, SYSDATETIMEOFFSET(), @Creator)

        SELECT *
        FROM dbo.Contacts
        WHERE ID IN (SELECT ID FROM @ INSERTEDID)

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
        RAISERROR
            (@ErrorMessage, -- Message text.
            @ErrorSeverity, -- Severity.
            @ErrorState -- State.
            );
    END CATCH
END
  
