

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DO;
/// <summary>
/// task entity represents a task with all its props
/// </summary>
/// <param name="id"> uniqe id for the task </param>
/// <param name="description"> description of the task </param>
/// <param name="alias"> short name of the tasd </param>
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
    int id,
    string description,
    string alias,
    int engineerid,
    EngineerExperience complexityLevel,
    DateTime ceratedAt,
    bool milestone=false,
    DateTime? start=null,
    DateTime? scheduledDate = null,
    DateTime? deadline = null,
    DateTime? complete = null,
    string? deliverabels = null,    
    string? remarks=null
    );
