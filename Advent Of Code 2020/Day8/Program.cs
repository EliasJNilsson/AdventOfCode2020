using System;
using System.Collections.Generic;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            Part1(input);

            Part2(input);
        }

        public static void Part2(string[] input)
        {
            bool notFound = true;
            int skip = 0;
            int finalCount = 0;

            while (notFound)
            {
                finalCount = Part2Program(input, skip);
                if (finalCount != -1)
                {
                    notFound = false;
                }

                if (skip >= input.Length)
                {
                    Console.WriteLine("Something broke....REAlly.");
                }

                skip++;
            }

            Console.WriteLine($"We got out! Part 2, accumulator is at {finalCount}");
        }

        public static int Part2Program(string[] input, int skip)
        {
            var passage = new Dictionary<int, bool>();

            int line = 0;
            int accumulator = 0;
            int countedSkip = 0;

            while (!passage.ContainsKey(line))
            {
                passage.Add(line, true);

                if(line >= input.Length)
                {
                    return accumulator;
                }

                var currentLine = input[line];

                switch (currentLine.Substring(0, 3))
                {
                    case "acc":
                        {
                            var plusOrMinus = int.Parse(currentLine.Substring(5));

                            if (currentLine[4] == '+')
                            {
                                accumulator += plusOrMinus;
                            }
                            else
                            {
                                accumulator -= plusOrMinus;
                            }
                            line++;
                            break;
                        }
                    case "nop":
                        {
                            if (skip == countedSkip)
                            {
                                countedSkip++;
                                var plusOrMinus = int.Parse(currentLine.Substring(5));

                                if (currentLine[4] == '+')
                                {
                                    line += plusOrMinus;
                                }
                                else
                                {
                                    line -= plusOrMinus;
                                }
                                break;
                            }
                            countedSkip++;
                            line++;
                            break;
                        }
                    case "jmp":
                        {
                            if (skip == countedSkip)
                            {
                                countedSkip++;
                                line++;
                                break;
                            }

                            var plusOrMinus = int.Parse(currentLine.Substring(5));

                            if (currentLine[4] == '+')
                            {
                                line += plusOrMinus;
                            }
                            else
                            {
                                line -= plusOrMinus;
                            }
                            countedSkip++;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            return -1;
        }

        public static void Part1(string[] input)
        {
            var passage = new Dictionary<int, bool>();

            int line = 0;
            int accumulator = 0;

            while (!passage.ContainsKey(line))
            {
                passage.Add(line, true);

                var currentLine = input[line];

                switch (currentLine.Substring(0, 3))
                {
                    case "acc":
                        {
                            var plusOrMinus = int.Parse(currentLine.Substring(5));

                            if (currentLine[4] == '+')
                            {
                                accumulator += plusOrMinus;
                            }
                            else
                            {
                                accumulator -= plusOrMinus;
                            }
                            line++;
                            break;
                        }
                    case "nop":
                        {
                            line++;
                            break;
                        }
                    case "jmp":
                        {
                            var plusOrMinus = int.Parse(currentLine.Substring(5));


                            if (currentLine[4] == '+')
                            {
                                line += plusOrMinus;
                            }
                            else
                            {
                                line -= plusOrMinus;
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }

            Console.WriteLine($"We got out! Part 1, doubled up on line {line}, and the accumulator is at {accumulator}");
        }
    }
}
