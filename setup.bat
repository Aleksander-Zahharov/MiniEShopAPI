@echo off

:: Check if .NET SDK is installed
dotnet --version >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo .NET SDK is not installed. Please install it from https://dotnet.microsoft.com/download.
    exit /b 1
)

:: Restore .NET dependencies
echo Restoring .NET dependencies...
dotnet restore

:: Check if Node.js is installed
node --version >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo Node.js is not installed. Please install it from https://nodejs.org/.
    exit /b 1
)

:: Check if Docker is installed
docker --version >nul 2>&1
if %ERRORLEVEL% neq 0 (
    echo Docker is not installed. Please install it from https://www.docker.com/.
    exit /b 1
)

:: Build the project
echo Building the project...
dotnet build

:: Run database migrations
echo Applying database migrations...
dotnet ef database update --project MiniEShopAPI/MiniEShopAPI.csproj

:: Install Node.js dependencies (if applicable)
if exist package.json (
    echo Installing Node.js dependencies...
    npm install
)

echo Setup completed successfully.