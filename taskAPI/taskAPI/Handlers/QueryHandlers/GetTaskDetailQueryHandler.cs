using MediatR;
using taskAPI.DataContext;
using taskAPI.Models;
using taskAPI.Requests.Queries;

namespace taskAPI.Handlers.QueryHandlers
{
    public class GetTaskDetailQueryHandler : IRequestHandler<GetTaskDetailQuery, TaskDetail>
    {
        private readonly TasksDataContext _context;

        public GetTaskDetailQueryHandler(TasksDataContext context)
        {
            _context = context;
        }

        public async Task<TaskDetail> Handle(GetTaskDetailQuery query, CancellationToken cancellationToken)
        {
            if (_context.TaskDetails == null)
            {
                return null;
            }
            var taskDetail = await _context.TaskDetails.FindAsync(query.Id);

            if (taskDetail == null)
            {
                return null;
            }

            return taskDetail;
        }
    }
}
