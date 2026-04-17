using robot4_controller_api.Models;
using Xunit;

namespace robot4_controller_api.Tests;

public class RobotCommandTests
{
    [Fact]
    public void RobotCommand_MoveCommand_ShouldBeTrue()
    {
        var command = new RobotCommand
        {
            Name = "MOVE",
            IsMoveCommand= true
        };

        Assert.True(command.IsMoveCommand);
    }

    [Fact]
    public void RobotCommand_NonMoveCommand_ShouldBeFalse()
    {
        var command = new RobotCommand
        {
            Name = "REPORT",
            IsMoveCommand = false
        };

        Assert.False(command.IsMoveCommand);
    }
}