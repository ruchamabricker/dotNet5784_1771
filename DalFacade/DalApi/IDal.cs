using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;

namespace DalApi
{
    public interface IDal
    {
        IEngineer Engineer { get; }
        ITask Task { get; }
        IDependency Dependency { get; }

        DateTime? startProjectDate { get; }

        DateTime? endProjectDate { get; }

        public void Reset();

    }
}
