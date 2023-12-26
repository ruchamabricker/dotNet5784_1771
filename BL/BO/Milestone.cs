namespace BO;
/// <summary>
/// 
/// </summary>
public class Milestone
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public Status? Status { get; set; } = null;
    public DateTime? ForecastDate { get; set; } = null;
    public DateTime? DeadlineDate { get; set; } = null;
    public DateTime? CompleteDate { get; set; } = null;)
    public double? CompletionPercentage { get; set; } = null;
    public string? Remarks { get; set; } = null;
    public TaskInList Dependencies { get; set; }
    //public override string ToString() => this.ToStringProperty();
}
