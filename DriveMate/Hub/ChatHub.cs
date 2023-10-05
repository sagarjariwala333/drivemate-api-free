using Microsoft.AspNetCore.SignalR;

namespace DriveMate.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Clients.All.SendAsync("Viraj hey!", user, message); //Whoever is connected send message
            //Clients.User("id"); for a specific user
        }
    }
}

