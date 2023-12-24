
using BlApi;
using BO;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Engineer engineer)
    {
        if (engineer.Id <= 0)
            throw new BO.BlInValidDataException("In valid id");
        if (engineer.Name.Length < 0)
            throw new BO.BlInValidDataException("In valid name");
        if (engineer.Cost <= 0)
            throw new BO.BlInValidDataException("no such cost, must be positive");

        DO.Engineer doEngineer = new DO.Engineer(engineer.Id, engineer.Name, engineer.Email, (DO.EngineerExperience)engineer.Level, engineer.Cost);
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
        throw new NotImplementedException();
    }

    public BO.Engineer Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"engineer with id: {id} does not exist");
        return new BO.Engineer()
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost
        };

    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<DO.Engineer, bool>? filter = null)
    {
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll(filter)
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost,
                    Task = new TaskInEngineer()
                    {
                        Id = _dal.Task.ReadAll().FirstOrDefault(task => task?.Id == doEngineer.Id)!.Id!,
                        Alias = _dal.Task.ReadAll().FirstOrDefault(task => task?.Id == doEngineer.Id)?.Alias!
                    }
                });
    }

    public void Update(BO.Engineer engineer)
    {
        if (Read(engineer.Id) == null)
            throw new BO.BlDoesNotExistException($"There is no engineer with id: {engineer.Id}");
        if (engineer.Id <= 0)
            throw new BO.BlInValidDataException("In valid id");
        if (engineer.Name.Length < 0)
            throw new BO.BlInValidDataException("In valid name");
        if (engineer.Cost <= 0)
            throw new BO.BlInValidDataException("no such cost, must be positive");
        DO.Engineer doEngineer = new DO.Engineer(engineer.Id, engineer.Name, engineer.Email, (DO.EngineerExperience)engineer.Level, engineer.Cost);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={engineer.Id} already exists", ex);
        }
    }
}
