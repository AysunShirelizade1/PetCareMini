using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PetCareMini.Application.Abstracts.Hubs;

namespace PetCareMini.WebApi.Hubs;

[Authorize]
public class NotificationHub : Hub, INotificationHub
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationHub(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(int userId, object notification)
    {
        await _hubContext.Clients
            .Group($"user_{userId}")
            .SendAsync("ReceiveNotification", notification);
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");

        await base.OnDisconnectedAsync(exception);
    }
}