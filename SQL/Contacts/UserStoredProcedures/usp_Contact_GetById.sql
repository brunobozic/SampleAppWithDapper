CREATE OR ALTER PROCEDURE [dbo].[usp_Contact_GetById](
    @Id int
)
As
BEGIN
    SELECT con.Id, con.FirstName, con.LastName, con.EMail, con.TelephoneNumber_Entry, con.CreatedUtc, con.CreatedBy, con.ModifiedUtc, con.ModifiedBy
    FROM dbo.Contacts con
    WHERE con.Id = @Id
      AND IsDeleted = 0
END