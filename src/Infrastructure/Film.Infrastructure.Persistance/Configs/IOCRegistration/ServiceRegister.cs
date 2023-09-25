using Film.Application.Contract.Category;
using Film.Application.Contract.Film;
using Film.Application.Contract.UpLoad;
using Film.Application.Services.Category;
using Film.Application.Services.Film;
using Film.Application.Services.Upload;
using Microsoft.Extensions.DependencyInjection;

namespace Film.Infrastructure.Persistance.Configs.IOCRegistration
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection ServiceRegister(this IServiceCollection services)
        {
           return services.AddScoped();
        }
        private static IServiceCollection AddScoped(this IServiceCollection services)
        {
           return services.AddScoped<IFilmService, FilmService>()
                          .AddScoped<ICategoryService, CategoryService>()
                          .AddScoped<IUploadService, UploadService>()
                          ;
                        
        }
       
    }

}
