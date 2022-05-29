using MediatR;
using taskAPI.Models;

namespace taskAPI.Requests.Queries
{
    public class GetTaskDetailsQuery : IRequest<List<TaskDetail>>
    {

        public string SearchText { get;  }
        public string SortBy { get;  }
        public int SortOrder { get;}

        public GetTaskDetailsQuery(string titie, string sortBy, int sortOrder)
        {
            SearchText = titie;
            SortBy = sortBy;
            SortOrder = sortOrder;
        }

    }
}
