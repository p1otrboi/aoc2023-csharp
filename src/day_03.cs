using System.Text.RegularExpressions;

namespace aoc2023_csharp.src;

public class Day_03
{
    public static void Run()
    {
        Console.WriteLine(Part1());
    }

    public static string Part1()
    {
        string[] inputArray = File.ReadAllLines("inputs/sample.txt");
        var regex = new Regex(@"\d+");

        foreach (string line in inputArray)
        {
            
        }

        return "Day 03, Part 1:";
    }
}
