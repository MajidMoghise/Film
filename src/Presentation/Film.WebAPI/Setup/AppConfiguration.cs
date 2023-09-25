using Microsoft.AspNetCore.Builder;
using Film.WebAPI.Middelwares;
using Film.Infrastructure.Persistance.Configs;

namespace Film.WebAPI.Setup
{
    internal static class AppConfiguration
    {
       
        public static void AppConfig(this WebApplication app)
        {

            app.UseExceptionHandler(err => err.Use((HttpContext httpContext, Func<Task> next) => ExceptionMiddleware.ExceptionMiddle(httpContext)));
            app.SwaggerConfig();
            app.MvcConfig();
        }

        private static void SwaggerConfig(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
        }

        private static void MvcConfig(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();

        }
    }

}
