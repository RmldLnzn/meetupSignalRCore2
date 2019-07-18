using meetupSignalRCore2.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meetupSignalRCore2.Hubs
{
    public class TennisHub: Hub<ITennisClient>
    {
        public async Task SubscribeToPointsEvents()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "pointsEventsSubscribers");
        }

        public async Task SubscribeToGamesEvents()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "gamesEventsSubscribers");
        }

        public async Task UnsubscribeToPointsEvents()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "pointsEventsSubscribers");
        }

        public async Task UnsubscribeToGamesEvents()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "gamesEventsSubscribers");
        }
    }
}
