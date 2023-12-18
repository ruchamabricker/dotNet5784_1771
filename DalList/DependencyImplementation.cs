
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
        DataSource.Dependencies.Add(copy);
        return newid;
    }

    public void Delete(int id)
    {
        if (DataSource.Dependencies.FirstOrDefault(item => item.Id == id) == null)
        {
            throw new DalDoesNotExistException($"there is no dependency with this id: {id}");
        }
        DataSource.Engineers.RemoveAll(item => item.Id == id);
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(lk => lk.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter) // stage 2
    {
        return DataSource.Dependencies.FirstOrDefault(filter);
    }
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null) // stage 2
    {
        //List<Dependency> listD = DataSource.dependencies;
        //return listD;
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }

    public void Update(Dependency item)
    {
        Dependency d = DataSource.Dependencies.FirstOrDefault(lk => lk.Id == item.Id)!;
        if (d != null)
        {
            DataSource.Dependencies.Remove(d);
            DataSource.Dependencies.Add(item);
        }
        else
            throw new DalDoesNotExistException($"no such item with {item.Id} id in dependcy");
    }
    public void Reset()
    {
        if (DataSource.Dependencies.Count > 0)
            DataSource.Dependencies.Clear();
    }
}
