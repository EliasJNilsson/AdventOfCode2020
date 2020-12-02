using System;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string streamText = System.IO.File.ReadAllText(@"..\..\..\input.txt");

            var splitText = streamText.Split("\n");

            int validCount = 0;

            foreach(var line in splitText)
            {
                //split the line to the information needed
                var validationPasswordRequest = GenerateValidationRequest(line);

                if(validationPasswordRequest == null)
                {
                    continue;
                }

                //Do the checks
                if(ValidatePassword(validationPasswordRequest))
                {
                    validCount++;
                }
            }

            Console.WriteLine($"The valid count of passwords is {validCount}");
        }

        public static ValidationPasswordRequest GenerateValidationRequest(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            var seperatedStrings = input.Split(" ");

            var range = seperatedStrings[0].Split("-");

            var returnOutput = new ValidationPasswordRequest()
            {
                Range = new Range() { lower = int.Parse(range[0]), upper = int.Parse(range[1]) },
                Letter = seperatedStrings[1][0],
                Password = seperatedStrings[2]
            };

            return returnOutput;
        }

        public static bool ValidatePassword(ValidationPasswordRequest request)
        {
            //could also do group by, sort of splitting of the string, but to save braincells today.. I'm just going to loop and count.

            var countOfChar = 0;

            foreach(var letter in request.Password)
            {
                if(letter == request.Letter)
                {
                    countOfChar++;
                }
            }

            if((countOfChar >= request.Range.lower) && (countOfChar <= request.Range.upper))
            {
                return true;
            }
            return false;
        }
    }

    public class Range
    {
        public int lower { get; set; }
        public int upper { get; set; }
    }

    public class ValidationPasswordRequest
    {
        public Range Range { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }
    }
}
