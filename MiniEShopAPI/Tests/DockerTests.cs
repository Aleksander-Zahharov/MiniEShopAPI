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
        using var client = new HttpClient();
        var requestUrl = "http://localhost:8080/api/products/test-db-connection"; // plural endpoint
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        HttpResponseMessage? response = null; // allow null initial value
        var delay = TimeSpan.FromSeconds(1);

        // Retry until API is ready or timeout
        for (int i = 0; i < 30; i++)
        {
            try
            {
                response = await client.GetAsync(requestUrl, cts.Token);
                if (response.IsSuccessStatusCode)
                    break;
            }
            catch (HttpRequestException)
            {
                // Ignore and retry
            }
            await Task.Delay(delay, cts.Token);
        }

        // Assert
        Assert.NotNull(response);
        Assert.True(response!.IsSuccessStatusCode, "API is not accessible inside Docker.");
    }
}