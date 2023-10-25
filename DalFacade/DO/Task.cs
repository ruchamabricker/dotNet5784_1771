

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DO;
/// <summary>
/// task entity represents a task
/// </summary>
/// <param name="id"></param>
/// <param name="description"></param>
/// <param name="alias"></param>
/// <param name="milestone"></param>
/// <param name="ceratedAt"></param>
/// <param name="start"></param>
/// <param name="scheduledDate"></param>
/// <param name="deadline"></param>
/// <param name="complete"></param>
/// <param name="deliverabels"></param>
/// <param name="remarks"></param>
/// <param name="engineerid"></param>
/// <param name="complexityLevel"></param>
public record Task(
    int id,
    string description,
    string alias,
    bool milestone,
    DateTime ceratedAt,
    DateTime start,
    DateTime scheduledDate,
    DateTime deadline,
    DateTime complete,
    string deliverabels,
    string remarks,
    int engineerid,
    EngineerExperience complexityLevel
    );
