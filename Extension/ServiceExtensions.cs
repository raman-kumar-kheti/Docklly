using Docklly.Services;

namespace Docklly.Extension
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Extension method to register all application services
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Register domain services
            services.AddScoped<IUsersService, UsersServices>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IAiDocumentService, AiDocumentService>();
            services.AddScoped<IComplianceService, ComplianceService>();n            
            return services;
        }
    }
}
