using Microsoft.Extensions.DependencyInjection;
using NoviAMS.BFF.Interfaces;
using NoviAMS.BFF.Services;

namespace NoviAMS.BFF.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
        }
    }
}

