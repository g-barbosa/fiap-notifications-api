namespace FiapCloudGames.Notifications.Application.Interaces
{
    public interface IEmailService
    {
        Task Enviar(string texto, string email);
    }
}
