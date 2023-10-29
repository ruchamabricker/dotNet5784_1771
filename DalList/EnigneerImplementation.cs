

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
        //if (!DataSource.Enigneers.Contains(item=>id))

            DataSource.Enigneers.Find(item=>id==item.id);

            throw new NotImplementedException();

        
    }

    public Enigneer? Read(int id)
    {
        return DataSource.Enigneers.Find(lk => lk.id == id);
    }

    public List<Enigneer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Enigneer item)
    {
        throw new NotImplementedException();
    }
}
