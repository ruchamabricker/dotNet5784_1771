using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

internal static class Tools
{
    public static string ToStringProperty(this object p)
    {
        var prop = p.GetType().GetProperties();
        string str = "";
        foreach (var property in prop)
        {
            str += property.Name + ": " + property.GetValue(p) + "  ";
        }
        return str;
    }
}
