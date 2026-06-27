using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Copyer
{
    /// <summary>
    /// Command-line argument parser for Copyer
    /// </summary>
    public class CliParser
    {
        public string? SourceDirectory { get; set; }
        public string? DestinationDirectory { get; set; }
        public bool ForceOverwrite { get; set; }
        public bool VerifyFiles { get; set; }
        public bool ShowLogging { get; set; }
        public string? ExcludePattern { get; set; }
        public string? IncludePattern { get; set; }
        public bool QuietMode { get; set; }
        public bool ShowHelp { get; set; }
        public bool ShowVersion { get; set; }

        /// <summary>
        /// Parses command-line arguments
        /// </summary>
        public static CliParser Parse(string[] args)
        {
            var parser = new CliParser();

            if (args.Length == 0)
            {
                parser.ShowHelp = true;
                return parser;
            }

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                switch (arg.ToLower())
                {
                    case "-h":
                    case "--help":
                        parser.ShowHelp = true;
                        break;

                    case "-v":
                    case "--version":
                        parser.ShowVersion = true;
                        break;

                    case "--verify":
                        parser.VerifyFiles = true;
                        break;

                    case "-f":
                    case "--force":
                        parser.ForceOverwrite = true;
                        break;

                    case "--log":
                        parser.ShowLogging = true;
                        break;

                    case "-q":
                    case "--quiet":
                        parser.QuietMode = true;
                        break;

                    case "-d":
                    case "--destination":
                        if (i + 1 < args.Length)
                        {
                            parser.DestinationDirectory = args[++i];
                        }
                        break;

                    case "-e":
                    case "--exclude":
                        if (i + 1 < args.Length)
                        {
                            parser.ExcludePattern = args[++i];
                        }
                        break;

                    case "-i":
                    case "--include":
                        if (i + 1 < args.Length)
                        {
                            parser.IncludePattern = args[++i];
                        }
                        break;

                    default:
                        // Assume it's the source directory if it doesn't start with -
                        if (!arg.StartsWith("-") && parser.SourceDirectory == null)
                        {
                            parser.SourceDirectory = arg;
                        }
                        break;
                }
            }

            return parser;
        }

        /// <summary>
        /// Prints help information
        /// </summary>
        public static void PrintHelp()
        {
            Console.WriteLine("Copyer v2.0.0 - Advanced Directory Cloning Tool");
            Console.WriteLine("==============================================\n");
            Console.WriteLine("USAGE:");
            Console.WriteLine("  copy <source> [OPTIONS]\n");

            Console.WriteLine("ARGUMENTS:");
            Console.WriteLine("  <source>              Source directory to clone\n");

            Console.WriteLine("OPTIONS:");
            Console.WriteLine("  -d, --destination     Destination path (default: current directory)");
            Console.WriteLine("  -f, --force           Force overwrite if destination exists");
            Console.WriteLine("  -v, --verify          Verify file integrity after copying");
            Console.WriteLine("  -e, --exclude <PATTERN>  Exclude files matching pattern (e.g., '*.log')");
            Console.WriteLine("  -i, --include <PATTERN>  Include only files matching pattern");
            Console.WriteLine("  -q, --quiet           Suppress progress dialog");
            Console.WriteLine("  --log                 Show detailed logging output");
            Console.WriteLine("  -h, --help            Show this help message");
            Console.WriteLine("  --version             Show version information\n");

            Console.WriteLine("EXAMPLES:");
            Console.WriteLine("  copy C:\\MyProject");
            Console.WriteLine("  copy C:\\MyProject -d D:\\Backup");
            Console.WriteLine("  copy C:\\MyProject --exclude '*.tmp' --exclude '*.log'");
            Console.WriteLine("  copy /home/user/project --verify --force\n");

            Console.WriteLine("PATTERNS:");
            Console.WriteLine("  * matches any characters");
            Console.WriteLine("  ? matches a single character");
            Console.WriteLine("  Example: *.log matches all .log files\n");
        }

        /// <summary>
        /// Prints version information
        /// </summary>
        public static void PrintVersion()
        {
            Console.WriteLine("Copyer v2.0.0");
            Console.WriteLine("Advanced directory cloning tool with progress dialog");
            Console.WriteLine("Repository: https://github.com/akinofcam/copyer");
        }
    }
}
