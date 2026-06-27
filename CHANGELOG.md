# Changelog

All notable changes to Copyer will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2026-06-27

### Added
- **Advanced CLI Argument Parsing** - Custom argument parser with support for long and short options
- **File Filtering** - Include/exclude files using glob patterns (`*.log`, `*.tmp`, etc.)
- **File Verification** - SHA256 hash verification option to ensure data integrity
- **Comprehensive Logging** - Serilog integration with console and file output
- **Force Overwrite** - `-f` / `--force` flag to overwrite existing directories
- **Quiet Mode** - `-q` / `--quiet` flag to suppress progress dialog for scripting
- **Help System** - `--help` and `--version` commands with detailed usage information
- **Professional Documentation** - Contributing guide, security policy, architecture docs
- **GitHub CI/CD** - Automated build and release workflows
- **GitHub Templates** - Issue and PR templates for structured submissions

### Changed
- Complete project restructuring for production readiness
- Enhanced error handling with detailed logging
- Improved progress dialog with better UI/UX
- Updated dependencies to latest stable versions
- Refactored DirectoryCopier for advanced filtering

### Fixed
- Null reference handling in progress dialog initialization
- Path normalization for cross-platform compatibility
- File permission error handling

## [1.0.0] - 2026-06-27

### Added
- Initial release of Copyer
- Basic directory cloning functionality
- Windows Forms progress dialog
- Real-time progress tracking with file counter
- Recursive directory copying
- Cross-platform path handling
- Installation scripts (PowerShell and Batch)
- Basic documentation
- MIT License

### Features
- Simple command-line interface
- Visual progress feedback
- Error handling for common scenarios
- Global tool installation support
- Windows 10/11 compatibility

---

## Planned Features

### Version 2.1 (Q3 2026)
- [ ] Parallel file copying for improved performance
- [ ] Pause and resume functionality
- [ ] Advanced filtering with regex patterns
- [ ] Copy speed and time estimation
- [ ] Performance optimizations

### Version 2.2 (Q4 2026)
- [ ] Batch operations from configuration files
- [ ] Pre/post operation hooks
- [ ] Advanced logging with rotation policies
- [ ] Copy history tracking

### Version 3.0 (2027)
- [ ] macOS support (cross-platform)
- [ ] Linux support (cross-platform)
- [ ] Web-based UI option
- [ ] Network copy support
- [ ] Scheduled copying

---

## How to Use This Changelog

- **Added** - New features
- **Changed** - Changes in existing functionality
- **Deprecated** - Soon-to-be removed features
- **Removed** - Removed features
- **Fixed** - Bug fixes
- **Security** - Security vulnerability fixes

---

## Release Notes

### Latest Release
- **Version:** 2.0.0
- **Release Date:** 2026-06-27
- **Status:** Production Ready

For detailed changes, see [v2.0.0 Release](https://github.com/akinofcam/copyer/releases/tag/v2.0.0)

---

## Version History

| Version | Release Date | Status | Download |
|---------|--------------|--------|----------|
| 2.0.0   | 2026-06-27   | Latest | [Download](https://github.com/akinofcam/copyer/releases/tag/v2.0.0) |
| 1.0.0   | 2026-06-27   | Legacy | [Download](https://github.com/akinofcam/copyer/releases/tag/v1.0.0) |

---

## Contributing

We welcome contributions! If you'd like to contribute to Copyer:

1. Check [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines
2. Create a new branch for your changes
3. Submit a pull request with detailed description
4. Update CHANGELOG.md with your changes

---

## Support

- **Documentation:** See [docs/](docs/) folder
- **Issues:** Report bugs on [GitHub Issues](https://github.com/akinofcam/copyer/issues)
- **Questions:** Ask on [GitHub Discussions](https://github.com/akinofcam/copyer/discussions)

---

Last Updated: 2026-06-27
