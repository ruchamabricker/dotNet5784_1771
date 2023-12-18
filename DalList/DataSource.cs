
namespace Dal;

internal static class DataSource
{

    internal static class Config
    {
        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
        

        internal const int startTaskId = 1000;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal static DateTime startProjectDate = new DateTime(1 / 5 / 2023);
        internal static DateTime endProjectDate = new DateTime(1 / 1 / 2024);

    }


    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();

}
