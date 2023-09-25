using Category.Infrastructure.Persistance.Repositories.Category;
using Film.Domain.Contract.Base.Repository;
using Film.Domain.Contract.Category;
using Film.Domain.Contract.Film;
using Film.Infrastructure.Persistance.Repositories.Base;
using Film.Infrastructure.Persistance.Repositories.Film;
using Microsoft.Extensions.DependencyInjection;

namespace Film.Infrastructure.Persistance.Configs.IOCRegistration
{
    internal static class RepositoryRegisteration
    {
        internal static IServiceCollection RepositoryRegister(this IServiceCollection services)
        {
            return services.AddScoped();
        }
        private static IServiceCollection AddScoped(this IServiceCollection services)
        {
            return services.AddScoped<IFilmRepository, FilmRepository>()
                           .AddScoped<ICategoryRepository, CategoryRepository>()
                           .AddScoped<IUnitOfWork, UnitOfWork>();

        }

    }

}
