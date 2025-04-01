using Microsoft.AspNetCore.SignalR;

namespace StudentCenterWeb.Util;

public class StatusHub : Hub
{
    public async Task JoinGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        await Clients.Group(groupId).SendAsync("Notification", $"Conectado ao grupo {groupId}");
    }

    public async Task UpdateStatus(string groupId, string status)
    {
        await Clients.Group(groupId).SendAsync("AtualizarBadge", groupId, status);
    }
}
