using Npgsql;

namespace robot4_controller_api.Persistence;

public interface IRepository
{
    string ConnectionString { get; }

    public List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[]? dbParams = null)
        where T : class, new()
    {
        var entities = new List<T>();

        using var conn = new NpgsqlConnection(ConnectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand(sqlCommand, conn);

        if (dbParams is not null)
        {
            cmd.Parameters.AddRange(dbParams.Where(x => x.Value is not null).ToArray());
        }

        using var dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            var entity = new T();
            dr.MapTo(entity);
            entities.Add(entity);
        }

        return entities;
    }
}