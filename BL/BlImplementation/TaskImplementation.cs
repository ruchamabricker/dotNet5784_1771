using BlApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(BO.Task task)
    {
        if (task.Id <= 0)
            throw new BO.BlInValidDataException("In valid value of id");
        if (task.Alias.Length < 0)
            throw new BO.BlInValidDataException("In valid length of alias");

        DO.Task doTask = new DO.Task(task.Id,
            task.Description,
            task.Alias,
            task.Engineer!.Id,
            (DO.EngineerExperience)task.ComplexityLevel!,
            task.CreatedAtDate,
            false,
            true,
            (TimeSpan)(task.CompleteDate - task.StartDate)!,
            task.StartDate,
            task.BaselineStartDate,
            task.DeadlineDate,
            task.CompleteDate,
            task.Deliverables,
            task.Remarks);
        try
        {
            foreach (var dependency in task.DependenciesList)
            {
                _dal.Dependency.Create(new DO.Dependency(0, task.Id, dependency.Id));
            }

            int id = _dal.Task.Create(doTask);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task Read(int id)
    {
        DO.Task doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"task with id: {id} does not exist");

        List<DO.Dependency> dependencyList = new List<DO.Dependency>(_dal.Dependency.ReadAll(dependency => dependency.Id == id)!);
        List<BO.TaskInList> taskInLists = (from dependency in dependencyList
                                           select new BO.TaskInList
                                           {
                                               Id = dependency.DependsOnTask,
                                               Description = _dal.Task.Read(dependency.DependsOnTask)!.Description,
                                               Alias = _dal.Task.Read(dependency.DependsOnTask)!.Alias,
                                               // Status = _dal.Task.Read(dependency.DependsOnTask)!.
                                           }).ToList();
        BO.Task boTask = new BO.Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            //Status=??(task) => task.Engineerid == id
            DependenciesList = taskInLists,
            Milestone = new BO.MilestoneInTask() { Id = id },//very not sure what to do here
            BaselineStartDate = doTask.ScheduledDate,
            StartDate = doTask.StartDate,
            //ForecastDate=??  in what stage they are at????
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = new BO.EngineerInTask()
            {
                Id = doTask.Engineerid,
                Name = _dal.Engineer.Read(doTask.Id)!.Name
            },
            ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
        };
        return boTask;
    }

    public IEnumerable<BO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return _dal.Task.ReadAll(filter).Select(doTask =>
        {
            if (doTask == null)
                return null;
            List<DO.Dependency> dependencyList = new List<DO.Dependency>(_dal.Dependency.ReadAll(dependency => dependency.DependsOnTask == doTask.Id)!);
            List<BO.TaskInList> tasksInList = dependencyList.Select(dependency => new BO.TaskInList
            {
                Id = dependency.Id,
                Description = _dal.Task.Read(dependency.DependsOnTask)!.Description,
                Alias = _dal.Task.Read(dependency.DependsOnTask)!.Alias
            }).ToList();

            return new BO.Task
            {
                Id = doTask.Id,
                Description = doTask.Description,
                Alias = doTask.Alias,
                CreatedAtDate = doTask.CeratedAtDate,
                //Status = ?? (task) => task.Engineerid == id
                DependenciesList = tasksInList,

                BaselineStartDate = doTask.ScheduledDate,
                StartDate = doTask.StartDate,
                //ForecastDate=?? in what stage are they at????
                DeadlineDate = doTask.DeadlineDate,
                CompleteDate = doTask.CompleteDate,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Engineer = new BO.EngineerInTask
                {
                    Id = doTask.Engineerid,
                    Name = _dal.Engineer.Read(doTask.Engineerid)!.Name
                },
                ComplexityLevel = (BO.EngineerExperience)doTask.Complexity
            };
        });
    }


    public void Update(BO.Task task)
    {

        if (Read(task.Id) is null)
        {
            throw new BO.BlDoesNotExistException($"There is no task with id:{task.Id}");
        }
        if (task.Alias.Length < 0)
            throw new BO.BlInValidDataException("In valid length of alias");

        DO.Task doTask = new DO.Task(task.Id,
                   task.Description,
                   task.Alias,
                   task.Engineer!.Id,
                   (DO.EngineerExperience)task.ComplexityLevel!,
                   task.CreatedAtDate,
                   false,
                   true,
                   (TimeSpan)(task.CompleteDate - task.StartDate)!,
                   task.StartDate,
                   task.BaselineStartDate,
                   task.DeadlineDate,
                   task.CompleteDate,
                   task.Deliverables,
                   task.Remarks);
        try
        {
            foreach (var dependency in task.DependenciesList)
            {
                _dal.Dependency.Update(new DO.Dependency(0, task.Id, dependency.Id));
            }
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex);
        }
    }
}
