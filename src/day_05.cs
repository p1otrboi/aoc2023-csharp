using System.Text.RegularExpressions;

namespace aoc2023_csharp.src
{
    public class Day_05
    {
        public static void Run()
        {
            Console.WriteLine(Part1());
        }

        public static string Part1()
        {
            string[] input = File.ReadAllLines("inputs/sample.txt");
            var regex = new Regex(@"\d+");
            int group = 0;
            var groupedInput = input
                .Select((line, index) => new { line, group = line == "" ? group++ : group })
                .GroupBy(x => x.group, x => x.line)
                .Select(g => g.ToArray())
                .ToArray();

            long[] seeds = regex.Matches(input[0]).Select(x => long.Parse(x.Value)).ToArray();
            var seedToSoil = new Dictionary<long, long>();
            var soilToFertilizer = new Dictionary<long, long>();
            var fertilizerToWater = new Dictionary<long, long>();
            var waterToLight = new Dictionary<long, long>();
            var lightToTemperature = new Dictionary<long, long>();
            var temperatureToHumidity = new Dictionary<long, long>();
            var humidityToLocation = new Dictionary<long, long>();
            List<Dictionary<long, long>> map = [seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation];

            // Populate the map
            for (int i = 1; i < groupedInput.Length; i++)
            {
                foreach (var line in groupedInput[i])
                {
                    if (line.EndsWith(':'))
                        continue;
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var split = line.Split(' ');
                    long destination = long.Parse(split[0]);
                    long source = long.Parse(split[1]);
                    long range = long.Parse(split[2]);
                    
                    for (long j = destination; j < destination + range; j++)
                    {
                        map[i - 1].Add(source++, j);
                    }
                }
            }

            // Find the lowest location number that corresponds to any of the initial seed numbers
            long lowestLocation = 0;
            foreach (var seed in seeds)
            {
                var currentNumber = seed;
                if (seedToSoil.ContainsKey(seed))
                    currentNumber = seedToSoil[seed];
                if (soilToFertilizer.ContainsKey(currentNumber))
                    currentNumber = soilToFertilizer[currentNumber];
                if (fertilizerToWater.ContainsKey(currentNumber))
                    currentNumber = fertilizerToWater[currentNumber];
                if (waterToLight.ContainsKey(currentNumber))
                    currentNumber = waterToLight[currentNumber];
                if (lightToTemperature.ContainsKey(currentNumber))
                    currentNumber = lightToTemperature[currentNumber];
                if (temperatureToHumidity.ContainsKey(currentNumber))
                    currentNumber = temperatureToHumidity[currentNumber];
                if (humidityToLocation.ContainsKey(currentNumber))
                    currentNumber = humidityToLocation[currentNumber];
                
                if (lowestLocation == 0 || currentNumber < lowestLocation)
                    lowestLocation = currentNumber;
            }

            return $"Day 5, Part 1: {lowestLocation}";
        }
    }

}