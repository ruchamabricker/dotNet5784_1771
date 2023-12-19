using DalApi;
namespace Dal;

sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
    public void Reset()
    {
        Engineer.Reset();
        Task.Reset();
        Dependency.Reset();
    }

    public DateTime? startProjectDate => DataSource.Config.startProjectDate;

    public DateTime? endProjectDate => DataSource.Config.endProjectDate;
}

