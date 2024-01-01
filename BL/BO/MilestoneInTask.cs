namespace BO;

public class MilestoneInTask
{
    public int Id { get; init; }
    public string Alias { get; set; }
    public override string ToString() => this.ToStringProperty();
}
