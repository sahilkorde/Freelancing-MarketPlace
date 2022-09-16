using Microsoft.AspNetCore.SignalR;

namespace Trial3.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectioId() =>
            Context.ConnectionId;
    }
}