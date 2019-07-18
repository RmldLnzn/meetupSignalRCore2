using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meetupSignalRCore2.Hubs;
using meetupSignalRCore2.Hubs.Interfaces;
using meetupSignalRCore2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace meetupSignalRCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TennisController : Controller
    {
        private readonly TennisGameService _tennisGameService;

        public TennisController(TennisGameService tennisGameService)
        {
            _tennisGameService = tennisGameService;
        }

        public IActionResult Get()
        {
            _tennisGameService.StartGame();
            return Ok();
        }
    }
}