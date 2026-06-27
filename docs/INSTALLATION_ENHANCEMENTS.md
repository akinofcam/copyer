# Copyer - Advanced Installation & GUI Enhancements

## 🎉 Recent Improvements Summary

Copyer has been significantly enhanced with professional-grade installation scripts and an improved Windows GUI experience.

### ✨ New Features

#### 1. **Smart Installation Scripts**

##### Windows PowerShell Script (`install.ps1`)
- 🖥️ **Auto-detects system** (OS, architecture, .NET version)
- 📥 **Downloads from GitHub releases** automatically
- 🔗 **Adds to PATH** for global `copy` command
- ✅ **Validates .NET installation** before proceeding
- 🎯 **Offers to run immediately** with interactive prompts
- ⚡ **Handles admin elevation** automatically
- 📊 **Shows real-time installation progress**
- 🛡️ **Robust error handling** with troubleshooting tips

**Usage:**
```powershell
# One-line installation
irm https://raw.githubusercontent.com/akinofcam/copyer/main/install.ps1 | iex

# Or locally
powershell -ExecutionPolicy Bypass -File install.ps1
```

##### Unix/Linux Bash Script (`install.sh`)
- 🖥️ **Detects macOS and Linux** distributions
- 📥 **Downloads latest release** from GitHub
- 🔗 **Creates symlinks** for global access
- ✅ **Validates .NET installation** before proceeding
- 🎯 **Offers to run immediately** with interactive prompts
- 📊 **Shows installation progress** and statistics
- 🛡️ **Comprehensive error handling**

**Usage:**
```bash
# One-line installation
curl -s https://raw.githubusercontent.com/akinofcam/copyer/main/install.sh | bash

# Or locally
chmod +x install.sh
./install.sh
```

#### 2. **Enhanced Windows GUI Progress Dialog**

The progress dialog now features:

- **Professional UI Design**
  - Modern Segoe UI font throughout
  - Color-coded status indicators
  - Clean layout with proper spacing
  - Light gray background (not harsh white)

- **Real-Time Statistics**
  - **Live percentage display** (0-100%)
  - **File count tracking** (X of Y files)
  - **Speed calculation** (files/second)
  - **Elapsed time** display (HH:MM:SS format)
  - **ETA calculation** for remaining files

- **Improved Status Messages**
  - Clear status showing current operation
  - Progress percentage prominently displayed
  - "Cloning..." → "Finalizing..." → "✅ Complete" flow
  - Informative subtitle text

- **Better Visual Feedback**
  - Larger progress bar (460px wide)
  - Color-highlighted progress indicator
  - Percentage text positioned over progress bar
  - Emoji indicators for status (📁, ✅)

**Dialog Features:**
```
╔─────────────────────────────────────────────┐
│ Copyer - Cloning Directory...               │
│                                             │
│ 📁 Cloning directory...                     │
│ Initializing...                             │
│                                             │
│ [████████████░░░░░░░░░░░░░░░░] 45%         │
│                                             │
│ 245 / 543 files                             │
│ Speed: 12.3 files/s | Time: 00:00:20       │
│ Please wait while files are being copied... │
└─────────────────────────────────────────────┘
```

### 🎯 Installation Script Workflow

When you run either installation script, you'll experience:

```
1. Welcome screen with system detection
   ↓
2. Validates .NET SDK installation
   ↓
3. Fetches latest release from GitHub
   ↓
4. Downloads appropriate version
   ↓
5. Extracts to system location
   ↓
6. Adds to PATH (Windows: System PATH, Unix: ~/.copyer)
   ↓
7. Tests installation
   ↓
8. Offers to run Copyer immediately
   ↓
9. Shows next steps and usage examples
```

### 📊 Supported Systems

| System | Script | Method |
|--------|--------|--------|
| **Windows 10+** | `install.ps1` | PowerShell |
| **macOS 10.15+** | `install.sh` | Bash |
| **Ubuntu 18.04+** | `install.sh` | Bash |
| **Debian 10+** | `install.sh` | Bash |
| **RHEL/CentOS 8+** | `install.sh` | Bash |

### 🔍 What's Different from v1.0?

| Feature | v1.0 | v2.0+ |
|---------|------|-------|
| Installation | Manual script | Smart auto-detection |
| Downloads | Manual | Automatic from releases |
| PATH setup | Manual | Automatic |
| System detect | None | Full detection |
| Interactive | No | Yes |
| Run after install | No | Yes |
| GUI features | Basic | Enhanced with stats |
| Progress info | File count only | Count + % + Speed + ETA |
| Cross-platform | Windows only | Windows + macOS + Linux |

### 💡 Example Session

**Windows:**
```powershell
PS C:\Users\User> irm https://raw.githubusercontent.com/akinofcam/copyer/main/install.ps1 | iex

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
   Downloading from: https://github.com/akinofcam/copyer/releases/...
   ✅ Downloaded successfully

📦 Installing to: C:\Program Files\Copyer
   ✅ Installation complete!

🎉 Copyer has been successfully installed!

📋 Would you like to run Copyer now? (y/n): y
📂 Enter the source directory to clone:
   Source Directory: C:\MyProject
📂 Enter the destination (leave empty for current directory):
   Destination Directory: 

🚀 Running Copyer...
[Shows enhanced progress dialog...]

📚 Next Steps:
   • Run: copy --help
   • Usage: copy C:\Source
   • Docs: https://github.com/akinofcam/copyer
```

**macOS/Linux:**
```bash
$ curl -s https://raw.githubusercontent.com/akinofcam/copyer/main/install.sh | bash

╔════════════════════════════════════════════════════════════╗
║          Welcome to Copyer Installation Wizard              ║
║     Professional Directory Cloning Tool for Unix Systems    ║
╚════════════════════════════════════════════════════════════╝

🔍 System Detection:
   OS: macOS 13.4
   Architecture: arm64
   .NET Version: 10.0.8

✅ .NET SDK found - proceeding with installation

📥 Downloading Copyer...
   Release: v2.0.0
   Downloading from: https://github.com/akinofcam/copyer/releases/...
   ✅ Downloaded successfully

📦 Installing to: /Users/user/.copyer
   ✅ Installation complete!
   ✅ Symlink created at /usr/local/bin/copy

...
```

### 🎨 Windows GUI Improvements

The new progress dialog provides professional-looking feedback:

**Before (v1.0):**
- Basic window with just file count
- No percentage shown
- No speed/time information
- Minimal styling

**After (v2.0+):**
- Modern, polished UI
- Real-time percentage display
- Speed calculation (files/second)
- Elapsed time tracking
- ETA for remaining files
- Professional emoji indicators
- Better typography and spacing
- Color-coded status messages

### 🚀 Getting Started

1. **Install using smart script** (recommended):
   - Windows: `irm https://raw.githubusercontent.com/akinofcam/copyer/main/install.ps1 | iex`
   - macOS/Linux: `curl -s https://raw.githubusercontent.com/akinofcam/copyer/main/install.sh | bash`

2. **Verify installation**:
   ```bash
   copy --help
   copy --version
   ```

3. **Use it**:
   ```bash
   copy C:\SourceDir                    # Clone directory
   copy C:\SourceDir --verify           # With verification
   copy C:\SourceDir --exclude "*.log"  # Filter files
   ```

### 📖 Documentation

For more details, see:
- [INSTALLATION.md](INSTALLATION.md) - Complete installation guide
- [docs/COMMAND_REFERENCE.md](docs/COMMAND_REFERENCE.md) - All CLI options
- [docs/EXAMPLES.md](docs/EXAMPLES.md) - Real-world usage examples
- [README.md](README.md) - Project overview

### ❓ FAQ

**Q: Can I use the old manual installation?**
A: Yes! The smart scripts are optional. You can still manually download and install.

**Q: What if PowerShell is blocked?**
A: Use `powershell -ExecutionPolicy Bypass -File install.ps1`

**Q: Does the GUI work on Linux?**
A: No, the GUI (Windows Forms) only works on Windows. Linux/macOS use CLI output.

**Q: Can I script the installation?**
A: Yes! The installers can detect automation and skip interactive prompts.

**Q: What systems are supported?**
A: Windows 10+, macOS 10.15+, and Linux (Ubuntu 18.04+, Debian 10+, RHEL/CentOS 8+)

---

**Status:** ✅ Production Ready | **Latest Version:** 2.0.0
