using Npgsql;
using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public class MapRepository : IMapDataAccess, IRepository
{
    public string ConnectionString { get; }

    private IRepository _repo => this;

    public MapRepository(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public List<Map> GetMaps()
    {
        return _repo.ExecuteReader<Map>(
            "SELECT * FROM public.map ORDER BY id");
    }

    public Map? GetMapById(int id)
    {
        var sqlParams = new NpgsqlParameter[]
        {
            new("id", id)
        };

        return _repo.ExecuteReader<Map>(
            "SELECT * FROM public.map WHERE id=@id",
            sqlParams).FirstOrDefault();
    }

    public Map InsertMap(Map map)
    {
        var sqlParams = new NpgsqlParameter[]
        {
            new("name", map.Name),
            new("description", map.Description ?? (object)DBNull.Value),
            new("columns", map.Columns),
            new("rows", map.Rows)
        };

        return _repo.ExecuteReader<Map>(
            @"INSERT INTO public.map(name, description, columns, rows, createddate, modifieddate)
              VALUES (@name, @description, @columns, @rows, current_timestamp, current_timestamp)
              RETURNING *;",
            sqlParams).Single();
    }

    public Map UpdateMap(Map updatedMap)
    {
        var sqlParams = new NpgsqlParameter[]
        {
            new("id", updatedMap.Id),
            new("name", updatedMap.Name),
            new("description", updatedMap.Description ?? (object)DBNull.Value),
            new("columns", updatedMap.Columns),
            new("rows", updatedMap.Rows)
        };

        return _repo.ExecuteReader<Map>(
            @"UPDATE public.map
              SET name=@name,
                  description=@description,
                  columns=@columns,
                  rows=@rows,
                  modifieddate=current_timestamp
              WHERE id=@id
              RETURNING *;",
            sqlParams).Single();
    }

    public bool DeleteMap(int id)
    {
        using var conn = new NpgsqlConnection(ConnectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("DELETE FROM public.map WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}