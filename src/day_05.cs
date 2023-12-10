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
            string[] input = File.ReadAllLines("inputs/day5.txt");
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
            const long MapGroupCount = 7;

            using var inputStream = File.OpenRead("inputs/day5.txt");
            using var reader = new StreamReader(inputStream);

            var seedsLine = reader.ReadLine()!["seeds:".Length..];
            var seedPairs = seedsLine.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(long.Parse)
                .ToArray();

            reader.ReadLine(); // empty line

            var mapGroups = new List<RangeMapGroup>();

            for (long i = 0; i < MapGroupCount; i++)
            {
                reader.ReadLine(); // header

                var maps = new List<RangeMap>();
                string? line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var parts = line.Split(' ').Select(long.Parse).ToArray();
                    maps.Add(new RangeMap(parts[0], parts[1], parts[2]));
                    line = reader.ReadLine();
                }

                var seedRangeGroup = new RangeMapGroup([.. maps]);
                mapGroups.Add(seedRangeGroup);
            }

            var seeds = new List<SeedRange>();
            for (int seedPairId = 0; seedPairId < seedPairs.Length / 2; seedPairId++)
            {
                var startingSeed = seedPairs[seedPairId * 2];
                var length = seedPairs[seedPairId * 2 + 1];
                seeds.Add(new(startingSeed, length));
            }

            var seedRanges = seeds;
            foreach (var group in mapGroups)
            {
                var newSeedRanges = new List<SeedRange>();

                foreach (var seedRange in seedRanges)
                {
                    var mappedRanges = group.Map(seedRange);
                    newSeedRanges.AddRange(mappedRanges);
                }

                seedRanges = newSeedRanges;
            }

            return $"Day 5, Part 2: {seedRanges.Select(s => s.Start).Min()}";
        }
    }

    class RangeMapGroup(RangeMap[] maps)
    {
        private readonly RangeMap[] _maps = [.. maps.OrderBy(s => s.SourceStart)];

        public SeedRange[] Map(SeedRange range)
        {
            var results = new List<SeedRange>();

            var remainingRange = range;

            foreach (var map in _maps)
            {
                // The a part or whole remaining range is before the map starts
                //    identity map for this part
                if (remainingRange.Start < map.SourceStart)
                {
                    var cutOffLength = Math.Min(
                        remainingRange.Length,
                        map.SourceStart - remainingRange.Start);

                    var cutOff = new SeedRange(remainingRange.Start, cutOffLength);
                    results.Add(cutOff);

                    remainingRange = new SeedRange(
                        remainingRange.Start + cutOffLength,
                        remainingRange.Length - cutOffLength);
                }

                if (remainingRange.Length <= 0)
                {
                    break;
                }

                // check for intersection with current map
                if (remainingRange.Start >= map.SourceStart &&
                    remainingRange.Start < (map.SourceStart + map.RangeLength))
                {
                    var intersectionLength = Math.Min(
                        remainingRange.Length,
                        map.SourceStart + map.RangeLength - remainingRange.Start);
                    var intersection = new SeedRange(remainingRange.Start, intersectionLength);
                    var transformedRange = map.Transform(intersection);
                    results.Add(transformedRange);

                    remainingRange = new SeedRange(
                        remainingRange.Start + intersectionLength,
                        remainingRange.Length - intersectionLength);
                }

                if (remainingRange.Length <= 0)
                {
                    break;
                }
            }

            if (remainingRange.Length > 0)
            {
                results.Add(remainingRange);
            }

            return [.. results];
        }
    }

    record RangeMap(long DestinationStart, long SourceStart, long RangeLength)
    {
        public bool IsInSourceRange(long value) =>
            value >= SourceStart &&
            value < (SourceStart + RangeLength);

        public long MapSource(long value) =>
            DestinationStart + (value - SourceStart);

        internal SeedRange Transform(SeedRange intersection) =>
            new(MapSource(intersection.Start), intersection.Length);
    }

    record struct SeedRange(long Start, long Length)
    {
        public readonly long End => Start + Length - 1;
    }

}