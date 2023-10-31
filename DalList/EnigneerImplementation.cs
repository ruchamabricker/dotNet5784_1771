

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EnigneerImplementation : IEnigneer
{

    public int Create(Enigneer item)
    {
        if (item == null)
            throw new NotImplementedException();

        DataSource.Enigneers.Add(item);
        return item.id;

    }

    public void Delete(int id)
    {
        if (DataSource.Enigneers.Find(item => item.id == id) == null)
        {
            throw new Exception($"there is no enigeer with this id: {id}");
        }
        if (DataSource.Tasks.Find(item => item.engineerid == id) != null)
        {
            throw new Exception($"this enigeer {id} has tasks, he can not be deleted!");
        }
        DataSource.Enigneers.Remove(DataSource.Enigneers.Find(item => id == id));
    }

    public Enigneer? Read(int id)
    {
        return DataSource.Enigneers.Find(lk => lk.id == id);
    }

    public List<Enigneer> ReadAll()
    {
        List<Enigneer> listE = DataSource.Enigneers;
        return listE;
    }

    public void Update(Enigneer item)
    {
        Enigneer e = DataSource.Enigneers.Find(lk => lk.id == item.id);
        if (e != null)
        {
            DataSource.Enigneers.Remove(e);
            DataSource.Enigneers.Add(item);
        }
        throw new Exception($"no such item with {item.id} id in Enigneers");
    }
}
