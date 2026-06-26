using Docklly.Services;
using Microsoft.AspNetCore;

namespace Docklly.Extension
{
    public sealed class AddServiceScop 
    {
        public AddServiceScop(ServiceCollection services)
        {
            services.AddScoped<UsersServices>();
        }
    }
}