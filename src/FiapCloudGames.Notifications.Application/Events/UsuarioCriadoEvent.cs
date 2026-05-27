namespace FiapCloudGames.Notifications.Application.Events
{
    public class UsuarioCriadoEvent
    {
        public Guid UsuarioId { get; init; }

        public string Nome { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;
    }
}
