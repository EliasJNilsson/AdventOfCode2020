using System;
using System.Collections.Generic;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            var doubleList = new List<double>(input.Length);

            foreach(var inputString in input)
            {
                doubleList.Add(double.Parse(inputString));
            }

            var FoundNumber = Part1(doubleList);
        }

        public static double Part1(List<double> input)
        {
            var subList = input.GetRange(0, 25);
            var checkList = input.GetRange(25, input.Count - 25);

            foreach(var check in checkList)
            {
                if(SumOf2InSubList(check, subList))
                {
                    subList.Remove(subList[0]);
                    subList.Add(check);
                }
                else
                {
                    Console.WriteLine($"Found the odd one out: {check}");
                    return check;
                }
            }

            return -1;
        }

        public static bool SumOf2InSubList(double val, List<double> subList)
        {
            foreach(var integer in subList)
            {
                if(subList.Contains(val - integer))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
