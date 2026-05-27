using FiapCloudGames.Notifications.Application.Events;
using FiapCloudGames.Notifications.Application.Interaces;

namespace FiapCloudGames.Notifications.Application.Services
{
    public class UsuarioCriadoNotificacaoService
    {
        private readonly IEmailService _emailService;
        public UsuarioCriadoNotificacaoService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Notificar(UsuarioCriadoEvent evento)
        {
            var texto = $"Bem-vindo, {evento.Nome}! Seu usuário foi criado com sucesso.";
            await _emailService.Enviar(texto, evento.Email);
        }
    }
}
