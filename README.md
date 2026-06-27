# Copyer

<div align="center">

![Copyer Logo](https://img.shields.io/badge/Copyer-Directory%20Cloning%20Tool-brightgreen)
[![.NET 10.0](https://img.shields.io/badge/.NET-10.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![Windows](https://img.shields.io/badge/Platform-Windows%2010%2B-0078D4?logo=windows)](https://www.microsoft.com/windows)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![GitHub stars](https://img.shields.io/github/stars/akinofcam/copyer?style=social)](https://github.com/akinofcam/copyer)

**Advanced directory cloning tool with real-time progress dialog, filtering, verification, and comprehensive logging**

[Quick Start](#quick-start) • [Features](#features) • [Installation](#installation) • [Documentation](#documentation) • [Contributing](#contributing)

</div>

---

## Overview

**Copyer** is a powerful command-line utility for Windows that makes cloning entire directory structures simple and efficient. With a modern progress dialog (just like Windows GUI), advanced filtering options, file verification, and detailed logging, Copyer is perfect for developers, DevOps engineers, and system administrators.

## ✨ Key Features

### Core Functionality
- 🚀 **Fast Recursive Copying** - Efficiently clones entire directory trees
- 📊 **Real-time Progress Dialog** - Visual feedback with progress bar and file counter
- 🎯 **Smart Filtering** - Include/exclude files using glob patterns
- ✅ **File Verification** - Optional SHA256 hash verification after copying
- 📝 **Comprehensive Logging** - Detailed logs for troubleshooting
- ⚡ **Async Operations** - Non-blocking UI for responsive experience

### Advanced Options
- Force overwrite existing destinations
- Quiet mode for scripting
- Pattern matching (glob patterns)
- Flexible destination paths
- SHA256 integrity verification

## 🚀 Quick Start

### Installation (PowerShell)

```powershell
  irm https://raw.githubusercontent.com/akinofcam/copyer/main/install.ps1 | iex
```

### Usage

```bash
# Basic copy
copy C:\Users\YourName\Projects\MyProject

# With verification
copy C:\MyProject --verify

# Exclude files
copy C:\MyProject --exclude "*.log" --exclude "*.tmp"

# Custom destination
copy C:\MyProject -d D:\Backups

# Get help
copy --help
```

## 📋 System Requirements

- **OS:** Windows 10/11 or Windows Server 2016+
- **.NET:** Runtime 10.0 or later
- **Disk:** ~100 MB for installation
- **Architecture:** x86, x64, or ARM64

## 📚 Documentation

- [Installation Guide](INSTALLATION.md) - Step-by-step installation
- [Contributing Guide](CONTRIBUTING.md) - How to contribute
- [Code of Conduct](CODE_OF_CONDUCT.md) - Community standards
- [Security Policy](SECURITY.md) - Security guidelines
- [Changelog](CHANGELOG.md) - Version history

## 🔧 Building from Source

```bash
dotnet build                              # Debug build
dotnet publish -c Release -o ./publish    # Release build
```

## 📝 License

Licensed under the MIT License. See [LICENSE](LICENSE) for details.

## 🤝 Contributing

We welcome contributions! See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

---

<div align="center">

Made with ❤️ for the Windows developer community

[GitHub](https://github.com/akinofcam/copyer) • [Issues](https://github.com/akinofcam/copyer/issues) • [Discussions](https://github.com/akinofcam/copyer/discussions)

</div>
