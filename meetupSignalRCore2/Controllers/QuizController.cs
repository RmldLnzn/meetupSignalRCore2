using meetupSignalRCore2.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace meetupSignalRCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        #region ctor
        private IHubContext<QuizHub> _hub;

        public QuizController(IHubContext<QuizHub> hub)
        {
            _hub = hub;
        }
        #endregion
    }
}