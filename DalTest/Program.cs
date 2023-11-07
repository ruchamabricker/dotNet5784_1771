// See https://aka.ms/new-console-template for more information
using System;
using Dal;
using DalApi;
using DalList;
namespace Program // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IDependency? s_dalCourse = new DependencyImplementation(); //stage 1
        private static IEnigneer? s_dalLinks = new EnigneerImplementation(); //stage 1
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}

