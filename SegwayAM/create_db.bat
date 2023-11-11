ECHO off

sqlcmd -S localhost -E -i segwaydbam.sql

rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE
