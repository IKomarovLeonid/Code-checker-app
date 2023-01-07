using System.Threading.Tasks;
using API.View;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Objects.Models;
using Processing.Commands;
using Processing.Queries;

namespace API.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TasksController : ApplicationController
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _mediator.Send(new SelectQuery<CodeTask>());

            return ToView(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(ulong id)
        {
            var result = await _mediator.Send(new FindQuery<CodeTask>(id));

            return ToView(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateTaskRequestModel request)
        {
            var command = new CreateTaskCommand()
            {
                Title = request.Title,
                Description = request.Description,
                MethodName = request.MethodName,
                NamespaceName = request.NamespaceName
            };

            var response = await _mediator.Send(command);

            return ToView(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(ulong id)
        {
            return Ok("deleted");
        }
    }
}
