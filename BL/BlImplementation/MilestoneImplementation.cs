using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;

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
                                               // Status = _dal.Task.Read(dependency.DependsOnTask)!.
                                           }).ToList();
        BO.Milestone boTask = new BO.Milestone
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CeratedAtDate,
            //Status=??(task) => task.Engineerid == id
            StartDate = doTask.StartDate,
            //ForecastDate=??  in what stage they are at????
            DeadlineDate = doTask.DeadlineDate,
            CompleteDate = doTask.CompleteDate,
            //CompletionPercentage=
            Remarks = doTask.Remarks,
            Dependencies = taskInLists,

        };
        return boTask;
    }

    public BO.Milestone Update(int id)
    {
        
        throw new NotImplementedException();
    }
}
