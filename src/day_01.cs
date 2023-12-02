namespace aoc2023_csharp.src;

using System.Text;
using System.Text.RegularExpressions;

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
}