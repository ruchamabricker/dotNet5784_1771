
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
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
        if (DataSource.Dependencys.FirstOrDefault(item => item.id == id) == null)
        {
            throw new DalDoesNotExistException($"there is no dependency with this id: {id}");
        }
        DataSource.Engineers.RemoveAll(item => item.id == id);
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.FirstOrDefault(lk => lk.id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter) // stage 2
    {
        return DataSource.Dependencys.FirstOrDefault(filter);
    }
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null) // stage 2
    {
        //List<Dependency> listD = DataSource.Dependencys;
        //return listD;
        if (filter != null)
        {
            return from item in DataSource.Dependencys
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencys
               select item;
    }

    public void Update(Dependency item)
    {
        Dependency d = DataSource.Dependencys.FirstOrDefault(lk => lk.id == item.id);
        if (d != null)
        {
            DataSource.Dependencys.Remove(d);
            DataSource.Dependencys.Add(item);
        }
        else
            throw new DalDoesNotExistException($"no such item with {item.id} id in dependcy");
    }
}
