using System;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string streamText = System.IO.File.ReadAllText(@"..\..\..\input.txt");

            System.Console.WriteLine($"Content of input.txt = {streamText}");
        }
    }
}
