using System.Collections.Generic;
using System;
using System.Collections;
using System.Diagnostics.Tracing;

namespace PL;

internal class EngineerCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> e_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;
    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
   // public BO.EngineerExperience EngineerLevel { get; set; } = BO.EngineerExperience.Novice;

}

internal class TaskCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> e_enums =
    (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;
    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
    public BO.EngineerExperience EngineerLevel { get; set; } = BO.EngineerExperience.Novice;
}

internal class TaskIdsCollection 
{
    static readonly IEnumerable<BO.Status> e_enums =
    (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;
    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
    public BO.EngineerExperience EngineerLevel { get; set; } = BO.EngineerExperience.Novice;
}
