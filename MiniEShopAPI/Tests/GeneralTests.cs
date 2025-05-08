using Xunit;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MiniEShopAPI;
using MiniEShopAPI.Services;
using System.Diagnostics;

namespace Tests
{
    public class ConfigurationTests
    {
        [Fact]
        /**
         * Validates that the configuration files are loaded correctly.
         */
        public void ConfigurationFiles_AreLoaded()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            // Act
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Assert
            Assert.False(string.IsNullOrEmpty(connectionString), "Connection string should not be null or empty.");
        }

        [Fact]
        public void ConfigurationFiles_ShouldBeLoaded()
        {
            // Arrange
            var solutionDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

            if (!Directory.Exists(solutionDirectory))
            {
                throw new DirectoryNotFoundException($"Solution directory not found: {solutionDirectory}");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(solutionDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true);

            var configuration = builder.Build();

            // Act
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Assert
            Assert.False(string.IsNullOrEmpty(connectionString), "Connection string should not be null or empty.");
        }

        [Fact]
        /**
         * Prints the current directory for debugging purposes.
         */
        public void PrintCurrentDirectory()
        {
            // Print the current directory to debug the issue
            Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());
        }

        [Fact]
        public void PrintTestExecutionDirectory()
        {
            // Write the current directory to a file for debugging
            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "test_execution_directory.txt");
            File.WriteAllText(outputPath, Directory.GetCurrentDirectory());
        }

        [Fact]
        public void DebugCurrentDirectory()
        {
            // Output the current directory to the console for debugging
            Console.WriteLine("Test Execution Directory: " + Directory.GetCurrentDirectory());
        }

        [Fact]
        public void PrintCurrentDirectoryForDebugging()
        {
            // Print the current directory to debug the issue
            Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());
        }

        [Fact]
        public void PrintBaseDirectory()
        {
            // Print the base directory to debug the issue
            Console.WriteLine("Base Directory: " + AppContext.BaseDirectory);
        }

        [Fact]
        /**
         * Prints the solution directory to verify its existence.
         */
        public void PrintSolutionDirectory()
        {
            // Calculate and print the solution directory
            var solutionDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            Console.WriteLine("Calculated Solution Directory: " + solutionDirectory);
            Console.WriteLine("Directory Exists: " + Directory.Exists(solutionDirectory));
        }

        [Fact]
        public void PrintExecutionPath()
        {
            // Print the execution path to debug the issue
            var executionPath = Path.Combine(Directory.GetCurrentDirectory(), "test_execution_directory.txt");
            File.WriteAllText(executionPath, Directory.GetCurrentDirectory());
            Console.WriteLine("Execution Path Written To: " + executionPath);
        }

        [Fact]
        public void CheckRootDirectoryExists()
        {
            var rootDirectory = "C:\\MiniEShopAPI";
            Assert.True(Directory.Exists(rootDirectory), $"Root directory not found: {rootDirectory}");
        }
    }

    public class OrderApiTests
    {
        private readonly HttpClient _client;

        public OrderApiTests()
        {
            _client = new HttpClient();
        }

        [Fact]
        public void RunPostmanTestsWithNewman()
        {
            // Arrange
            var solutionDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            var postmanFilePath = Path.Combine(solutionDirectory, "Tests", "postman_tests.json");

            if (!File.Exists(postmanFilePath))
            {
                throw new FileNotFoundException($"Postman collection file not found: {postmanFilePath}");
            }

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c newman run \"{postmanFilePath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process? process = null;
            try
            {
                process = Process.Start(processStartInfo);
                if (process == null)
                {
                    throw new InvalidOperationException("Failed to start the process.");
                }

                process.WaitForExit();

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                // Assert
                Assert.True(process.ExitCode == 0, $"Newman test run failed with exit code {process.ExitCode}. Error: {error}\nOutput: {output}");
                Assert.Contains("executed", output, StringComparison.OrdinalIgnoreCase);
            }
            finally
            {
                process?.Dispose();
            }
        }
    }
}