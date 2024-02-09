USE [ContactManager]
GO

IF OBJECT_ID('usp_Unflatten_Codebook', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].usp_Unflatten_Codebook;
GO

CREATE OR ALTER PROCEDURE [dbo].usp_Unflatten_Codebook @Test VARCHAR(50) = ''
As
BEGIN

 -- zupanije  

IF OBJECT_ID(N'tempdb..#ZupTempLocation') IS NOT NULL
        BEGIN
        PRINT 'Truncating table #ZupTempLocation' 
        DROP TABLE #ZupTempLocation;
END

CREATE TABLE #ZupTempLocation(
    [Name] nvarchar(50) NOT NULL

)

INSERT INTO #ZupTempLocation
SELECT DISTINCT(CAST(sif.ZUPANIJA AS NVARCHAR)) AS [Name]
FROM dbo.SifrarnikZupanija sif
WHERE sif.ZUPANIJA IS NOT NULL
      
   IF OBJECT_ID(N'dbo.ZupanijeRH') IS NOT NULL
        BEGIN
        PRINT 'Truncating table ZupanijeRH' 
        --ALTER TABLE [dbo].[MjestaRH] DROP CONSTRAINT [FK_MjestaRH_to_ZupanijaRH];
        --ALTER TABLE [dbo].[MjestaRH] ADD CONSTRAINT [FK_MjestaRH_to_ZupanijaRH] FOREIGN KEY ([ZupanijaID]) REFERENCES [ZupanijeRH]([Id])
        TRUNCATE TABLE dbo.ZupanijeRH;
  END       

--SET IDENTITY_INSERT dbo.ZupanijeRH ON

INSERT INTO dbo.ZupanijeRH 
SELECT ztl.[Name], null, null, SYSDATETIME(), SYSDATETIME(), SYSDATETIME(), null, null, null FROM #ZupTempLocation ztl

SELECT * FROM #ZupTempLocation

--SET IDENTITY_INSERT dbo.ZupanijeRH OFF

-- endof zupanije


-- for each "ZUPANIJA" entry, get its current Id and start inserting distinct "GRAD" (+"POSTA", +"POSTANSKIBROJ") that share the same "ZUPANIJA"
DECLARE @RowCnt INT;
DECLARE @ZupID INT = 1;
DECLARE @ZUPANIJA1 NVARCHAR(250);
DECLARE @GRAD1 NVARCHAR(250);
DECLARE @POSTA1 NVARCHAR(250);
 
SELECT @RowCnt = COUNT(*) FROM dbo.ZupanijeRH;
 
IF OBJECT_ID(N'tempdb..#GradTempLocation') IS NOT NULL
        BEGIN
        PRINT 'Truncating table #GradTempLocation' 
        DROP TABLE #GradTempLocation;
END

 ---Declare the temporary table---
PRINT 'Creating table #GradTempLocation' 

CREATE TABLE #GradTempLocation(
    GRAD nvarchar(250) NOT NULL,
    POSTA nvarchar(250) NOT NULL,
    PB int NOT NULL,
    ZUPANIJAID int NOT NULL
)

IF OBJECT_ID(N'dbo.temp_log_gradovi') IS NOT NULL
BEGIN
PRINT 'Dropping table temp_log_gradovi' 
DROP TABLE dbo.temp_log_gradovi
END

PRINT 'Creating table temp_log_gradovi'

CREATE TABLE dbo.temp_log_gradovi (ID int identity(1,1) primary key not null, GRAD varchar(255), POSTA varchar(255), PB varchar(255), ZUPANIJAID int not null)


WHILE @ZupID <= @RowCnt
BEGIN

   SELECT @ZUPANIJA1 = [Name]
      FROM dbo.ZupanijeRH WHERE Id = @ZupID
 
   PRINT 'Current iteration of ZUPANIJA: ' + CAST(@ZupID AS NVARCHAR) + ' is ' + 
      CAST(@ZUPANIJA1 AS NVARCHAR)

 -- for each GRAD/POSTA/PB in the SifrarnikZupanija table (where GRAD is not NULL), insert a new record into "GradRH" with ZupanijaId, Grad, Posta, PostanskiBroj

-- gradovi
 PRINT 'Inserting into table #GradTempLocation' 
   INSERT INTO #GradTempLocation
     SELECT  DISTINCT(sifZup.GRAD), sifZup.POSTA, sifZup.PB, @ZupID from dbo.SifrarnikZupanija sifZup
     WHERE sifZup.ZUPANIJA = @ZUPANIJA1
     AND sifZup.GRAD IS NOT NULL

--SELECT * FROM #GradTempLocation

   SET @ZupID += 1
   PRINT 'New iteration'

END

 PRINT 'Inserting into table temp_log_gradovi' 
     INSERT INTO dbo.temp_log_gradovi
     SELECT  gtl.GRAD, gtl.POSTA, gtl.PB, gtl.ZUPANIJAID from #GradTempLocation gtl
 -- endof gradovi





  -- mjesto/općina
PRINT 'Starting mjesto općina iterations...'

DECLARE @RowCntM INT;
DECLARE @ItemID INT = 1;
DECLARE @GRAD2 NVARCHAR(250);
DECLARE @MJESTOOPCINA NVARCHAR(250);
DECLARE @POSTANSKIBROJ NVARCHAR(250);
DECLARE @POSTA2 NVARCHAR(250);
DECLARE @PB NVARCHAR(250);

SELECT @RowCntM = COUNT(*) FROM dbo.temp_log_gradovi;
 
IF OBJECT_ID(N'dbo.temp_log_mjestaopcine') IS NOT NULL
BEGIN
PRINT 'Dropping table temp_log_mjestaopcine' 
DROP TABLE dbo.temp_log_mjestaopcine
END

PRINT 'Creating table temp_log_mjestaopcine'
CREATE TABLE dbo.temp_log_mjestaopcine (RowID int identity(1,1) primary key not null, GRADID int, MJESTOOPCINA varchar(255), PostBroj varchar(255))
 
WHILE @ItemID <= @RowCntM
BEGIN

SELECT @GRAD2 = GRAD
FROM dbo.temp_log_gradovi WHERE Id = @ItemID
 
PRINT 'Current iteration: ' + CAST(@ItemID AS NVARCHAR) 
PRINT 'Current grad: ' + @GRAD2 
PRINT 'Inserting into table temp_log_mjestaopcine' 

INSERT INTO dbo.temp_log_mjestaopcine
--DECLARE @ItemID INT = 1;
--DECLARE @GRAD2 NVARCHAR(250);
--SELECT @GRAD2 = GRAD
--FROM dbo.temp_log_gradovi WHERE Id = @ItemID
--PRINT 'Current grad: ' + @GRAD2 
--SELECT DISTINCT(sz.MJESTOOPCINA) FROM dbo.SifrarnikZupanija sz
SELECT TOP 1 @ItemID AS GRADID, sz.MJESTOOPCINA, sz.PostBroj FROM dbo.SifrarnikZupanija sz
WHERE sz.MJESTOOPCINA = @GRAD2 

SET @ItemID += 1
PRINT 'New iteration'

END

 -- endof mjesto/općina








 -- start of naselja
PRINT 'Starting naselja iterations...'

DECLARE @RowCntN INT;
DECLARE @ItemNaseljaID INT = 1;
DECLARE @MJESTOOPCINA NVARCHAR(250);
DECLARE @POSTANSKIBROJ NVARCHAR(250);

SELECT @RowCntN = COUNT(*) FROM dbo.temp_log_mjestaopcine;

IF OBJECT_ID(N'dbo.temp_log_naselja') IS NOT NULL
BEGIN
PRINT 'Dropping table temp_log_naselja' 
DROP TABLE dbo.temp_log_naselja
END

PRINT 'Creating table temp_log_naselja'
CREATE TABLE dbo.temp_log_naselja (RowID int identity(1,1) primary key not null, MJESTOOPCINAID int, NASELJE varchar(255), PostBroj varchar(255))
 
WHILE @ItemNaseljaID <= @RowCntN
BEGIN

SELECT @MJESTOOPCINA = MJESTOOPCINA
FROM dbo.temp_log_mjestaopcine WHERE Id = @ItemNaseljaID
 
PRINT 'Current iteration: ' + CAST(@ItemNaseljaID AS NVARCHAR) 
PRINT 'Current mjesto opcina: ' + @MJESTOOPCINA 
PRINT 'Inserting into table temp_log_naselja' 

INSERT INTO dbo.temp_log_naselja
--DECLARE @ItemID INT = 1;
--DECLARE @GRAD2 NVARCHAR(250);
--SELECT @GRAD2 = GRAD
--FROM dbo.temp_log_gradovi WHERE Id = @ItemID
--PRINT 'Current grad: ' + @GRAD2 
--SELECT DISTINCT(sz.MJESTOOPCINA) FROM dbo.SifrarnikZupanija sz
SELECT @ItemNaseljaID AS MJESTOOPCINAID, sz.NASELJE, sz.PostBroj FROM dbo.SifrarnikZupanija sz
WHERE sz.MJESTOOPCINA = @MJESTOOPCINA 

SET @ItemID += 1
PRINT 'New iteration'

END




 -- endof naselja



END