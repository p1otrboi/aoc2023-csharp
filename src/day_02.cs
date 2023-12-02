namespace aoc2023_csharp.src;

public class Day_02
{

    public static void Run()
    {
        Console.WriteLine("Day 2");
        Console.WriteLine("Part 1: " + Part1());
        Console.WriteLine("Part 2: " + Part2());
    }


    public static int Part1()
    {
        // Determine which games would have been possible if the bag had been loaded with only 12 red cubes, 13 green cubes, and 14 blue cubes. What is the sum of the IDs of those games?

        string[] input = File.ReadAllLines("./inputs/day2.txt");

        int sumOfGameIds = 0;

        foreach (string line in input)
        {
            int red = 0;
            int blue = 0;
            int green = 0;


        }

        return sumOfGameIds;
    }

    public static int Part2()
    {
        return 0;
    }
    
}