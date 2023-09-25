using Film.Infrastructure.Persistance.Configs;

namespace Film.WebAPI.Setup
{
    internal static class ServiceRegistration
    {

        public static WebApplicationBuilder Setup(this WebApplicationBuilder builder)
        {
            builder.Services.SwaggerService()
                            .MVCService()
                            .AppConfig(builder.Configuration);
            return builder;
        }

        private static IServiceCollection SwaggerService(this IServiceCollection services)
        {
           return services.AddSwaggerGen();
        }

        private static IServiceCollection MVCService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            return services;
        }






    }

}
