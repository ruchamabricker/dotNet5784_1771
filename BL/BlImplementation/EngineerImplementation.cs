
using BlApi;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Engineer engineer)
    {
        if (engineer.Id <= 0)
            throw new BO.BlInValidDataException("In valid id");
        if (engineer.Name!.Length < 0)
            throw new BO.BlInValidDataException("In valid name");
        if (engineer.Cost <= 0)
            throw new BO.BlInValidDataException("no such cost, must be positive");

        DO.Engineer doEngineer = new DO.Engineer(engineer.Id, engineer.Name, engineer.Email!, (DO.EngineerExperience)engineer.Level, engineer.Cost);

        try
        {
            int id = _dal.Engineer.Create(doEngineer);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={engineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        if (_dal.Task.ReadAll(task => task.Id == id) is not null)
        {
            throw new BO.BlDeletionImpossibleException($"engineer with id: {id} cant be deleted, it has task that are dependend on him");
        }
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} already exists", ex);
        }
    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);

        if (doEngineer == null)
            return null!;
        //finds if it has an task that has this engineer's id
        DO.Task? doTask;
        try
        {
            doTask = _dal.Task.ReadAll((task) => task.Engineerid == id).FirstOrDefault();// ?? null;
        }
        catch (Exception)
        {
            doTask = null;
        }
        //builds the task
        BO.TaskInEngineer taskInEngineer;

        if (doTask == null)
            taskInEngineer = null!;
        else
            taskInEngineer = new BO.TaskInEngineer() { Id = doTask.Id, Alias = doTask.Alias };

        return new BO.Engineer()
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost,
            Task = taskInEngineer
        };
    }

    //public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    //{

    //    IEnumerable<BO.Engineer?> engineers = from DO.Engineer doEngineer in _dal.Engineer.ReadAll()                                              
    //                                          let task = _dal.Task.ReadAll(task => task?.Id == doEngineer.Id).FirstOrDefault()
    //                                          select new BO.Engineer
    //                                          {
    //                                              Id = doEngineer.Id,
    //                                              Name = doEngineer.Name,
    //                                              Email = doEngineer.Email,
    //                                              Level = (BO.EngineerExperience)doEngineer.Level,
    //                                              Cost = doEngineer.Cost,
    //                                              Task = task != null ? new BO.TaskInEngineer
    //                                              {
    //                                                  Id = task.Id,
    //                                                  Alias = task.Alias
    //                                              } : null
    //                                          };

    //    if (filter == null)
    //        return engineers;
    //    return engineers.Where(filter!);
    //}

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        IEnumerable<BO.Engineer?> engineers = null!;
        try
        {
            engineers = from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                        let task = GetTask(doEngineer.Id)
                        select new BO.Engineer
                        {
                            Id = doEngineer.Id,
                            Name = doEngineer.Name,
                            Email = doEngineer.Email,
                            Level = (BO.EngineerExperience)doEngineer.Level,
                            Cost = doEngineer.Cost,
                            Task = task != null ? new BO.TaskInEngineer
                            {
                                Id = task.Id,
                                Alias = task.Alias
                            } : null
                        };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        if (filter == null)
            return engineers;
        return engineers.Where(filter!);
    }

    private DO.Task? GetTask(int engineerId)
    {
        try
        {
            return _dal.Task.ReadAll(task => task?.Engineerid == engineerId).FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while getting the task: {ex.Message}");
            return null;
        }
    }






    public void Update(BO.Engineer engineer)
    {

        if (Read(engineer.Id) == null)
            throw new BO.BlDoesNotExistException($"There is no engineer with id: {engineer.Id}");
        if (engineer.Id <= 0)
            throw new BO.BlInValidDataException("In valid id");
        if (engineer.Name!.Length < 0)
            throw new BO.BlInValidDataException("In valid name");
        if (engineer.Cost <= 0)
            throw new BO.BlInValidDataException("no such cost, must be positive");

        DO.Engineer doEngineer = new DO.Engineer(engineer.Id, engineer.Name, engineer.Email!, (DO.EngineerExperience)engineer.Level, engineer.Cost);

        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={engineer.Id} already exists", ex);
        }
    }
}
