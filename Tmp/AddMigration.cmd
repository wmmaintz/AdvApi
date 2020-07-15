@ECHO OFF
CLS
ECHO OFF
REM Execute the following: tmp\ADDMIGRATION "Initial Creation" LITE NEW
REM Set the variables to be used in this Script
REM

REM PROJ is the name of the project
SET PROJ=AdvAPI
REM PROJDIR is the name of the project directory
SET PROJDIR=C:\src\Core\%PROJ%
REM CMD is the name of the executed command
SET CMD=
REM MN is the name of the migration change
SET MN=
REM DC is the Data Context Name
SET DC=ApiContext
REM DCDir is the Data Context Directory Name
REM Set the default DCDir
SET DCDir=Migrations/SqlSrvrMigrations
REM DatabaseFile is the name of the database file in the Data directory
SET DatabaseFile=NotYetAssigned

ECHO.
IF %1.. == .. GOTO CMDSYNTAX
SET MN=%1
ECHO Adding an EF Migration named [%MN%] in Project %PROJ%
ECHO.
IF %2.. == LITE.. (
	SET DCDir=Migrations\SqliteMigrations
	SET DatabaseFile=Data\SqlLiteAdvDb.sqlite
	GOTO STARTMIGRATION
	)
IF %2.. == CE..   (
	SET DCDir=Migrations\SqlCeMigrations
	SET DatabaseFile=Data\SqlCeAdvDb.sdf
	GOTO STARTMIGRATION
	)
IF %2.. == PROJ.. (
	SET DCDir=Migrations\SqlProjMigrations
	GOTO STARTMIGRATION
	)

:STARTMIGRATION
ECHO ON
IF %3.. == NEW.. (
	ECHO.
	IF EXIST %DatabaseFile% (
		ECHO Deleting %DatabaseFile%
		SET CMD=dotnet ef database drop --force
		ECHO.
		ECHO Executing: %CMD%
		ECHO.
		CALL %CMD%
	)
	IF EXIST %DatabaseFile% DEL %DatabaseFile%
	IF EXIST %DatabaseFile% GOTO NOTDELETED
	IF EXIST %DCDir% RMDIR /S /Q %DCDir%
	ECHO.
)
ECHO OFF

CD %PROJDIR%
SET CMD=dotnet-ef migrations add %MN% --context %DC% --output-dir %DCDir% --msbuildprojectextensionspath %PROJ%
ECHO.
ECHO Executing: %CMD%
ECHO.
%CMD%
ECHO.

ECHO ERRORLEVEL = [%ERRORLEVEL%]
IF ERRORLEVEL 1 GOTO ENDPROG

:UPDDB
SET CMD=dotnet ef database update --context %DC% --msbuildprojectextensionspath %PROJ%
ECHO.
ECHO Executing: %CMD%
ECHO.
%CMD%
ECHO.

GOTO ENDPROG

:NOTDELETED
@ECHO OFF
ECHO.
ECHO ERROR: THE DATABASEFILE [%DatabaseFile%] COULD NOT BE DELETED
ECHO.
GOTO ENDPROG

:CMDSYNTAX
ECHO Command Syntax:
ECHO.
ECHO ADDMIGRATION NameOfChange [DbType]
ECHO        WHERE NameOfChange is the name of the change to be applied to the database.
ECHO          AND DbType is an optional argument indicating the database type
ECHO              I.E. LITE = SqlLite database
ECHO                or CE = SQL Compact 4.0 database
ECHO                or PROJ =  (localdb)\ProjectsV13
ECHO                or Blank = ABCS21\MSSQLEXPRESS
ECHO.


:ENDPROG
ECHO.
ECHO End of Migration Script
ECHO.
SET PROJ=
SET CMD=
SET MN=
SET DC=
SET DCDir=
ECHO ON