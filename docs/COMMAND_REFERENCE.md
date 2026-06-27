# Command Reference

Complete reference for all Copyer command-line options and usage patterns.

## Syntax

```
copy <source> [OPTIONS]
```

## Arguments

### source (Required)
The source directory path to clone.

**Format:**
- Absolute path: `C:\Users\Username\Projects\MyProject`
- Relative path: `...\Projects\MyProject`
- UNC path: `\\server\share\folder`

**Examples:**
```bash
copy C:\MyProject
copy ..\source
copy \\network\shared\project
```

## Global Options

### `-h, --help`
Display help information.

```bash
copy --help
```

### `--version`
Display version information.

```bash
copy --version
```

## Command Options

### `-d, --destination <PATH>`
Specify destination directory (default: current directory).

```bash
copy C:\Source -d D:\Destination
copy C:\Source --destination "D:\My Backup"
```

### `-f, --force`
Force overwrite if destination already exists.

```bash
copy C:\Source --force
copy C:\Source -f
```

**Warning:** This will delete the existing destination directory before copying.

### `--verify`
Verify file integrity after copying using SHA256 hashing.

```bash
copy C:\Source --verify
```

**Performance Note:** Verification adds 10-20% to total copy time.

### `-e, --exclude <PATTERN>`
Exclude files matching the pattern. Can be used multiple times.

```bash
# Single exclude pattern
copy C:\Source --exclude "*.log"

# Multiple exclude patterns
copy C:\Source --exclude "*.log" --exclude "*.tmp" --exclude "node_modules"
```

### `-i, --include <PATTERN>`
Include only files matching the pattern. Can be used multiple times.

```bash
# Only copy C# source files
copy C:\Source --include "*.cs" --include "*.csproj"

# Only copy documentation
copy C:\Source --include "*.md" --include "*.txt"
```

**Note:** If specified, only matching files are copied. Other files are excluded.

### `-q, --quiet`
Suppress progress dialog (useful for scripts and automation).

```bash
copy C:\Source --quiet
copy C:\Source -q
```

### `--log`
Show detailed logging output in console.

```bash
copy C:\Source --log
```

Logs are also saved to: `copyer-log-YYYY-MM-DD.txt`

## Pattern Syntax

### Glob Patterns

Copyer supports standard glob patterns:

| Pattern | Matches | Example |
|---------|---------|---------|
| `*` | Any characters | `*.log` = all .log files |
| `?` | Single character | `file?.txt` = file1.txt, fileA.txt |
| `*.EXT` | Extension matching | `*.exe` = all executables |
| `name*` | Prefix matching | `test*` = test1, test2, test.js |

### Common Examples

```bash
# Exclude temporary files
copy C:\Source --exclude "*.tmp" --exclude "*.bak" --exclude "*.swp"

# Exclude build directories
copy C:\Source --exclude "bin" --exclude "obj" --exclude "dist"

# Exclude cache
copy C:\Source --exclude ".git" --exclude "node_modules" --exclude ".vs"

# Exclude logs and temp
copy C:\Source --exclude "*.log" --exclude "Temp"

# Only copy source code
copy C:\Source --include "*.cs" --include "*.vb" --include "*.ts" --include "*.js"

# Only copy documentation
copy C:\Source --include "*.md" --include "*.txt" --include "*.doc"

# Exclude everything except code
copy C:\Source --exclude "*" --include "*.cs" --include "*.json"
```

## Common Usage Patterns

### Backup with Verification
```bash
copy C:\MyProject -d D:\Backups --verify
```

### Copy Excluding Build Artifacts
```bash
copy C:\MyProject --exclude "bin" --exclude "obj" --exclude ".git"
```

### Mirror Only Source Code
```bash
copy C:\SourceCode --include "*.cs" --include "*.csproj" --include "*.sln"
```

### Quiet Bulk Copy
```bash
copy C:\Project1 -d D:\Backup1 --quiet
copy C:\Project2 -d D:\Backup2 --quiet
copy C:\Project3 -d D:\Backup3 --quiet
```

### Logging for Audit Trail
```bash
copy C:\SensitiveData --verify --log
# Check: copyer-log-2026-06-27.txt
```

## Exit Codes

| Code | Meaning |
|------|---------|
| 0 | Success |
| 1 | Error (source not found, permission denied, etc.) |
| 2 | Invalid arguments |

## Environment Variables

No environment variables are currently used by Copyer.

## Configuration Files

Copyer does not use configuration files. All settings are specified via command-line arguments.

## Tips & Tricks

### Skip Verification by Default
Create a batch file wrapper:
```batch
@echo off
copy %1 --verify %2 %3
```

### Backup Script
```batch
@echo off
REM Daily backup script
set SOURCE=C:\MyProject
set BACKUP=D:\Backups\Backup-%date%
copy %SOURCE% -d %BACKUP% --verify
```

### Large File Copy
```bash
# For large directories, use quiet mode and logging
copy C:\LargeProject --quiet --log

# Monitor progress:
# tail -f copyer-log-YYYY-MM-DD.txt
```

### Selective Sync
```bash
# Copy only recent/important files
copy C:\Source --include "*.cs" --include "*.json" --include "README.md"
```

---

For more examples, see [EXAMPLES.md](EXAMPLES.md)
