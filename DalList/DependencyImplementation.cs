
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
        Dependency copy = item with { Id = newid };
        DataSource.dependencies.Add(copy);
        return newid;
    }

    public void Delete(int id)
    {
        if (DataSource.dependencies.FirstOrDefault(item => item.Id == id) == null)
        {
            throw new DalDoesNotExistException($"there is no dependency with this id: {id}");
        }
        DataSource.Engineers.RemoveAll(item => item.Id == id);
    }

    public Dependency? Read(int id)
    {
        return DataSource.dependencies.FirstOrDefault(lk => lk.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter) // stage 2
    {
        return DataSource.dependencies.FirstOrDefault(filter);
    }
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null) // stage 2
    {
        //List<Dependency> listD = DataSource.dependencies;
        //return listD;
        if (filter != null)
        {
            return from item in DataSource.dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.dependencies
               select item;
    }

    public void Update(Dependency item)
    {
        Dependency d = DataSource.dependencies.FirstOrDefault(lk => lk.Id == item.Id)!;
        if (d != null)
        {
            DataSource.dependencies.Remove(d);
            DataSource.dependencies.Add(item);
        }
        else
            throw new DalDoesNotExistException($"no such item with {item.Id} id in dependcy");
    }
}
