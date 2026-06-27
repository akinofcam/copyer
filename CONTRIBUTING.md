# Contributing to Copyer

Thank you for your interest in contributing to Copyer! This document provides guidelines and instructions for contributing.

## Code of Conduct

This project adheres to the Contributor Covenant Code of Conduct. By participating, you are expected to uphold this code.

## Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- Visual Studio 2022, VS Code, or your preferred C# IDE
- Git

### Setup Development Environment

1. **Fork and clone the repository**
```bash
git clone https://github.com/YOUR_USERNAME/copyer.git
cd copyer
```

2. **Build the project**
```bash
dotnet build
```

3. **Run the application**
```bash
dotnet run -- --help
```

## Development Workflow

### Creating a Feature Branch

1. Create a new branch from `main`
```bash
git checkout -b feature/your-feature-name
```

2. Follow naming conventions:
   - `feature/description` - New features
   - `fix/description` - Bug fixes
   - `docs/description` - Documentation updates
   - `refactor/description` - Code refactoring

### Making Changes

1. **Code Style**
   - Follow Microsoft C# coding conventions
   - Use meaningful variable names
   - Keep methods focused and concise
   - Add XML documentation comments for public APIs

2. **Commit Messages**
   - Use clear, descriptive commit messages
   - Start with a verb: "Add", "Fix", "Update", "Refactor"
   - Reference issues when applicable: "Fixes #123"

```bash
git commit -m "Add feature for parallel copying"
git commit -m "Fix progress dialog not showing (Fixes #42)"
```

3. **Testing**
   - Test your changes locally before pushing
   - Build in both Debug and Release configurations
   - Test on Windows 10/11 if possible

```bash
dotnet build
dotnet publish -c Release
```

## Submitting Changes

### Creating a Pull Request

1. Push your branch to GitHub
```bash
git push origin feature/your-feature-name
```

2. Open a pull request on GitHub
   - Use the PR template provided
   - Link related issues
   - Provide a clear description of changes
   - Include screenshots if applicable

3. PR Guidelines
   - Keep PRs focused (one feature per PR)
   - Update documentation if needed
   - Ensure all checks pass
   - Request review from maintainers

### Review Process

- At least one maintainer review is required
- Address review feedback promptly
- Request re-review after making changes
- Be respectful and constructive in discussions

## Reporting Bugs

### Before Submitting

- Check if the bug has already been reported
- Try to reproduce it consistently
- Gather relevant information (OS, .NET version, error messages)

### Submitting a Bug Report

Use the Bug Report issue template and include:
- Clear description of the issue
- Steps to reproduce
- Expected vs actual behavior
- Environment details
- Error messages and logs
- Screenshots if applicable

## Feature Requests

Use the Feature Request issue template and include:
- Clear description of the feature
- Motivation and use cases
- Proposed implementation (optional)
- Alternative approaches considered

## Project Structure

```
copyer/
├── Program.cs              # Entry point
├── CliParser.cs            # Command-line parsing
├── DirectoryCopier.cs      # Core copying logic
├── ProgressDialog.cs       # Windows Forms UI
├── Copyer.csproj          # Project file
├── .github/               # GitHub templates and workflows
│   ├── workflows/         # CI/CD workflows
│   ├── ISSUE_TEMPLATE/    # Issue templates
│   └── pull_request_template.md
├── bin/                   # Build output
├── publish/               # Release build
└── [documentation files]
```

## Key Files to Know

- **Program.cs** - CLI entry point and argument handling
- **CliParser.cs** - Command-line argument parsing logic
- **DirectoryCopier.cs** - Core directory copying with filtering, verification
- **ProgressDialog.cs** - Windows Forms progress UI

## Common Development Tasks

### Running Tests
```bash
dotnet build
```

### Building Release Version
```bash
dotnet publish -c Release -o ./publish
```

### Viewing Help
```bash
dotnet run -- --help
```

### Testing Features
```bash
# Test basic copy
dotnet run -- C:\SourceDir

# Test with filtering
dotnet run -- C:\SourceDir --exclude "*.log"

# Test with verification
dotnet run -- C:\SourceDir --verify

# Test logging
dotnet run -- C:\SourceDir --log
```

## Documentation

- **README.md** - Main documentation and features
- **INSTALLATION.md** - Installation instructions
- **PROJECT_SUMMARY.md** - Technical overview
- **CONTRIBUTING.md** - This file
- **CODE_OF_CONDUCT.md** - Community standards

When adding features, update:
1. Code comments and XML documentation
2. README.md if it affects usage
3. INSTALLATION.md if it affects installation
4. Example scripts if applicable

## Code Guidelines

### Naming Conventions
- Classes: `PascalCase` (ProgressDialog)
- Methods: `PascalCase` (CopyDirectoryAsync)
- Properties: `PascalCase` (EnableLogging)
- Local variables: `camelCase` (sourceFile)
- Constants: `PascalCase` (MaxRetries)

### Documentation
- Add XML comments to public methods
- Include `/// <summary>` sections
- Document parameters and return values
- Provide examples for complex methods

```csharp
/// <summary>
/// Copies a directory and all its contents recursively.
/// </summary>
/// <param name="sourceDir">Source directory path</param>
/// <param name="destDir">Destination directory path</param>
/// <returns>A task representing the asynchronous operation</returns>
public async Task CopyDirectoryAsync(string sourceDir, string destDir)
{
    // Implementation
}
```

### Error Handling
- Use meaningful exception messages
- Log errors using Serilog
- Handle null references appropriately
- Provide user-friendly error messages

## Getting Help

- Check existing issues and pull requests
- Review the documentation
- Ask questions in issues or discussions
- Contact maintainers for guidance

## License

By contributing, you agree that your contributions will be licensed under the project's MIT License.

## Recognition

Contributors will be recognized in:
- Commit history
- GitHub contributors page
- Release notes for significant contributions

---

Thank you for contributing to Copyer!
