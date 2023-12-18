//using System.Xml.Linq;

//namespace Dal;

//internal class Config
//{
//    static string s_data_config_xml = "data-config";
//    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
//    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }

//    internal static DateTime? startProjectDate = XMLTools.LoadListFromXMLElement(@"..\xml\data-config.xml").ToDateTimeNullable("startProjectDate");

//    internal static DateTime? endProjectDate = XMLTools.LoadListFromXMLElement(@"..\xml\data-config.xml").ToDateTimeNullable("endProjectDate");
//}

namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }

    internal static DateTime? startProjectDate = XMLTools.LoadListFromXMLElement(@"data-config").ToDateTimeNullable("startProjectDate");

    internal static DateTime? endProjectDate = XMLTools.LoadListFromXMLElement(@"data-config").ToDateTimeNullable("endProjectDate");
}