using System;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            Part1(input);

            Part2(input);

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

        static public void Part2(string[] input)
        {
            var cardinalDir1 = 'E';
            var cardinalDir2 = 'N';

            var cardinalUnit1 = 10;
            var cardinalUnit2 = 1;


            var TotalCardinalNS = 0;
            var TotalCardinalEW = 0;

            foreach (var str in input)
            {
                if (str[0] == 'R')
                {
                    cardinalDir1 = TurnRight(cardinalDir1, int.Parse(str.Substring(1)));
                    cardinalDir2 = TurnRight(cardinalDir2, int.Parse(str.Substring(1)));
                    continue;
                }
                if (str[0] == 'L')
                {
                    cardinalDir1 = TurnLeft(cardinalDir1, int.Parse(str.Substring(1)));
                    cardinalDir2 = TurnLeft(cardinalDir2, int.Parse(str.Substring(1)));
                    continue;
                }

                // Move in directions.
                if (str[0] == 'N')
                {
                    if(cardinalDir1 == 'N')
                    {
                        cardinalUnit1 += int.Parse(str.Substring(1));
                    }
                    else if( cardinalDir1 == 'S')
                    {
                        cardinalUnit1 -= int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'N')
                    {
                        cardinalUnit2 += int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'S')
                    {
                        cardinalUnit2 -= int.Parse(str.Substring(1));
                    }
                    continue;
                }
                if (str[0] == 'S')
                {
                    if (cardinalDir1 == 'N')
                    {
                        cardinalUnit1 -= int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir1 == 'S')
                    {
                        cardinalUnit1 += int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'N')
                    {
                        cardinalUnit2 -= int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'S')
                    {
                        cardinalUnit2 += int.Parse(str.Substring(1));
                    }
                    continue;
                }
                if (str[0] == 'E')
                {
                    if (cardinalDir1 == 'E')
                    {
                        cardinalUnit1 += int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir1 == 'W')
                    {
                        cardinalUnit1 -= int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'E')
                    {
                        cardinalUnit2 += int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'W')
                    {
                        cardinalUnit2 -= int.Parse(str.Substring(1));
                    }
                    continue;
                }
                if (str[0] == 'W')
                {
                    if (cardinalDir1 == 'E')
                    {
                        cardinalUnit1 -= int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir1 == 'W')
                    {
                        cardinalUnit1 += int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'E')
                    {
                        cardinalUnit2 -= int.Parse(str.Substring(1));
                    }
                    else if (cardinalDir2 == 'W')
                    {
                        cardinalUnit2 += int.Parse(str.Substring(1));
                    }
                    continue;
                }

                if(str[0] == 'F')
                {
                    int forwardCount = int.Parse(str.Substring(1));

                    //MOVE FORWARD
                    if(cardinalDir1 == 'N' || cardinalDir1 == 'S')
                    {
                        //cardinal dir 1 == north or south

                        if(cardinalDir1 == 'N')
                        {
                            TotalCardinalNS += cardinalUnit1 * forwardCount;
                        }
                        else
                        {
                            TotalCardinalNS -= cardinalUnit1 * forwardCount;
                        }
                    }
                    else
                    {
                        //Cardinal dir 1 == east or west
                        if (cardinalDir1 == 'E')
                        {
                            TotalCardinalEW += cardinalUnit1 * forwardCount;
                        }
                        else
                        {
                            TotalCardinalEW -= cardinalUnit1 * forwardCount;
                        }
                    }

                    if (cardinalDir2 == 'N' || cardinalDir2 == 'S')
                    {
                        //cardinal dir 2 == north or south

                        if (cardinalDir2 == 'N')
                        {
                            TotalCardinalNS += cardinalUnit2 * forwardCount;
                        }
                        else
                        {
                            TotalCardinalNS -= cardinalUnit2 * forwardCount;
                        }
                    }
                    else
                    {
                        //Cardinal dir 2 == east or west
                        if (cardinalDir2 == 'E')
                        {
                            TotalCardinalEW += cardinalUnit2 * forwardCount;
                        }
                        else
                        {
                            TotalCardinalEW -= cardinalUnit2 * forwardCount;
                        }
                    }

                }

            }
            Console.WriteLine("Look here");

            //Results
            //Console.WriteLine($"The final Direction is {direction}, the eastWest is {eastWest}, the northSouth is {northSouth}");

            Console.WriteLine($"The Manhattan distance is {Math.Abs(TotalCardinalNS) + Math.Abs(TotalCardinalEW)}");

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
