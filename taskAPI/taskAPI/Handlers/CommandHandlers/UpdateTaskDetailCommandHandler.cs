using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Notification;
using System.Net;
using taskAPI.DataContext;
using taskAPI.Models;
using taskAPI.Requests.Commands;

namespace taskAPI.Handlers.CommandHandlers
{
    public class UpdateTaskDetailCommandHandler : IRequestHandler<UpdateTaskDetailCommand, HttpStatusCode>
    {
        private readonly TasksDataContext _context;
        private readonly IHubContext<SignalHub> _hub;
        public UpdateTaskDetailCommandHandler(TasksDataContext context, IHubContext<SignalHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<HttpStatusCode> Handle(UpdateTaskDetailCommand command, CancellationToken cancellationToken)
        {
            if (command.Id != command.TaskDetail.Id)
            {
                return HttpStatusCode.BadRequest;
            }

            _context.Entry(command.TaskDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskDetailExists(command.Id))
                {
                    return HttpStatusCode.NotFound;
                }
                else
                {
                    throw;
                }
            }
            await _hub.Clients.All.SendAsync("publicMessageMethodName", "updateTheList");
            return HttpStatusCode.NoContent;
        }
        private bool TaskDetailExists(string id)
        {
            return (_context.TaskDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
