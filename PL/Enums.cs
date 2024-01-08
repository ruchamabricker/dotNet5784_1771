using System.Collections.Generic;
using System;
using System.Collections;

namespace PL;

internal class Enums
{
    internal class EngineersCollection : IEnumerable
    {
        static readonly IEnumerable<BO.Engineer> e_enums =
        (Enum.GetValues(typeof(BO.Engineer)) as IEnumerable<BO.Engineer>)!;
        public IEnumerator<BO.Engineer> GetEnumerator() => e_enums.GetEnumerator();
    }

}
