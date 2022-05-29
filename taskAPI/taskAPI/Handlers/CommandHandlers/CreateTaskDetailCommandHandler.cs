using MediatR;
using Microsoft.AspNetCore.SignalR;
using Notification;
using taskAPI.DataContext;
using taskAPI.Models;
using taskAPI.Requests.Commands;

namespace taskAPI.Handlers.CommandHandlers
{
    public class CreateTaskDetailCommandHandler : IRequestHandler<CreateTaskDetailCommand, TaskDetail>
    {
        private readonly TasksDataContext _context;
        private readonly IHubContext<SignalHub> _hub;
        public CreateTaskDetailCommandHandler(TasksDataContext context, IHubContext<SignalHub> hub)
        {
            _context = context;
            _hub = hub;
        }


        public async Task<TaskDetail> Handle(CreateTaskDetailCommand command, CancellationToken cancellationToken)
        {
            if (_context.TaskDetails == null)
            {
                return null;
            }
            _context.TaskDetails.Add(command.TaskDetail);
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("publicMessageMethodName", "updateTheList");
            return command.TaskDetail;
        }
    }
}
