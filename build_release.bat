@echo off
setlocal
title Fusion Client - Build
echo === Fusion Client Windows Release ===
where dotnet >nul 2>nul
if errorlevel 1 (
 echo .NET 8 SDK fehlt. Bitte .NET 8 SDK installieren.
 pause
 exit /b 1
)
dotnet publish FusionClient\FusionClient.csproj -c Release -r win-x64 --self-contained true ^
 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true ^
 /p:EnableCompressionInSingleFile=true -o publish
if errorlevel 1 goto failed
where ISCC.exe >nul 2>nul
if errorlevel 1 (
 echo.
 echo App wurde gebaut. Inno Setup 6 installieren und Installer\FusionClient.iss
 echo mit Inno Setup Compiler kompilieren.
 echo Einzeldatei-App: publish\Fusion Client.exe
 pause
 exit /b 0
)
ISCC.exe Installer\FusionClient.iss
if errorlevel 1 goto failed
echo.
echo FERTIG: release\FusionClient-Setup.exe
pause
exit /b 0
:failed
echo Build fehlgeschlagen. Bitte die Fehlermeldung pruefen.
pause
exit /b 1
