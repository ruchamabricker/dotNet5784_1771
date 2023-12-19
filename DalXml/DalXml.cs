using DalApi;
using System.Diagnostics;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
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
