using DriveMate.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace DriveMate.Services.Hubs
{
    public class Hubs : Hub<IChat>
    {

        public async Task SendMessage(string message)
        {
            await Clients.All.SendMessage(message);
        }
    }
}
