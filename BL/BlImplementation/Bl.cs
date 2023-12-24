
namespace BlImplementation;
using BlApi;

internal class Bl : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMilestone milestone => new MilestoneImplementation();

    public ITask task => new TaskImplementation();
}
