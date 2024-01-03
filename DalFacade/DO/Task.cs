
namespace DO;
/// <summary>
/// task entity represents a task with all its props
/// </summary>
/// <param name="id"> uniqe id for the task </param>
/// <param name="description"> description of the task </param>
/// <param name="alias"> short name of the task </param>
/// <param name="milestone"> significant event or achievement that marks a key point in the progress of a task </param>
/// <param name="ceratedAt"> date the task was created </param>
/// <param name="start"> date the task started </param>
/// <param name="scheduledDate">  scheduled date to finish the task </param>
/// <param name="deadline"> deadline to finish the task </param>
/// <param name="complete"> date the task was completed </param>
/// <param name="deliverabels"> a string describing the deliverabels </param>
/// <param name="remarks"> remarks about the task </param>
/// <param name="engineerid"> id of the engineer </param>
/// <param name="complexityLevel"> level of coplexity </param>

public record Task(
    int Id,
    string Description,
    string Alias,   
    DateTime CeratedAtDate,//תאריך יצירת משימה
    bool IsMilestone = false,
    int? Engineerid=null,
    EngineerExperience? Complexity=null,
    bool Active = true,//delete??
    TimeSpan? RequiredEffortTime = null,
    DateTime? StartDate = null,//תאריך התחלה
    DateTime? ScheduledDate = null,//תאריך מתוכנן להתחלה
    DateTime? DeadlineDate = null,//תאריך סיום אחרון אפשרי
    DateTime? CompleteDate = null,//תאריך סיום
    string? Deliverables = null,
    string? Remarks = null
    )
{
    public Task() : this(0, "", "", DateTime.Now) { }
}
