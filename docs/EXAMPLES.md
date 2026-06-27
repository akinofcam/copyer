# Usage Examples

Real-world examples of using Copyer for common tasks.

## Basic Operations

### Simple Directory Clone
```bash
copy C:\Users\John\Documents\MyProject
# Creates MyProject folder in current directory
```

### Clone to Specific Location
```bash
copy C:\Users\John\Documents\MyProject -d D:\Backups
# Creates D:\Backups\MyProject
```

### Force Overwrite
```bash
copy C:\Users\John\Documents\MyProject --force
# Deletes existing MyProject and recreates it
```

## Development Workflows

### Project Template Cloning
```bash
# Clone a template project for new work
copy C:\Templates\WebApi -d C:\Projects\NewWebApi
# Now you have a fresh project to customize
```

### Backup Before Major Changes
```bash
copy C:\ActiveProject -d D:\Backups\BeforeLargeRefactor --verify
# Verify ensures backup integrity
```

### Mirror Only Source Code
```bash
copy C:\Development\FullProject --include "*.cs" --include "*.json" --include "*.md"
# Creates copy with only essential files
```

## Backup & Archival

### Daily Backup with Verification
```bash
copy C:\Important\Data -d E:\Dailybackups\Data_%date% --verify
# Ensures all files copied correctly
```

### Selective Backup (Exclude Build Files)
```bash
copy C:\VisualStudioProject `
  --exclude "bin" `
  --exclude "obj" `
  --exclude ".git" `
  --exclude "node_modules"
# Saves space by excluding generated files
```

### Archive Only Documentation
```bash
copy C:\Project --include "*.md" --include "*.txt" --include "*.doc"
# Creates documentation-only copy
```

## Deployment & DevOps

### Quick Project Setup on New Machine
```bash
copy \\network\templates\project-structure -d C:\Dev\NewProject
# Get latest project template from network
```

### Environment Cloning
```bash
copy C:\Prod\ConfiguredApp -d C:\Staging\App --force
# Create staging environment from production config
```

### Batch Deployment
```powershell
$projects = @(
    @{src="C:\Project1"; dest="D:\Deploy1"},
    @{src="C:\Project2"; dest="D:\Deploy2"},
    @{src="C:\Project3"; dest="D:\Deploy3"}
)

foreach ($project in $projects) {
    copy $project.src -d $project.dest --quiet
    Write-Host "Deployed $($project.src)"
}
```

## Data Migration

### Large Dataset Backup with Logging
```bash
copy C:\DatabaseExports -d E:\Backup -d E:\Backups\DBExport_%date% --verify --log
# Logs every operation for audit trail
```

### Selective File Migration
```bash
# Copy only CSV exports from last month
copy C:\DataWarehouse --include "*.csv" --include "2026-06-*.json"
```

## Scripting & Automation

### Backup Script
```batch
@echo off
setlocal enabledelayedexpansion

REM Daily backup at 2 AM
set SOURCE=C:\ImportantData
set BACKUP=D:\Backups\Backup_%date:~-4,4%-%date:~-10,2%-%date:~-7,2%
copy %SOURCE% -d %BACKUP% --verify --quiet

if %errorlevel% equ 0 (
    echo Backup completed successfully
) else (
    echo Backup failed
    exit /b 1
)
```

### PowerShell Automation
```powershell
function Backup-WithTimestamp {
    param(
        [string]$Source,
        [string]$DestinationBase
    )
    
    $timestamp = Get-Date -Format "yyyy-MM-dd_HHmmss"
    $destination = "$DestinationBase\$timestamp"
    
    copy $Source -d $destination --verify --quiet
    
    Write-Host "Backup completed to: $destination"
}

# Usage
Backup-WithTimestamp -Source "C:\MyData" -DestinationBase "D:\Backups"
```

## Special Use Cases

### Exclude Everything Except One File Type
```bash
# Copy only PowerShell scripts
copy C:\Scripts --exclude "*" --include "*.ps1"
```

### Development to Production Mirror
```bash
copy C:\Dev\WebApp -d C:\Prod\WebApp --force --verify
# Ensures exact copy with integrity check
```

### Clone Without Version Control
```bash
# Copy project excluding .git, .svn, .hg
copy C:\Repo `
  --exclude ".git" `
  --exclude ".svn" `
  --exclude ".hg"
# Get clean copy without version history
```

### Multi-Format Export Backup
```bash
# Backup documents in multiple formats
copy C:\Documents `
  --include "*.docx" `
  --include "*.xlsx" `
  --include "*.pptx" `
  --include "*.pdf"
```

## Performance Examples

### Large Directory with Quiet Mode
```bash
# Copy 10GB+ without UI overhead
copy "C:\LargeDatabase" -d "E:\Backup\Large" --quiet --log
```

### Network Drive Operation
```bash
# Copy to network with logging for monitoring
copy "C:\LocalData" -d "\\NetworkServer\Share\Backup" --log
# Logs to local file for progress tracking
```

### Parallel Batch Operations
```batch
REM Copy multiple projects in sequence
start "Backup1" cmd /c "copy C:\Project1 -d D:\Backup\Project1 --quiet"
start "Backup2" cmd /c "copy C:\Project2 -d D:\Backup\Project2 --quiet"
start "Backup3" cmd /c "copy C:\Project3 -d D:\Backup\Project3 --quiet"
```

## Troubleshooting Examples

### Verify Why Copy Might Have Failed
```bash
# Use logging to see detailed operation
copy C:\Source -d C:\Destination --log

# Check log file
type copyer-log-*.txt | findstr /I "error failed"
```

### Test Pattern Matching
```bash
# First, do a dry run with include to verify pattern
copy C:\BigFolder --include "*.log" --quiet

# Check if the right files were included before committing
```

### Retry Failed Copy with Verification
```bash
copy C:\Source -d C:\Destination --force --verify
# Force overwrites any partial copy and verifies result
```

---

For complete command reference, see [COMMAND_REFERENCE.md](COMMAND_REFERENCE.md)
