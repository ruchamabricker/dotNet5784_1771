using DalApi;
namespace Dal;

sealed public class DalList : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
}

