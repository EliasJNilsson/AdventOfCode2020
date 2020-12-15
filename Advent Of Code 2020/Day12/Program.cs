using System;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            Part1(input);

        }

        static public void Part1(string[] input)
        {
            var direction = 'E';
            //east is neg, west is pos
            var eastWest = 0;
            //north is neg, south is pos
            var northSouth = 0;

            foreach(var str in input)
            {
                if(str[0] == 'R')
                {
                    direction = TurnRight(direction, int.Parse(str.Substring(1)));
                    continue;
                }
                if(str[0] == 'L')
                {
                    direction = TurnLeft(direction, int.Parse(str.Substring(1)));
                    continue;
                }

                // Move in directions.
                if (str[0] == 'N' || (str[0] == 'F' && direction == 'N'))
                {
                    northSouth -= int.Parse(str.Substring(1));
                    continue;
                }
                if (str[0] == 'S' || (str[0] == 'F' && direction == 'S'))
                {
                    northSouth += int.Parse(str.Substring(1));
                    continue;
                }
                if (str[0] == 'E' || (str[0] == 'F' && direction == 'E'))
                {
                    eastWest -= int.Parse(str.Substring(1));
                    continue;
                }
                if (str[0] == 'W' || (str[0] == 'F' && direction == 'W'))
                {
                    eastWest += int.Parse(str.Substring(1));
                    continue;
                }

            }


            //Results
            Console.WriteLine($"The final Direction is {direction}, the eastWest is {eastWest}, the northSouth is {northSouth}");

            Console.WriteLine($"The Manhattan distance is {Math.Abs(eastWest) + Math.Abs(northSouth)}");

            return;
        }

        static public char TurnRight(char direction, int inputDegree)
        {
            switch (direction)
            {
                case 'N':
                    {
                        if(inputDegree == 90)
                        {
                            return 'E';
                        }
                        else if(inputDegree == 180)
                        {
                            return 'S';
                        }
                        else if(inputDegree == 270)
                        {
                            return 'W';
                        }
                        else
                        {
                            return 'N';
                        }
                    }
                case 'E':
                    {
                        if (inputDegree == 90)
                        {
                            return 'S';
                        }
                        else if (inputDegree == 180)
                        {
                            return 'W';
                        }
                        else if (inputDegree == 270)
                        {
                            return 'N';
                        }
                        else
                        {
                            return 'E';
                        }
                    }
                case 'S':
                    {
                        if (inputDegree == 90)
                        {
                            return 'W';
                        }
                        else if (inputDegree == 180)
                        {
                            return 'N';
                        }
                        else if (inputDegree == 270)
                        {
                            return 'E';
                        }
                        else
                        {
                            return 'S';
                        }
                    }
                case 'W':
                default:
                    {
                        if (inputDegree == 90)
                        {
                            return 'N';
                        }
                        else if (inputDegree == 180)
                        {
                            return 'E';
                        }
                        else if (inputDegree == 270)
                        {
                            return 'S';
                        }
                        else
                        {
                            return 'W';
                        }
                    }
            }
        }

        static public char TurnLeft(char direction, int inputDegree)
        {
            switch (direction)
            {
                case 'N':
                    {
                        if (inputDegree == 90)
                        {
                            return 'W';
                        }
                        else if (inputDegree == 180)
                        {
                            return 'S';
                        }
                        else if (inputDegree == 270)
                        {
                            return 'E';
                        }
                        else
                        {
                            return 'N';
                        }
                    }
                case 'W':
                    {
                        if (inputDegree == 90)
                        {
                            return 'S';
                        }
                        else if (inputDegree == 180)
                        {
                            return 'E';
                        }
                        else if (inputDegree == 270)
                        {
                            return 'N';
                        }
                        else
                        {
                            return 'W';
                        }
                    }
                case 'S':
                    {
                        if (inputDegree == 90)
                        {
                            return 'E';
                        }
                        else if (inputDegree == 180)
                        {
                            return 'N';
                        }
                        else if (inputDegree == 270)
                        {
                            return 'W';
                        }
                        else
                        {
                            return 'S';
                        }
                    }
                case 'E':
                default:
                    {
                        if (inputDegree == 90)
                        {
                            return 'N';
                        }
                        else if (inputDegree == 180)
                        {
                            return 'W';
                        }
                        else if (inputDegree == 270)
                        {
                            return 'S';
                        }
                        else
                        {
                            return 'E';
                        }
                    }
            }
        }
    }
}
