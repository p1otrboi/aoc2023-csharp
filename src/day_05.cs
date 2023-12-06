using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2023_csharp.src
{
    public class Day_05
    {
        public static void Run()
        {
            Console.WriteLine(Part1());
            Console.WriteLine(Part2());
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
            var seedToSoil = new List<Tuple<long, long, long>>();
            var soilToFertilizer = new List<Tuple<long, long, long>>();
            var fertilizerToWater = new List<Tuple<long, long, long>>();
            var waterToLight = new List<Tuple<long, long, long>>();
            var lightToTemperature = new List<Tuple<long, long, long>>();
            var temperatureToHumidity = new List<Tuple<long, long, long>>();
            var humidityToLocation = new List<Tuple<long, long, long>>();
            List<List<Tuple<long, long, long>>> map = [seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation];

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

                    map[i - 1].Add(new Tuple<long, long, long>(destination, source, range));
                }
            }

            static long ProcessMap(long seed, List<Tuple<long, long, long>> map)
            {
                foreach (var mapEntry in map)
                {
                    if (seed >= mapEntry.Item2 && seed < mapEntry.Item2 + mapEntry.Item3)
                    {
                        return mapEntry.Item1 + (seed - mapEntry.Item2);
                    }
                }
                return seed;
            }

            // Find the lowest location number that corresponds to any of the initial seed numbers
            long lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                long currentSeed = seed;
                currentSeed = ProcessMap(currentSeed, seedToSoil);
                currentSeed = ProcessMap(currentSeed, soilToFertilizer);
                currentSeed = ProcessMap(currentSeed, fertilizerToWater);
                currentSeed = ProcessMap(currentSeed, waterToLight);
                currentSeed = ProcessMap(currentSeed, lightToTemperature);
                currentSeed = ProcessMap(currentSeed, temperatureToHumidity);
                currentSeed = ProcessMap(currentSeed, humidityToLocation);
                lowestLocation = Math.Min(lowestLocation, currentSeed);
            }

            return $"Day 5, Part 1: {lowestLocation}";
        }

        public static string Part2()
        {
            string[] input = File.ReadAllLines("inputs/day5.txt");
            var regex = new Regex(@"\d+");
            int group = 0;
            var groupedInput = input
                .Select((line, index) => new { line, group = line == "" ? group++ : group })
                .GroupBy(x => x.group, x => x.line)
                .Select(g => g.ToArray())
                .ToArray();

            List<long> seeds = regex.Matches(input[0]).Select(x => long.Parse(x.Value)).ToList();
            var seedToSoil = new List<Tuple<long, long, long>>();
            var soilToFertilizer = new List<Tuple<long, long, long>>();
            var fertilizerToWater = new List<Tuple<long, long, long>>();
            var waterToLight = new List<Tuple<long, long, long>>();
            var lightToTemperature = new List<Tuple<long, long, long>>();
            var temperatureToHumidity = new List<Tuple<long, long, long>>();
            var humidityToLocation = new List<Tuple<long, long, long>>();
            List<List<Tuple<long, long, long>>> map = [seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation];

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

                    map[i - 1].Add(new Tuple<long, long, long>(destination, source, range));
                }
            }

            // Add new seed entries
            List<Tuple<long, long>> newSeeds = [];
            List<Tuple<long, long>> newSeeds2 = [];


            for (int i = 0; i < seeds.Count; i+=2)
            {
                newSeeds.Add(new Tuple<long, long>(seeds[i], seeds[i] + seeds[i + 1]));
            }

            while (newSeeds.Count > 0)
            {
                var seedCopy = newSeeds.Last();
                newSeeds.RemoveAt(0);

                foreach (var mapEntry in map)
                {
                    foreach (var range in mapEntry)
                    {
                        var overlapStart = Math.Max(seedCopy.Item1, range.Item2);
                        var overlapEnd = Math.Min(seedCopy.Item2, range.Item2 + range.Item3);
                        if (overlapStart < overlapEnd)
                        {
                            newSeeds2.Add(new Tuple<long, long>(overlapStart - range.Item2 + range.Item1, 
                                overlapEnd - range.Item2 + range.Item1));
                            if (overlapStart > seedCopy.Item1)
                            {
                                newSeeds.Add(new Tuple<long, long>(seedCopy.Item1, overlapStart));
                            }
                            if (range.Item2 > overlapEnd)
                            {
                                newSeeds.Add(new Tuple<long, long>(overlapEnd, range.Item2 + range.Item3));
                            }
                            break;
                        }
                        else
                        {
                            newSeeds2.Add(new Tuple<long, long>(seedCopy.Item1, seedCopy.Item2));
                        }
                    }
                }
            }
            newSeeds = newSeeds2;

            return $"Day 5, Part 2: {newSeeds.Min()}";
        }
    }

}