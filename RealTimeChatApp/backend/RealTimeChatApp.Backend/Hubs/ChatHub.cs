using Microsoft.AspNetCore.SignalR;

namespace YourNamespace.Hubs
{
    public class ChatHub : Hub
    {
        // ✅ Send private message (with optional file)
        public async Task SendPrivate(string receiverUserId, string message, string fileUrl = null)
        {
            await Clients.User(receiverUserId).SendAsync("ReceiveMessage",
                Context.UserIdentifier, message, fileUrl, DateTime.UtcNow);
        }

        // ✅ Send group message (with optional file)
        public async Task SendGroup(string groupName, string message, string fileUrl = null)
        {
            await Clients.Group(groupName).SendAsync("ReceiveGroupMessage",
                Context.UserIdentifier, message, fileUrl, DateTime.UtcNow);
        }

        // Typing indicator (no file needed here)
        public async Task Typing(string toUserId)
            => await Clients.User(toUserId).SendAsync("Typing", Context.UserIdentifier);

        // Join group
        public async Task JoinGroup(string groupName)
            => await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        // Leave group
        public async Task LeaveGroup(string groupName)
            => await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
