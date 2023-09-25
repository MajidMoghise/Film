using Film.WebAPI.Setup;
using Film.Infrastructure.Persistance.Configs;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args)
                                    .Setup();

        var app = builder.Build();
        app.AppConfig();
    }
}