namespace aoc2023_csharp.src
{
    public class Day_06
    {
        public static void Run()
        {
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
        }

        public static string Part1()
        {
            var input = File.ReadAllLines("inputs/day6.txt");

            var times = input[0]
                ["Time: ".Length..]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();

            var records = input[1]
                ["Distance: ".Length..]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();

            var total = 1;

            for (int race = 0; race < times.Length; race++)
            {
                var waysToWin = 0;
                var totalTime = times[race];
                var record = records[race];

                for (int time = 0; time < totalTime; time++)
                {
                    if (time * (totalTime - time) > record)
                        waysToWin++;
                }

                total *= waysToWin;
            }
            
            return $"Day 6 Part 1: {total}";
        }

        public static string Part2()
        {
            var input = File.ReadAllLines("inputs/day6.txt");

            var totalTime = long
                .Parse(input[0]["Time:".Length..]
                .Replace(" ", string.Empty));
            var record = long
                .Parse(input[1]["Distance:".Length..]
                .Replace(" ", string.Empty));

            var d = totalTime * totalTime - 4 * (record + 1);
            var x1 = (totalTime + Math.Sqrt(d)) / 2;
            var x2 = (totalTime - Math.Sqrt(d)) / 2;

            var time = (long)Math.Ceiling(Math.Min(x1, x2));
            var waysToWin = totalTime - time - time + 1;

            return $"Day 6 Part 2: {waysToWin}";
        }
    }
}