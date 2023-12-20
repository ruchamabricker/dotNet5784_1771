using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class TaskInList
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Alias { get; set; }
    public Status? Status { get; set; } = null;
}
