@ECHO off
cls
SET SQLCMD="C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE"


set /p SERVER=Please enter the sqlServerName:
SET UName="PrisonAdmin"
SET Pwd="ABCD"
SET FirstScriptsPath="Database\"
SET Tables="Tables\"
SET StoredProcedures=..\StoredProcedures\
SET Inserts=..\Inserts\
SET Types=..\Types\
SET LastScriptsPath=..\Constraints\
SET DB="Prison"
SET OUTPUT="E:\OutputLog.txt"

ECHO %date% %time% > %OUTPUT%


CD %FirstScriptsPath%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -i %%~f >> %OUTPUT%
)

CD %Tables%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %Types%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %StoredProcedures%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %Inserts%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)

CD %LastScriptsPath%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -U %UName% -P %Pwd% -i %%~f >> %OUTPUT%
)
