using MediatR;
using Microsoft.AspNetCore.SignalR;
using Notification;
using System.Net;
using taskAPI.DataContext;
using taskAPI.Requests.Commands;

namespace taskAPI.Handlers.CommandHandlers
{
    public class DeleteTaskDetailCommandHandler : IRequestHandler<DeleteTaskDetailCommand, HttpStatusCode>
    {
        private readonly TasksDataContext _context;
        private readonly IHubContext<SignalHub> _hub;

        public DeleteTaskDetailCommandHandler(IHubContext<SignalHub> hub, TasksDataContext context)
        {
            _hub = hub;
            _context = context;
        }

        public async Task<HttpStatusCode> Handle(DeleteTaskDetailCommand command, CancellationToken cancellationToken)
        {
            if (_context.TaskDetails == null)
            {
                return HttpStatusCode.NotFound;
            }
            var taskDetail = await _context.TaskDetails.FindAsync(command.Id);
            if (taskDetail == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.TaskDetails.Remove(taskDetail);
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("publicMessageMethodName", "updateTheList");
            return HttpStatusCode.NoContent;
        }
    }
}
