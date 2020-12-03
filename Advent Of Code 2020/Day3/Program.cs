using System;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            string streamText = System.IO.File.ReadAllText(@"..\..\..\input.txt");

            var splitText = streamText.Split("\n");

            int col = splitText[0].Length;
            var treeCount = 0;
            int placement = 0;

            foreach(var row in splitText)
            {
                //Part 1
                placement += 3;
                if(placement >= col)
                {
                    placement -= col;
                }

                if(IsThereTree(placement, row))
                {
                    treeCount++;
                }
            }

            Console.WriteLine($"The count of how many trees hit is: {treeCount}");            

        }

        static bool IsThereTree(int col, string row)
        {
            if (row[col] == '#')
            {
                Console.WriteLine($"hit tree: {row[col]}");
                return true;
            }

            Console.WriteLine($"clear snow: {row[col]}");
            return false;
        }
    }
}
