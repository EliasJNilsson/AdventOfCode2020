using System;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string streamText = System.IO.File.ReadAllText(@"..\..\..\input.txt");

            var splitText = streamText.Split("\n");
            
            //Part 1
            int treeCount = Run(3, splitText);

            Console.WriteLine($"The count of how many trees hit in part 1 (3r, 1d) is: {treeCount}");


            //Part 2
            //I could do it the more "effiecent" way and have all the runs at once, but for simplicity of my brain, i will just alter each slightly for each outcome.

            var treeCount1R = RunNew(splitText, 1, 1);
            Console.WriteLine($"The count of how many trees hit in part 2 (1r, 1d) is: {treeCount1R}");

            var treeCount5R = RunNew(splitText, 5, 1);
            Console.WriteLine($"The count of how many trees hit in part 2 (5r, 1d) is: {treeCount5R}");

            var treeCount7R = RunNew(splitText, 7, 1);
            Console.WriteLine($"The count of how many trees hit in part 2 (7r, 1d) is: {treeCount7R}");


            //Special
            var treeCount1RSpecial = RunNew(splitText, 1, 2);
            Console.WriteLine($"The count of how many trees hit in part 2 (1r, 2d) is: {treeCount1RSpecial}");


            //Had to cast one of the int as a double to get a double outcome, so we don't overflow.
            var result = (double)treeCount * treeCount1R * treeCount5R * treeCount7R * treeCount1RSpecial;

            Console.WriteLine($"The full multiplier is: {result}");
        }

        static int Run(int right, string[] input)
        {
            int col = input[0].Length;
            var treeCount = 0;
            int placement = 0;
            bool isFirstRow = true;

            foreach (var row in input)
            {
                if(isFirstRow)
                {
                    isFirstRow = false;
                    continue;
                }

                placement += right;
                if (placement >= col)
                {
                    placement -= col;
                }

                if (IsThereTree(placement, row))
                {
                    treeCount++;
                }

            }

            return treeCount;
        }


        static int RunNew(string[] input, int right, int down)
        {
            int colMax = input[0].Length;
            int rowMax = input.Length;

            var treeCount = 0;

            var row = 0;
            var col = 0;

            while(row + down < rowMax)
            {
                row += down;

                if(col + right >= colMax)
                {
                    col = col + right - colMax;
                }
                else
                {
                    col += right;
                }

                if(input[row][col] == '#')
                {
                    treeCount++;
                }

            }

            return treeCount;
        }

        static bool IsThereTree(int col, string row)
        {
            if (row[col] == '#')
            {
                //Console.WriteLine($"hit tree: {row[col]}");
                return true;
            }

            //Console.WriteLine($"clear snow: {row[col]}");
            return false;
        }
    }
}
