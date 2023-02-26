using Application.Models;
using Application.Services.Category.Command;
using Application.Services.Category.Query;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CategoryController : ControllerBase
{
    private readonly ISender _sender;

    public CategoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetCategoryListQueryAsync()
    {
        var result = await _sender.Send(new GetCategoryListQuery());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryQueryAsync([FromQuery(Name = "name")] string name)
    {
        var result = await _sender.Send(new GetCategoryQuery(name));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
    {
        var result = await _sender.Send(command);
        return CreatedAtAction("GetCategoryQuery", new { name = result.Name }, result);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchCategoryAsync(string name, [FromBody] JsonPatchDocument<Category> jsonPatchDocument)
    {
        var command = new PatchCategoryCommand(name, jsonPatchDocument);
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Category was updated successfully.");
        return Ok(json);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryAsync([FromBody] DeleteCategoryCommand command)
    {
        var result = await _sender.Send(command);
        if (result is false)
        {
            throw new Exception();
        }

        var json = JsonConvert.SerializeObject("Category was deleted successfully.");
        return Ok(json);
    }
}