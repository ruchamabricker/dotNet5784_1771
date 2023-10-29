
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
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.Find(lk => lk.id == id);
    }

    public List<Dependency> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
