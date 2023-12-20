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
    public Status? Status { get; set; } = null;
    public MilestoneInTask? Milestone { get; set; } = null;
    public DateTime? BaselineStartDate { get; set; } = null;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? ScheduledStartDate { get; set; } = null;
    public DateTime? ForecastDate { get; set; } = null;
    public DateTime? DeadlineDate { get; set; } = null;
    public DateTime? CompleteDate { get; set; } = null;
    public string? Deliverables { get; set; } = null;
    public string? Remarkes { get; set; } = null;
    public EngineerInTask? Engineer { get; set; } = null;
    public EngineerExperience? ComplexityLevel { get; set; } = null;
}
