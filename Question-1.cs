using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            //simple program display your name
            Console.WriteLine("Hello, What is your name?\n");
            string Name = Console.ReadLine();
            Console.WriteLine(" \n Hello " + Name + ". Welcome to this world!!!!");
        }
    }
}
