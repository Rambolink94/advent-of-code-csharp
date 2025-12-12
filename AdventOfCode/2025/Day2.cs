using AdventOfCode.Utility;

namespace AdventOfCode._2025;

public class Day2(InputParser.Mode parseMode, int option = 0) : Solution<Day2>(parseMode, option)
{
    public override long Part1()
    {
        long invalidIdSum = 0;
        foreach (string line in Input)
        {
            var ranges = line.Split(',',  StringSplitOptions.RemoveEmptyEntries);
            foreach (var range in ranges)
            {
                var parts = range.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);

                for (long i = start; i <= end; i++)
                {
                    var value = i.ToString();
                    if (value.Length % 2 == 0)
                    {
                        string part1 = value[..(value.Length / 2 )];
                        string part2 = value[(value.Length / 2)..];
                        
                        if (part1 == part2) invalidIdSum += i;
                    }
                }
            }   
        }
        
        return invalidIdSum;
    }

    public override long Part2()
    {
        long invalidIdSum = 0;
        var foundIds = new HashSet<long>();
        foreach (string line in Input)
        {
            var ranges = line.Split(',',  StringSplitOptions.RemoveEmptyEntries);
            foreach (var range in ranges)
            {
                var parts = range.Split('-');
                long start = long.Parse(parts[0]);
                long end = long.Parse(parts[1]);

                for (long i = start; i <= end; i++)
                {
                    var value = i.ToString();
                    int rangeSizeMax = value.Length / 2;
                    for (int rangeSize = 1; rangeSize <= rangeSizeMax; rangeSize++)
                    {
                        if (value.Length % rangeSize != 0) continue;
                        
                        string lastRange = value[..rangeSize];
                        bool valid = true;
                        for (int j = rangeSize; j + rangeSize <= value.Length; j += rangeSize)
                        {
                            if (lastRange != value[j..(j + rangeSize)])
                            {
                                valid = false;
                                break;
                            }
                        }

                        if (valid && !foundIds.Contains(i))
                        {
                            foundIds.Add(i);
                            invalidIdSum += i;
                        }
                    }
                    
                    // 123123   -> 1
                    // 5555     -> 2
                    // 121212   -> 2
                    // 12341234 -> 1
                }
            }   
        }
        
        return invalidIdSum;
    }
}