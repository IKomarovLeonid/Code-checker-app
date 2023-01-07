using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.View;
using MediatR;
using Objects.Models;
using Processing.Commands;
using Processing.Queries;

namespace API.Controllers
{
    [ApiController, Route("api/solutions")]
    public class CodeSolutionsController : ApplicationController
    {
        private readonly IMediator _mediator;

        public CodeSolutionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _mediator.Send(new SelectQuery<CodeSolution>());

            return ToView(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(ulong id)
        {
            var result = await _mediator.Send(new FindQuery<CodeSolution>(id));

            return ToView(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostCodeSolutionRequest request)
        {
            var command = new CreateSolutionCommand()
            {
                Code = request.Code,
                TaskId = request.TaskId,
                UserId = request.UserId
            };

            var result = await _mediator.Send(command);

            return ToView(result);
        }
    }
}
