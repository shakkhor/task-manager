using MediatR;
using taskAPI.Models;

namespace taskAPI.Requests.Queries
{
    public class GetTaskDetailQuery : IRequest<TaskDetail>
    {
        public string Id { get; }
        public GetTaskDetailQuery(string id)
        {
            Id = id;
        }
    }
}
