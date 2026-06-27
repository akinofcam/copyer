# Copyer Installer for Windows

# ============================================================================
# Copyer - Professional Directory Cloning Tool
# Installation Script for Windows
# ============================================================================

# Detect if running in admin mode
function Test-Administrator {
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
    return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

# Greeting and System Detection
Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║          Welcome to Copyer Installation Wizard              ║" -ForegroundColor Cyan
Write-Host "║     Professional Directory Cloning Tool for Windows         ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""
Write-Host "🔍 System Detection:" -ForegroundColor Yellow
$OS = [System.Environment]::OSVersion.Platform
$Architecture = [System.Runtime.InteropServices.RuntimeInformation]::ProcessArchitecture
$DotNetVersion = dotnet --version

Write-Host "   OS: Windows" -ForegroundColor Green
Write-Host "   Architecture: $Architecture" -ForegroundColor Green
Write-Host "   .NET Version: $DotNetVersion" -ForegroundColor Green

# Check for .NET installation
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host ""
    Write-Host "❌ Error: .NET SDK is not installed!" -ForegroundColor Red
    Write-Host "   Please install .NET 10.0 or later from: https://dotnet.microsoft.com/download" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "✅ .NET SDK found - proceeding with installation" -ForegroundColor Green
Write-Host ""

# Determine download URL based on architecture
$Arch = if ($Architecture -eq "X64") { "x64" } elseif ($Architecture -eq "Arm64") { "arm64" } else { "x64" }
$AssetName = "copyer-$Arch.zip"

Write-Host "📥 Downloading Copyer..." -ForegroundColor Yellow

try {
    # Get latest release from GitHub
    $ApiUrl = "https://api.github.com/repos/akinofcam/copyer/releases/latest"
    $Release = Invoke-RestMethod -Uri $ApiUrl -ErrorAction Stop
    
    $DownloadUrl = $Release.assets | Where-Object { $_.name -match $AssetName } | Select-Object -First 1 -ExpandProperty browser_download_url
    
    if (-not $DownloadUrl) {
        # Fallback: try generic name
        $DownloadUrl = $Release.assets | Select-Object -First 1 -ExpandProperty browser_download_url
    }
    
    if (-not $DownloadUrl) {
        throw "No release assets found"
    }
    
    Write-Host "   Release: $($Release.tag_name)" -ForegroundColor Green
    Write-Host "   Downloading from: $DownloadUrl" -ForegroundColor Cyan
    
    $TempPath = "$env:TEMP\copyer-install"
    $ZipPath = "$TempPath\copyer.zip"
    
    # Create temp directory
    if (-not (Test-Path $TempPath)) {
        New-Item -ItemType Directory -Path $TempPath -Force | Out-Null
    }
    
    # Download file
    Invoke-WebRequest -Uri $DownloadUrl -OutFile $ZipPath -UseBasicParsing -ErrorAction Stop
    Write-Host "   ✅ Downloaded successfully" -ForegroundColor Green
    
    # Extract to Program Files
    $InstallPath = "C:\Program Files\Copyer"
    
    if (Test-Path $InstallPath) {
        Write-Host ""
        Write-Host "⚠️  Copyer is already installed at: $InstallPath" -ForegroundColor Yellow
        $Overwrite = Read-Host "   Do you want to overwrite it? (y/n)"
        
        if ($Overwrite -ne "y" -and $Overwrite -ne "Y") {
            Write-Host "   Installation cancelled." -ForegroundColor Yellow
            exit 0
        }
        
        Remove-Item $InstallPath -Recurse -Force
    }
    
    # Check if running as admin
    if (-not (Test-Administrator)) {
        Write-Host ""
        Write-Host "⚠️  Elevated privileges required for installation to Program Files" -ForegroundColor Yellow
        Write-Host "   Restarting script with administrator rights..." -ForegroundColor Cyan
        
        # Relaunch as admin
        $ScriptPath = $MyInvocation.MyCommand.Path
        Start-Process powershell -ArgumentList "-NoProfile -ExecutionPolicy Bypass -File `"$ScriptPath`"" -Verb RunAs
        exit 0
    }
    
    Write-Host ""
    Write-Host "📦 Installing to: $InstallPath" -ForegroundColor Yellow
    
    # Extract zip
    Expand-Archive -Path $ZipPath -DestinationPath $InstallPath -Force
    
    # Add to PATH if not already there
    $CurrentPath = [Environment]::GetEnvironmentVariable("Path", "Machine")
    if ($CurrentPath -notlike "*$InstallPath*") {
        Write-Host "   Adding to system PATH..." -ForegroundColor Cyan
        [Environment]::SetEnvironmentVariable(
            "Path",
            "$CurrentPath;$InstallPath",
            "Machine"
        )
    }
    
    Write-Host "   ✅ Installation complete!" -ForegroundColor Green
    
    # Refresh environment
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
    
    Write-Host ""
    Write-Host "🎉 Copyer has been successfully installed!" -ForegroundColor Green
    Write-Host ""
    
    # Test installation
    Write-Host "🧪 Testing installation..." -ForegroundColor Yellow
    $CopyerPath = Get-Command copy -ErrorAction SilentlyContinue
    
    if ($CopyerPath) {
        Write-Host "   ✅ Command 'copy' is available globally" -ForegroundColor Green
    }
    
    Write-Host ""
    
    # Ask if user wants to run
    $RunNow = Read-Host "📋 Would you like to run Copyer now? (y/n)"
    
    if ($RunNow -eq "y" -or $RunNow -eq "Y") {
        Write-Host ""
        Write-Host "📂 Enter the source directory to clone:" -ForegroundColor Cyan
        $SourceDir = Read-Host "   Source Directory"
        
        if (-not (Test-Path $SourceDir)) {
            Write-Host "❌ Directory not found: $SourceDir" -ForegroundColor Red
            Read-Host "Press Enter to exit"
            exit 1
        }
        
        Write-Host ""
        Write-Host "📂 Enter the destination (leave empty for current directory):" -ForegroundColor Cyan
        $DestDir = Read-Host "   Destination Directory"
        
        Write-Host ""
        Write-Host "🚀 Running Copyer..." -ForegroundColor Cyan
        Write-Host ""
        
        if ([string]::IsNullOrWhiteSpace($DestDir)) {
            & $InstallPath\Copyer.exe $SourceDir
        } else {
            & $InstallPath\Copyer.exe $SourceDir -d $DestDir
        }
        
        Write-Host ""
    }
    
    Write-Host "📚 Next Steps:" -ForegroundColor Yellow
    Write-Host "   • Run: copy --help              (show all options)" -ForegroundColor Cyan
    Write-Host "   • Usage: copy C:\Source         (clone directory)" -ForegroundColor Cyan
    Write-Host "   • Verify: copy C:\Source -v    (verify copied files)" -ForegroundColor Cyan
    Write-Host "   • Docs: https://github.com/akinofcam/copyer" -ForegroundColor Cyan
    Write-Host ""
    
} catch {
    Write-Host ""
    Write-Host "❌ Installation failed!" -ForegroundColor Red
    Write-Host "   Error: $_" -ForegroundColor Red
    Write-Host ""
    Write-Host "💡 Troubleshooting:" -ForegroundColor Yellow
    Write-Host "   • Ensure .NET 10.0+ is installed" -ForegroundColor Cyan
    Write-Host "   • Check your internet connection" -ForegroundColor Cyan
    Write-Host "   • Try running as Administrator" -ForegroundColor Cyan
    Write-Host "   • Visit: https://github.com/akinofcam/copyer/issues" -ForegroundColor Cyan
    Write-Host ""
    Read-Host "Press Enter to exit"
    return
}

Write-Host "✨ Installation wizard completed!" -ForegroundColor Green
Write-Host ""
