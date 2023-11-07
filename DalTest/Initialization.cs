namespace DalTest;
using DalApi;
using DO;
using System;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Xml.Linq;

public static class Initialization
{
    private static ITask? s_dalTask;
    private static IEnigneer? s_dalEnigneer;
    private static IDependency? s_dalDependency;

    private static readonly Random s_rand = new();

    private static void createTasks()
    {
        for (int i = 0; i < 100; i++)
        {

        }
    }



    private static void createEnigneers()
    {
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
            string name = arrNames[s_rand.Next(0, 39)];
            string email = "e" + id + "@gmail.com";
            EngineerExperience engineerExperience = (EngineerExperience)(s_rand.Next(0, 3));
            double cost = s_rand.Next(120, 590);

            s_dalEnigneer.Create(new Enigneer(id, name, email, engineerExperience, cost));
        }

    }

    private static void createDependencys()
    {
        for (int i = 0; i < 250; i++)
        {
            s_dalDependency.Create(new Dependency(0, s_rand.Next(0, 100), s_rand.Next(0, 100)));
        }
    }

    public static void DO()
    {
        //ITask? dalTask;
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
    
    createDependencys();
        createEnigneers();
        createTasks();
    } 
}
