ECHO off

rem sqlcmd -S localhost -E -i segwaydbam.sql
sqlcmd -S localhost -E -i Northwind.sql
rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE
