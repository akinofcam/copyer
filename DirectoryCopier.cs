using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;

namespace Copyer
{
    /// <summary>
    /// Handles recursive directory copying with progress reporting, filtering, and verification.
    /// </summary>
    public class DirectoryCopier
    {
        private readonly ProgressDialog? _progressDialog;
        private string? _excludePattern;
        private string? _includePattern;
        private bool _verifyFiles;
        private long _totalFiles = 0;
        private long _copiedFiles = 0;
        private long _skippedFiles = 0;
        private long _failedFiles = 0;

        /// <summary>
        /// Gets or sets whether to enable detailed logging.
        /// </summary>
        public bool EnableLogging { get; set; }

        public DirectoryCopier(
            ProgressDialog? progressDialog,
            string? excludePattern = null,
            string? includePattern = null,
            bool verifyFiles = false)
        {
            _progressDialog = progressDialog;
            _excludePattern = excludePattern;
            _includePattern = includePattern;
            _verifyFiles = verifyFiles;
        }

        /// <summary>
        /// Copies a directory and all its contents recursively.
        /// </summary>
        public async Task CopyDirectoryAsync(string sourceDir, string destDir)
        {
            // First pass: count total files
            _totalFiles = CountFiles(sourceDir);
            _copiedFiles = 0;
            _skippedFiles = 0;
            _failedFiles = 0;

            if (EnableLogging)
                Log.Information("Total files to copy: {Count}", _totalFiles);

            // Update dialog with total count
            _progressDialog?.SetTotalFiles(_totalFiles);
            _progressDialog?.Show();

            try
            {
                // Second pass: perform the actual copy
                await CopyDirectoryRecursiveAsync(sourceDir, destDir);

                if (EnableLogging)
                    Log.Information("Copy completed: {Copied} copied, {Skipped} skipped, {Failed} failed",
                        _copiedFiles, _skippedFiles, _failedFiles);
            }
            finally
            {
                // Keep dialog open for a moment so user can see completion
                await Task.Delay(500);
                _progressDialog?.Hide();
            }
        }

        /// <summary>
        /// Recursively copies directory contents with filtering.
        /// </summary>
        private async Task CopyDirectoryRecursiveAsync(string sourceDir, string destDir)
        {
            // Create the destination directory
            Directory.CreateDirectory(destDir);

            var sourceInfo = new DirectoryInfo(sourceDir);

            // Copy all files
            foreach (var file in sourceInfo.GetFiles())
            {
                // Check if file should be excluded/included
                if (!ShouldProcessFile(file.Name))
                {
                    _skippedFiles++;
                    if (EnableLogging)
                        Log.Debug("Skipping file (filtered): {File}", file.Name);
                    continue;
                }

                string sourceFile = file.FullName;
                string destFile = Path.Combine(destDir, file.Name);

                try
                {
                    File.Copy(sourceFile, destFile, true);

                    // Verify if requested
                    if (_verifyFiles)
                    {
                        if (!VerifyFileCopy(sourceFile, destFile))
                        {
                            _failedFiles++;
                            if (EnableLogging)
                                Log.Warning("Verification failed for: {File}", file.Name);
                            continue;
                        }
                    }

                    _copiedFiles++;
                    _progressDialog?.UpdateProgress(_copiedFiles, _totalFiles);

                    if (EnableLogging)
                        Log.Debug("Copied: {File}", file.Name);
                }
                catch (Exception ex)
                {
                    _failedFiles++;
                    if (EnableLogging)
                        Log.Error(ex, "Failed to copy file: {File}", file.Name);
                }

                // Allow UI to update
                await Task.Delay(5);
            }

            // Copy all subdirectories recursively
            foreach (var dir in sourceInfo.GetDirectories())
            {
                string sourceSubDir = dir.FullName;
                string destSubDir = Path.Combine(destDir, dir.Name);

                await CopyDirectoryRecursiveAsync(sourceSubDir, destSubDir);
            }
        }

        /// <summary>
        /// Determines if a file should be processed based on include/exclude patterns.
        /// </summary>
        private bool ShouldProcessFile(string fileName)
        {
            // If include pattern is specified, file must match it
            if (!string.IsNullOrEmpty(_includePattern))
            {
                if (!PatternMatch(fileName, _includePattern))
                    return false;
            }

            // If exclude pattern is specified, file must not match it
            if (!string.IsNullOrEmpty(_excludePattern))
            {
                if (PatternMatch(fileName, _excludePattern))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if a filename matches a glob pattern.
        /// </summary>
        private bool PatternMatch(string fileName, string pattern)
        {
            // Convert glob pattern to regex
            string regexPattern = "^" + Regex.Escape(pattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".")
                + "$";

            return Regex.IsMatch(fileName, regexPattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Verifies that a file was copied correctly by comparing file sizes and hashes.
        /// </summary>
        private bool VerifyFileCopy(string sourceFile, string destFile)
        {
            try
            {
                // First check: file size
                var sourceInfo = new FileInfo(sourceFile);
                var destInfo = new FileInfo(destFile);

                if (sourceInfo.Length != destInfo.Length)
                {
                    return false;
                }

                // Second check: SHA256 hash
                using (var sha256 = SHA256.Create())
                {
                    byte[] sourceHash = ComputeFileHash(sourceFile, sha256);
                    byte[] destHash = ComputeFileHash(destFile, sha256);

                    return CompareHashes(sourceHash, destHash);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Computes SHA256 hash of a file.
        /// </summary>
        private byte[] ComputeFileHash(string filePath, HashAlgorithm algorithm)
        {
            using (var stream = File.OpenRead(filePath))
            {
                return algorithm.ComputeHash(stream);
            }
        }

        /// <summary>
        /// Compares two hash byte arrays.
        /// </summary>
        private bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
                return false;

            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Counts total files in a directory recursively.
        /// </summary>
        private long CountFiles(string directory)
        {
            long count = 0;

            try
            {
                var dir = new DirectoryInfo(directory);

                // Count files in current directory
                foreach (var file in dir.GetFiles())
                {
                    if (ShouldProcessFile(file.Name))
                        count++;
                }

                // Count files in subdirectories
                foreach (var subDir in dir.GetDirectories())
                {
                    count += CountFiles(subDir.FullName);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Skip directories we can't access
            }

            return count;
        }
    }
}
