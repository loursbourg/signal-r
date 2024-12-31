using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    public async Task SendNotification(string message)
    {
        // Broadcast the event to all connected clients
        await Clients.All.SendAsync("NotificationReceived", message);
    }
}
