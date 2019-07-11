using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace meetupSignalRCore2.Hubs
{
    public class HelloHub : Hub
    {
        public async Task SayHello(string message)
        {
            await Clients.All.SendAsync("HelloFrom", message);
        }

        public async Task SayolleH(string message)
        {
            string reverseMessage = new string(message.Reverse().ToArray());
            //reverseMessage = $"{reverseMessage} (Id: {Context.ConnectionId})";
            await Clients.All.SendAsync("HelloFrom", reverseMessage);
        }
    }
}