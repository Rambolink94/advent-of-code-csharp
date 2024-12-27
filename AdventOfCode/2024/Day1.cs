using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day1 : Solution<Day1>
{
    public Day1(InputParser.Mode parseMode, int option = 0) : base(parseMode, option) {}

    public override int Part1()
    {
        var leftList = new List<int>();
        var rightList = new List<int>();
        foreach (var line in Input)
        {
            var parts = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
            leftList.Add(int.Parse(parts[0]));
            rightList.Add(int.Parse(parts[1]));
        }
        
        leftList.Sort();
        rightList.Sort();

        int total = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            total += Math.Abs(leftList[i] - rightList[i]);
        }

        return total;
    }

    public override int Part2()
    {
        var leftList = new List<int>();
        var rightList = new List<int>();
        foreach (var line in Input)
        {
            var parts = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
            leftList.Add(int.Parse(parts[0]));
            rightList.Add(int.Parse(parts[1]));
        }
        
        leftList.Sort();
        rightList.Sort();

        int similarityScore = 0;
        int foundIndex = 0;
        int endIndex = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            int j = foundIndex;
            bool found = false;
            int foundCount = 0;

            if (leftList[i] != rightList[j])
            {
                j = endIndex;
            }
            
            for (; j < rightList.Count; j++)
            {
                if (rightList[j] == leftList[i])
                {
                    if (!found)
                    {
                        foundIndex = j;
                        found = true;
                    }
                    
                    foundCount++;
                }
                else if (found)
                {
                    similarityScore += leftList[i] * foundCount;
                    endIndex = j;
                    break;
                }
            }
        }

        return similarityScore;
    }
}