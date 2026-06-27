# Installation Guide

Copyer provides **smart, automated installation scripts** that detect your system and install the appropriate version with a single command.

## 🚀 Quick Start (Recommended)

### Windows

```powershell
# One-line installation
irm https://raw.githubusercontent.com/akinofcam/copyer/main/install.ps1 | iex
```

Or run locally:
```powershell
powershell -ExecutionPolicy Bypass -File install.ps1
```

### macOS / Linux

```bash
# One-line installation
curl -s https://raw.githubusercontent.com/akinofcam/copyer/main/install.sh | bash
```

Or download and run:
```bash
curl -O https://raw.githubusercontent.com/akinofcam/copyer/main/install.sh
chmod +x install.sh
./install.sh
```

## ✨ What the Smart Installers Do

Both `install.ps1` (Windows) and `install.sh` (macOS/Linux) automatically:

- 🖥️ **Detect your system** (OS, architecture, .NET version)
- 🔍 **Validate .NET is installed** (required for running Copyer)
- 📥 **Download from latest release** on GitHub
- 📦 **Extract to appropriate location**:
  - Windows: `C:\Program Files\Copyer`
  - macOS/Linux: `$HOME/.copyer`
- 🔗 **Add to PATH** for global `copy` command
- ✅ **Verify installation** works correctly
- 🎯 **Offer to run immediately** with interactive prompts
- 📚 **Show next steps** and usage examples

### Example Interactive Session

```
╔════════════════════════════════════════════════════════════╗
║          Welcome to Copyer Installation Wizard              ║
║     Professional Directory Cloning Tool for Windows         ║
╚════════════════════════════════════════════════════════════╝

🔍 System Detection:
   OS: Windows
   Architecture: X64
   .NET Version: 10.0.8

✅ .NET SDK found - proceeding with installation

📥 Downloading Copyer...
   Release: v2.0.0
   Downloaded successfully

📦 Installing to: C:\Program Files\Copyer
   ✅ Installation complete!

🎉 Copyer has been successfully installed!

📋 Would you like to run Copyer now? (y/n): y
📂 Enter the source directory to clone:
   Source Directory: C:\MyProject
📂 Enter the destination (leave empty for current directory):
   Destination Directory: 

🚀 Running Copyer...
[Shows progress dialog with live progress bar]

📚 Next Steps:
   • Run: copy --help              (show all options)
   • Usage: copy C:\Source         (clone directory)
   • Verify: copy C:\Source -v     (verify copied files)
   • Docs: https://github.com/akinofcam/copyer
```

## 📋 Alternative Installation Methods

### Method 1: Manual Download

**Windows:**
1. Download from [GitHub Releases](https://github.com/akinofcam/copyer/releases)
2. Extract ZIP to your preferred location
3. Add folder to system PATH:
   - Settings → System → About → Advanced system settings
   - Environment Variables → Edit PATH
   - Add installation folder
4. Restart terminal

**macOS/Linux:**
1. Download from [GitHub Releases](https://github.com/akinofcam/copyer/releases)
2. Extract: `tar -xzf copyer-x64.tar.gz`
3. Install: `sudo mv copyer /usr/local/bin/copyer`
4. Make executable: `sudo chmod +x /usr/local/bin/copyer`

### Method 2: .NET Global Tool

If you have .NET SDK installed:

```bash
# Install
dotnet tool install -g Copyer

# Update
dotnet tool update -g Copyer

# Uninstall
dotnet tool uninstall -g Copyer
```

### Method 3: Package Manager (Coming Soon)

- 🍺 Homebrew (macOS): `brew install copyer`
- 📦 Chocolatey (Windows): `choco install copyer`
- 🐧 apt (Linux): `sudo apt install copyer`

## 🔧 System Requirements

| Requirement | Minimum | Recommended |
|------------|---------|-------------|
| **OS** | Windows 10 / macOS 10.15 / Ubuntu 18.04 | Latest version |
| **.NET** | 10.0 Runtime | 10.0+ SDK |
| **Architecture** | x86-64 | x86-64 or ARM64 |
| **Disk Space** | 100 MB | 500 MB |
| **RAM** | 256 MB | 1 GB |
| **Internet** | Required for download | Only for installation |

## ✔️ Verify Installation

After installation, verify Copyer is accessible:

```bash
# Windows
copy --help
copy --version

# macOS/Linux
copyer --help
copyer --version
```

Or test with a real copy:
```bash
copy C:\Source --verify
```

## 🐛 Troubleshooting

### Installation Scripts Don't Run

**Windows PowerShell:**
```powershell
# If blocked by execution policy
powershell -ExecutionPolicy Bypass -File install.ps1

# Or set permanently
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

**macOS/Linux Bash:**
```bash
# Ensure bash is installed
bash --version

# Run with explicit bash
bash install.sh
```

### ".NET SDK Not Found"

The installer checks for .NET 10.0. Install it:

**Windows:**
- Download: https://dotnet.microsoft.com/download
- Or: `winget install Microsoft.DotNet.SDK.10`

**macOS:**
```bash
# Using Homebrew
brew install dotnet
```

**Linux (Ubuntu/Debian):**
```bash
# Microsoft package repository
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
sudo chmod +x dotnet-install.sh
sudo ./dotnet-install.sh --version latest
```

**Linux (RHEL/CentOS):**
```bash
sudo dnf install dotnet-sdk-10.0
```

### "Command not found" After Installation

**Windows:**
- Restart PowerShell/Command Prompt
- Check PATH: Run `$env:Path` in PowerShell
- Manually add folder: `C:\Program Files\Copyer` to PATH

**macOS/Linux:**
- Add to PATH: `export PATH=$PATH:$HOME/.copyer`
- Make permanent in `~/.bashrc` or `~/.zshrc`:
  ```bash
  echo 'export PATH="$HOME/.copyer:$PATH"' >> ~/.bashrc
  source ~/.bashrc
  ```
- Restart terminal

### "Permission Denied" (Linux/macOS)

Make the executable have proper permissions:
```bash
chmod +x ~/.copyer/copyer
# or for global install
sudo chmod +x /usr/local/bin/copyer
```

### Corporate Network / Proxy Issues

If behind a corporate proxy, configure PowerShell:

```powershell
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
[Net.ServicePointManager]::DefaultConnectionLimit = 9999

# Optional: Set proxy
[Net.WebRequest]::DefaultWebProxy = New-Object Net.WebProxy "proxy.company.com:8080"
```

### Firewall Issues

If installer can't download:
- Check internet connection: `ping github.com`
- Try manually: https://github.com/akinofcam/copyer/releases
- Configure firewall to allow GitHub downloads

## 🆘 Getting Help

- **Questions:** [GitHub Discussions](https://github.com/akinofcam/copyer/discussions)
- **Issues:** [GitHub Issues](https://github.com/akinofcam/copyer/issues)
- **Documentation:** [README.md](README.md)
- **Command Reference:** [docs/COMMAND_REFERENCE.md](docs/COMMAND_REFERENCE.md)
- **Disk Space:** ~50 MB for installation

## Support

For issues or questions, please create an issue in the repository.
