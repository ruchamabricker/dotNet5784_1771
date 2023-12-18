using DalApi;

namespace Dal;

sealed public class DalXml : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public DateTime? startProjectDate => throw new NotImplementedException();

    public DateTime? endProjectDate => throw new NotImplementedException();

    public void Reset()
    {
        Engineer.Reset();
        Task.Reset();
        Dependency.Reset();
    }
}
