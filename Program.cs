using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;

namespace Copyer
{
    /// <summary>
    /// Copyer - Advanced directory cloning tool with progress dialog
    /// </summary>
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("copyer-log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                // Parse command-line arguments
                var parser = CliParser.Parse(args);

                // Handle special commands
                if (parser.ShowHelp)
                {
                    CliParser.PrintHelp();
                    return 0;
                }

                if (parser.ShowVersion)
                {
                    CliParser.PrintVersion();
                    return 0;
                }

                // Validate source directory
                if (string.IsNullOrEmpty(parser.SourceDirectory))
                {
                    Console.WriteLine("Error: Source directory not specified.");
                    Console.WriteLine("Use 'copy --help' for usage information.");
                    return 1;
                }

                await HandleCopyCommand(parser);
                return 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Fatal error occurred");
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }

        /// <summary>
        /// Handles the copy command execution
        /// </summary>
        static async Task HandleCopyCommand(CliParser parser)
        {
            string source = parser.SourceDirectory!;

            // Validate source
            if (!Directory.Exists(source))
            {
                Log.Error("Source directory not found: {Source}", source);
                Console.WriteLine($"Error: Source directory '{source}' does not exist.");
                return;
            }

            // Normalize path
            source = Path.GetFullPath(source);

            // Resolve destination
            string destination = parser.DestinationDirectory ?? Directory.GetCurrentDirectory();
            string dirName = new DirectoryInfo(source).Name;
            string targetPath = Path.Combine(destination, dirName);

            // Check destination
            if (Directory.Exists(targetPath) && !parser.ForceOverwrite)
            {
                Log.Warning("Destination already exists and force is not enabled: {Target}", targetPath);
                Console.WriteLine($"Error: Destination '{targetPath}' already exists.");
                Console.WriteLine("Use --force or -f to overwrite existing directories.");
                return;
            }

            if (Directory.Exists(targetPath) && parser.ForceOverwrite)
            {
                Log.Information("Removing existing destination directory: {Target}", targetPath);
                Console.WriteLine("Removing existing destination...");
                Directory.Delete(targetPath, true);
            }

            try
            {
                Log.Information("Starting copy operation: {Source} -> {Target}", source, targetPath);
                if (parser.ShowLogging)
                {
                    Console.WriteLine($"Copying from: {source}");
                    Console.WriteLine($"Copying to: {targetPath}");
                }

                using (var progressDialog = parser.QuietMode ? null : new ProgressDialog())
                {
                    var copier = new DirectoryCopier(
                        progressDialog,
                        parser.ExcludePattern,
                        parser.IncludePattern,
                        parser.VerifyFiles)
                    {
                        EnableLogging = parser.ShowLogging
                    };

                    await copier.CopyDirectoryAsync(source, targetPath);
                }

                Log.Information("Copy operation completed successfully: {Target}", targetPath);
                Console.WriteLine($"\n✓ Success! Directory cloned to: {targetPath}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Copy operation failed");
                Console.WriteLine($"✗ Error during copy: {ex.Message}");
            }
        }
    }
}


