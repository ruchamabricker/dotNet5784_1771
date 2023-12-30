using BlApi;

namespace BlImplementation;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;

    private BO.Status calculateStatus(DO.Task doTask)
    {
        BO.Status status;
        if (doTask.ScheduledDate == null || doTask.StartDate == null)//עדיין לא התחיל
            status = 0;
        else if ((doTask.StartDate != null && doTask.StartDate > DateTime.Now) || doTask.CompleteDate == null)//מתוכנן כבר, עדין לא התחיל
            status = (BO.Status)2;
        else if ((doTask.CompleteDate != null && doTask.CompleteDate > DateTime.Now) || doTask.DeadlineDate == null)//התחיל, עדין לא נגמר
            status = (BO.Status)3;
        else if (doTask.DeadlineDate != null && doTask.DeadlineDate < DateTime.Now)//עבר את תאריך הסיום המתוכנן
            status = (BO.Status)4;
        else
            status = (BO.Status)0;

        return status;
    }

    private BO.MilestoneInTask? calculateMilestone(List<BO.TaskInList> tasksInList)
    {
        if (tasksInList.Count == 0) return null;
        BO.TaskInList? task = tasksInList.Where(task => _dal.Task.Read(task.Id)!.IsMilestone == true).FirstOrDefault();
        if (task == null) return null;
        return new BO.MilestoneInTask() { Id = task.Id };
    }

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
            if (task.DependenciesList != null)
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
        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message, ex);
        }
    }

    public BO.Task Read(int id)
    {
        DO.Task doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"task with id: {id} does not exist");

        List<DO.Dependency?>? dependencyList = new List<DO.Dependency?>(_dal.Dependency.ReadAll(dependency => dependency.Id == id));
        List<BO.TaskInList>? tasksInList = (from dependency in dependencyList
                                            select new BO.TaskInList
                                            {
                                                Id = dependency.DependsOnTask,
                                                Description = _dal.Task.Read(dependency.DependsOnTask)!.Description,
                                                Alias = _dal.Task.Read(dependency.DependsOnTask)!.Alias,
                                                Status = calculateStatus(_dal.Task.Read(dependency.DependsOnTask)!)
                                            }).ToList();

        List<BO.TaskInList?>? taskInLists2 = new List<BO.TaskInList?>(dependencyList.Select(dependency =>
        {
            if (dependency?.DependentTask != null)
                return new BO.TaskInList
                {
                    Id = dependency.DependsOnTask,
                    Description = _dal.Task.Read(dependency.DependsOnTask)!.Description,
                    Alias = _dal.Task.Read(dependency.DependsOnTask)!.Alias,
                    Status = calculateStatus(_dal.Task.Read(dependency.DependsOnTask)!)
                };
            return null;
        }) 
        );


        BO.Status status = calculateStatus(doTask);

        BO.Task boTask = new BO.Task
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            Status = status,
            DependenciesList = tasksInList,
            Milestone = calculateMilestone(tasksInList),
            BaselineStartDate = doTask.ScheduledDate,
            StartDate = doTask.StartDate,
            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
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

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task?> tasks = _dal.Task.ReadAll().Select(doTask =>
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
                Status = calculateStatus(doTask),
                DependenciesList = tasksInList,
                Milestone = calculateMilestone(tasksInList),
                BaselineStartDate = doTask.ScheduledDate,
                StartDate = doTask.StartDate,
                ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
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
        if (filter == null)
            return tasks;
        return tasks.Where(filter!);
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
            if (task.DependenciesList != null)
                foreach (var dependency in task.DependenciesList!)
                {
                    _dal.Dependency.Create(new DO.Dependency(0, task.Id, dependency.Id));
                }
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", ex);
        }
    }
}
