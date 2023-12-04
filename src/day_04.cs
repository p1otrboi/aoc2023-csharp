namespace aoc2023_csharp.src
{
    public class Day_04
    {
        public static void Run()
        {
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
        }

        public static string Part1()
        {
            int totalPoints = 0;

            string[] inputArray = File.ReadAllLines("inputs/day4.txt");

            foreach (var inputLine in inputArray)
            {
                var parts = inputLine.Split(':');
                var numbers = parts[1].Split('|');
                var winningNumbers = numbers[0]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();
                var ticketNumbers = numbers[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();

                var matchCount = winningNumbers.Intersect(ticketNumbers).Count();
                
                if (matchCount == 0)
                {
                    continue;
                }

                totalPoints += 1 << (matchCount - 1);
            }

            return $"Day 4, Part 1: {totalPoints}";
        }
        public static string Part2()
        {
            string[] inputArray = File.ReadAllLines("inputs/day4.txt");

            int[] cardCount = new int[inputArray.Length];
            for (int i = 0; i < cardCount.Length; i++)
            {
                cardCount[i] = 1;
            }

            for (int cardId = 0; cardId < inputArray.Length; cardId++)
            {
                string? line = inputArray[cardId];
                var parts = line.Split(':');
                var numbers = parts[1].Split('|');
                var pickedNumbers = numbers[0]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();
                var ourNumbers = numbers[1]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(int.Parse)
                    .ToArray();

                var matchCount = pickedNumbers.Intersect(ourNumbers).Count();

                for (int i = 0; i < matchCount; i++)
                {
                    cardCount[cardId + 1 + i] += cardCount[cardId];
                }
            }
            return $"Day 4, Part 2: {cardCount.Sum()}";
        }
    }
}