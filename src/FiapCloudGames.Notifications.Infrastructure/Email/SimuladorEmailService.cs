using FiapCloudGames.Notifications.Application.Interaces;

namespace FiapCloudGames.Notifications.Infrastructure.Email
{
    public class SimuladorEmailService : IEmailService
    {
        public async Task Enviar(string texto, string email)
        {
            Console.WriteLine($"Enviando email para {email} com o texto: {texto}");
        }
    }
}
