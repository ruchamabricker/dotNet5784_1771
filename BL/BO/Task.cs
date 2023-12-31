namespace BO;

public class Task
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public Status? Status { get; set; } = null;
    public List<BO.TaskInList?>? DependenciesList { get; set; } = null;
    public MilestoneInTask? Milestone { get; set; } = null;
    public DateTime? BaselineStartDate { get; set; } = null;//תאריך התחלה משעור
    public DateTime? StartDate { get; set; } = null;//תאריך התחלה בפועל
   // public DateTime? ScheduledStartDate { get; set; } = null;//i think should be deleted
    public DateTime? ForecastDate { get; set; } = null;//תאריך משוער לסיום
    public DateTime? DeadlineDate { get; set; } = null;//דד ליין תאריך אחרון לסיום
    public DateTime? CompleteDate { get; set; } = null;// תאריך סיום בפועל
    public string? Deliverables { get; set; } = null;//מחרוזת המתארת את התוצר
    public string? Remarks { get; set; } = null;//הערות
    public EngineerInTask? Engineer { get; set; } = null;
    public EngineerExperience? ComplexityLevel { get; set; } = null;
}
