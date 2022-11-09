USE [ContactManager]
GO

CREATE OR ALTER FUNCTION [dbo].[udf_IsValidEmail](
    @EmailAddr varchar(360) -- Email address to check
) RETURNS BIT -- 1 if @EmailAddr is a valid email address

AS
BEGIN
   DECLARE
        @AlphabetPlus VARCHAR(255)
        , @Max        INT -- Length of the address
        , @Pos        INT -- Position in @EmailAddr
        , @OK         BIT
    -- Is @EmailAddr OK
    -- Check basic conditions
    IF @EmailAddr IS NULL
        OR @EmailAddr NOT LIKE '[0-9a-zA-Z]%@__%.__%'
        OR @EmailAddr LIKE '%@%@%'
        OR @EmailAddr LIKE '%..%'
        OR @EmailAddr LIKE '%.@'
        OR @EmailAddr LIKE '%@.'
        OR @EmailAddr LIKE '%@%.-%'
        OR @EmailAddr LIKE '%@%-.%'
        OR @EmailAddr LIKE '%@-%'
        OR CHARINDEX(' ', LTRIM(RTRIM(@EmailAddr))) > 0
        RETURN (0)


    declare
        @AfterLastDot varchar(360);
    declare
        @AfterArobase varchar(360);
    declare
        @BeforeArobase varchar(360);
    declare
        @HasDomainTooLong bit=0;

    --Control des longueurs et autres incoherence
    set @AfterLastDot = REVERSE(SUBSTRING(REVERSE(@EmailAddr), 0, CHARINDEX('.', REVERSE(@EmailAddr))));
    if len(@AfterLastDot) not between 2 and 17
        RETURN (0);

    set @AfterArobase = REVERSE(SUBSTRING(REVERSE(@EmailAddr), 0, CHARINDEX('@', REVERSE(@EmailAddr))));
    if len(@AfterArobase) not between 2 and 255
        RETURN (0);

    select top 1 @BeforeArobase = value from string_split(@EmailAddr, '@');
    if len(@AfterArobase) not between 2 and 255
        RETURN (0);

    --Controle sous-domain pas plus grand que 63
    select top 1 @HasDomainTooLong = 1 from string_split(@AfterArobase, '.') where LEN(value) > 63
    if @HasDomainTooLong = 1
        return (0);

    --Control de la partie locale en detail
    SELECT @AlphabetPlus = 'abcdefghijklmnopqrstuvwxyz01234567890!#$%&‘*+-/=?^_`.{|}~'
         , @Max = LEN(@BeforeArobase)
         , @Pos = 0
         , @OK = 1


    WHILE @Pos < @Max AND @OK = 1 BEGIN
        SET @Pos = @Pos + 1
        IF @AlphabetPlus NOT LIKE '%' + SUBSTRING(@BeforeArobase, @Pos, 1) + '%'
            SET @OK = 0
    END

    if @OK = 0
        RETURN (0);

    --Control de la partie domaine en detail
    SELECT @AlphabetPlus = 'abcdefghijklmnopqrstuvwxyz01234567890-.'
         , @Max = LEN(@AfterArobase)
         , @Pos = 0
         , @OK = 1

    WHILE @Pos < @Max AND @OK = 1 BEGIN
        SET @Pos = @Pos + 1
        IF @AlphabetPlus NOT LIKE '%' + SUBSTRING(@AfterArobase, @Pos, 1) + '%'
            SET @OK = 0
    END

    if @OK = 0
        RETURN (0);

    return (1);
END