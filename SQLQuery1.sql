USE [Dapper]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_Insert_Contact]
		@FirstName = N'Bruno',
		@LastName = N'Bozic',
		@TelephoneNumber_Entry = N'0994721471',
		@EMail = N'bruno.bozicgmail.com',
		@Creator = N'bbozic'

SELECT	@return_value as 'Return Value'

GO
