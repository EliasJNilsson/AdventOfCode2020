using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            var listOfJolts = new List<int>(input.Length);
            foreach (var stg in input)
            {
                listOfJolts.Add(int.Parse(stg));
            }

            //part 1
            var output1 = Part1(listOfJolts);

            Console.WriteLine($"The 1-jumps found was {output1.JumpOne}, the 3-jumps found was {output1.JumpThree}, the multiplied of them are {(double)output1.JumpOne * output1.JumpThree}");


            //Part 2
            List<long> inputs = (Array.ConvertAll(input, s => Int64.Parse(s))).ToList();
            inputs.Sort();

            inputs.Add(inputs.Last() + 3);
            inputs.Insert(0, 0);

            var part2Output = Part2(inputs.ToArray());
            Console.WriteLine($"Total number of mixes are: {part2Output}");
        }

        public static Part1Output Part1(List<int> input)
        {
            var returnObject = new Part1Output();
            input.Sort();

            if(input[0] == 1)
            {
                returnObject.JumpOne++;
            }
            else if(input[0] == 2)
            {
                returnObject.JumpTwo++;
            }
            else if(input[0] == 3)
            {
                returnObject.JumpThree++;
            }

            foreach(var current in input)
            {
                var indexOfCurrent = input.IndexOf(current);
                if(input.Count == indexOfCurrent + 1)
                {
                    continue;
                }
                var currentPlusOne = input[indexOfCurrent + 1];

                switch(currentPlusOne - current)
                {
                    case 1:
                        {
                            returnObject.JumpOne++;
                            break;
                        }
                    case 2:
                        {
                            returnObject.JumpTwo++;
                            break;
                        }
                    case 3:
                        {
                            returnObject.JumpThree++;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            
            //the default for the ask.
            returnObject.JumpThree++;

            return returnObject;
        }


        static Dictionary<long, long> resultSet = new Dictionary<long, long>();
        static int counter = 0;
        private static long Part2(long[] inputs)
        {
            counter++;
            if (resultSet.ContainsKey(inputs.Length))
            {
                return resultSet[inputs.Length];
            }

            if (inputs.Length == 1)
            {
                return 1;
            }

            long total = 0;
            long temp = inputs[0];
            for (int i = 1; i < inputs.Length; i++)
            {
                if (inputs[i] - temp <= 3)
                {
                    total += Part2(inputs[i..]);
                }
                else
                {
                    break;
                }
            }

            resultSet.Add(inputs.Length, total);

            return total;
        }

    } 

    public class Part1Output
    {
        public int JumpOne { get; set; }
        public int JumpTwo { get; set; }
        public int JumpThree { get; set; }
    }

}