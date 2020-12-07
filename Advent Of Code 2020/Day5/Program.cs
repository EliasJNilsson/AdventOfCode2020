using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");
            var rows = input.Select(l => Convert.ToByte(l.Substring(0, 7).Aggregate("0", (s, c) =>
                s + c switch { 'F' => '0', 'B' => '1' }), 2)
            ).ToArray();
            var cols = input.Select(l => Convert.ToByte(l.Substring(7).Aggregate("0", (s, c) =>
                s + c switch { 'L' => '0', 'R' => '1' }), 2)
            ).ToArray();

            var ids = rows.Zip(cols).Select(z => z.First * 8 + z.Second).OrderBy(id => id).ToArray();
            Console.WriteLine($"Part 1: {ids.Max()}");

            var mine = ids.Zip(ids.Skip(1)).Where(z => z.Second - z.First > 1);
            Console.WriteLine($"Part 2: {mine.First()} => {mine.First().First + 1}");




            //{
            //    string streamText = System.IO.File.ReadAllText(@"..\..\..\input.txt");
            //    var splitText = streamText.Split("\n");

            //    var part1HighestValue = 0;

            //    foreach (string input in splitText)
            //    {
            //        if (string.IsNullOrWhiteSpace(input))
            //        {
            //            continue;
            //        }

            //        //make queues
            //        var subRows = input.Substring(0, 7);
            //        var subSeat = input.Substring(7);

            //        Queue<char> rowsQueue = new Queue<char>(subRows);
            //        Queue<char> seatQueue = new Queue<char>(subSeat);

            //        if(input == "BBBBBFFRRL")
            //        {
            //            ;
            //        }


            //        //get row
            //        var outputRows = searchRows(rowsQueue, 0, 127);

            //        //get seat
            //        var outputSeat = searchSeats(seatQueue, 0, 8);

            //        //multiply
            //        var boardingPassId = outputRows * 8 + outputSeat;
            //        Console.WriteLine($"The search row and seat multiply to: {boardingPassId}");

            //        if(part1HighestValue < boardingPassId)
            //        {
            //            part1HighestValue = boardingPassId;
            //        }
            //    }

            //    Console.WriteLine($"The largest boarding pass Id value is: {part1HighestValue}");

        }

        static int searchRows(Queue<char> queue, int low, int high)
        {
            if(queue.Count == 1)
            {
                if(queue.Peek() == 'F')
                {
                    return low;
                }

                return high;
            }

            var pop = queue.Dequeue();

            var mid = ((high - low) / 2) + low; 

            if(pop == 'F')
            {
                return searchRows(queue, low, mid);
            }
            else
            {
                return searchRows(queue, mid + 1, high);
            }
        }

        static int searchSeats(Queue<char> queue, int low, int high)
        {
            if (queue.Count == 1)
            {
                if (queue.Peek() == 'L')
                {
                    return low;
                }

                return high;
            }

            var pop = queue.Dequeue();

            var mid = ((high - low) / 2) + low;

            if (pop == 'L')
            {
                return searchSeats(queue, low, mid);
            }
            else
            {
                return searchSeats(queue, mid + 1, high);
            }
        }
    }
}
