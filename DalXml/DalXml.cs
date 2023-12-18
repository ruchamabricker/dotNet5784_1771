using DalApi;

namespace Dal;

sealed public class DalXml : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public DateTime? startProjectDate => Config.startProjectDate;
    public DateTime? endProjectDate => Config.endProjectDate;

    public void Reset()
    {
        Engineer.Reset();
        Task.Reset();
        Dependency.Reset();
    }
}
