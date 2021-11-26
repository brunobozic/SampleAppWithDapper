USE [ContactManager]
GO

IF OBJECT_ID('usp_Contact_Delete', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[usp_Contact_Delete];
GO

CREATE PROCEDURE [dbo].[usp_Contact_Delete](@Id BIGINT,
                                            @Deleter varchar(50) -- start with 50, then expand if needed
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

  
        -- check if the contact to be deleted exists
        IF EXISTS(
                SELECT TOP 1 Id
                FROM [dbo].[Contacts]
                WHERE Id = @Id
            )
            BEGIN
                UPDATE [dbo].[Contacts]
                SET IsDeleted             = 1,
                    DeletedBy            = @Deleter,
                    DeletedUtc           = SYSDATETIMEOFFSET()
                WHERE Id = @Id
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
