//using System.Net.Security;

namespace DO;
/// <summary>
/// Dependency between task and engineer
/// </summary>
/// <param name="id"> uniqe id for each dependency </param>
/// <param name="dependentTask"> ID number of pending task </param>
/// <param name="dependsOnTask"> Previous assignment ID number </param>
public record Dependency(
    int id,
    int dependentTask,
    int dependsOnTask
    );
