using System;
using System.Collections.Generic;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            var passage = new Dictionary<int, bool>();

            int line = 0;
            int accumulator = 0;

            while(!passage.ContainsKey(line))
            {
                passage.Add(line, true);

                var currentLine = input[line];

                switch (currentLine.Substring(0,3))
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

            Console.WriteLine($"We got out! doubled up on line {line}, and the accumulator is at {accumulator}");
        }
    }
}
