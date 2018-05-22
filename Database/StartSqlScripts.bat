@ECHO on

SET SQLCMD="C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE"
SET Disk=c:
SET FirstScriptsPath="\Prison\Database\"
SET SecondScriptsPath="\Prison\Database\Tables\"
SET ThirdScriptsPath=\Prison\Database\Tables\StoredProcedures\
SET FourthScriptsPath=\Prison\Database\Tables\Inserts\
SET LastScriptsPath=\Prison\Database\Tables\Constraints\
SET SERVER="DESKTOP-DVDAFAQ\SQLEXPRESS"
SET DB="Prison"
SET LOGIN="sa"
SET PASSWORD="pass"
SET OUTPUT="E:\OutputLog.txt"

ECHO %date% %time% > %OUTPUT%

%Disk%

CD %FirstScriptsPath%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -i %%~f >> %OUTPUT%
)

CD %SecondScriptsPath%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -i %%~f >> %OUTPUT%
)

CD %ThirdScriptsPath%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -i %%~f >> %OUTPUT%
)

CD %FourthScriptsPath%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -i %%~f >> %OUTPUT%
)

CD %LastScriptsPath%

for %%f in (*.sql) do (
%SQLCMD% -S %SERVER% -i %%~f >> %OUTPUT%
)

pause