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


        DO.Task doTask = new DO.Task(task.Id, task.Description, task.Alias, task.Engineer!.Id, (DO.EngineerExperience)task.ComplexityLevel!, task.CreatedAtDate);
        try
        {
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
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll(filter)
                select new BO.Task
                {
                    Id = doTask.Id,
                    Description = doTask.Description,
                    Alias = doTask.Alias,
                    CreatedAtDate = doTask.CeratedAtDate,
                    Status = (BO.Status)2
                });


        throw new NotImplementedException();
    }

    public void Update(BO.Task task)
    {
        throw new NotImplementedException();
    }
}
