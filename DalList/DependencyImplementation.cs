
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        //for entities with auto id
        int newid = DataSource.Config.NextDependencyId;
        Dependency copy = item with { id = newid };
        DataSource.Dependencys.Add(copy);
        return newid;
    }

    public void Delete(int id)
    {
        if (DataSource.Dependencys.Find(item => item.id == id) == null)
        {
            throw new Exception($"there is no dependency with this id: {id}");
        }
        //if (DataSource.Tasks.Find(item => item.engineerid == id) != null)
        //{
        //    throw new Exception($"this enigeer {id} has tasks, he can not be deleted!");
        //}
        DataSource.Enigneers.Remove(DataSource.Enigneers.Find(item => id == id));
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.Find(lk => lk.id == id);
    }

    public List<Dependency> ReadAll()
    {
        List<Dependency> listD = DataSource.Dependencys;
        return listD;
    }

    public void Update(Dependency item)
    {
        Dependency d = DataSource.Dependencys.Find(lk => lk.id == item.id);
        if (d != null)
        {
            DataSource.Dependencys.Remove(d);
            DataSource.Dependencys.Add(item);
        }
        throw new Exception($"no such item with {item.id} id in dependcy");
    }
}
