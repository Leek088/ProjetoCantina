using Microsoft.AspNetCore.SignalR;
using ProjetoCantina.WEB.Hub;

namespace ProjetoCantina.WEB.Util;

public static class InfoNotificacao
{
    public static string Mensagem { get; set; } = string.Empty;

    public static async Task EnviarNotificacaoAsync(IHubContext<NotificacaoHub> hubContext)
    {
        await hubContext.Clients.All
                .SendAsync("ReceiveMessage", Mensagem);
    }   
}