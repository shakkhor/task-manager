using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using taskAPI.Models;
using taskAPI.Requests.Commands;
using taskAPI.Requests.Queries;

namespace taskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TaskDetailsController(IMediator mediator)
        {       
            this.mediator = mediator;
        }

        // GET: api/TaskDetails
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<TaskDetail>>> GetTaskDetails( string sortBy, int sortOrder , string? title)
        {
            var query = new GetTaskDetailsQuery(title, sortBy, sortOrder);
            var result = await mediator.Send(query);
            return Ok(result);          
        }

        // GET: api/TaskDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetail>> GetTaskDetail(string id)
        {
            var query = new GetTaskDetailQuery(id);
            var result = await mediator.Send(query);            
            return result != null? Ok(result) : NotFound();          
        }

        // PUT: api/TaskDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskDetail(string id, TaskDetail taskDetail)
        {
            var command = new UpdateTaskDetailCommand(id, taskDetail);
            var result = await mediator.Send(command);
            return ResolveHttpCode(result);      
        }    

        // POST: api/TaskDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskDetail>> PostTaskDetail(TaskDetail taskDetail)
        {
            var command = new CreateTaskDetailCommand(taskDetail);
            var result = await mediator.Send(command);
            return result != null ? Ok(result) : Problem("Entity set 'TasksDataContext.TaskDetails'  is null.");                    
        }

        // DELETE: api/TaskDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskDetail(string id)
        {
            var command = new DeleteTaskDetailCommand(id);
            var result = await mediator.Send(command);
            return ResolveHttpCode(result);            
        }

        private IActionResult ResolveHttpCode(System.Net.HttpStatusCode result)
        {
            switch (result)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest();
                case HttpStatusCode.NotFound:
                    return NotFound();
                case HttpStatusCode.NoContent:
                    return NoContent();
                default:
                    return Ok();

            }
        }
    }
}
