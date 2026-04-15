using Npgsql;
using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public class RobotCommandADO : IRobotCommandDataAccess
{
    private readonly string _connectionString;

    public RobotCommandADO(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public List<RobotCommand> GetRobotCommands()
    {
        var commands = new List<RobotCommand>();

        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM public.robotcommand ORDER BY id", conn);
        using var dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            commands.Add(new RobotCommand
            {
                Id = Convert.ToInt32(dr["id"]),
                Name = dr["name"].ToString() ?? "",
                Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
                IsMoveCommand = Convert.ToBoolean(dr["ismovecommand"]),
                CreatedDate = Convert.ToDateTime(dr["createddate"]),
                ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
            });
        }

        return commands;
    }

    public RobotCommand? GetRobotCommandById(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM public.robotcommand WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        using var dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            return new RobotCommand
            {
                Id = Convert.ToInt32(dr["id"]),
                Name = dr["name"].ToString() ?? "",
                Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
                IsMoveCommand = Convert.ToBoolean(dr["ismovecommand"]),
                CreatedDate = Convert.ToDateTime(dr["createddate"]),
                ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
            };
        }

        return null;
    }

    public RobotCommand InsertRobotCommand(RobotCommand robotCommand)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand(@"
        INSERT INTO public.robotcommand(name,description,ismovecommand,createddate,modifieddate)
        VALUES(@name,@description,@ismovecommand,current_timestamp,current_timestamp)
        RETURNING *;", conn);

        cmd.Parameters.AddWithValue("name", robotCommand.Name);
        cmd.Parameters.AddWithValue("description", (object?)robotCommand.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("ismovecommand", robotCommand.IsMoveCommand);

        using var dr = cmd.ExecuteReader();
        dr.Read();

        return new RobotCommand
        {
            Id = Convert.ToInt32(dr["id"]),
            Name = dr["name"].ToString() ?? "",
            Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
            IsMoveCommand = Convert.ToBoolean(dr["ismovecommand"]),
            CreatedDate = Convert.ToDateTime(dr["createddate"]),
            ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
        };
    }

    public RobotCommand UpdateRobotCommand(RobotCommand robotCommand)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand(@"
        UPDATE public.robotcommand
        SET name=@name,
        description=@description,
        ismovecommand=@ismovecommand,
        modifieddate=current_timestamp
        WHERE id=@id
        RETURNING *;", conn);

        cmd.Parameters.AddWithValue("id", robotCommand.Id);
        cmd.Parameters.AddWithValue("name", robotCommand.Name);
        cmd.Parameters.AddWithValue("description", (object?)robotCommand.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("ismovecommand", robotCommand.IsMoveCommand);

        using var dr = cmd.ExecuteReader();
        dr.Read();

        return new RobotCommand
        {
            Id = Convert.ToInt32(dr["id"]),
            Name = dr["name"].ToString() ?? "",
            Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
            IsMoveCommand = Convert.ToBoolean(dr["ismovecommand"]),
            CreatedDate = Convert.ToDateTime(dr["createddate"]),
            ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
        };
    }

    public bool DeleteRobotCommand(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("DELETE FROM public.robotcommand WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}