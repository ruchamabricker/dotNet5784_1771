using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Task
{
    public int Id { get; init; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public DateTime CreatedAtDate { get; set; }
    public Status? Status { get; set; }
    public MilestoneInTask? Milestone { get; set; }
    public DateTime? BaselineStartDate { get; set;}
    public DateTime? StartDate { get; set;}
    public DateTime? ScheduledStartDate { get; set;}
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverables {  get; set; }
    public string? Remarkes { get; set; }
    public EngineerInTask Engineer { get; set; }
    public EngineerExperience ComplexityLevel { get; set; }
}
