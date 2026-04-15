using Npgsql;
using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public class MapADO : IMapDataAccess
{
    private readonly string _connectionString;

    public MapADO(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public List<Map> GetMaps()
    {
        var maps = new List<Map>();

        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM public.map ORDER BY id", conn);
        using var dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            maps.Add(new Map
            {
                Id = Convert.ToInt32(dr["id"]),
                Name = dr["name"].ToString() ?? "",
                Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
                Columns = Convert.ToInt32(dr["columns"]),
                Rows = Convert.ToInt32(dr["rows"]),
                CreatedDate = Convert.ToDateTime(dr["createddate"]),
                ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
            });
        }

        return maps;
    }

    public Map? GetMapById(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM public.map WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        using var dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            return new Map
            {
                Id = Convert.ToInt32(dr["id"]),
                Name = dr["name"].ToString() ?? "",
                Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
                Columns = Convert.ToInt32(dr["columns"]),
                Rows = Convert.ToInt32(dr["rows"]),
                CreatedDate = Convert.ToDateTime(dr["createddate"]),
                ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
            };
        }

        return null;
    }

    public Map InsertMap(Map map)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand(@"
            INSERT INTO public.map(name, description, columns, rows, createddate, modifieddate)
            VALUES (@name, @description, @columns, @rows, current_timestamp, current_timestamp)
            RETURNING *;", conn);

        cmd.Parameters.AddWithValue("name", map.Name);
        cmd.Parameters.AddWithValue("description", (object?)map.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("columns", map.Columns);
        cmd.Parameters.AddWithValue("rows", map.Rows);

        using var dr = cmd.ExecuteReader();
        dr.Read();

        return new Map
        {
            Id = Convert.ToInt32(dr["id"]),
            Name = dr["name"].ToString() ?? "",
            Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
            Columns = Convert.ToInt32(dr["columns"]),
            Rows = Convert.ToInt32(dr["rows"]),
            CreatedDate = Convert.ToDateTime(dr["createddate"]),
            ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
        };
    }

    public Map UpdateMap(Map map)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand(@"
            UPDATE public.map
            SET name=@name,
                description=@description,
                columns=@columns,
                rows=@rows,
                modifieddate=current_timestamp
            WHERE id=@id
            RETURNING *;", conn);

        cmd.Parameters.AddWithValue("id", map.Id);
        cmd.Parameters.AddWithValue("name", map.Name);
        cmd.Parameters.AddWithValue("description", (object?)map.Description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("columns", map.Columns);
        cmd.Parameters.AddWithValue("rows", map.Rows);

        using var dr = cmd.ExecuteReader();
        dr.Read();

        return new Map
        {
            Id = Convert.ToInt32(dr["id"]),
            Name = dr["name"].ToString() ?? "",
            Description = dr["description"] == DBNull.Value ? null : dr["description"].ToString(),
            Columns = Convert.ToInt32(dr["columns"]),
            Rows = Convert.ToInt32(dr["rows"]),
            CreatedDate = Convert.ToDateTime(dr["createddate"]),
            ModifiedDate = Convert.ToDateTime(dr["modifieddate"])
        };
    }

    public bool DeleteMap(int id)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("DELETE FROM public.map WHERE id=@id", conn);
        cmd.Parameters.AddWithValue("id", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}