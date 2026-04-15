using Npgsql;
using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public class RobotCommandRepository : IRobotCommandDataAccess, IRepository
{
    public string ConnectionString { get; }

    private IRepository _repo => this;

    public RobotCommandRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public List<RobotCommand> GetRobotCommands()
    {
        return _repo.ExecuteReader<RobotCommand>(
            "SELECT * FROM public.robotcommand ORDER BY id");
    }

    public RobotCommand? GetRobotCommandById(int id)
    {
        var sqlParams = new NpgsqlParameter[]
        {
            new("id", id)
        };

        return _repo.ExecuteReader<RobotCommand>(
            "SELECT * FROM public.robotcommand WHERE id=@id",
            sqlParams).FirstOrDefault();
    }

    public RobotCommand InsertRobotCommand(RobotCommand robotCommand)
    {
        var sqlParams = new NpgsqlParameter[]
        {
            new("name", robotCommand.Name),
            new("description", robotCommand.Description ?? (object)DBNull.Value),
            new("ismovecommand", robotCommand.IsMoveCommand)
        };

        return _repo.ExecuteReader<RobotCommand>(
            @"INSERT INTO public.robotcommand(name, description, ismovecommand, createddate, modifieddate)
              VALUES (@name, @description, @ismovecommand, current_timestamp, current_timestamp)
              RETURNING *;",
            sqlParams).Single();
    }

    public RobotCommand UpdateRobotCommand(RobotCommand updatedCommand)
    {
        var sqlParams = new NpgsqlParameter[]
        {
            new("id", updatedCommand.Id),
            new("name", updatedCommand.Name),
            new("description", updatedCommand.Description ?? (object)DBNull.Value),
            new("ismovecommand", updatedCommand.IsMoveCommand)
        };

        return _repo.ExecuteReader<RobotCommand>(
            @"UPDATE public.robotcommand
              SET name=@name,
                  description=@description,
                  ismovecommand=@ismovecommand,
                  modifieddate=current_timestamp
              WHERE id=@id
              RETURNING *;",
            sqlParams).Single();
    }

    public bool DeleteRobotCommand(int id)
    {
        using var conn = new NpgsqlConnection(ConnectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("DELETE FROM public.robotcommand WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}