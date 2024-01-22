namespace DalTest;
using DalApi;
using DO;
using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Xml.Linq;

/// <summary>
/// The function initializes all data
/// </summary>
public static class Initialization
{
    private static IDal? s_dal; //stage 2

    private static readonly Random s_rand = new();


    /// <summary>
    /// the function creates 10 tasks
    /// </summary>
    private static void createTasks()
    {
        s_dal!.Task.Reset();
        string[] taskAlias = new string[10]
{
            "Fix a bug",
            "Refactor code",
            "Implement a feature",
            "Optimize performance",
            "Write unit tests",
            "Document the code",
            "Review pull requests",
            "Deploy the application",
            "Troubleshoot issues",
            "Create technical specifications"
};

        string[] taskDescriptions = new string[10]
        {
            "Identify and resolve a software bug",
            "Restructure and improve existing code",
            "Add a new functionality to the software",
            "Enhance the speed or efficiency of the code",
            "Create automated tests to verify software behavior",
            "Write documentation to explain the code",
            "Review code changes submitted by other developers",
            "Deploy the software to a production environment",
            "Investigate and fix issues reported by users",
            "Create detailed plans and requirements for development tasks"
        };

        for (int i = 0; i < 10; i++)
        {
            int randNumber = s_rand.Next(0, 10);

            string _description = taskDescriptions[randNumber];
            string _alias = taskAlias[randNumber];
            //chooses a random engineer id, that he should do the task
            var engineers = s_dal!.Engineer!.ReadAll().ToList();
            int randomIndex = s_rand.Next(0, engineers.Count);
            int _engineerid = engineers[randomIndex]?.Id ?? 0;

            EngineerExperience _complexityLevel = (EngineerExperience)(s_rand.Next(0, 3));
            DateTime _ceratedAt = new DateTime(s_rand.Next(2017, 2023), s_rand.Next(1, 13), s_rand.Next(1, 29));
            s_dal!.Task.Create(new Task(0, _description, _alias, _ceratedAt, false, _engineerid, _complexityLevel));
        }

    }


    /// <summary>
    /// fills the list of the engineer
    /// </summary>
    private static void createEngineers()
    {
        s_dal!.Engineer.Reset();
        string[] arrNames = {
  "Ethan Cohen",
  "Levi Goldberg",
  "Isaac Levy",
  "Aaron Rosenberg",
  "Jacob Katz",
  "Samuel Schwartz",
  "Noah Friedman",
  "Elijah Stein",
  "Benjamin Weiss",
  "Daniel Silverman",
  "David Cohen",
  "Joseph Schwartz",
  "Joshua Abramowitz",
  "Samuel Levy",
  "Jacob Goldstein",
  "Ethan Rosenberg",
  "Levi Cohen",
  "Isaac Katz",
  "Aaron Friedman",
  "Noah Stein",
  "Elijah Weiss",
  "Benjamin Silverman",
  "Daniel Cohen",
  "David Schwartz",
  "Joseph Abramowitz",
  "Joshua Levy",
  "Samuel Goldstein",
  "Jacob Rosenberg",
  "Ethan Cohen",
  "Levi Goldberg",
  "Isaac Levy",
  "Aaron Rosenberg",
  "Jacob Katz",
  "Samuel Schwartz",
  "Noah Friedman",
  "Elijah Stein",
  "Benjamin Weiss",
  "Daniel Silverman",
  "David Cohen",
  "Joseph Schwartz",
  "Joshua Abramowitz",
  "Samuel Levy"
};

        for (int i = 0; i < 40; i++)
        {
            int id = s_rand.Next(200000000, 400000000);
            string name = arrNames[s_rand.Next(0, 40)];
            string email = "e" + id + "@gmail.com";
            EngineerExperience engineerExperience = (EngineerExperience)(s_rand.Next(0, 3));
            double cost = s_rand.Next(120, 590);

            //s_dalEngineer!.Create(new Engineer(id, name, email, engineerExperience, cost));
            s_dal!.Engineer.Create(new Engineer(id, name, email, engineerExperience, cost));

        }

    }
    /// <summary>
    /// fills the list of the dependencies
    /// </summary>
    private static void createdependencies()
    {
        s_dal!.Dependency.Reset();
        for (int i = 0; i < 25; i++)
        {
            //s_dalDependency!.Create(new Dependency(0, s_rand.Next(0, 10), s_rand.Next(0, 10)));
            s_dal!.Dependency.Create(new Dependency(0, s_rand.Next(0, 10), s_rand.Next(0, 10)));
        }
    }
    /// <summary>
    /// the funtion Calls the private methods we prepared and initializes the lists.
    /// </summary>
    /// <param name="dalTask">checks the tasks arent null</param>
    /// <param name="dalDependency">checks the engineers arent null</param>
    /// <param name="dalEngineer">checks the dependencies arent null</param>
    /// <exception cref="NullReferenceException"></exception>
    /// 


    //public static void DO(ITask? dalTask, IDependency? dalDependency, IEngineer? dalEngineer)
    //  public static void Do(IDal dal) //stage 2
    public static void Do() //stage 4

    {
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");

        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        s_dal = Factory.Get; //stage 4

        createEngineers();
        createTasks();
        createdependencies();
    }
}
