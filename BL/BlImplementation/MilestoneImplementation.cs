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

    public void BuildSchdual() { }

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
                                               Status = calculateStatus( _dal.Task.Read(dependency.DependsOnTask)!)
                                           }).ToList();

        BO.Milestone boTask = new BO.Milestone
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            Status=  calculateStatus(doTask),
            StartDate = doTask.StartDate,
            ForecastDate = doTask.StartDate + doTask.RequiredEffortTime,
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            CompletionPercentage= (taskInLists.Count(t => t.Status == BO.Status.OnTrack) / (double)taskInLists.Count) * 100,//checks how many tasks were done already
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
                  milestoneToUpdate.Engineerid,
                  (DO.EngineerExperience)milestoneToUpdate.Complexity!,
                  milestoneToUpdate.CeratedAtDate,
                  milestoneToUpdate.IsMilestone,
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
