using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day13 : Solution<Day13>
{
    public Day13(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        const int aTokenCost = 3;
        const int bTokenCost = 1;

        (long X, long Y) buttonA = (0, 0);
        (long X, long Y) buttonB = (0, 0);
        int totalTokens = 0;
        foreach (var line in Input)
        {
            if (line.Length < 1) continue;

            string[] parts = line.Split(':')[1].Split(',');
            if (line.Contains("A:"))
            {
                buttonA = (
                    long.Parse(parts[0].Split('+')[1]),
                    long.Parse(parts[1].Split('+')[1]));
            }
            else if (line.Contains("B:"))
            {
                buttonB = (
                    long.Parse(parts[0].Split('+')[1]),
                    long.Parse(parts[1].Split('+')[1]));
            }
            else
            {
                var prizeLocation = (
                    long.Parse(parts[0].Split('=')[1]),
                    long.Parse(parts[1].Split('=')[1]));

                (decimal X, decimal Y) solution = CramersRule(buttonA, buttonB, prizeLocation);
                
                // Move on if solution isn't a whole number.
                if (solution.X % 1 != 0 || solution.Y % 1 != 0) continue;

                totalTokens += aTokenCost * (int)solution.X + bTokenCost * (int)solution.Y;
            }
        }

        // 33068 : Incorrect
        // 29187 : Correct
        return totalTokens;
    }

    public override long Part2()
    {
        const long offset = 10000000000000;
        const long aTokenCost = 3;
        const long bTokenCost = 1;

        (long X, long Y) buttonA = (0, 0);
        (long X, long Y) buttonB = (0, 0);
        long totalTokens = 0;
        foreach (var line in Input)
        {
            if (line.Length < 1) continue;

            string[] parts = line.Split(':')[1].Split(',');
            if (line.Contains("A:"))
            {
                buttonA = (
                    long.Parse(parts[0].Split('+')[1]),
                    long.Parse(parts[1].Split('+')[1]));
            }
            else if (line.Contains("B:"))
            {
                buttonB = (
                    long.Parse(parts[0].Split('+')[1]),
                    long.Parse(parts[1].Split('+')[1]));
            }
            else
            {
                var prizeLocation = (
                    long.Parse(parts[0].Split('=')[1]) + offset,
                    long.Parse(parts[1].Split('=')[1]) + offset);

                (decimal X, decimal Y) solution = CramersRule(buttonA, buttonB, prizeLocation);
                
                // Move on if solution isn't a whole number.
                if (solution.X % 1 != 0 || solution.Y % 1 != 0) continue;

                totalTokens += aTokenCost * (long)solution.X + bTokenCost * (long)solution.Y;
            }
        }
        
        // 102879832810557 : Incorrect - High
        // 99968222587852  : Correct
        return totalTokens;
    }

    private (decimal X, decimal Y) CramersRule((long X, long Y) a, (long X, long Y) b, (long X, long Y) prizePos)
    {
        checked
        {
            var aX1 = a.X;
            var aX2 = b.X;
            var bX1 = a.Y;
            var bX2 = b.Y;

            var determinant = aX1 * bX2 - aX2 * bX1;
            var determinantA = prizePos.X * bX2 - aX2 * prizePos.Y;
            var determinantB = aX1 * prizePos.Y - prizePos.X * bX1;

            var x1 = determinantA / (decimal)determinant;
            var x2 = determinantB / (decimal)determinant;

            return (x1, x2);
        }
    }
}