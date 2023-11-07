using System;
using Dal;
using DalApi;
using DalList;
using DalTest;
using DO;

namespace Program // Note: actual namespace depends on the project name.
{/// <summary>
/// 
/// </summary>
    internal class Program
    {
        public enum ENTITY { BREAK, DEPENDENCY, ENGINEER, TASK };
        public enum CRUD { BREAK, CREATE, READ, READALL, UPTADE, DELETE };

        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1


        static void Main(string[] args)
        {
            Initialization.DO(s_dalTask, s_dalDependency, s_dalEngineer);

            int entityChoice, crudChoice;
            Console.WriteLine("choose: 1-dependency, 2-engineer, 3-task, 4-exit");
            entityChoice = int.Parse(Console.ReadLine());
            while (entityChoice != null)
            {
                switch ((ENTITY)entityChoice) // Explicitly cast the integer to ENTITY enum
                {

                    case ENTITY.DEPENDENCY:
                        Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete, 0-exit");
                        crudChoice = int.Parse(Console.ReadLine());
                        int id;
                        switch ((CRUD)crudChoice)
                        {

                            case CRUD.CREATE:
                                Console.WriteLine("Enter 2 id tasks");
                                int dependsOn, dependent;
                                dependsOn = int.Parse(Console.ReadLine());
                                dependent = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalDependency.Create(new Dependency(0, dependsOn, dependent)));
                                break;
                            case CRUD.READ:
                                Console.WriteLine("enter ID to be read");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalDependency.Read(id));
                                break;
                            case CRUD.READALL:
                                foreach (Dependency dependency in s_dalDependency.ReadAll())
                                {
                                    Console.WriteLine(dependency);
                                }
                                break;
                            case CRUD.DELETE:
                                Console.WriteLine("ERROR! Can't delete a dependency");
                                break;
                            case CRUD.UPTADE:
                                Console.WriteLine("Enter id ");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalDependency.Read(id));
                                Console.WriteLine("if you want to update please enter: 2 task id's");
                                dependsOn = int.Parse(Console.ReadLine());
                                dependent = int.Parse(Console.ReadLine());
                                if (dependsOn == 0 && dependent == 0)
                                    break;
                                try
                                {
                                    s_dalDependency.Update(new Dependency(id, dependent, dependsOn));
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            default:
                                break;
                        }
                        break;



                    case ENTITY.ENGINEER:
                        Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete, 0-exit");
                        crudChoice = int.Parse(Console.ReadLine());
                        string name, email;
                        double cost;
                        EngineerExperience level;
                        switch ((CRUD)crudChoice)
                        {
                            case CRUD.BREAK:
                                break;
                            case CRUD.CREATE:
                                Console.WriteLine("Enter ID, name, email address, experience level, cost");
                                id = int.Parse(Console.ReadLine());
                                name = Console.ReadLine();
                                email = Console.ReadLine();
                                cost = double.Parse(Console.ReadLine());
                                level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine());
                                s_dalEngineer.Create(new Engineer(id, name, email, level, cost));

                                break;
                            case CRUD.READ:
                                Console.WriteLine("Enter ID");
                                id = int.Parse(Console.ReadLine());
                                try
                                {
                                    Console.WriteLine(s_dalEngineer.Read(id));
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex.Source);
                                }

                                break;
                            case CRUD.READALL:
                                foreach (Engineer engineer in s_dalEngineer.ReadAll())
                                {
                                    Console.WriteLine(engineer);
                                }
                                break;
                            case CRUD.UPTADE:
                                Console.WriteLine("Enter ID");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalEngineer.Read(id));
                                Console.WriteLine("if you want to update please enter: name, email address, experience level, cost");
                                name = Console.ReadLine();
                                email = Console.ReadLine();
                                cost = double.Parse(Console.ReadLine());
                                level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine());
                                try
                                {
                                    s_dalEngineer.Update(new Engineer(id, name, email, level, cost));
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            case CRUD.DELETE:
                                Console.WriteLine("Enter ID");
                                id = int.Parse(Console.ReadLine());
                                try
                                {
                                    s_dalEngineer.Delete(id);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            default:
                                break;
                        }

                        break;




                    case ENTITY.TASK:
                        string description, alias;
                        int engineerID;
                        EngineerExperience complexityLevel;
                        DateTime createdAt;
                        Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete, 0-exit");
                        crudChoice = int.Parse(Console.ReadLine());
                        switch ((CRUD)crudChoice)
                        {
                            case CRUD.BREAK:
                                break;
                            case CRUD.CREATE:
                                Console.WriteLine("Enter id, description, alias, engineer ID, complexity Level, Date it was cerated at");
                                description = Console.ReadLine();
                                alias = Console.ReadLine();
                                engineerID = int.Parse(Console.ReadLine());
                                complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine());
                                createdAt = DateTime.Parse(Console.ReadLine());
                                s_dalTask.Create(new DO.Task(0, description, alias, engineerID, complexityLevel, createdAt));
                                break;
                            case CRUD.READ:
                                Console.WriteLine("Enter ID of task");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalTask.Read(id));
                                break;
                            case CRUD.READALL:
                                foreach (DO.Task task in s_dalTask.ReadAll())
                                {
                                    Console.WriteLine(task);
                                }
                                break;
                            case CRUD.UPTADE:
                                try
                                {
                                    Console.WriteLine("Enter id(of the task you want to update) description, alias, engineer ID, complexity Level, Date it was cerated at");
                                    id = int.Parse(Console.ReadLine());
                                    description = Console.ReadLine();
                                    alias = Console.ReadLine();
                                    engineerID = int.Parse(Console.ReadLine());
                                    complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine());
                                    createdAt = DateTime.Parse(Console.ReadLine());
                                    s_dalTask.Create(new DO.Task(id, description, alias, engineerID, complexityLevel, createdAt));
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                break;
                            case CRUD.DELETE:
                                id = int.Parse(Console.ReadLine());
                                try
                                {
                                    s_dalTask.Delete(id);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                break;
                            default:
                                break;
                        }
                        break;


                    default:
                        break;
                }
                Console.WriteLine("choose: 1-dependency, 2-engineer, 3-task, 4-exit");
                entityChoice = int.Parse(Console.ReadLine());
            }

        }
    }
}
