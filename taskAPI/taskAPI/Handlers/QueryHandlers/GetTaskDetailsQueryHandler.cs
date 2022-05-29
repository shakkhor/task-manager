using MediatR;
using Microsoft.EntityFrameworkCore;
using taskAPI.DataContext;
using taskAPI.Models;
using taskAPI.Requests.Queries;

namespace taskAPI.Handlers.QueryHandlers
{
    public class GetTaskDetailsQueryHandler : IRequestHandler<GetTaskDetailsQuery, List<TaskDetail>>
    {
        private readonly TasksDataContext _context;

        public GetTaskDetailsQueryHandler(TasksDataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskDetail>> Handle(GetTaskDetailsQuery query, CancellationToken cancellationToken)
        {
            if (_context.TaskDetails == null)
            {
                return null;
            }
            
            var filtered = _context.TaskDetails.Where(x => !string.IsNullOrEmpty(x.Id));
            if(!string.IsNullOrEmpty(query.SearchText))
            {
                filtered = filtered.Where(x => x.Title.ToLower().Contains(query.SearchText.ToLower()));
            }
            if(query.SortBy == "Title" && query.SortOrder == 0)
            {
                filtered = filtered.OrderBy(x => x.Title);
            }
            else if (query.SortBy == "Title" && query.SortOrder == 1)
            {
                filtered = filtered.OrderByDescending(x => x.Title);
            }
            else if (query.SortBy == "CreateDate" && query.SortOrder == 0)
            {
                filtered = filtered.OrderBy(x => x.CreatedDate);
            }
            else if (query.SortBy == "CreateDate" && query.SortOrder == 1)
            {
                filtered = filtered.OrderByDescending(x => x.CreatedDate);
            }

            return await filtered.ToListAsync();
        }
    }
}
