using System;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            Part1(input[0], input[1]);

            Part2(input);
        }

        static public void Part1(string input1, string input2)
        {
            var timestamp = int.Parse(input1);

            var listOfRoutes = input2.Split(',').ToList();
            _ = listOfRoutes.RemoveAll(s => s.Contains('x'));

            int smallestDif = int.MaxValue;
            int smallestBus = int.MaxValue;

            foreach(var bus in listOfRoutes)
            {
                var busNum = int.Parse(bus);
                int tempTimestamp = timestamp - (timestamp % busNum) + busNum;

                var dif = tempTimestamp - timestamp;

                if(dif < smallestDif)
                {
                    smallestDif = dif;
                    smallestBus = busNum;
                }
            }
            
            //Results
            Console.WriteLine($"The bus {smallestBus} got to {timestamp}, at timestamp {smallestDif + timestamp}, only {smallestDif} after ");
            Console.WriteLine($"Multiplication of bus times time after is {smallestDif * smallestBus}");

            return;
        }

        static public void Part2(string[] input)
        {



            //Results
            //Console.WriteLine($"The Manhattan distance is {Math.Abs(eastWest) + Math.Abs(northSouth)}");

            return;
        }
    }
}
