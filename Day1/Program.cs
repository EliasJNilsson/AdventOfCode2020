using System;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get the input into a good place
            string inputString = "1539,1914,1866,1407,1706,1423,1834,1700,1573,1486,1743,1394,1693,1705,1530,1811,1626,1473,1901,1481,1527,1841,1891,1750,1343,1899,401,1896,1627,1593,1541,874,1484,1210,1692,1963,1964,1780,671,1862,1393,1309,1740,1831,1932,1185,1979,1504,1663,1610,1494,1511,1103,1738,1816,1871,1545,1595,1784,1412,1815,1998,1783,1770,1426,1699,1416,1880,1612,1989,1360,1869,1762,1690,1999,1990,1521,1730,703,1463,1670,1472,1413,1669,1502,1548,1475,1694,1314,1980,980,1667,890,1569,1456,1406,1924,1973,1965,1533,1827,2000,1847,1520,1729,1512,1555,1566,1505,1672,1169,1835,1850,1493,1861,1288,1675,1676,1556,1320,1757,1870,1642,1903,1372,1967,1894,176,1908,1418,1535,1487,1496,1491,1611,1970,1758,1563,1766,1629,1937,1763,1829,1772,1632,1517,1736,1971,1721,1716,1429,1408,1560,1958,1359,1890,1825,1536,1819,1697,1887,1832,2005,892,1471,1425,1677,1673,1128,1878,1062,1470,1875,1854,1518,1568,1919,256,1532,1711,1944,1344,1330,1636,1957,1709,1551,1983,1674,1671,1959,1760,1689,1767,1477,1589,1897,1144,1982,1544";

            List<int> inputList = new List<int>();
            var test = inputString.Split(",");
            
            foreach(var input in test)
            {
                inputList.Add(int.Parse(input));
            }

            //Check inputs to find 2020
            inputList.Sort();

            int num1 = 0;
            int num2 = 0;

            foreach(var integer in inputList)
            {
                var checkOutput = CheckNumber(2020, integer, inputList);

                if(checkOutput != -1)
                {
                    num1 = integer;
                    num2 = checkOutput;
                }
            }

            //Take the two numbers and multiply them, then write
            Console.WriteLine($"The numbers of part1 are {num1} and {num2} = {(num1 * num2)}");

            //Part 2
            int num3 = 0;

            foreach (var integer1 in inputList)
            {
                foreach(var integer2 in inputList)
                {
                    if(integer1 == integer2)
                    {
                        continue;
                    }

                    var checkOutput = CheckNumber((2020 - integer1), integer2, inputList);

                    if (checkOutput != -1)
                    {
                        num1 = integer1;
                        num2 = integer2;
                        num3 = checkOutput;
                    }
                }
            }

            Console.WriteLine($"The numbers of part2 are {num1}, {num2}, {num3} = {(num1 * num2 * num3)}");
        }

        static int CheckNumber(int expectedTotal, int checkNum, List<int> listToReference)
        {
            var huntedNumber = expectedTotal - checkNum;
            if (listToReference.Contains(huntedNumber))
            {
                return huntedNumber;
            }
            return -1;
        }
    }
}
