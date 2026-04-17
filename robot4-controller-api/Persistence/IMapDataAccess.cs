using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public interface IMapDataAccess
{
    List<Map> GetMaps();

    Map? GetMapById(int id);

    Map InsertMap(Map map);

    Map UpdateMap(Map map);

    bool DeleteMap(int id);
}