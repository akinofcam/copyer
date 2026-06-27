# Architecture

Technical design and implementation details of Copyer.

## Overview

Copyer is a Windows desktop CLI application built with .NET 10.0 and Windows Forms. It provides a responsive user interface with real-time progress feedback while performing complex file operations.

## Design Principles

1. **User-Centric** - Prioritize user experience with clear feedback
2. **Reliable** - Ensure data integrity with verification options
3. **Efficient** - Optimize performance for large directories
4. **Flexible** - Support advanced filtering and customization
5. **Maintainable** - Clean code with clear separation of concerns

## Technology Stack

| Component | Technology | Version | Purpose |
|-----------|-----------|---------|---------|
| Runtime | .NET | 10.0 | Application platform |
| UI | Windows Forms | Built-in | Progress dialog |
| Logging | Serilog | 3.1.1 | Structured logging |
| Terminal | Spectre.Console | 0.49.0 | Console formatting |

## Architecture Layers

### 1. CLI Layer (`Program.cs`, `CliParser.cs`)

**Responsibility:** Command-line argument parsing and user interface

**Key Components:**
- `Program.Main()` - Entry point
- `CliParser` - Custom argument parser
- `HandleCopyCommand()` - Command orchestration

**Responsibilities:**
- Parse command-line arguments
- Validate user input
- Display help and version information
- Coordinate copy operations
- Handle logging configuration

### 2. Application Layer (`DirectoryCopier.cs`)

**Responsibility:** Core business logic for directory copying

**Key Components:**
- `DirectoryCopier` - Main copying engine
- File counting algorithm
- Filtering logic
- Verification logic

**Features:**
- Two-pass approach:
  1. Count total files (for progress accuracy)
  2. Copy files with progress updates
- Pattern matching for filtering
- SHA256 verification option
- Detailed logging support

### 3. UI Layer (`ProgressDialog.cs`)

**Responsibility:** User interface feedback during operations

**Key Components:**
- `ProgressDialog` - Windows Forms dialog
- `ProgressBar` control
- File count display
- Thread-safe operations

**Features:**
- Non-blocking dialog updates
- Thread-safe property updates
- Professional UI styling
- Responsive progress feedback

## Data Flow

### Copy Operation Flow

```
User Input
    ↓
CliParser (Parse Arguments)
    ↓
Program (Validate Input)
    ↓
DirectoryCopier (Initialize)
    ↓
DirectoryCopier.CountFiles() (First Pass)
    ↓
ProgressDialog.Show()
    ↓
DirectoryCopier.CopyFilesRecursive() (Second Pass)
    ├→ Apply Filters
    ├→ Copy File
    ├→ Optional Verification
    ├→ Update Progress
    └→ Update UI
    ↓
ProgressDialog.Hide()
    ↓
Log Results
    ↓
Exit
```

## Key Classes

### `DirectoryCopier`

```csharp
public class DirectoryCopier
{
    // Two-pass algorithm
    public async Task CopyDirectoryAsync(string sourceDir, string destDir)
    {
        // Pass 1: Count files
        // Pass 2: Copy with progress
    }
    
    // Filtering
    private bool ShouldProcessFile(string fileName)
    
    // Verification
    private bool VerifyFileCopy(string sourceFile, string destFile)
    private byte[] ComputeFileHash(string filePath, HashAlgorithm algorithm)
}
```

### `CliParser`

```csharp
public class CliParser
{
    public string? SourceDirectory { get; set; }
    public string? DestinationDirectory { get; set; }
    public bool ForceOverwrite { get; set; }
    // ... other options
    
    public static CliParser Parse(string[] args)
    public static void PrintHelp()
    public static void PrintVersion()
}
```

### `ProgressDialog`

```csharp
public class ProgressDialog : Form
{
    public void SetTotalFiles(long totalFiles)
    public void UpdateProgress(long copiedFiles, long totalFiles)
    
    // Thread-safe UI updates
    private Label _statusLabel;
    private ProgressBar _progressBar;
    private Label _filesCountLabel;
}
```

## Algorithms

### Two-Pass Copy Algorithm

**Why Two Passes?**
- Accurate progress bar (know total count upfront)
- Better user experience (realistic expectations)
- Fail fast if source has issues

**Pass 1: Counting**
```
Count files in source recursively:
├─ Count files in current directory
├─ For each subdirectory:
│  └─ Recursively count files
└─ Skip inaccessible directories (permissions)
```

**Pass 2: Copying**
```
Copy files from source to destination:
├─ Create destination directory
├─ For each file in source:
│  ├─ Check filter patterns (include/exclude)
│  ├─ Copy file to destination
│  ├─ Optional: Verify file
│  └─ Update progress UI
├─ For each subdirectory:
│  └─ Recursively copy contents
└─ Close UI when complete
```

### Pattern Matching

Glob patterns are converted to regex:

```csharp
private bool PatternMatch(string fileName, string pattern)
{
    // "*.log" → "^.*\.log$"
    // "test?.txt" → "^test.\.txt$"
    // "file*" → "^file.*$"
    
    string regex = "^" + Regex.Escape(pattern)
        .Replace("\\*", ".*")    // * = any chars
        .Replace("\\?", ".")     // ? = single char
        + "$";
    
    return Regex.IsMatch(fileName, regex, RegexOptions.IgnoreCase);
}
```

### SHA256 Verification

```
For each copied file:
1. Compute SHA256 hash of source
2. Compute SHA256 hash of destination
3. Compare hashes bit-by-bit
4. If different: Mark as failed
5. Report verification results
```

## Performance Optimizations

### 1. File System Efficiency
- Batch file operations where possible
- Minimize directory traversals
- Cache DirectoryInfo objects

### 2. UI Responsiveness
- Async/await for long operations
- Thread-safe UI updates (Invoke pattern)
- Small delays between UI updates

### 3. Filtering Efficiency
- Check filters during main pass (not separate)
- Regex compiled once per operation
- Skip files early in pipeline

### 4. Memory Management
- Stream-based operations (not loaded into memory)
- Dispose patterns for file handles
- GC collection hints for large operations

## Error Handling Strategy

### Hierarchy

```
Application Errors
├─ CLI Errors
│  ├─ Invalid arguments
│  ├─ Missing source directory
│  └─ Destination already exists
├─ Copy Errors
│  ├─ Permission denied
│  ├─ Disk full
│  └─ File access errors
├─ Verification Errors
│  └─ Hash mismatch
└─ Logging Errors
   └─ Log file creation
```

### Error Recovery

- **Non-blocking:** Single file failures don't stop entire operation
- **Logged:** All errors captured in detailed logs
- **Reported:** User informed of failures with counts
- **Resumable:** Can retry with `--force` flag

## Logging Architecture

### Serilog Configuration

```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()              // Console output
    .WriteTo.File(                  // File output
        "copyer-log-.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

### Log Levels

| Level | Usage | Examples |
|-------|-------|----------|
| Error | Failures | Permission denied, copy failed |
| Warning | Potential issues | Destination exists, files skipped |
| Information | Normal operations | Starting copy, completion |
| Debug | Detailed info | Individual file copies |

## Thread Safety

### UI Thread Considerations

Windows Forms UI updates must happen on UI thread:

```csharp
if (control.InvokeRequired)
{
    control.Invoke(new Action(() => {
        // Update on UI thread
    }));
}
else
{
    // Already on UI thread
}
```

## Security Considerations

### File Access
- Respects Windows file permissions
- Fails gracefully on access denied
- No privilege escalation

### Data Integrity
- Optional SHA256 verification
- Atomic file operations
- No data corruption on failure

### Input Validation
- Path validation and normalization
- Pattern validation
- Filter validation

## Future Architecture Improvements

### Version 2.1+
- Parallel copying for performance
- Plugin system for custom filters
- Advanced scheduling

### Version 3.0+
- Cross-platform support (macOS/Linux)
- Web UI backend
- Network protocols

## Deployment Model

- **Standalone EXE** - Single executable file
- **Global Tool** - .NET tool installation
- **Portable** - No installation required
- **Automated** - Installation scripts provided

## Configuration

Copyer uses **CLI arguments only** (no config files):
- Command-line focused design
- Easier for scripts and automation
- Simple to document and discover

---

For implementation details, see the source code comments in:
- [Program.cs](../Program.cs)
- [DirectoryCopier.cs](../DirectoryCopier.cs)
- [ProgressDialog.cs](../ProgressDialog.cs)
