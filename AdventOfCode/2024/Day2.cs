using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day2 : Solution<Day2>
{
    public Day2(InputParser.Mode parseMode, int option = 0) : base(parseMode, option) {}

    public override long Part1()
    {
        int safeLevels = 0;
        foreach (var levels in Input.Select(line => line.Split(' ').Select(int.Parse).ToList()))
        {
            if (IsSafe(levels))
            {
                safeLevels++;
            }
        }

        return safeLevels;
    }

    public override long Part2()
    {
        // 329 - 374
        int safeLevels = 0;
        foreach (var levels in Input.Select(line => line.Split(' ').Select(int.Parse).ToList()))
        {
            if (IsSafe(levels))
            {
                safeLevels++;
                continue;
            }

            if (levels.Select((t, i) => levels.Where((_, index) => index != i).ToList()).Any(IsSafe))
            {
                safeLevels++;
            }
        }

        return safeLevels;
    }
    
    private static bool IsDataSafe(List<int> levels, int removalIndex = -1)
    {
        int lastLevel = levels[0];
        bool isDecreasing = false;
        bool isFirst = true;
        for (int i = 1; i < levels.Count; i++)
        {
            if (i == removalIndex) continue;
            
            int currentLevel = levels[i];
            if (Math.Abs(lastLevel - currentLevel) is < 1 or > 3 || lastLevel == currentLevel)
            {
                return false;
            }

            // Either if first value or error was found, order needs to be recalculated.
            if (isFirst)
            {
                isDecreasing = lastLevel - currentLevel > 0;
                isFirst = false;
            }
            else if ((isDecreasing && lastLevel < currentLevel)
                     || (!isDecreasing && lastLevel > currentLevel))
            {
                return false;
            }

            lastLevel = currentLevel;
        }

        // No errors found
        return true;
    }

    private static bool IsSafe(List<int> levels)
    {
        if (!IsIncreasing(levels) && !IsDecreasing(levels)) return false;

        for (int i = 0; i < levels.Count - 1; i++)
        {
            int diff = Math.Abs(levels[i] - levels[i + 1]);
            if (diff is < 1 or > 3)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsIncreasing(List<int> levels)
    {
        for (int i = 1; i < levels.Count; i++)
        {
            if (levels[i] < levels[i - 1]) return false;
        }

        return true;
    }
    
    private static bool IsDecreasing(List<int> levels)
    {
        for (int i = 1; i < levels.Count; i++)
        {
            if (levels[i] > levels[i - 1]) return false;
        }

        return true;
    }
}