using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
using System.Numerics;

namespace Day14
{
    //Solution found on: https://github.com/LennardF1989/AdventOfCode2020/blob/master/Src/AdventOfCode2020/Days/Day14.cs#L71

    class Program
    {
        static void Main(string[] args)
        {
            StartA();
            StartB();
        }

        public static void StartA()
        {
            var lines = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            var mask = string.Empty;
            var memory = new Dictionary<long, long>();

            foreach (string line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    mask = Regex.Match(line, "mask = ([01X]+)").Groups[1].Value;
                }
                else
                {
                    var match = Regex.Match(line, "mem\\[(\\d+)\\] = (\\d+)");
                    var memoryAddress = int.Parse(match.Groups[1].Value);
                    var value = long.Parse(match.Groups[2].Value);

                    Console.WriteLine(Convert.ToString(value, 2).PadLeft(36, '0'));

                    for (var i = 0; i < mask.Length; i++)
                    {
                        char c = mask[mask.Length - 1 - i];

                        if (c == '0')
                        {
                            value &= ~(1L << i);
                        }
                        else if (c == '1')
                        {
                            value |= (1L << i);
                        }
                        else if (c == 'X')
                        {
                            //Do nothing
                        }
                    }

                    if (!memory.ContainsKey(memoryAddress))
                    {
                        memory.Add(memoryAddress, 0);
                    }

                    memory[memoryAddress] = value;

                    Console.WriteLine(Convert.ToString(value, 2).PadLeft(36, '0'));
                }
            }

            long answer = memory.Values.Sum();

            Console.WriteLine($"Day 14A: {answer}");
        }

        public static void StartB()
        {
            var lines = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            var mask = string.Empty;
            var memory = new Dictionary<long, long>();

            long numberOfCombinations = 0;

            foreach (string line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    mask = Regex.Match(line, "mask = ([01X]+)").Groups[1].Value;
                    numberOfCombinations = (long)BigInteger.Pow(2, mask.Count(x => x == 'X'));
                }
                else
                {
                    var match = Regex.Match(line, "mem\\[(\\d+)\\] = (\\d+)");
                    var memoryAddress = long.Parse(match.Groups[1].Value);
                    var value = long.Parse(match.Groups[2].Value);

                    for (long i = 0; i < numberOfCombinations; i++)
                    {
                        long memoryAddressCopy = memoryAddress;

                        int offset = 0;
                        for (var x = 0; x < mask.Length; x++)
                        {
                            char c = mask[mask.Length - 1 - x];

                            if (c == '0')
                            {
                                //Do nothing
                            }
                            else if (c == '1')
                            {
                                memoryAddressCopy |= (1L << x);
                            }
                            else if (c == 'X')
                            {
                                var onOrOff = (i >> offset) & 1;

                                if (onOrOff == 0)
                                {
                                    memoryAddressCopy &= ~(1L << x);
                                }
                                else
                                {
                                    memoryAddressCopy |= (1L << x);
                                }

                                offset++;
                            }
                        }

                        if (!memory.ContainsKey(memoryAddressCopy))
                        {
                            memory.Add(memoryAddressCopy, 0);
                        }

                        memory[memoryAddressCopy] = value;
                    }
                }
            }

            long answer = memory.Values.Sum();

            Console.WriteLine($"Day 14B: {answer}");
        }

        //static public void Part1(List<InputObject> input)
        //{
        //    var totalMemDict = new Dictionary<string, uint>();

        //    var mask = "";

        //    foreach(var line in input)
        //    {
        //        if(line.Operation == "mask")
        //        {
        //            mask = line.Value;
        //        }
        //        else 
        //        {
        //            //calculate with mask
        //            uint maskedValue = CalculateWithMask(mask, line.Value);

        //            //add to memoryDict
        //            if(totalMemDict.ContainsKey(line.Operation))
        //            {
        //                totalMemDict[line.Operation] = maskedValue;
        //            }
        //            else
        //            {
        //                totalMemDict.Add(line.Operation, maskedValue);
        //            }

        //        }
        //    }

        //    UInt64 sum = 0;
        //    foreach(var count in totalMemDict)
        //    {
        //        sum += count.Value;
        //    }

        //    Console.WriteLine($"Total sum is {sum}");

        //    return;
        //}

        //static public uint CalculateWithMask(string mask, string inputValue)
        //{
        //    var inputBase2 = Convert.ToString(int.Parse(inputValue), 2);

        //    while(inputBase2.Length < 32)
        //    {
        //        inputBase2 = inputBase2.Insert(0, "0");
        //    }

        //    var inputArray = inputBase2.ToArray();
        //    for (var i = 0; i < inputArray.Length; i++)
        //    {
        //        var check = mask[i];
        //        if(check != 'X')
        //        {
        //            inputArray[i] = mask[i];
        //        }
        //    }

        //    var num = Convert.ToUInt32(new string(inputArray), 2);

        //    return num;
        //}

        //static public void Part2(string[] input)
        //{

        //    return;
        //}

        //static public List<InputObject> ParseInput(string[] input)
        //{
        //    var output = new List<InputObject>();

        //    foreach(var val in input)
        //    {
        //        var split = val.Split(" ");

        //        output.Add(new InputObject()
        //        {
        //            Operation = split[0],
        //            Value = split[2]
        //        }); ;
        //    }

        //    return output;
        //}

        //public class InputObject
        //{
        //    public string Operation { get; set; }
        //    public string Value { get; set; }
        //}
    }
}
