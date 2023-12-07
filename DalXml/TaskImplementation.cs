
namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        //try
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Task>));

        //    using (TextReader textReader = new StringReader(tasksFile))
        //    {
        //        List<DO.Task> lst = (List<DO.Task>)serializer.Deserialize(textReader) ?? new List<DO.Task>();
        //        lst.Add(item);

        //        using (TextWriter writer = new StreamWriter(tasksFile))
        //        {
        //            serializer.Serialize(writer, lst);
        //        }
        //    }

        //    return item.id;
        //}
        //catch (Exception ex)
        //{
        //    // Handle the exception, log it, or rethrow it based on your application's needs
        //    Console.WriteLine($"Error in Create method: {ex.Message}");
        //    throw;
        //}
        // item.id = Config.NextDependencyId;
        // DO.Task task = new DO.Task(Config.NextDependencyId);
        int newid = Config.NextTaskId;
        Task copy = item with { id = newid };

        List<DO.Task> lst = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");

        lst.Add(copy);
        XMLTools.SaveListToXMLSerializer<DO.Task>(lst, "tasks");
        return item.id;
    }



    public void Delete(int id)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Task with ID {id} does not exists");
        tasksList.RemoveAll(t => t.id == id);
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, "tasks");
    }

    public Task? Read(int id)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = tasksList.FirstOrDefault(e => e.id == id);
        if (task != null)
        {
            return task;
        }
        throw new DalDoesNotExistException($"Cannot find task with ID:{id}.");
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = tasksList.FirstOrDefault(filter);
        if (task != null)
        {
            return task;
        }
        throw new DalDoesNotExistException($"Cannot find Task with given filter");
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");

        if (filter != null)
        {
            List<Task> tasks = tasksList.Where(filter).ToList();
            if (tasks.Count > 0)
                return tasks;
            throw new DalDoesNotExistException($"There is no tasks");
        }
        else
        {
            if (tasksList.Count > 0)
                return tasksList;
            throw new DalDoesNotExistException($"There is no tasks");
        }
    }

    public void Update(DO.Task item)
    {
        List<Task> tasksList = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? existingEngineer = tasksList.FirstOrDefault(e => e.id == item.id);
        if (existingEngineer == null)
            throw new DalDoesNotExistException($"There is no task with ID {item.id}");
        tasksList.Remove(existingEngineer);
        tasksList.Add(item);
        XMLTools.SaveListToXMLSerializer<Task>(tasksList, "tasks");
    }
}
