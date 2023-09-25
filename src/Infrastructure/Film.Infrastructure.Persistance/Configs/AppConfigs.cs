using Film.Infrastructure.Persistance.Configs.IOCRegistration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Film.Infrastructure.Persistance.Configs
{
    public static class AppConfigs
    {
        public static IServiceCollection AppConfig(this IServiceCollection services,IConfiguration configuration) {

            var cnn = configuration.GetConnectionString("BloggingDatabase");
            return services.RegisterIOC(cnn);
        }
    }

}
