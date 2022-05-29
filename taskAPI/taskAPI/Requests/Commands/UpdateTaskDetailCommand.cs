using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using taskAPI.Models;

namespace taskAPI.Requests.Commands
{
    public class UpdateTaskDetailCommand :IRequest<HttpStatusCode>
    {
        public string Id { get; }
        public TaskDetail TaskDetail { get; set; }
        public UpdateTaskDetailCommand(string id, TaskDetail taskDetail)
        {
            Id = id;
            TaskDetail = taskDetail;
        }
    }
}
