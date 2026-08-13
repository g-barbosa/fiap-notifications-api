
using FiapCloudGames.Notifications.Application.Interaces;
using FiapCloudGames.Notifications.Application.Services;
using FiapCloudGames.Notifications.Infrastructure.Email;
using FiapCloudGames.Notifications.Infrastructure.Messaging.Consumers;
using Serilog;
using Prometheus;

namespace FiapCloudGames.Notifications.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Host.UseSerilog((context, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext();
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "FIAP Cloud Games API - API de Notificações"
                });
            });

            builder.Services.AddHealthChecks();

            builder.Services.AddScoped<UsuarioCriadoNotificacaoService>();
            builder.Services.AddScoped<PagamentoProcessadoNotificacaoService>();
            builder.Services.AddScoped<IEmailService, SimuladorEmailService>();
            builder.Services.AddHostedService<UsuarioCriadoConsumer>();
            builder.Services.AddHostedService<PagamentoProcessadoConsumer>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpMetrics();
            app.MapHealthChecks("/health");
            app.MapMetrics();

            app.Run();
        }
    }
}
