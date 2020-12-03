using System;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string streamText = System.IO.File.ReadAllText(@"..\..\..\input.txt");

            var splitText = streamText.Split("\n");

            int treeCount = Run(3, splitText);

            Console.WriteLine($"The count of how many trees hit in part 1 (3r, 1d) is: {treeCount}");


            //Part 2
            //I could do it the more "effiecent" way and have all the runs at once, but for simplicity of my brain, I'll copy part 1, and just alter each slightly for each outcome.

        }

        static int Run(int right, string[] input)
        {
            //Part 1
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
