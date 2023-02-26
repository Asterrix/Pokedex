using Application.Models;
using Application.Services.Pokemon.Command;
using Application.Services.Pokemon.Query;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PokemonController : ControllerBase
{
    private readonly ISender _sender;

    public PokemonController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetPokemonListQueryAsync()
    {
        var result = await _sender.Send(new GetPokemonListQuery());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPokemonQueryAsync([FromQuery(Name = "name")] string name)
    {
        var result = await _sender.Send(new GetPokemonQuery(name));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePokemonAsync([FromBody] CreatePokemonCommand command)
    {
        var result = await _sender.Send(command);
        return CreatedAtAction("GetPokemonQuery", new { name = result.Name }, result);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchPokemonAsync(string name, [FromBody] JsonPatchDocument<Pokemon> jsonPatchDocument)
    {
        var command = new PatchPokemonCommand(name, jsonPatchDocument);
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Pokemon was updated successfully.");
        return Ok(json);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePokemonAsync([FromBody] DeletePokemonCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Pokemon was deleted successfully.");
        return Ok(json);
    }
}