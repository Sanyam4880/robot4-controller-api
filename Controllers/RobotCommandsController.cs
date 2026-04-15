using Microsoft.AspNetCore.Mvc;
using robot4_controller_api.Models;
using robot4_controller_api.Persistence;

namespace robot4_controller_api.Controllers;

[ApiController]
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{
    private readonly IRobotCommandDataAccess _robotRepo;

    public RobotCommandsController(IRobotCommandDataAccess robotRepo)
    {
        _robotRepo = robotRepo;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_robotRepo.GetRobotCommands());
    }

    [HttpGet("{id}")]
    public IActionResult GetOne(int id)
    {
        var cmd = _robotRepo.GetRobotCommandById(id);

        if (cmd == null)
            return NotFound();

        return Ok(cmd);
    }

    [HttpPost]
    public IActionResult AddOne([FromBody] RobotCommand newCommand)
    {
        var created = _robotRepo.InsertRobotCommand(newCommand);

        return CreatedAtAction(nameof(GetOne), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOne(int id, RobotCommand updatedCommand)
    {
        if (id != updatedCommand.Id)
            return BadRequest();

        var result = _robotRepo.UpdateRobotCommand(updatedCommand);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult RemoveOne(int id)
    {
        var deleted = _robotRepo.DeleteRobotCommand(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}