using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace meetupSignalRCore2.Hubs.Interfaces
{
    public interface ITennisClient
    {
        Task ReceivePointsEvents(string points);

        Task ReceiveGamesEvents(string game);

        Task SendFinishGame(string result);
    }
}
