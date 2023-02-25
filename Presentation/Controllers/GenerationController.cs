using Application.Services.Generation.Command;
using Application.Services.Generation.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class GenerationController : ControllerBase
{
    private readonly ISender _sender;

    public GenerationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllGenerationListQueryAsync()
    {
        var result = await _sender.Send(new GetGenerationListQuery());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetGenerationQueryAsync([FromQuery(Name = "name")] string name)
    {
        var result = await _sender.Send(new GetGenerationQuery(name));
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenerationAsync([FromBody] CreateGenerationCommand command)
    {
        var result = await _sender.Send(command);
        return CreatedAtAction("GetGenerationQuery", new {name = result.Name}, result);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchGenerationAsync([FromBody] PatchGenerationCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        return Ok($"{command.Name.Trim()} was successfully updated to a new value of \"{command.NewValue.Trim()}\".");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGenerationAsync([FromBody] DeleteGenerationCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }
        return Ok($"Generation was deleted successfully.");
    }
}