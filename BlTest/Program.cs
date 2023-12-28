// See https://aka.ms/new-console-template for more information
using BO;
using DalApi;

namespace Program // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public enum ENTITY { BREAK, MILESTONE, ENGINEER, TASK };
        public enum CRUD { BREAK, CREATE, READ, READALL, UPTADE, DELETE };

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public static void MilestoneFunction()
        {
            int id;
            Console.WriteLine("choose: 2-read, 4-update, 0-exit");
            int crudChoice = int.Parse(Console.ReadLine()!);
            switch ((CRUD)crudChoice)
            {
                case CRUD.BREAK:
                    break;

                case CRUD.READ:
                    Console.WriteLine("Enter ID");
                    try
                    {
                        id = int.Parse(Console.ReadLine()!);
                        Console.WriteLine(s_bl.milestone.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case CRUD.UPTADE:
                    string description, alias, remarks;
                    Console.WriteLine("Enter decripsion, alias and remarks to update");
                    try
                    {
                        description = Console.ReadLine()!;
                        alias = Console.ReadLine()!;
                        remarks = Console.ReadLine() ?? "";
                        s_bl.milestone.Update(new BO.Milestone() { Description = description, Alias = alias, Remarks = remarks });

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message, ex);
                    }

                    break;

                default:
                    break;

            }
        }
        public static void EngineerFunction()
        {
            Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete, 6-reset, 0-exit");
            int crudChoice = int.Parse(Console.ReadLine()!);
            int id;
            string name, email;
            double cost;
            EngineerExperience level;
            switch (((CRUD)crudChoice))
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

                        int returnedId = s_bl!.Engineer.Create(new BO.Engineer()
                        {
                            Id = id,
                            Name = name,
                            Email = email,
                            Level = level,
                            Cost = cost,
                        });
                        Console.WriteLine("Id of new engineer: " + returnedId);
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
                        Console.WriteLine(s_bl.Engineer.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CRUD.READALL:
                    try
                    {
                        foreach (Engineer? engineer in s_bl!.Engineer.ReadAll())
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
                        Console.WriteLine(s_bl!.Engineer.Read(id));
                        Console.WriteLine("if you want to update please enter: name, email address, experience level, cost");
                        name = Console.ReadLine()!;
                        email = Console.ReadLine()!;
                        level = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
                        cost = double.Parse(Console.ReadLine()!);
                        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email) && cost == 0 && level == 0)
                            break;
                        s_bl!.Engineer.Update(new BO.Engineer()
                        {
                            Id = id,
                            Name = name,
                            Email = email,
                            Level = level,
                            Cost = cost,
                        });
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
                        s_bl!.Engineer.Delete(id);
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
        public static void TaskFunction()
        {
            string description, alias, deliverables, remarks;
            int engineerID, dependencyId;
            EngineerExperience complexityLevel;
            DateTime createdAt;
            DateTime? baseLineStartDate = null, startDate = null, forcastDate = null, deadlineDate = null, completeDate = null;
            List<BO.TaskInList>? tasksInList = new List<TaskInList> { };
            MilestoneInTask? mileStone = new MilestoneInTask();
            bool isMilestone;
            EngineerInTask? engineerInTask = null;

            Console.WriteLine("choose: 1-create, 2-read, 3-read all, 4-update, 5-delete,  6-reset, 0-exit");
            int crudChoice = int.Parse(Console.ReadLine()!);
            int id;
            switch (((CRUD)crudChoice))
            {
                case CRUD.BREAK:
                    break;
                case CRUD.CREATE:
                    try
                    {
                        Console.WriteLine("Enter description, alias, engineer ID, complexity Level, Date it was cerated at");
                        description = Console.ReadLine()!;
                        alias = Console.ReadLine()!;
                        engineerID = int.Parse(Console.ReadLine()!);
                        complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
                        createdAt = DateTime.Parse(Console.ReadLine()!);

                        Console.WriteLine("Enter base line start date, start date, forcast date, complete date");
                        baseLineStartDate = DateTime.Parse(Console.ReadLine() ?? "");
                        startDate = DateTime.Parse(Console.ReadLine()!);
                        forcastDate = DateTime.Parse(Console.ReadLine() ?? "");
                        deadlineDate = DateTime.Parse(Console.ReadLine() ?? "");
                        completeDate = DateTime.Parse(Console.ReadLine() ?? "");

                        Console.WriteLine("Enter deliverables, ramarks");
                        deliverables = Console.ReadLine() ?? "";
                        remarks = Console.ReadLine() ?? "";

                        //is a milestone
                        Console.WriteLine("enter y if it is a milestone otherwise press any key");
                        string isMilestoneYN = Console.ReadLine()!;
                        isMilestone = isMilestoneYN == "y" ? true : false;
                        if (isMilestoneYN == "y")
                        {
                            Console.WriteLine("Enter id of milestone");
                            int milestoneId = int.Parse(Console.ReadLine()!);
                            mileStone = new MilestoneInTask() { Id = milestoneId, Alias = s_bl.milestone.Read(milestoneId).Alias };

                        }

                        //dependencies this task is dependent on
                        Console.WriteLine("Enter dependency id");
                        dependencyId = int.Parse(Console.ReadLine() ?? "-1");
                        while (dependencyId > 0)
                        {
                            BO.Task task = s_bl.task.Read(dependencyId);
                            tasksInList.Add(new TaskInList()
                            {
                                Id = task.Id,
                                Alias = task.Alias,
                                Description = task.Description,
                                Status = task.Status
                            });
                            Console.WriteLine("enter another task, your task is dependent on it");

                        }

                        //engineer of this task
                        if (engineerID > 0)
                        {
                            engineerInTask = new BO.EngineerInTask()
                            {
                                Id = engineerID,
                                Name = s_bl.Engineer.Read(engineerID)!.Name
                            };
                        }

                        //the task that was build now
                        int newID = s_bl!.task.Create(new BO.Task
                        {
                            Id = 0,
                            Description = description,
                            Alias = alias,
                            CreatedAtDate = createdAt,
                            Status = (Status)0,//every task status starts from the beggining
                            DependenciesList = tasksInList,
                            Milestone = mileStone,
                            BaselineStartDate = baseLineStartDate,
                            StartDate = startDate,
                            ForecastDate = forcastDate,
                            DeadlineDate = deadlineDate,
                            CompleteDate = completeDate,
                            Deliverables = deliverables,
                            Remarks = remarks,
                            Engineer = engineerInTask,
                            ComplexityLevel = complexityLevel
                        });
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
                        Console.WriteLine(s_bl!.task.Read(id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case CRUD.READALL:
                    try
                    {
                        foreach (BO.Task? task in s_bl!.task.ReadAll())
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
                    try
                    {
                        Console.WriteLine("Enter description, alias, engineer ID, complexity Level, Date it was cerated at");
                        description = Console.ReadLine()!;
                        alias = Console.ReadLine()!;
                        engineerID = int.Parse(Console.ReadLine()!);
                        complexityLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), Console.ReadLine()!);
                        createdAt = DateTime.Parse(Console.ReadLine()!);

                        Console.WriteLine("Enter base line start date, start date, forcast date, complete date");
                        baseLineStartDate = DateTime.Parse(Console.ReadLine() ?? "");
                        startDate = DateTime.Parse(Console.ReadLine()!);
                        forcastDate = DateTime.Parse(Console.ReadLine() ?? "");
                        deadlineDate = DateTime.Parse(Console.ReadLine() ?? "");
                        completeDate = DateTime.Parse(Console.ReadLine() ?? "");

                        Console.WriteLine("Enter deliverables, ramarks");
                        deliverables = Console.ReadLine() ?? "";
                        remarks = Console.ReadLine() ?? "";

                        //is a milestone
                        Console.WriteLine("enter y if it is a milestone otherwise press any key");
                        string isMilestoneYN = Console.ReadLine()!;
                        isMilestone = isMilestoneYN == "y" ? true : false;
                        if (isMilestoneYN == "y")
                        {
                            Console.WriteLine("Enter id of milestone");
                            int milestoneId = int.Parse(Console.ReadLine()!);
                            mileStone = new MilestoneInTask() { Id = milestoneId, Alias = s_bl.milestone.Read(milestoneId).Alias };

                        }

                        //dependencies this task is dependent on
                        Console.WriteLine("Enter dependency id");
                        dependencyId = int.Parse(Console.ReadLine() ?? "-1");
                        while (dependencyId > 0)
                        {
                            BO.Task task = s_bl.task.Read(dependencyId);
                            tasksInList.Add(new TaskInList()
                            {
                                Id = task.Id,
                                Alias = task.Alias,
                                Description = task.Description,
                                Status = task.Status
                            });
                            Console.WriteLine("enter another task, your task is dependent on it");

                        }

                        //engineer of this task
                        if (engineerID > 0)
                        {
                            engineerInTask = new BO.EngineerInTask()
                            {
                                Id = engineerID,
                                Name = s_bl.Engineer.Read(engineerID)!.Name
                            };
                        }

                        //the task that was build now
                        s_bl!.task.Update(new BO.Task
                        {
                            Id = 0,
                            Description = description,
                            Alias = alias,
                            CreatedAtDate = createdAt,
                            Status = (Status)0,//every task status starts from the beggining
                            DependenciesList = tasksInList,
                            Milestone = mileStone,
                            BaselineStartDate = baseLineStartDate,
                            StartDate = startDate,
                            ForecastDate = forcastDate,
                            DeadlineDate = deadlineDate,
                            CompleteDate = completeDate,
                            Deliverables = deliverables,
                            Remarks = remarks,
                            Engineer = engineerInTask,
                            ComplexityLevel = complexityLevel
                        });
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
                        s_bl!.task.Delete(id);
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


        static void Main(string[] args)
        {
            int entityChoice;
            Console.WriteLine("choose: 0-exit 1-milestone, 2-engineer, 3-task");
            entityChoice = int.Parse(Console.ReadLine()!);

            while (entityChoice != 0)
            {
                switch ((ENTITY)entityChoice) // Explicitly cast the integer to ENTITY enum
                {

                    case ENTITY.MILESTONE:
                        MilestoneFunction();
                        break;

                    case ENTITY.ENGINEER:
                        EngineerFunction();
                        break;

                    case ENTITY.TASK:
                        TaskFunction();
                        break;

                    default:
                        break;
                }
                Console.WriteLine("choose: 0-exit, 1-milestone, 2-engineer, 3-task");
                entityChoice = int.Parse(Console.ReadLine()!);
            }
        }
    }
}