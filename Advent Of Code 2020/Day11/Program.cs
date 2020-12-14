using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadInput();

            RunPart2();
        }

        private static readonly char Floor = '.';
        private static readonly char EmptySeat = 'L';
        private static readonly char OccupiedSeat = '#';

        static private List<List<char>> _input;

        static public void ReadInput()
        {
            _input = System.IO.File.ReadLines(@"..\..\..\input.txt")
                .Select(s => s.ToList())
                .ToList();
        }

        public void RunPart1()
        {
            var previous = _input;
            var running = true;

            while (running)
            {
                var (run, change) = Simulate(previous, (input, x, y) =>
                {
                    var oldPos = input[y][x];

                    if (oldPos == EmptySeat && GetOccupiedAdjacentSeats(input, x, y) == 0)
                    {
                        return (OccupiedSeat, true);
                    }

                    if (oldPos == OccupiedSeat && GetOccupiedAdjacentSeats(input, x, y) >= 4)
                    {
                        return (EmptySeat, true);
                    }

                    return (oldPos, false);
                });
                previous = run;
                running = change;
            }

            var result = CountOccupiedSeats(previous);
            Console.WriteLine($"Result: {result}");
        }

        static public void RunPart2()
        {
            var previous = _input;
            var running = true;

            while (running)
            {
                var (run, change) = Simulate(previous, (input, x, y) =>
                {
                    var oldPos = input[y][x];

                    if (oldPos == EmptySeat && GetOccupiedVisibleSeats(input, x, y) == 0)
                    {
                        return (OccupiedSeat, true);
                    }

                    if (oldPos == OccupiedSeat && GetOccupiedVisibleSeats(input, x, y) >= 5)
                    {
                        return (EmptySeat, true);
                    }

                    return (oldPos, false);
                });
                previous = run;
                running = change;
            }

            var result = CountOccupiedSeats(previous);
            Console.WriteLine($"Result: {result}");
        }

        public static (List<List<char>>, bool) Simulate(List<List<char>> input, RunRule rule)
        {
            var output = new List<List<char>>();
            var change = false;

            for (var y = 0; y < input.Count; y++)
            {
                var oldRow = input[y];
                var newRow = new List<char>();

                for (var x = 0; x < oldRow.Count; x++)
                {
                    var (newPos, chg) = rule(input, x, y);
                    if (chg)
                    {
                        change = true;
                    }

                    newRow.Add(newPos);
                }

                output.Add(newRow);
            }

            return (output, change);
        }

        public static int CountOccupiedSeats(List<List<char>> input) =>
            input.SelectMany(y =>
            {
                return y.Select(r => r);
            }).Count(seat => seat == OccupiedSeat);

        public static int GetOccupiedAdjacentSeats(List<List<char>> input, int x, int y) =>
            new List<int> { -1, 0, +1 }.SelectMany(dx =>
            {
                return new List<int> { -1, 0, +1 }.Select(dy =>
                {
                    // For the purposes of counting, pretend its floor
                    if (dx == 0 && dy == 0) return Floor;

                    var posY = y + dy;
                    var posX = x + dx;

                    if (posY < 0 || posY >= input.Count)
                        return Floor;
                    if (posX < 0 || posX >= input[posY].Count)
                        return Floor;

                    return input[posY][posX];
                }).Where(seat => seat != Floor);
            }).Count(seat => seat == OccupiedSeat);

        public static int GetOccupiedVisibleSeats(List<List<char>> input, int x, int y) =>
            new List<int> { -1, 0, +1 }.SelectMany(dx =>
            {
                return new List<int> { -1, 0, +1 }.Select(dy =>
                {
                    // For the purposes of counting, pretend its floor
                    if (dx == 0 && dy == 0) return Floor;

                    var posY = y + dy;
                    var posX = x + dx;

                    if (posY < 0 || posY >= input.Count)
                        return Floor;
                    if (posX < 0 || posX >= input[posY].Count)
                        return Floor;

                    while (input[posY][posX] == Floor)
                    {
                        posY += dy;
                        posX += dx;

                        if (posY < 0 || posY >= input.Count)
                            return Floor;
                        if (posX < 0 || posX >= input[posY].Count)
                            return Floor;
                    }

                    return input[posY][posX];
                }).Where(seat => seat != Floor);
            }).Count(seat => seat == OccupiedSeat);

        public delegate (char, bool) RunRule(List<List<char>> input, int x, int y);

        //public static int Part1(char[][] input)
        //{
        //    var boolChanges = false;

        //    do
        //    {
        //        //Check for changes
        //        var changes = CheckForNeededChanges(input);

        //        var changeBool = false;
        //        foreach(var row in changes)
        //        {
        //            if(row.Contains('L') || row.Contains('#'))
        //            {
        //                changeBool = true;
        //            }
        //        }

        //        //If changes: Make changes, else exit
        //        if(changeBool)
        //        {
        //            boolChanges = true;
        //            input = MakeChanges(input, changes);
        //        }
        //        else
        //        {
        //            boolChanges = false;
        //        }

        //    } while (boolChanges == true);

        //    var Part1Count = 0;
        //    //count seats?
        //    foreach(var line in input)
        //    {
        //        Part1Count += line.Count(c => c.Equals('#'));
        //    }

        //    return Part1Count;
        //}

        //public static char[][] CheckForNeededChanges(char[][] input)
        //{
        //    var output = new char[input.Length][];

        //    for(int row = 0; row < input.Length; row++)
        //    {
        //        output[row] = new char[input[row].Length];
        //        for(int col = 0; col < input[row].Length; col++)
        //        {
        //            if(input[row][col] == '.')
        //            {
        //                //floor, do not check or change.
        //                continue;
        //            }

        //            var count = 0;
        //            //Do the checks themselves.
        //            if(row == 0)
        //            {
        //                //first row
        //                if(col == 0)
        //                {
        //                    //look down, and look right, and downright
        //                    if(input[row + 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if(input[row][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if(input[row + 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }

        //                }
        //                else if(col == input[row].Length - 1)
        //                {
        //                    //look down, and left, and downleft
        //                    if (input[row + 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                }
        //                else 
        //                {
        //                    //look down, right, downright, left, downleft
        //                    if (input[row + 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                }
        //            }
        //            else if(row == input.Length - 1)
        //            {
        //                //last row
        //                if (col == 0)
        //                {
        //                    //look up, and look right, and upright
        //                    if (input[row - 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }

        //                }
        //                else if (col == input[row].Length - 1)
        //                {
        //                    //look up, and left, and upleft
        //                    if (input[row - 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                }
        //                else
        //                {
        //                    //look up, right, upright, left, upleft
        //                    if (input[row - 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //every other row
        //                if(col == 0)
        //                {
        //                    //look up, right, down, upright, downright
        //                    if (input[row - 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if(input[row + 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if(input[row + 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                }
        //                else if(col == input[row].Length - 1)
        //                {
        //                    //Look left, up, down, upleft, downleft
        //                    if (input[row - 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                }
        //                else
        //                {
        //                    //look everywhere.
        //                    if (input[row - 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col - 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row - 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                    if (input[row + 1][col + 1] == '#')
        //                    {
        //                        count++;
        //                    }
        //                }
        //            }

        //            var checkChar = input[row][col];
        //            if(count < 4)
        //            {
        //                if(checkChar == 'L')
        //                {
        //                    output[row][col] = '#';
        //                }
        //            }
        //            else if(count >= 4)
        //            {
        //                if(checkChar == '#')
        //                {
        //                    output[row][col] = 'L';
        //                }
        //            }
        //        }
        //    }

        //    return output;
        //}

        //public static char[][] MakeChanges(char[][] input, char[][] changes)
        //{
        //    var output = new char[input.Length][];
        //    for (int r = 0; r < input.Length; r++)
        //    {
        //        output[r] = new char[input[r].Length];
        //        for (int c = 0; c < input[r].Length; c++)
        //        {
        //            if (changes[r][c] == '\0')
        //            {
        //                output[r][c] = input[r][c];
        //            }
        //            else 
        //            {
        //                output[r][c] = changes[r][c];
        //            }
        //        }
        //    }

        //    return output;
        //}
    }
}
