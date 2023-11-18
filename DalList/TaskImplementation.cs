
namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

internal class TaskImplementation : ITask
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
        if (DataSource.Tasks.FirstOrDefault(lk => lk.id == id) == null)
        {
            throw new DalDoesNotExistException($"there is no task with this id: {id}");
        }
        if (DataSource.Dependencys.FirstOrDefault(lk => lk.dependsOnTask == id) != null)
        {
            throw new DalDeletionImpossible($"this task {id} has tasks depented on it, it can not be deleted!");
        }
        DataSource.Tasks.Remove(DataSource.Tasks.FirstOrDefault(lk => lk.id == id));

    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(lk => lk.id == id);
    }

   public Task? Read(Func<Task, bool> filter) // stage 2
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null) // stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }

    public void Update(Task item)
    {
        //Task d = DataSource.Tasks.Find(lk => lk.id == item.id);
        Task d = DataSource.Tasks.FirstOrDefault(lk => lk.id == item.id);
        if (d != null)
        {
            DataSource.Tasks.Remove(d);
            DataSource.Tasks.Add(item);
        }
        else
            throw new DalDoesNotExistException($"no such item with {item.id} id in task");
    }
}
