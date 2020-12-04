using System;
using System.Collections.Generic;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string streamText = System.IO.File.ReadAllText(@"..\..\..\input.txt");

            var splitText = streamText.Split("\n");

            string validationString = "";

            int countPt1 = 0;
            int countPt2 = 0;

            //look for next whitespace, then validate
            foreach(var str in splitText)
            {
                if(string.IsNullOrWhiteSpace(str))
                {
                    //Do validation check
                    if(IsValidPassportPart1(validationString))
                    {
                        countPt1++;
                    }

                    if(IsValidPassportPart2(validationString))
                    { 
                        countPt2++;
                    }

                    //Clear validationString
                    validationString = "";

                    continue;
                }

                //Add string to validation string
                if(validationString.Length != 0)
                {
                    validationString += " " + str;
                }
                else
                {
                    validationString = str;
                }
            }

            Console.WriteLine($"For Part1: the valid passports count is {countPt1}");
            Console.WriteLine($"For Part2: the valid passports count is {countPt2}");
        }

        static bool IsValidPassportPart1(string passport)
        {
            var subParts = passport.Split(" ");

            var keyVal = new Dictionary<string, bool>();
            foreach(var part in subParts)
            {
                switch(part.Substring(0,3))
                {
                    case "byr":
                        keyVal.Add("byr", true);
                        break;

                    case "iyr":
                        keyVal.Add("iyr", true);
                        break;

                    case "eyr":
                        keyVal.Add("eyr", true);
                        break;

                    case "hgt":
                        keyVal.Add("hgt", true);
                        break;

                    case "hcl":
                        keyVal.Add("hcl", true);
                        break;

                    case "ecl":
                        keyVal.Add("ecl", true);
                        break;

                    case "pid":
                        keyVal.Add("pid", true);
                        break;

                    case "cid":
                        keyVal.Add("cid", true);
                        break;

                    default:
                        break;
                }
            }

            if((keyVal.Count == 8) || ((keyVal.Count == 7) && (!keyVal.ContainsKey("cid"))))
            {
                return true;
            }

            return false;
        }

        static bool IsValidPassportPart2(string passport)
        {
            var subParts = passport.Split(" ");

            var keyVal = new Dictionary<string, bool>();
            foreach (var part in subParts)
            {
                var check = part.Split(":");

                switch (check[0])
                {
                    case "byr":
                        {
                            var intVal = int.Parse(check[1]);
                            keyVal.Add("byr", (intVal >= 1920 && intVal <= 2002));
                            break;
                        }
                    case "iyr":
                        {
                            var intVal = int.Parse(check[1]);
                            keyVal.Add("iyr", (intVal >= 2010 && intVal <= 2020));
                            break;
                        }
                    case "eyr":
                        {
                            var intVal = int.Parse(check[1]);
                            keyVal.Add("eyr", (intVal >= 2020 && intVal <= 2030));
                            break;
                        }
                    case "hgt":
                        {
                            var length = check[1].Length;
                            var lastTwo = check[1].Substring(length - 2);

                            int number = 0;
                            if(length > 2)
                            {
                                number = int.Parse(check[1].Substring(0, length - 2));
                            }
                            else
                            {
                                keyVal.Add("hgt", false);
                                break;
                            }

                            switch(lastTwo)
                            {
                                case "cm":
                                    {
                                        keyVal.Add("hgt", ((number >= 150) && (number <= 193)));
                                        break;
                                    }
                                case "in":
                                    {
                                        keyVal.Add("hgt", ((number >= 59) && (number <= 76)));
                                        break;
                                    }
                                default:
                                    break;
                            }

                            break;
                        }
                    case "hcl":
                        {
                            var sub = check[1].Substring(1);

                            if (check[1][0] != '#' || sub.Length != 6)
                            {
                                keyVal.Add("hcl", false);
                                break;
                            }

                            foreach(var ch in sub)
                            {
                                //I KNOW I COULD LEARN REGEX FOR THIS, BUT NO.
                                if(!"abcdefABCDEF0123456789".Contains(ch))
                                {
                                    keyVal.Add("hcl", false);
                                    break;
                                }
                            }

                            keyVal.Add("hcl", true);
                            break;
                        }
                    case "ecl":
                        {   
                            keyVal.Add("ecl", ((check[1] == "amb") || (check[1] == "blu") || (check[1] == "brn") || (check[1] == "gry") || (check[1] == "grn") || (check[1] == "hzl") || (check[1] == "oth")));
                            break;
                        }
                    case "pid":
                        {
                            var integer = 0;
                                
                            int.TryParse(check[1], out integer);

                            keyVal.Add("pid", (check[1].Length == 9 && integer != 0));
                            break;
                        }
                    case "cid":
                        {
                            keyVal.Add("cid", true);
                            break;
                        }
                    default:
                        break;
                }
            }

            if ((!keyVal.ContainsValue(false)) && ((keyVal.Count == 8) || ((keyVal.Count == 7) && (!keyVal.ContainsKey("cid")))))
            {
                return true;
            }

            return false;
        }
    }
}
