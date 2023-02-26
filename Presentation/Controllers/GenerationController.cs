using Application.Models;
using Application.Services.Generation.Command;
using Application.Services.Generation.Query;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
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
        return CreatedAtAction("GetGenerationQuery", new { name = result.Name }, result);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchGenerationAsync(string name, [FromBody] JsonPatchDocument<Generation> patchDocument)
    {
        var command = new PatchGenerationCommand(name, patchDocument);
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Generation was updated successfully.");
        return Ok(json);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGenerationAsync([FromBody] DeleteGenerationCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Generation was deleted successfully.");
        return Ok(json);
    }
}