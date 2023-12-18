using DalApi;
namespace Dal;

sealed public class DalList : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
    public void Reset()
    {
        Engineer.Reset();
        Task.Reset();
        Dependency.Reset();
    }

    DateTime? IDal.startProjectDate => DataSource.Config.startProjectDate;

    public DateTime? endProjectDate => DataSource.Config.endProjectDate;
}

