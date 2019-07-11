using meetupSignalRCore2.DataStorage;
using meetupSignalRCore2.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;

namespace meetupSignalRCore2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        #region ctor
        private IHubContext<ChartHub> _hub;

        public ChartController(IHubContext<ChartHub> hub)
        {
            _hub = hub;
        }
        #endregion

        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", ChartManager.GetData()));

            return Ok(new { Message = "Request Completed" });
        }
    }

    /// <summary>
    /// To simulate a real-time data flow from the server
    /// </summary>
    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimerStarted { get; }

        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
            TimerStarted = DateTime.Now;
        }

        public void Execute(object stateInfo)
        {
            _action();

            if ((DateTime.Now - TimerStarted).Seconds > 60)
            {
                _timer.Dispose();
            }
        }
    }
}
