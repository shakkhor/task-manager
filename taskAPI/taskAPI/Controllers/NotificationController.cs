﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notification;

namespace taskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IHubContext<SignalHub> _hub;
        public NotificationController(IHubContext<SignalHub> hub)
        {
            _hub = hub;
        }

        /// <summary>
        /// Send message to all
        /// </summary>
        /// <param name="message"></param>
        [HttpPost("{message}")]
        public void Post(string message)
        {
            _hub.Clients.All.SendAsync("publicMessageMethodName", message);
        }

        /// <summary>
        /// Send message to specific client
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="message"></param>
        [HttpPost("{connectionId}/{message}")]
        public void Post(string connectionId, string message)
        {
            _hub.Clients.Client(connectionId).SendAsync("privateMessageMethodName", message);
        }
    }
}
