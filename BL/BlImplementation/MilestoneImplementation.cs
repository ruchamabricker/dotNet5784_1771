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
        IEnumerable<DO.Task?> tasks = _dal.Task.ReadAll();

        var groupedDependencies = _dal.Dependency.ReadAll()
            .GroupBy(d => d?.DependentTask, (Id, Dependencies) => new { Id = Id, Dependencies = Dependencies.ToList() })
            .OrderBy(dep => dep.Id)
            .GroupBy(d => d.Dependencies, (Dependencies, Ids) => new { Dependencies = Dependencies, Ids = Ids.ToList() })//each 2 tasks that are dependent on the same tasks, are together now
            .ToList();

        int indexMilestone = 1;

        foreach (var x in groupedDependencies)
        {
            foreach (var y in x.Dependencies)
            {
                Console.WriteLine(y);
            }
            foreach (var z in x.Ids)
            {
                Console.WriteLine("IN SIDE" + z);
            }
        }

        foreach (var task in _dal.Dependency.ReadAll())
            Console.WriteLine(task);
        Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //resets all dependencys it had before
        _dal.Dependency.Reset();

        foreach (var groupOfTasks in groupedDependencies)
        {

            //}

            //var listOfMilestones = groupedDependencies2.Select(groupOfTasks =>
            //{
            if (groupOfTasks != null)
            {
                DO.Task task = new DO.Task(
                    indexMilestone++, $"Milestone{indexMilestone}", $"M{indexMilestone}", DateTime.Now, true);
                int idOfNewMilestone = _dal.Task.Create(task);

                //creating new dependencies, so that each task dependes on the mileston that was just created

                //goes threw all the task that depend on the same dependencies 
                foreach (var dependency in groupOfTasks.Dependencies)
                {
                    if (dependency != null)
                        _dal.Dependency.Create(new DO.Dependency(0, idOfNewMilestone, dependency.Id));
                }

                //goes threw all the tasks that the milestone depends on them
                foreach (var idOfTask in groupOfTasks.Ids)
                {
                    if (idOfTask != null)
                        _dal.Dependency.Create(new DO.Dependency(0, idOfTask.Id!.Value, idOfNewMilestone));
                }

                //return task;
            }
            //  return null;
        }

        //var d = _dal.Task.ReadAll();
        //foreach (var dependency in d)
        //{
        //    Console.WriteLine(dependency);
        //    Console.WriteLine("***");
        //}


        //first milestone
        int firstMilestoneId = _dal.Task.Create(new DO.Task(0, "M0", "MileStone0", DateTime.Now, true));

        //returns all the task that don't depend on any other tasks
        var tasksNotDependent = _dal.Task.ReadAll()
            .Where(task => !_dal.Dependency.ReadAll().Any(dependency => dependency?.DependentTask == task?.Id))
            .ToList();

        //builds new dependencies for these tasks
        tasksNotDependent.Select(task =>
           _dal.Dependency.Create(new DO.Dependency(0, task!.Id, firstMilestoneId))
        );

        //last milestone
        int lastMilestoneId = _dal.Task.Create(new DO.Task(0, $"M{indexMilestone}", $"MileStone{indexMilestone}", DateTime.Now, true));

        //returns all task that no other tasks depend on them
        var tasksNotDependensOn = _dal.Task.ReadAll()
           .Where(task => !_dal.Dependency.ReadAll().Any(dependency => dependency?.DependsOnTask == task?.Id))
           .ToList();

        //builds new dependencies for these tasks
        tasksNotDependensOn.Select(task =>
           _dal.Dependency.Create(new DO.Dependency(0, lastMilestoneId, task!.Id))
        );

    //   .Select(dep => Console.WriteLine(dep));
        foreach (var task in _dal.Dependency.ReadAll())
            Console.WriteLine(task);
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
                  milestone.Description,
                  milestone.Alias,
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



