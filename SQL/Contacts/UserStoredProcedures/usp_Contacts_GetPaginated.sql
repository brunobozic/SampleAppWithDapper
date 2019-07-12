CREATE OR ALTER PROCEDURE [dbo].[usp_Contacts_GetPaginated] @SearchTerm VARCHAR(50) = NULL,
                                                            @SortColumn VARCHAR(50) = NULL,
                                                            @SortOrder VARCHAR(50) = NULL,
                                                            @PageNumber INT = 1,
                                                            @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @StartRow INT
    DECLARE
        @EndRow INT
    DECLARE
        @SearchValue VARCHAR(50)
    DECLARE
        @FilteredCount INT

    SET @SortColumn = LOWER(ISNULL(@SortColumn, ''))
    SET @SortOrder = LOWER(ISNULL(@SortOrder, ''))
    SET @StartRow = (@PageNumber - 1) * @PageSize
    SET @EndRow = (@PageNumber * @PageSize) + 1;
    WITH CTEResult AS (
        SELECT ROW_NUMBER() OVER (ORDER BY
            CASE WHEN (@SortColumn = 'firstname' AND @SortOrder = 'asc') THEN FirstName END ASC,
            CASE WHEN (@SortColumn = 'firstname' AND @SortOrder = 'desc') THEN FirstName END DESC,

            CASE WHEN (@SortColumn = 'lastname' AND @SortOrder = 'asc') THEN LastName END ASC,
            CASE WHEN (@SortColumn = 'lastname' AND @SortOrder = 'desc') THEN LastName END DESC,

            CASE WHEN (@SortColumn = 'email' AND @SortOrder = 'asc') THEN Email END ASC,
            CASE WHEN (@SortColumn = 'email' AND @SortOrder = 'desc') THEN Email END DESC,

            CASE WHEN (@SortColumn = 'telephoneNumber_entry' AND @SortOrder = 'asc') THEN TelephoneNumber_Entry END ASC,
            CASE
                WHEN (@SortColumn = 'telephoneNumber_entry' AND @SortOrder = 'desc') THEN TelephoneNumber_Entry END DESC,

            CASE WHEN (@SortColumn = 'id' AND @SortOrder = 'asc') THEN Id END ASC,
            CASE WHEN (@SortColumn = 'id' AND @SortOrder = 'desc') THEN Id END DESC
            )                   AS RowNumber
             , COUNT(*) OVER () AS TotalCount
             , [Id]
             , [FirstName]
             , [LastName]
             , [TelephoneNumber_Entry]
             , [Email]
             , [IsDeleted]
             , [ModifiedUTC]
             , [ModifiedBy]
             , [CreatedUTC]
             , [CreatedBy]
        FROM [dbo].[Contacts]
        WHERE IsDeleted = 0
          AND (
                (ISNULL(@SearchTerm, '') = '' OR TelephoneNumber_Entry LIKE '%' + @SearchTerm + '%')
                OR (ISNULL(@SearchTerm, '') = '' OR FirstName LIKE '%' + @SearchTerm + '%')
                OR (ISNULL(@SearchTerm, '') = '' OR LastName LIKE '%' + @SearchTerm + '%')
                OR (ISNULL(@SearchTerm, '') = '' OR Email LIKE '%' + @SearchTerm + '%')
                OR (ISNULL(@SearchTerm, '') = '' OR Id LIKE '%' + @SearchTerm + '%')
            )
    )

    SELECT RowNumber,
           TotalCount,
           @StartRow,
           @EndRow,
           [Id],
           [FirstName],
           [LastName],
           [TelephoneNumber_Entry],
           [Email],
           [ModifiedUTC],
           [ModifiedBy],
           [CreatedUTC],
           [CreatedBy],
           [IsDeleted]
    FROM CTEResult
    WHERE RowNumber > @StartRow
      AND RowNumber < @EndRow
    ORDER BY RowNumber

    SELECT COUNT(*)
    FROM [dbo].[Contacts]
    WHERE IsDeleted = 0
      AND (
            (ISNULL(@SearchTerm, '') = '' OR TelephoneNumber_Entry LIKE '%' + @SearchTerm + '%')
            OR (ISNULL(@SearchTerm, '') = '' OR FirstName LIKE '%' + @SearchTerm + '%')
            OR (ISNULL(@SearchTerm, '') = '' OR LastName LIKE '%' + @SearchTerm + '%')
            OR (ISNULL(@SearchTerm, '') = '' OR Email LIKE '%' + @SearchTerm + '%')
            OR (ISNULL(@SearchTerm, '') = '' OR Id LIKE '%' + @SearchTerm + '%')
        )
END