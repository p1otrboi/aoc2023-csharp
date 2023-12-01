using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class day_01
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
        
        var regex = new Regex(@"\d");
        var sb = new StringBuilder();
        int sum = 0;

        foreach (string line in inputArray)
        {
            var matches = regex.Matches(line);
            
            if (matches.Count == 0)
            {
                continue;
            }

            if (matches.Count == 1)
            {
                sum += int.Parse(matches[0].Value) ^ 2;
                continue;
            }

            if (matches.Count > 1)
            {
                sb.Append(matches[0].Value);
                sb.Append(matches[matches.Count - 1].Value);
                sum += int.Parse(sb.ToString());
                sb.Clear();
                continue;
            }
        }

        return sum;
    }

    public static int Part2()
    {
        return 0;
    }
}