using MediatR;
using System.Net;

namespace taskAPI.Requests.Commands
{
    public class DeleteTaskDetailCommand : IRequest<HttpStatusCode>
    {
        public string Id { get; }

        public DeleteTaskDetailCommand(string id)
        {
            Id = id;
        }
    }
}
