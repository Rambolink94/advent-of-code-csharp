using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day11 : Solution<Day11>
{
    public Day11(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        List<long> stones = Input.First()
            .Split(' ')
            .Select(long.Parse)
            .ToList();
        
        Console.WriteLine("Initial arrangement:");
        PrintStones(stones);

        const int blinks = 25;
        for (int i = 0; i < blinks; i++)
        {
            for (int j = 0; j < stones.Count; j++)
            {
                long number = stones[j];
                if (number == 0)
                {
                    stones[j] = 1;
                }
                else if (AreDigitsEven(number, out var strValue, out var digitLength))
                {
                    stones[j] = long.Parse(strValue[..digitLength]);
                    stones.Insert(j + 1, long.Parse(strValue[digitLength..]));
                    j++;
                }
                else
                {
                    stones[j] *= 2024;
                }
            }
            
            Console.WriteLine("Processed Blink {0}.", i);
        }

        return stones.Count;
    }

    public override long Part2()
    {
        Dictionary<long, long> stones = Input.First()
            .Split(' ')
            .Select(long.Parse)
            .ToDictionary(num => num, num => (long)1);

        const int blinks = 75;
        for (int i = 0; i < blinks; i++)
        {
            ProcessBlink(stones);
            // Console.WriteLine("After {0} blink:", i + 1);
            // PrintStones(stones);
        }

        return stones.Values.Sum();
    }

    private void ProcessBlink(Dictionary<long, long> initialStones)
    {
        var workingStones = new Dictionary<long, long>(initialStones);
        foreach (var key in workingStones.Keys)
        {
            if (workingStones[key] == 0) continue; 
            
            initialStones[key] -= workingStones[key];
            if (key == 0)
            {
                UpdateOrAdd(initialStones, 1, workingStones[key]);
            }
            else if (AreDigitsEven(key, out var strValue, out var digitLength))
            {
                UpdateOrAdd(initialStones, long.Parse(strValue[..digitLength]), workingStones[key]);
                UpdateOrAdd(initialStones, long.Parse(strValue[digitLength..]), workingStones[key]);
            }
            else
            {
                UpdateOrAdd(initialStones, key * 2024, workingStones[key]);
            }
        }
    }

    private void UpdateOrAdd(Dictionary<long, long> dict, long key, long value)
    {
        if (!dict.TryAdd(key, value))
        {
            dict[key] += value;
        }
    }

    private bool AreDigitsEven(long number, out string strValue, out int digitLength)
    {
        strValue = number.ToString();
        digitLength = strValue.Length / 2;
        
        return strValue.Length % 2 == 0;
    }

    private void PrintStones(List<long> stones)
    {
        foreach (var stone in stones)
        {
            Console.Write("{0} ", stone);
        }
        
        Console.WriteLine();
    }

    private void PrintStones(Dictionary<long, long> stones, bool includeCount = false)
    {
        foreach (var stone in stones)
        {
            if (stone.Value > 0)
            {
                long count = includeCount ? stone.Value : 1;
                for (int i = 0; i < count; i++)
                {
                    Console.Write("{0} ", stone.Key);
                }
            }
        }
        
        Console.WriteLine();
    }
}