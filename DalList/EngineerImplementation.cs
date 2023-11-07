namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{

    public int Create(Engineer item)
    {
        if (item == null)
            throw new NotImplementedException();

        DataSource.Engineers.Add(item);
        return item.id;
    }

    public void Delete(int id)
    {
        if (DataSource.Engineers.Find(item => item.id == id) == null)
        {
            throw new Exception($"there is no enigeer with this id: {id}");
        }
        if (DataSource.Tasks.Find(item => item.engineerid == id) != null)
        {
            throw new Exception($"this enigeer {id} has tasks, he can not be deleted!");
        }
        DataSource.Engineers.Remove(DataSource.Engineers.Find(item => id == id));
    }

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(lk => lk.id == id);
    }

    public List<Engineer> ReadAll()
    {
        List<Engineer> listE = DataSource.Engineers;
        return listE;
    }

    public void Update(Engineer item)
    {
        Engineer e = DataSource.Engineers.Find(lk => lk.id == item.id);
        if (e != null)
        {
            DataSource.Engineers.Remove(e);
            DataSource.Engineers.Add(item);
        }
        throw new Exception($"no such item with {item.id} id in Engineers");
    }
}
