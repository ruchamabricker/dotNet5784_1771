using BlApi;
using DalApi;

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

        // Step 1: Create a grouped list with dependent tasks as keys and previous tasks as values
        var groupedDependencies = _dal.Dependency.ReadAll().GroupBy(dependency => dependency?.Id, dependency => dependency?.DependentTask, (Id, Dependencies) => new { Id = Id, Dependencies = Dependencies.ToList() });
        //כל משימה והמשימות שהיא תלויה בהם


        // Step 2: Sort the list of values
        var sortedValues = groupedDependencies.Select(g => g.Dependencies.OrderBy(v => v)).ToList();

        // Step 3: Create a filtered list with distinct values
        var distinctDependencies = sortedValues.SelectMany(v => v).Distinct().ToList();


        //var groupedDependencies2 = _dal.Dependency.ReadAll()
        //    .GroupBy(d => d?.Id, d => d?.DependentTask, (Id, Dependencies) => new { Id = Id, Dependencies = Dependencies.ToList() })
        //    .Select(g => g.Dependencies.OrderBy(v => v)).SelectMany(v => v).Distinct().ToList();



        var groupedDependencies2 = _dal.Dependency.ReadAll()
            .GroupBy(d => d?.Id, d => d?.DependentTask, (Id, Dependencies) => new { Id = Id, Dependencies = Dependencies.ToList() })
            .OrderBy(dep => dep.Id)
            .GroupBy(d => d.Dependencies, (Dependencies, Ids) => new { Dependencies = Dependencies, Ids = Ids.ToList() })
            .ToList();
        //.Select(g => g.Dependencies.OrderBy(v => v).ToArray())
        //.SelectMany(v => v)
        //.Distinct()
        //.ToList();

        // var groupedDependencies2 = _dal.Dependency.ReadAll()
        //.GroupBy(d => d?.Id, d => d?.DependentTask, (Id, Dependencies) => new { Id = Id, Dependencies = Dependencies.ToList() })
        //.Select(g => g.Dependencies.OrderBy(v => v).ToArray())
        //.ToArray();

        int indexMilestone = 1;

        var listOfMilestones = groupedDependencies2.Select(dependency =>
        {
            if (dependency != null)
            {
                DO.Task task = new DO.Task(
                    indexMilestone++, $"Milestone{indexMilestone}", $"M{indexMilestone}", DateTime.Now, true);
                int id = _dal.Task.Create(task);

                foreach (var value in dependency.Dependencies)
                {
                    if (value != null)
                        _dal.Dependency.Create(new DO.Dependency(0, id, value.Value));
                }

                return task;
            }
            return null;
        }

        );


        //var groupedDependencies2 = _dal.Dependency.ReadAll()
        //    .GroupBy(d => d?.Id, d => d?.DependentTask, (Id, Dependencies) => new { Id = Id, Dependencies = Dependencies.ToList() })
        //    .ToList();

        //int indexMilestone = 1;
        //var listOfMilestones = groupedDependencies2.SelectMany(dependency =>
        //{
        //    if (dependency != null)
        //    {
        //        DO.Task task = new DO.Task(
        //            indexMilestone++, $"Milestone{indexMilestone}", $"M{indexMilestone}", DateTime.Now, true);
        //        int id = _dal.Task.Create(task);

        //        foreach (var value in dependency.Dependencies)
        //        {
        //            if (int.TryParse(value, out int intValue))
        //            {
        //                _dal.Dependency.Create(new DO.Dependency(0, id, intValue));
        //            }
        //            else
        //            {
        //                // Handle the case where the value cannot be converted to an int
        //            }
        //        }

        //        return new List<DO.Task> { task };
        //    }

        //    return null!;
        //});





        //resets all dependencys it had before
        _dal.Dependency.Reset();

        //creates new dependencies that are of the milestone
        foreach (var groupOfDependency in groupedDependencies)
        {
            foreach (var dependency in groupOfDependency.Dependencies)
            {
                if (dependency != null)
                {
                    //adds all dependencies for milestone to all the tasks that it depends on
                    _dal.Dependency.Create(new DO.Dependency(0, (int)groupOfDependency.Id!, (int)dependency.Value));

                    //adds a dependency for each task to a milestone
                    var task = _dal.Task.Read(dependency.Value);
                    if (task != null)
                        _dal.Dependency.Create(new DO.Dependency(0, dependency.Value, (int)groupOfDependency.Id!));
                }
            }
        }


        //first milestone
        _dal.Task.Create(new DO.Task(0, "M0", "MileStone0", DateTime.Now, true));

        var dependenciesNotInDependencies = _dal.Dependency.ReadAll().Where(dependency => !distinctDependencies.Contains(dependency?.Id));

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
        if (milestone.Alias.Length < 0)
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



