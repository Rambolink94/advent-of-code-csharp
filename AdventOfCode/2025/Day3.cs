using AdventOfCode.Utility;

namespace AdventOfCode._2025;

public class Day3(InputParser.Mode parseMode, int option = 0) : Solution<Day3>(parseMode, option)
{
    public override long Part1()
    {
        long totalJoltage = 0;
        foreach (string bank in Input)
        {
            var leftValue = bank[0];
            var rightValue = bank[1];
            for (int i = 2; i < bank.Length; i++)
            {
                var currentValue = bank[i];
                if (rightValue > leftValue)
                {
                    leftValue = rightValue;
                    rightValue = currentValue;
                    continue;
                }
                
                if (currentValue > rightValue)
                {
                    rightValue = currentValue;
                }
            }
            
            totalJoltage += int.Parse(leftValue.ToString() + rightValue);
        }
        
        return totalJoltage;
    }

    public override long Part2()
    {
        long totalJoltage = 0;
        foreach (string bank in Input)
        {
            int safeIndex = 11;
            var joltageMap = new char[12];
            var bestIndex = -1;
            for (int j = 0; j < 12; j++)
            {
                bestIndex++;
                for (int i = bestIndex; i < bank.Length - safeIndex; i++)
                {
                    if (bank[i] > bank[bestIndex])
                    {
                        bestIndex = i;
                    }

                }

                safeIndex--;
                joltageMap[j] = bank[bestIndex];
            }

            Console.WriteLine(joltageMap);
            totalJoltage += long.Parse(joltageMap);
        }
        
        return totalJoltage;
    }
}