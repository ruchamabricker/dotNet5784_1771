using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class EngineerInTask
{
    public int Id { get; init; }
    public string Name { get; set; }
    public override string ToString() => this.ToStringProperty();

}
