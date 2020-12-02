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
                if(ValidatePasswordPart1(validationPasswordRequest))
                {
                    validCount++;
                }
            }

            Console.WriteLine($"The valid count of part 1 passwords is {validCount}");

            int validCount2 = 0;

            foreach (var line in splitText)
            {
                //split the line to the information needed
                var validationPasswordRequest = GenerateValidationRequest(line);

                if (validationPasswordRequest == null)
                {
                    continue;
                }

                //Do the checks
                if (ValidatePasswordPart2(validationPasswordRequest))
                {
                    validCount2++;
                }
            }

            Console.WriteLine($"The valid count of part 2 passwords is {validCount2}");
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

        public static bool ValidatePasswordPart1(ValidationPasswordRequest request)
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

        public static bool ValidatePasswordPart2(ValidationPasswordRequest request)
        {
            //This validation is not upper and lower of count, as much as the placement of the letters. I will still use the range object for simplicity in this event.

            var letter1 = request.Password[request.Range.lower - 1];
            var letter2 = request.Password[request.Range.upper - 1];

            if((letter1 == letter2) && letter1 == request.Letter)
            { 
                return false; 
            }

            if(letter1 == request.Letter || letter2 == request.Letter)
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
