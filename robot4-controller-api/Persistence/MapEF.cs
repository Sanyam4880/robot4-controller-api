using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public class MapEF : IMapDataAccess
{
    private readonly RobotContext _context;

    public MapEF(RobotContext context)
    {
        _context = context;
    }

    public List<Map> GetMaps()
    {
        return _context.Maps
            .OrderBy(x => x.Id)
            .ToList();
    }

    public Map? GetMapById(int id)
    {
        return _context.Maps
            .FirstOrDefault(x => x.Id == id);
    }

    public Map InsertMap(Map map)
    {
        map.CreatedDate = DateTime.Now;
        map.ModifiedDate = DateTime.Now;

        _context.Maps.Add(map);
        _context.SaveChanges();

        return map;
    }

    public Map UpdateMap(Map map)
    {
        var existing = _context.Maps.FirstOrDefault(x => x.Id == map.Id);

        if (existing == null)
        {
            throw new KeyNotFoundException($"Map with Id {map.Id} not found.");
        }

        existing.Name = map.Name;
        existing.Description = map.Description;
        existing.Columns = map.Columns;
        existing.Rows = map.Rows;
        existing.ModifiedDate = DateTime.Now;

        _context.SaveChanges();

        return existing;
    }

    public bool DeleteMap(int id)
    {
        var existing = _context.Maps.FirstOrDefault(x => x.Id == id);

        if (existing == null)
        {
            return false;
        }

        _context.Maps.Remove(existing);
        _context.SaveChanges();

        return true;
    }
}