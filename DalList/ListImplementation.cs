
namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;

public class ListImplementation : ITask
{
    public int Create(Task item)
    {
        //for entities with auto id
        int newid = DataSource.Config.NextTaskId;
        Task copy = item with { id = newid };
        DataSource.Tasks.Add(copy);
        return newid;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(lk => lk.id == id);

      //  throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
