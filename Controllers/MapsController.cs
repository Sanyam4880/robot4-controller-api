using Microsoft.AspNetCore.Mvc;
using robot4_controller_api.Models;
using robot4_controller_api.Persistence;

namespace robot4_controller_api.Controllers;

[ApiController]
[Route("api/maps")]
public class MapsController : ControllerBase
{
    private readonly IMapDataAccess _mapsRepo;

    public MapsController(IMapDataAccess mapsRepo)
    {
        _mapsRepo = mapsRepo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_mapsRepo.GetMaps());
    }

    [HttpGet("{id}")]
    public IActionResult GetOne(int id)
    {
        var map = _mapsRepo.GetMapById(id);

        if (map == null)
            return NotFound();

        return Ok(map);
    }

    [HttpPost]
    public IActionResult AddOne([FromBody] Map newMap)
    {
        var created = _mapsRepo.InsertMap(newMap);
        return CreatedAtAction(nameof(GetOne), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOne(int id, [FromBody] Map updatedMap)
    {
        if (id != updatedMap.Id)
            return BadRequest();

        var result = _mapsRepo.UpdateMap(updatedMap);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveOne(int id)
    {
        var deleted = _mapsRepo.DeleteMap(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}