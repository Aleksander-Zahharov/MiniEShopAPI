/**
 * Program.cs is the entry point of the application.
 * It configures and starts the web application.
 */
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MiniEShopAPI.Data;
using MiniEShopAPI.Services;
using MiniEShopAPI.Repositories;
using System.Diagnostics;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;

namespace MiniEShopAPI
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            // Ensure the application is running with the correct encoding
            if (Console.OutputEncoding != System.Text.Encoding.UTF8)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8; // Sets console output encoding to UTF-8
                Console.WriteLine("Output encoding set to UTF-8."); // Logs the encoding change
            }

            var builder = WebApplication.CreateBuilder(args); // Creates a web application builder

            // Add services to the container.
            builder.Services.AddControllers(); // Adds support for controllers
            builder.Services.AddEndpointsApiExplorer(); // Enables API endpoint exploration
            builder.Services.AddSwaggerGen(); // Adds Swagger for API documentation
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                       .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))); // Suppress pending model changes warning
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // Enables legacy timestamp behavior for compatibility

            var cacheType = builder.Configuration["CacheSettings:CacheType"] ?? "InMemory"; // Reads cache type from configuration
            builder.Services.AddSingleton<ICache>(_ => CacheFactory.CreateCache(cacheType)); // Registers the cache service

            var app = builder.Build(); // Builds the web application

            // Apply pending migrations automatically
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Database migration failed but continuing to run the app.");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // Enables Swagger in development
                app.UseSwaggerUI(); // Enables Swagger UI in development
            }

            // app.UseHttpsRedirection(); // Disabled HTTPS redirection to allow HTTP access in Docker

            app.UseAuthorization(); // Adds authorization middleware

            app.MapControllers(); // Maps controller routes

            app.Run(); // Runs the application
        }
    }
}
