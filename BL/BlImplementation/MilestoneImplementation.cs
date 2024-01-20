using BlApi;
using DalApi;
using DO;
using System.Runtime.Intrinsics.Arm;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
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

    public void Create()
    {
        _dal.Engineer.Create(new Engineer(123456789, "e1", "e1@gmail.com", (EngineerExperience)5, 250));
        _dal.Engineer.Create(new Engineer(123456788, "e2", "e2@gmail.com", (EngineerExperience)4, 280));
        _dal.Engineer.Create(new Engineer(123456787, "e3", "e3@gmail.com", (EngineerExperience)3, 290));
        _dal.Engineer.Create(new Engineer(123456786, "e4", "e4@gmail.com", (EngineerExperience)2, 350));
        _dal.Engineer.Create(new Engineer(123456785, "e5", "e5@gmail.com", (EngineerExperience)5, 250));
        _dal.Engineer.Create(new Engineer(123456784, "e6", "e6@gmail.com", (EngineerExperience)4, 280));
        _dal.Engineer.Create(new Engineer(123456783, "e7", "e5@gmail.com", (EngineerExperience)5, 250));
        _dal.Engineer.Create(new Engineer(123456782, "e8", "e6@gmail.com", (EngineerExperience)4, 280));
        _dal.Task.Create(new DO.Task(0, "t1", "t1", DateTime.Now, false, 123456789, (EngineerExperience)3));
        _dal.Task.Create(new DO.Task(0, "t2", "t2", DateTime.Now, false, 123456788, (EngineerExperience)3));
        _dal.Task.Create(new DO.Task(0, "t3", "t3", DateTime.Now, false, 123456787, (EngineerExperience)3));
        _dal.Task.Create(new DO.Task(0, "t4", "t4", DateTime.Now, false, 123456786, (EngineerExperience)3));
        _dal.Task.Create(new DO.Task(0, "t5", "t5", DateTime.Now, false, 123456785, (EngineerExperience)3));
        _dal.Task.Create(new DO.Task(0, "t6", "t6", DateTime.Now, false, 123456784, (EngineerExperience)3));
        _dal.Task.Create(new DO.Task(0, "t7", "t7", DateTime.Now, false, 123456783, (EngineerExperience)3));
        _dal.Task.Create(new DO.Task(0, "t8", "t8", DateTime.Now, false, 123456782, (EngineerExperience)3));
        _dal.Dependency.Create(new Dependency(1000, 1002, 1000));
        _dal.Dependency.Create(new Dependency(1001, 1003, 1000));
        _dal.Dependency.Create(new Dependency(1002, 1004, 1000));
        _dal.Dependency.Create(new Dependency(1003, 1002, 1001));
        _dal.Dependency.Create(new Dependency(1004, 1003, 1001));
        _dal.Dependency.Create(new Dependency(1005, 1004, 1001));
        _dal.Dependency.Create(new Dependency(1006, 1005, 1002));
        _dal.Dependency.Create(new Dependency(1007, 1006, 1002));
        _dal.Dependency.Create(new Dependency(1008, 1005, 1003));
        _dal.Dependency.Create(new Dependency(1009, 1006, 1003));
        _dal.Dependency.Create(new Dependency(1010, 1007, 1005));
        _dal.Dependency.Create(new Dependency(1011, 1007, 1006));
        _dal.Dependency.Create(new Dependency(1012, 1007, 1004));


        IEnumerable<DO.Task?> tasks = _dal.Task.ReadAll();

        var firstGrouped = _dal.Dependency.ReadAll()
          .GroupBy(d => d.DependentTask, (dependentTask, dependencies) => new
          {
              Id = dependentTask,
              Dependencies = dependencies.Select(dep => dep?.DependsOnTask).ToList()
          })
          .OrderBy(dep => dep.Id);


        var groupedDependencies = firstGrouped
            .GroupBy(d => string.Join(",", d.Dependencies), (dependencies, ids) => new
            {
                Dependencies = dependencies.Split(',').Select(int.Parse).ToList(),
                Ids = ids.Select(i => i.Id).ToList()
            })
            .ToList();

        int indexMilestone = 0;

        //resets all dependencys it had before
        _dal.Dependency.Reset();

        foreach (var groupOfTasks in groupedDependencies)
        {
            if (groupOfTasks != null)
            {
                DO.Task task = new DO.Task(
                    indexMilestone++, $"Milestone{indexMilestone}", $"M{indexMilestone}", DateTime.Now, true);
                int idOfNewMilestone = _dal.Task.Create(task);

                //creating new dependencies, so that each task dependes on the mileston that was just created

                //goes threw all the task that depend on the same dependencies 
                foreach (var dependency in groupOfTasks.Dependencies)
                {
                    _dal.Dependency.Create(new DO.Dependency(0, idOfNewMilestone, dependency));
                }

                //goes threw all the tasks that the milestone depends on them
                foreach (var idOfTask in groupOfTasks.Ids)
                {
                    _dal.Dependency.Create(new DO.Dependency(0, idOfTask, idOfNewMilestone));
                }

            }
        }

        //returns all the task that don't depend on any other tasks
        var tasksNotDependent = _dal.Task.ReadAll()
            .Where(task => !_dal.Dependency.ReadAll().Any(dependency => dependency?.DependentTask == task?.Id))
            .ToList();


        //returns all task that no other tasks depend on them
        var tasksNotDependensOn = _dal.Task.ReadAll()
           .Where(task => !_dal.Dependency.ReadAll().Any(dependency => dependency?.DependsOnTask == task?.Id))
           .ToList();

        //first milestone
        int firstMilestoneId = _dal.Task.Create(new DO.Task(0, "MileStone0", "M0", DateTime.Now, true));

        //last milestone
        int lastMilestoneId = _dal.Task.Create(new DO.Task(indexMilestone++, $"MileStone{indexMilestone}", $"M{indexMilestone}", DateTime.Now, true));

        //builds new dependencies for these tasks
        foreach (var task in tasksNotDependent)
        {
            _dal.Dependency.Create(new DO.Dependency(0, task!.Id, firstMilestoneId));
        }

        //builds new dependencies for these tasks
        foreach (var task in tasksNotDependensOn )
        {
            _dal.Dependency.Create(new DO.Dependency(0, lastMilestoneId, task!.Id));
        }


        Console.WriteLine("ALL TASKS AND MILESTONE: ");
        foreach (var task in _dal.Task.ReadAll())
            Console.WriteLine(task);
        Console.WriteLine("ALL DEPENDENCIES: ");
        foreach (var task in _dal.Dependency.ReadAll())
            Console.WriteLine(task);

        //create dates for all tasks:

        //I have all the dependencies, and their I know what task is dependent on what task....

    }


    public BO.Milestone Read(int id)
    {

        DO.Task doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Milestome with id: {id} does not exist");
        if (!doTask.IsMilestone)
            throw new BO.BlDoesNotExistException($"Milestome with id: {id} does no exist");

        List<DO.Dependency> dependencyList = new List<DO.Dependency>(_dal.Dependency.ReadAll(dependency => dependency.Id == id)!);

        List<BO.TaskInList> taskInLists = (from dependency in dependencyList
                                           select new BO.TaskInList
                                           {
                                               Id = dependency.DependsOnTask,
                                               Description = _dal.Task.Read(dependency.DependsOnTask)!.Description,
                                               Alias = _dal.Task.Read(dependency.DependsOnTask)!.Alias,
                                               Status = calculateStatus(_dal.Task.Read(dependency.DependsOnTask)!)
                                           }).ToList();

        BO.Milestone boTask = new BO.Milestone
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            Status = calculateStatus(doTask),
            StartDate = doTask.StartDate,
            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            CompletionPercentage = (taskInLists.Count(t => t.Status == BO.Status.OnTrack) / (double)taskInLists.Count) * 100,//checks how many tasks were done already
            Remarks = doTask.Remarks,
            Dependencies = taskInLists,

        };
        return boTask;
    }

    public BO.Milestone Update(BO.Milestone milestone)
    {
        if (Read(milestone.Id) is null)
            throw new BO.BlDoesNotExistException($"There is no milestone with id:{milestone.Id}");
        if (milestone.Alias?.Length < 0)
            throw new BO.BlInValidDataException("In valid length of alias");

        DO.Task milestoneToUpdate = _dal.Task.Read(milestone.Id)!;

        DO.Task doTask = new DO.Task(milestone.Id,
                  milestone.Description!,
                  milestone.Alias!,
                  milestoneToUpdate.CeratedAtDate,
                  milestoneToUpdate.IsMilestone,
                  milestoneToUpdate.Engineerid,
                  (DO.EngineerExperience)milestoneToUpdate.Complexity!,
                  milestoneToUpdate.Active,
                  milestoneToUpdate.RequiredEffortTime,
                  milestoneToUpdate.StartDate,
                  milestoneToUpdate.ScheduledDate,
                  milestoneToUpdate.DeadlineDate,
                  milestoneToUpdate.CompleteDate,
                  milestoneToUpdate.Deliverables,
                  milestone.Remarks
                  );
        _dal.Task.Update(doTask);

        return Read(milestone.Id);
    }
}



