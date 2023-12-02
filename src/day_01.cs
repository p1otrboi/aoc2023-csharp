namespace aoc2023_csharp.src;

using System.Text;
using System.Text.RegularExpressions;
using Humanizer;

class Day_01
{
    public static void Run()
    {
        Console.WriteLine("Day 1");
        Console.WriteLine("Part 1: " + Part1());
        Console.WriteLine("Part 2: " + Part2());
    }

    public static int Part1()
    {
        string[] inputArray = File.ReadAllLines("./inputs/day1.txt");
        
        // regex to match all digits
        var regex = new Regex(@"\d");
        var stringBuilder = new StringBuilder();
        int sum = 0;

        foreach (string line in inputArray)
        {
            var regexMatches = regex.Matches(line);
            
            if (regexMatches.Count == 0)
            {
                continue;
            }

            // if there is only one match, we need to double it
            if (regexMatches.Count == 1)
            {
                stringBuilder.Append(regexMatches[0].Value);
                stringBuilder.Append(regexMatches[0].Value);
                sum += int.Parse(stringBuilder.ToString());
                stringBuilder.Clear();
                continue;
            }

            // if there are more than one match, we need to combine the first and last
            if (regexMatches.Count > 1)
            {
                stringBuilder.Append(regexMatches[0].Value);
                stringBuilder.Append(regexMatches[regexMatches.Count - 1].Value);
                sum += int.Parse(stringBuilder.ToString());
                stringBuilder.Clear();
                continue;
            }
        }

        return sum;
    }

    public static int Part2()
    {
        string[] inputArray = File.ReadAllLines("./inputs/day1.txt");
        var regex = new Regex(@"\d|(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)");
        var stringBuilder = new StringBuilder();

        int sum = 0;

        var numberTable = new Dictionary<string, int>
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9},
        };

        foreach (string line in inputArray)
        {
            var regexMatches = regex.Matches(line);

            if (regexMatches.Count == 0)
            {
                continue;
            }

            if (regexMatches.Count == 1)
            {
                var singleNumber = regexMatches[0].Value;

                if (numberTable.TryGetValue(singleNumber, out int value))
                {
                    singleNumber = value.ToString();
                }
                stringBuilder.Append(singleNumber);
                stringBuilder.Append(singleNumber);
                sum += int.Parse(stringBuilder.ToString());
                stringBuilder.Clear();
                continue;
            }

            if (regexMatches.Count > 1)
            {
                var firstNumber = regexMatches[0].Value;
                var secondNumber = regexMatches[^1].Value;

                if (numberTable.TryGetValue(firstNumber, out int value1))
                {
                    firstNumber = value1.ToString();
                }
                if (numberTable.TryGetValue(secondNumber, out int value2))
                {
                    secondNumber = value2.ToString();
                }

                sum += int.Parse(firstNumber + secondNumber);
            }
        }

        return sum;
    }
}