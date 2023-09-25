using Microsoft.Extensions.DependencyInjection;
using static System.Net.Mime.MediaTypeNames;

namespace Film.Infrastructure.Persistance.Configs.IOCRegistration
{
    internal static class Registration
    {
        internal static IServiceCollection RegisterIOC(this IServiceCollection services, string connectionString)
        {
            return services.RepositoryRegister()
                           .ServiceRegister()
                           .ContextRegister(connectionString);
           
        }
    }

}
