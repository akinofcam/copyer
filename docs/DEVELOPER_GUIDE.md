# Developer Quick Start

Welcome to Copyer development! This guide will get you started quickly.

## Prerequisites

- .NET 10.0 SDK or later
- Visual Studio 2022, VS Code, or preferred C# IDE
- Git

## Setup (5 minutes)

```bash
# 1. Clone repository
git clone https://github.com/akinofcam/copyer.git
cd copyer

# 2. Restore dependencies
dotnet restore

# 3. Build project
dotnet build

# 4. Test the application
dotnet run -- --help
```

## Project Structure

```
copyer/
├── Program.cs           # CLI entry point
├── CliParser.cs         # Command-line argument parsing
├── DirectoryCopier.cs   # Core directory copying logic
├── ProgressDialog.cs    # Windows Forms UI
└── Copyer.csproj        # Project configuration
```

## Common Tasks

### Build
```bash
# Debug build
dotnet build

# Release build
dotnet build -c Release
```

### Run
```bash
# With arguments
dotnet run -- C:\Source --help
dotnet run -- C:\Source --verify
```

### Test
```bash
dotnet build
dotnet run -- --help
dotnet run -- --version
```

### Publish
```bash
# Create Release executable
dotnet publish -c Release -o ./publish
```

## Key Classes

### `Program.cs` (Entry Point)
- Initializes Serilog logging
- Parses CLI arguments
- Orchestrates copy operation

### `CliParser.cs` (CLI Parsing)
- Parses command-line arguments
- Validates options
- Provides help/version output

### `DirectoryCopier.cs` (Core Logic)
- Two-pass copy algorithm
- File filtering (include/exclude)
- SHA256 verification
- Progress tracking

### `ProgressDialog.cs` (UI)
- Windows Forms dialog
- Progress bar updates
- File count display
- Thread-safe operations

## Development Guidelines

### Code Style
- Follow [EditorConfig](.editorconfig) rules
- Use Microsoft C# conventions
- PascalCase for public members
- camelCase for private members

### Documentation
- Add XML comments to public APIs
- Include `/// <summary>` sections
- Document parameters and return values

### Error Handling
- Use try-catch for recoverable errors
- Log all errors with Serilog
- Provide user-friendly messages

### Testing
- Test locally before committing
- Build both Debug and Release
- Test with various directory sizes

## Debugging

### Visual Studio
1. Open `Copyer.csproj` in Visual Studio
2. Set breakpoints in code
3. Press F5 to debug
4. Use Debug menu for breakpoint management

### VS Code
1. Install C# Dev Kit extension
2. Open folder in VS Code
3. Set breakpoints (F9)
4. Press F5 to start debugging

### Command Line
```bash
# Build with debug symbols
dotnet build

# Run under debugger
dotnet run -- C:\MyProject --log
```

## Common Issues

### Build Fails
```bash
# Clean and rebuild
rm -r bin obj
dotnet clean
dotnet build
```

### Restore Issues
```bash
# Clear NuGet cache
dotnet nuget locals all --clear
dotnet restore
```

### Windows Forms Issues
```bash
# Only runs on Windows
# For Linux development, use WSL (Windows Subsystem for Linux)
```

## Making Changes

1. **Create Feature Branch**
   ```bash
   git checkout -b feature/your-feature
   ```

2. **Make Changes**
   - Edit files
   - Follow code style
   - Add comments

3. **Build & Test**
   ```bash
   dotnet build
   dotnet run -- --help
   ```

4. **Commit Changes**
   ```bash
   git add .
   git commit -m "Add feature description"
   ```

5. **Push & Create PR**
   ```bash
   git push origin feature/your-feature
   # Create PR on GitHub
   ```

## Running Tests

```bash
# Compile (closest to testing without unit tests)
dotnet build

# Manual testing scenarios
dotnet run -- C:\TestDir --verify
dotnet run -- C:\TestDir --exclude "*.log"
dotnet run -- C:\TestDir --help
```

## Documentation

- **User Guide:** See [README.md](README.md)
- **Installation:** See [INSTALLATION.md](INSTALLATION.md)
- **Contributing:** See [CONTRIBUTING.md](CONTRIBUTING.md)
- **Architecture:** See [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md)
- **Commands:** See [docs/COMMAND_REFERENCE.md](docs/COMMAND_REFERENCE.md)
- **Examples:** See [docs/EXAMPLES.md](docs/EXAMPLES.md)

## Performance Profiling

```bash
# Build Release version
dotnet publish -c Release -o ./publish

# Run with timing
Measure-Command { .\publish\Copyer.exe C:\LargeDir }
```

## Troubleshooting

### "Windows Forms requires Windows"
- Copyer uses Windows Forms, must run on Windows
- For Linux development, use WSL

### Build Warnings (XML Comments)
- Expected documentation warnings
- Add `/// <summary>` to silence or suppress if intentional

### .NET Version Issues
```bash
# Check .NET version
dotnet --version

# Install correct version if needed
dotnet --list-sdks
```

## Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [C# Documentation](https://docs.microsoft.com/dotnet/csharp/)
- [Windows Forms](https://docs.microsoft.com/dotnet/desktop/winforms/)
- [Serilog](https://serilog.net/)

## Getting Help

- **Issues:** Check [GitHub Issues](https://github.com/akinofcam/copyer/issues)
- **Discussions:** Ask on [GitHub Discussions](https://github.com/akinofcam/copyer/discussions)
- **Contributing:** See [CONTRIBUTING.md](CONTRIBUTING.md)

---

Happy coding! 🚀
