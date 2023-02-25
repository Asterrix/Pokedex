using Application.Services.Specie.Command;
using Application.Services.Specie.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class SpecieController : ControllerBase
{
    private readonly ISender _sender;

    public SpecieController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetSpecieListQueryAsync()
    {
        var result = await _sender.Send(new GetSpecieListQuery());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSpecieQueryAsync([FromQuery(Name = "name")] string name)
    {
        var result = await _sender.Send(new GetSpecieQuery(name));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpecieAsync([FromBody] CreateSpecieCommand command)
    {
        var result = await _sender.Send(command);
        return CreatedAtAction("GetSpecieQuery", new { name = result.Name }, result);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchSpecieAsync([FromBody] PatchSpecieCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        return Ok($"{command.Name.Trim()} was successfully updated to a new value of \"{command.NewValue.Trim()}\".");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSpecieAsync([FromBody] DeleteSpecieCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        return Ok("Specie was deleted successfully.");
    }
}