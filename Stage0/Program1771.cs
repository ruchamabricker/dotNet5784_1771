//The project was done by only one student (because the number of students in the group is odd)

using System;

namespace Targil0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome1771();
            Welcome1771_2();
            Console.ReadKey();
        }

        static partial void Welcome1771_2();

        private static void Welcome1771()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}