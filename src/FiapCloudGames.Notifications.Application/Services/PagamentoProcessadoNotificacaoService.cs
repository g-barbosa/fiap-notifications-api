using FiapCloudGames.Notifications.Application.Events;
using FiapCloudGames.Notifications.Application.Interaces;

namespace FiapCloudGames.Notifications.Application.Services
{
    public class PagamentoProcessadoNotificacaoService
    {
        private readonly IEmailService _emailService;
        public PagamentoProcessadoNotificacaoService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Notificar(PagamentoProcessadoEvent evento)
        {
            string? texto;
            if (evento.Status == "Aprovado")
            {
                texto = $"Olá, {evento.NomeUsuario}! Seu pedido {evento.PedidoId} foi processado com sucesso.";
            }
            else
            {
                texto = $"Olá, {evento.NomeUsuario}! Infelizmente, seu pedido {evento.PedidoId} não foi aprovado. Por favor, entre em contato com o suporte para mais informações.";
            }

            await _emailService.Enviar(texto, evento.Email);
        }
    }
}
