using System;
using System.Linq;
using System.Collections.Generic;

namespace Day15
{
    class Program
    {
        //Runtime solution found with https://www.reddit.com/r/adventofcode/comments/kdf85p/2020_day_15_solutions/gfya0vt?utm_source=share&utm_medium=web2x&context=3
        static void Main(string[] args)
        {
            //var input = System.IO.File.ReadAllLines();
            var input = System.IO.File.ReadAllText(@"..\..\..\input.txt").Split(",")
                                .Select(x => int.Parse(x)).ToArray();
            for (int part = 1; part < 3; part++)
            {
                var limit = part == 1 ? 2020 : 30000000;
                var spoken = new int[limit];
                Array.Fill(spoken, 0);

                for (int i = 0; i < input.Length; i++)
                {
                    spoken[input[i]] = i + 1;
                }
                var lastNum = input.Last();
                for (int i = input.Length + 1; i <= limit; i++)
                {
                    if (spoken[lastNum] != 0)
                    {
                        var newVal = i - 1 - spoken[lastNum];
                        spoken[lastNum] = i - 1;
                        lastNum = newVal;
                    }
                    else
                    {
                        spoken[lastNum] = i - 1;
                        lastNum = 0;
                    }
                }
                Console.WriteLine(lastNum);
            }
        }


        //MY attempts, there is something wrong.
        static public void Part1()
        {
            var checkDict = new Dictionary<int, int>();
            checkDict.Add(8, 1);
            checkDict.Add(13, 2);
            checkDict.Add(1, 3);
            checkDict.Add(0, 4);
            checkDict.Add(18, 5);
            checkDict.Add(9, 6);
            //checkDict.Add(0, 7);

            //first number in the loop
            int calledNumber = 0;
            int countOfNumbersSaid = 6;

            while(countOfNumbersSaid < 2021)
            {
                Console.WriteLine($"Last turn was turn {countOfNumbersSaid}, the number called was {calledNumber}");
                //say the number
                if(!checkDict.ContainsKey(calledNumber))
                {
                    checkDict.Add(calledNumber, countOfNumbersSaid);
                    //it's a new number
                    calledNumber = 0;
                }
                else
                {
                    //old number, so math it up.

                    var lastSaid = checkDict.GetValueOrDefault(calledNumber);
                    var diffOfLastSaid = countOfNumbersSaid - lastSaid;

                    checkDict[calledNumber] = countOfNumbersSaid;

                    calledNumber = diffOfLastSaid;
                }
                
                //increment
                countOfNumbersSaid++;
            }
            Console.WriteLine(" ");
            Console.WriteLine($"The number said at 2020, was {calledNumber}");

            return;
        }
    }
}