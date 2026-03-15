using Microsoft.AspNetCore.SignalR;

namespace TarasMessanger.Services.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification()
        {
            await Clients.All.SendAsync("ReceiveNotification");
        }
    }
}