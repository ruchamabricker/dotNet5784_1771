
namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;

public class TaskImplementation : ITask
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
        if (DataSource.Tasks.Find(item => item.id == id) == null)
        {
            throw new Exception($"there is no task with this id: {id}");
        }
        if (DataSource.Dependencys.Find(item => item.dependsOnTask == id) != null)
        {
            throw new Exception($"this task {id} has tasks depented on it, it can not be deleted!");
        }
        DataSource.Tasks.Remove(DataSource.Tasks.Find(item => id == id));
        
    }
  
    public Task? Read(int id)
    {
        return DataSource.Tasks.Find(lk => lk.id == id);
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        Task d = DataSource.Tasks.Find(lk => lk.id == item.id);
        if (d != null)
        {
            DataSource.Tasks.Remove(d);
            DataSource.Tasks.Add(item);
        }
        throw new Exception($"no such item with {item.id} id in task");
    }
}
