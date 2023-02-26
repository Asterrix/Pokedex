using Application.Models;
using Application.Services.Specie.Command;
using Application.Services.Specie.Query;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
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
    public async Task<IActionResult> PatchSpecieAsync(string name, [FromBody] JsonPatchDocument<Specie>patchDocument)
    {
        var command = new PatchSpecieCommand(name, patchDocument);
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Specie was updated successfully.");
        return Ok(json);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSpecieAsync([FromBody] DeleteSpecieCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Specie was deleted successfully.");
        return Ok(json);
    }
}