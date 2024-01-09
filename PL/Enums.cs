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

