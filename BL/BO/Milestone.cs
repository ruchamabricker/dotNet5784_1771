namespace BO;
/// <summary>
/// 
/// </summary>
public class Milestone
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public Status? Status { get; set; } = null;
    public DateTime? StartDate { get; set; } = null;//תאריך התחלה בפועל
    public DateTime? ForecastDate { get; set; } = null;//תאריך משוער לסיום
    public DateTime? DeadlineDate { get; set; } = null;//תאריך אחרון לסיום
    public DateTime? CompleteDate { get; set; } = null;//תאריך סיום
    public double? CompletionPercentage { get; set; } = null;
    public string? Remarks { get; set; } = null;
    public List<BO.TaskInList>? Dependencies { get; set; } = null;
    public override string ToString() => this.ToStringProperty();
}
