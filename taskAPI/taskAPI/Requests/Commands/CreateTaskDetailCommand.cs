using MediatR;
using taskAPI.Models;

namespace taskAPI.Requests.Commands
{
    public class CreateTaskDetailCommand : IRequest<TaskDetail>
    {
        public TaskDetail TaskDetail { get; }
        public CreateTaskDetailCommand(TaskDetail taskDetail)
        {
            TaskDetail = taskDetail;
        }
    }
}
