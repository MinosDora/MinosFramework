@echo off
setlocal enabledelayedexpansion
echo /********************************************************************
echo  Start %0 %1 %2
echo ********************************************************************/

reg export "HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Unity" reg.$>nul
for /f "delims=" %%i in ('type reg.$ ^| findstr /i /c:"DisplayIcon" 2^>nul') do (
    set DI=%%~i
    set DI=!DI:"=!
    set DI=!DI:\\=\!
    set !DI!
)
del /s /q /f reg.$
if not defined DisplayIcon goto:showerror
set UNITY_LOCATION=%DisplayIcon:~0,-9%

"%UNITY_LOCATION%Data\MonoBleedingEdge\bin\mono" "%UNITY_LOCATION%Data\MonoBleedingEdge\lib\mono\4.5\pdb2mdb.exe" "%1"

:showerror
pause	