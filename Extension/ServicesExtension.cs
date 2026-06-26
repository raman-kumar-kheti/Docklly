using Docklly.Services;
using Microsoft.AspNetCore;

namespace Docklly.Extension
{
    public static class AddServiceScop
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<UsersServices>();
        }
    }
}