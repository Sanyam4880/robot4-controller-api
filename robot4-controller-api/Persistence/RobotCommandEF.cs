using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public class RobotCommandEF : IRobotCommandDataAccess
{
    private readonly RobotContext _context;

    public RobotCommandEF(RobotContext context)
    {
        _context = context;
    }

    public List<RobotCommand> GetRobotCommands()
    {
        return _context.RobotCommands
            .OrderBy(x => x.Id)
            .ToList();
    }

    public RobotCommand? GetRobotCommandById(int id)
    {
        return _context.RobotCommands
            .FirstOrDefault(x => x.Id == id);
    }

    public RobotCommand InsertRobotCommand(RobotCommand robotCommand)
    {
        robotCommand.CreatedDate = DateTime.Now;
        robotCommand.ModifiedDate = DateTime.Now;

        _context.RobotCommands.Add(robotCommand);
        _context.SaveChanges();

        return robotCommand;
    }

    public RobotCommand UpdateRobotCommand(RobotCommand robotCommand)
    {
        var existing = _context.RobotCommands.FirstOrDefault(x => x.Id == robotCommand.Id);

        if (existing == null)
        {
            throw new KeyNotFoundException($"RobotCommand with Id {robotCommand.Id} not found.");
        }

        existing.Name = robotCommand.Name;
        existing.Description = robotCommand.Description;
        existing.IsMoveCommand = robotCommand.IsMoveCommand;
        existing.ModifiedDate = DateTime.Now;

        _context.SaveChanges();

        return existing;
    }

    public bool DeleteRobotCommand(int id)
    {
        var existing = _context.RobotCommands.FirstOrDefault(x => x.Id == id);

        if (existing == null)
        {
            return false;
        }

        _context.RobotCommands.Remove(existing);
        _context.SaveChanges();

        return true;
    }
}