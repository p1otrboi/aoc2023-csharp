using System.Text.RegularExpressions;

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

        string[] inputArray = File.ReadAllLines("./inputs/day2.txt");
        var regex = new Regex(@"\d+");

        int sumOfGameIds = 0;

        foreach (string line in inputArray)
        {
            string[] lineSplit = line.Split(':', ',', ';')
                                    .Select(s => s.Trim())
                                    .ToArray();
            
            var gameId = int.Parse(regex.Matches(lineSplit[0])[0].Value);
            var maxRedCubes = 0;
            var maxGreenCubes = 0;
            var maxBlueCubes = 0;

            // skip the first element, which is the game id, and loop through the rest
            for (int i = 1; i < lineSplit.Length; i++)
            {
                if (lineSplit[i].Contains("red"))
                {
                    var redCubes = int.Parse(regex.Matches(lineSplit[i])[0].Value);

                    if (redCubes > maxRedCubes)
                    {
                        maxRedCubes = redCubes;
                    }
                }

                if (lineSplit[i].Contains("green"))
                {
                    var greenCubes = int.Parse(regex.Matches(lineSplit[i])[0].Value);

                    if (greenCubes > maxGreenCubes)
                    {
                        maxGreenCubes = greenCubes;
                    }
                }

                if (lineSplit[i].Contains("blue"))
                {
                    var blueCubes = int.Parse(regex.Matches(lineSplit[i])[0].Value);

                    if (blueCubes > maxBlueCubes)
                    {
                        maxBlueCubes = blueCubes;
                    }
                }

            }
            
            if (maxBlueCubes <= 14 && maxGreenCubes <= 13 && maxRedCubes <= 12)
            {
                sumOfGameIds += gameId;
            }
        }

        return sumOfGameIds;
    }

    public static int Part2()
    {
        return 0;
    }
    
}