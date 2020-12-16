using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            Part1(input[0], input[1]);

            SolvePartTwo(input);
        }

        static public void Part1(string input1, string input2)
        {
            var timestamp = int.Parse(input1);

            var listOfRoutes = input2.Split(',').ToList();
            _ = listOfRoutes.RemoveAll(s => s.Contains('x'));

            int smallestDif = int.MaxValue;
            int smallestBus = int.MaxValue;

            foreach(var bus in listOfRoutes)
            {
                var busNum = int.Parse(bus);
                int tempTimestamp = timestamp - (timestamp % busNum) + busNum;

                var dif = tempTimestamp - timestamp;

                if(dif < smallestDif)
                {
                    smallestDif = dif;
                    smallestBus = busNum;
                }
            }
            
            //Results
            Console.WriteLine($"The bus {smallestBus} got to {timestamp}, at timestamp {smallestDif + timestamp}, only {smallestDif} after ");
            Console.WriteLine($"Multiplication of bus times time after is {smallestDif * smallestBus}");

            return;
        }

        //My attempt, it would have worked, but it brute force
        static public void Part2(string input1, string input2)
        {
            var timestamp = int.Parse(input1);
            var listOfRoutes = input2.Split(',').ToList();

            //+timestamp, busNum
            var dictOfBusRoutes = new Dictionary<int, int>();

            int firstBus = 0;

            for(int i = 0; i < listOfRoutes.Count; i++)
            {
                if(listOfRoutes[i] == "x")
                {
                    continue;
                }

                if (i == 0)
                {
                    firstBus = int.Parse(listOfRoutes[i]);
                }
                else
                {
                    var bus = int.Parse(listOfRoutes[i]);
                    dictOfBusRoutes.Add(i, bus);
                }
            }

            var earliestAfterTimestamp = 100000000000000 - (100000000000000 % firstBus);
            
            //the spec said it would be larger than 100000000000000... so...
            
            bool allHavePassed = false;

            while(!allHavePassed)
            {
                earliestAfterTimestamp += firstBus;

                var testPassing = true;

                foreach(var keyVal in dictOfBusRoutes)
                {
                    testPassing = ((earliestAfterTimestamp + keyVal.Key) % keyVal.Value == 0) && testPassing;

                    if(testPassing == false)
                    {
                        break;
                    }
                }

                if(testPassing == true)
                {
                    allHavePassed = true;
                }
            }

            //Results
            Console.WriteLine($"The Earliest timestamp which all busses pass after the input in subsequent times is at {earliestAfterTimestamp}");
            return;
        }

        //By theElTea https://www.reddit.com/r/adventofcode/comments/kc4njx/2020_day_13_solutions/gfvhle1?utm_source=share&utm_medium=web2x&context=3
        static public void SolvePartTwo(string[] busScheduleStrings)
        {

            long startTime = 100000000000000;
            List<(int busNum, int requiredDepartureOffset)> busDepartureInfo = new List<(int, int)>();
            string[] allBusNumbersAsStrings = busScheduleStrings[1].Split(',');

            //Fill out the list of bus numbers and their required arrival offsets. In format (busNumber, offset)
            for (int i = 0; i < allBusNumbersAsStrings.Length; i++)
            {
                try
                {
                    busDepartureInfo.Add((int.Parse(allBusNumbersAsStrings[i]), i));
                }
                catch (FormatException)
                {
                    //Nothing to do here!
                }
            }

            //Choose the initial start time by finding the nearest or greater departure time of the first bus as we did in part 1.
            startTime = GetEqualOrGreaterDepartureTime(startTime, busDepartureInfo[0].busNum);

            long incrementAmount = busDepartureInfo[0].busNum; //As each periodicity pairing is found, increase the increment.
            int lockedIn = 0;                                    //Keeps track of which bags have been matched to a periodicity with the group.


            // Solution approach
            //-------------------
            // For any two elements a and b that each have their own periodicty, when treated as a group they have periodicity a*b.
            // EG: one planet does a loop in 50 days, another in 70 days, if they line up at day=0 they'll line up again at 50*70 = 3500 days.
            // So, this approach is to find the match with the next bus, then change the periodicity by multiplying by that bus's loop time
            //   Then, move on to add another bus to the match and repeat.
            for (long testTime = startTime; true; testTime += incrementAmount)
            {
                int nextBusToLookFor = lockedIn + 1;
                long requiredDepartureTime = testTime + busDepartureInfo[nextBusToLookFor].requiredDepartureOffset;
                long nearestDepartureTime = GetEqualOrGreaterDepartureTime(requiredDepartureTime, busDepartureInfo[nextBusToLookFor].busNum);

                if (requiredDepartureTime == nearestDepartureTime)
                {
                    incrementAmount *= busDepartureInfo[nextBusToLookFor].busNum;
                    lockedIn = nextBusToLookFor;

                    if (lockedIn == busDepartureInfo.Count - 1) //They're all lined up!
                    {
                        Console.WriteLine($"Part Two: Earliest departure is Bus ID {busDepartureInfo[0].busNum} on departure {testTime}");
                        break;
                    }
                }
            }
        }

        static public long GetEqualOrGreaterDepartureTime(long targetDepartureTime, int busNum)
        {
            //Busses get around the loop in the time equal to their bus number.
            //All busses depart at t=0.
            //Find the time the bus is at the station greater than or equal to the target departure.
            //To find it:
            //  1) Find the number of cycles the bus runs in the target departure time.
            //  2) If there's a remainder, round up to the next highest number of cycles.
            //  3) Multiply the number of cycles by the bus' loop time to get the arrival that is >= the desired departure time.
            //
            // An earlier solution used Mathf.CeilToInt() but that approach failed when using long values (it would wrap to -ve numbers)
            // So this more manual solution was needed.

            long quotient = targetDepartureTime / busNum;    // (1)
            long remainder = targetDepartureTime % busNum;
            if (remainder > 0)
            {
                quotient++;                                  // (2)
            }
            long earliestDepartureTime = quotient * busNum;  // (3)

            return earliestDepartureTime;
        }
    }
}
