/**
 * DockerTests contains tests to verify Docker container functionality.
 */
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class DockerTests
{
    [Fact]
    /**
     * Verifies that Docker Compose can successfully start the required containers.
     */
    public async Task DockerCompose_ShouldStartContainers()
    {
        // Arrange
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "docker", // Specifies the Docker executable
            Arguments = "compose up --build -d", // Command to build and start containers in detached mode
            RedirectStandardOutput = true, // Redirects standard output
            RedirectStandardError = true, // Redirects standard error
            UseShellExecute = false, // Disables shell execution
            CreateNoWindow = true // Prevents creating a new window
        };

        Process? process = null; // Nullable process variable
        try
        {
            // Act
            process = Process.Start(processStartInfo); // Starts the Docker process
            if (process == null)
            {
                throw new InvalidOperationException("Failed to start the process."); // Throws an exception if the process fails to start
            }

            await Task.Run(() => process.WaitForExit()); // Waits asynchronously for the process to exit

            // Assert
            Assert.NotNull(process); // Asserts that the process is not null
            if (process != null)
            {
                Assert.Equal(0, process.ExitCode); // Asserts that the process exited successfully
            }
        }
        finally
        {
            process?.Kill(); // Kills the process if it is still running
            process?.Dispose(); // Disposes of the process resources
        }
    }

    [Fact]
    /**
     * Ensures that the API is accessible when Docker containers are running.
     */
    public async Task Api_ShouldBeAccessible_WhenDockerIsRunning()
    {
        // Arrange
        using var client = new HttpClient(); // Creates an HTTP client
        var requestUrl = "http://localhost:5000/api/product"; // API endpoint to test

        // Act
        var response = await client.GetAsync(requestUrl); // Sends a GET request to the API

        // Assert
        Assert.True(response.IsSuccessStatusCode, "API is not accessible inside Docker."); // Asserts that the API is accessible
    }
}