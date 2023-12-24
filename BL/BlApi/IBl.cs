namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public IMilestone milestone { get; }
    public ITask task { get; }
}
