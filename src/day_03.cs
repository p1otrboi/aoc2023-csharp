using System.Drawing;
using System.Text;

namespace aoc2023_csharp.src;

public class Day_03
{
    public static void Run()
    {
        Console.WriteLine(Part1());
    }

    public static string Part1()
    {
        string[] inputArray = File.ReadAllLines("inputs/day3.txt");

        int width = inputArray[0].Length;
        int height = inputArray.Length;

        Point[] directions =
        [
            new(0, 1),
            new(1, 0),
            new(0, -1),
            new(-1, 0),
            new(1, 1),
            new(-1, 1),
            new(1, -1),
            new(-1, -1)
        ];

        var stringBuilder = new StringBuilder();
        bool isPartNumber = false;
        int sumOfPartNumbers = 0;

        void checkPartNumber()
        {
            if (isPartNumber)
            {
                sumOfPartNumbers += int.Parse(stringBuilder!.ToString());
            }
            stringBuilder.Clear();
            isPartNumber = false;
        }

        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                var character = inputArray[y][x];

                if (char.IsDigit(character))
                {
                    stringBuilder.Append(character);

                    foreach (var direction in directions)
                    {
                        var newX = x + direction.X;
                        var newY = y + direction.Y;

                        if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                        {
                            continue;
                        }

                        var newCharacter = inputArray[newY][newX];
                        if (!char.IsDigit(newCharacter) && newCharacter != '.')
                        {
                            isPartNumber = true;
                        }
                    }
                }
                else
                {
                    checkPartNumber();
                }
            }
            checkPartNumber();
        }
        
        return $"Day 03, Part 1: {sumOfPartNumbers}";
    }
}
