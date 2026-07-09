namespace Docklly.Extension
{
    public static class HealthCheckExtensions
    {
        public static void MapHealthCheck(this WebApplication app)
        {
            app.MapGet("/health", async (IServiceProvider serviceProvider) =>
            {
                try
                {
                    // Check database connectivity
                    var dbContext = serviceProvider.GetRequiredService<Database.AppDbContext>();
                    var canConnect = await dbContext.Database.CanConnectAsync();
                    
                    return new
                    {
                        status = canConnect ? "Healthy" : "Unhealthy",
                        timestamp = DateTime.UtcNow,
                        database = canConnect ? "Connected" : "Disconnected",
                        version = "1.0.0"
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        status = "Unhealthy",
                        error = ex.Message,
                        timestamp = DateTime.UtcNow
                    };
                }
            }).WithName("Health Check").WithOpenApi();
        }
    }
}
