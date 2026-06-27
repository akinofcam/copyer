@echo off
REM Copyer installation script for Windows
REM This script adds Copyer to the Windows PATH for global command access

setlocal enabledelayedexpansion

REM Get the directory where this script is located
set SCRIPT_DIR=%~dp0

REM Check if running with admin privileges
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo.
    echo ERROR: This script requires administrator privileges.
    echo Please right-click and select "Run as administrator"
    pause
    exit /b 1
)

REM Find the executable
set EXE_PATH=%SCRIPT_DIR%publish\Copyer.exe
if not exist "%EXE_PATH%" (
    echo.
    echo ERROR: Copyer.exe not found at %EXE_PATH%
    echo Please ensure the publish folder exists and contains Copyer.exe
    pause
    exit /b 1
)

REM Add to PATH using setx
echo.
echo Adding Copyer to Windows PATH...
setx PATH "%SCRIPT_DIR%publish;!PATH!" /m

if %errorLevel% equ 0 (
    echo.
    echo SUCCESS! Copyer has been installed globally.
    echo.
    echo You can now use the 'copy' command from any directory:
    echo   copy C:\path\to\directory
    echo.
    echo NOTE: Open a new Command Prompt or PowerShell window for the changes to take effect.
    echo.
) else (
    echo.
    echo ERROR: Failed to add Copyer to PATH
    pause
    exit /b 1
)

pause
