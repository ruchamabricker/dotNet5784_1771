using System;
using System.Data.SqlTypes;
using Dal;
using DalApi;
using DalList;
using DalTest;
using DO;
using DalXml;

namespace Program // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public enum ENTITY { BREAK, DEPENDENCY, ENGINEER, TASK, RESET };
        public enum CRUD { BREAK, CREATE, READ, READALL, UPTADE, DELETE };

        static readonly IDal s_dal = new Dal.DalXml();


        /// <summary>
        /// The function manages the sub-menu of possibility for dependencies
        /// </summary>
        public static void DependencyFunction()
        {
            int crudChoice;
            int id;
            Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete, 6-reset, 0-exit");
            crudChoice = int.Parse(Console.ReadLine()!);
            int dependsOn, dependent;

            switch ((CRUD)crudChoice)
            {
                case CRUD.CREATE:
                    Console.WriteLine("Enter 2 id tasks");
                    try
                    {

                        dependsOn = int.Parse(Console.ReadLine()!);
                        dependent = int.Parse(Console.ReadLine()!);
                        Console.WriteLine("the id of the new dependency is: " + s_dal!.Dependency.Create(new Dependency(0, dependsOn, dependent)));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case CRUD.READ:
                    Console.WriteLine("enter ID to be read");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_dal!.Dependency.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case CRUD.READALL:
                    try
                    {
                        foreach (Dependency dependency in s_dal!.Dependency.ReadAll())
                        {
                            Console.WriteLine(dependency);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case CRUD.DELETE:
                    Console.WriteLine("Enter ID");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        s_dal!.Dependency.Delete(id);
                        Console.WriteLine("deleted successfuly");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CRUD.UPTADE:
                    Console.WriteLine("Enter id ");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_dal!.Dependency.Read(id));
                        Console.WriteLine("if you want to update please enter: 2 task id's");
                        dependsOn = int.Parse(Console.ReadLine()!);
                        dependent = int.Parse(Console.ReadLine()!);
                        if (dependsOn == 0 && dependent == 0)
                            break;
                        s_dal!.Dependency.Update(new Dependency(id, dependent, dependsOn));
                        Console.WriteLine("updated successfuly");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The function manages the sub-menu of possibilities for engineer
        /// </summary>
        public static void EngineerFunction()
        {
            Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete, 6-reset, 0-exit");
            int crudChoice = int.Parse(Console.ReadLine()!);
            int id;
            string name, email;
            double cost;
            EngineerExperience level;
            switch ((CRUD)crudChoice)
            {
                case CRUD.BREAK:
                    break;
                case CRUD.CREATE:
                    Console.WriteLine("Enter ID, name, email , experience level, cost");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        name = Console.ReadLine()!;
                        email = Console.ReadLine()!;
                        level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
                        cost = double.Parse(Console.ReadLine()!);

                        s_dal!.Engineer.Create(new Engineer(id, name, email, level, cost));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case CRUD.READ:
                    Console.WriteLine("Enter ID");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_dal!.Engineer.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case CRUD.READALL:
                    try
                    {
                        foreach (Engineer engineer in s_dal!.Engineer.ReadAll())
                        {
                            Console.WriteLine(engineer);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case CRUD.UPTADE:
                    Console.WriteLine("Enter ID");

                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_dal!.Engineer.Read(id));
                        Console.WriteLine("if you want to update please enter: name, email address, experience level, cost");
                        name = Console.ReadLine()!;
                        email = Console.ReadLine()!;
                        level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
                        cost = double.Parse(Console.ReadLine()!);
                        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email) && cost == 0 && level == null)
                            break;
                        s_dal!.Engineer.Update(new Engineer(id, name, email, level, cost));
                        Console.WriteLine("updated successfuly");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case CRUD.DELETE:
                    Console.WriteLine("Enter ID");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        s_dal!.Engineer.Delete(id);
                        Console.WriteLine("deleted successfuly");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// The function manages the sub-menu of possibilities for tasks
        /// </summary>
        public static void TaskFunction()
        {
            string description, alias;
            int engineerID;
            EngineerExperience complexityLevel;
            DateTime createdAt;
            Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete,  6-reset, 0-exit");
            int crudChoice = int.Parse(Console.ReadLine()!);
            int id;
            switch ((CRUD)crudChoice)
            {
                case CRUD.BREAK:
                    break;
                case CRUD.CREATE:
                    Console.WriteLine("Enter description, alias, engineer ID, complexity Level, Date it was cerated at");
                    try
                    {
                        description = Console.ReadLine()!;
                        alias = Console.ReadLine()!;
                        engineerID = int.Parse(Console.ReadLine()!);
                        complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
                        createdAt = DateTime.Parse(Console.ReadLine()!);
                        int newID = s_dal!.Task.Create(new DO.Task(0, description, alias, engineerID, complexityLevel, createdAt));
                        Console.WriteLine("created successfuly");
                        Console.WriteLine("id is of the new task is: " + newID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CRUD.READ:
                    Console.WriteLine("Enter ID of task");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_dal!.Task.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CRUD.READALL:
                    try
                    {
                        foreach (DO.Task task in s_dal!.Task.ReadAll())
                        {
                            Console.WriteLine(task);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case CRUD.UPTADE:
                    Console.WriteLine("Enter id");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_dal!.Task.Read(id));
                        Console.WriteLine("if you want to update, enter description, alias, engineer ID, Level, Date it was cerated at");
                        description = Console.ReadLine()!;
                        alias = Console.ReadLine()!;
                        engineerID = int.Parse(Console.ReadLine()!);
                        complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
                        createdAt = DateTime.Parse(Console.ReadLine()!);

                        if (string.IsNullOrEmpty(description) && string.IsNullOrEmpty(alias) && engineerID == 0 && complexityLevel == null)
                            break;
                        s_dal!.Task.Update(new DO.Task(id, description, alias, engineerID, complexityLevel, createdAt));
                        Console.WriteLine("updated successfuly");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case CRUD.DELETE:
                    id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        s_dal!.Task.Delete(id);
                        Console.WriteLine("deleted successfuly");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                default:
                    break;
            }
        }

        public static void ResetFunction()
        {
            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
            if (ans == "Y") //stage 3
                s_dal.Reset();
        }


        static void Main(string[] args)
        {
            //Initialization.DO(s_dalTask, s_dalDependency, s_dalEngineer);
            Initialization.Do(s_dal); //stage 2

            int entityChoice;
            Console.WriteLine("choose: 0-exit 1-dependency, 2-engineer, 3-task, 4-reset all");
            entityChoice = int.Parse(Console.ReadLine()!);

            while (entityChoice != null)
            {
                switch ((ENTITY)entityChoice) // Explicitly cast the integer to ENTITY enum
                {

                    case ENTITY.DEPENDENCY:
                        DependencyFunction();
                        break;

                    case ENTITY.ENGINEER:
                        EngineerFunction();
                        break;

                    case ENTITY.TASK:
                        TaskFunction();
                        break;

                    case ENTITY.RESET:
                        ResetFunction();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("choose: 1-dependency, 2-engineer, 3-task, 4-exit");
                entityChoice = int.Parse(Console.ReadLine()!);
            }

        }
    }
}
