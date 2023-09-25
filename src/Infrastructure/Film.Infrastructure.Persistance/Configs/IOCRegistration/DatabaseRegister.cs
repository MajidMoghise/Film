using Film.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Infrastructure.Persistance.Configs.IOCRegistration
{
    public static class DatabaseRegister
    {
        public static IServiceCollection ContextRegister(this IServiceCollection services, string ConnectionString)
        {
           return services.AddDbContext<FilmDbContext>(options =>
                                options.UseSqlServer(ConnectionString));
        }
    }

}
