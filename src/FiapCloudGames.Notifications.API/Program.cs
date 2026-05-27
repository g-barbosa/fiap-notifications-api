
using FiapCloudGames.Notifications.Application.Interaces;
using FiapCloudGames.Notifications.Application.Services;
using FiapCloudGames.Notifications.Infrastructure.Email;
using FiapCloudGames.Notifications.Infrastructure.Messaging.Consumers;

namespace FiapCloudGames.Notifications.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<UsuarioCriadoNotificacaoService>();
            builder.Services.AddScoped<IEmailService, SimuladorEmailService>();
            builder.Services.AddHostedService<UsuarioCriadoConsumer>();

            var app = builder.Build();

            app.Run();
        }
    }
}
