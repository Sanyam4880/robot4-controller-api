using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public interface IRobotCommandDataAccess
{
    List<RobotCommand> GetRobotCommands();

    RobotCommand? GetRobotCommandById(int id);

    RobotCommand InsertRobotCommand(RobotCommand robotCommand);

    RobotCommand UpdateRobotCommand(RobotCommand robotCommand);

    bool DeleteRobotCommand(int id);
}