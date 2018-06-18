@echo off
cls
SET SQLCMD="C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE"


set /p SERVER=Please enter the sqlServerName:
SET UName="PrisonAdmin"
SET Pwd="ABCD"
SET ZeroPath="Prison.App.Data"
SET FirstScriptsPath="..\Database\"
SET Tables="Tables\"
SET StoredProcedures=..\StoredProcedures\
SET Inserts=..\Inserts\
SET Types=..\Types\
SET LastScriptsPath=..\Constraints\
SET DB="Prison"
SET OUTPUT="%~dp0\OutputLog.txt"

set targetFile=app.config


ECHO %date% %time% > %OUTPUT%

CD %ZeroPath%
@echo ^<connectionStrings^> >>%targetFile%
@echo ^<add name="PrisonConnection" connectionString="Data Source=%SERVER%;Initial Catalog=Prison;Persist Security Info=True;User ID=PrisonAdmin;Password=ABCD"/^> >>%targetFile%
@echo ^</connectionStrings^> >>%targetFile%
@echo ^</configuration^> >>%targetFile%

CD %FirstScriptsPath%

for %%f in (*.sql) do (
SQLCMD -S %SERVER% -i %%~f >> %OUTPUT%
)

CD %Tables%

for %%f in (*.sql) do (
SQLCMD -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %Types%

for %%f in (*.sql) do (
SQLCMD -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %StoredProcedures%

for %%f in (*.sql) do (
SQLCMD -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %Inserts%

for %%f in (*.sql) do (
SQLCMD -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %LastScriptsPath%

for %%f in (*.sql) do (
SQLCMD -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

@echo DataBase is created!
@echo off
pause

