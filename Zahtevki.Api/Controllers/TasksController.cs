using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zahtevki.Application.Commands;
using Zahtevki.Application.Queries;
using Zahtevki.Core.Entities;

namespace Zahtevki.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddTasksAsync([FromBody] TasksEntity task)
        {
            var result = await sender.Send(new AddTasksCommand(task));
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            var result = await sender.Send(new GetAllTasksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTasksByIdAsync([FromRoute] Guid id)
        {
            var result = await sender.Send(new GetTasksByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasksAsync([FromRoute] Guid id, [FromBody] TasksEntity task)
        {
            var result = await sender.Send(new UpdateTasksCommand(id, task));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksAsync([FromRoute] Guid id)
        {
            var result = await sender.Send(new DeleteTasksCommand(id));
            return Ok(result);
        }
    }
}
