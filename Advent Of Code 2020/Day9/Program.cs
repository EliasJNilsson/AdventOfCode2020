using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine($"Found the odd one out: {FoundNumber}");

            var finalNumber = Part2(FoundNumber, doubleList);
            Console.WriteLine($"Found the Sum, first and last add to {finalNumber}");
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

        public static double Part2(double foundNumber, List<double> fullList)
        {
            var startIndex = 0;
            var endIndex = 0;
            double sum = 0;

            foreach(var num in fullList)
            {
                sum += num;
            }

            var fullArr = fullList.ToArray();

            while ((sum = fullArr[startIndex..endIndex].Sum()) != foundNumber)
            {
                if (sum > foundNumber) startIndex++;
                if (sum < foundNumber) endIndex++;
            }

            var range = endIndex - startIndex;

            var endList = fullList.GetRange(startIndex, range);

            endList.Sort();

            return endList[0] + endList.Last();
        }
    }
}
