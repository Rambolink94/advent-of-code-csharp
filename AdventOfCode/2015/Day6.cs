using AdventOfCode.Utility;

namespace AdventOfCode._2015;

public class Day6 : Solution<Day6>
{
    public Day6(InputParser.Mode parseMode, int option = 0) : base(parseMode, option) {}
    
    public override int Part1()
    {
        var lights = CalculateLights<bool>((instruction, originalValue) => instruction switch
        {
            0 => false,
            1 => true,
            2 => !originalValue,
            _ => throw new InvalidOperationException($"Invalid instruction: {instruction}"),
        });

        return lights.Count(x => x);
    }

    public override int Part2()
    {
        var lights = CalculateLights<int>((instruction, originalValue) => instruction switch
        {
            0 => Math.Clamp(originalValue - 1, 0, int.MaxValue),
            1 => originalValue + 1,
            2 => originalValue + 2,
            _ => throw new InvalidOperationException($"Invalid instruction: {instruction}"),
        });

        return lights.Sum();
    }

    private T[] CalculateLights<T>(Func<int, T, T> instructor)
    {
        const int length = 1000;
        var lights = new T[length * length];
        foreach (var line in Input)
        {
            var parts = line.Split(',');
            var instructionParts = parts[0].Split(' ');
            var rangeParts = parts[1].Split(' ');
            
            int instruction = 0;    // 0 = off, 1 = on, 2 = toggle
            if (instructionParts.Length > 2)
                instruction = instructionParts[1] == "on" ? 1 : 0;
            else
                instruction = 2;

            (int X, int Y) coord1 = (int.Parse(instructionParts[^1]), int.Parse(rangeParts[0]));
            (int X, int Y) coord2 = (int.Parse(rangeParts[^1]), int.Parse(parts[^1]));

            for (int y = coord1.Y; y <= coord2.Y; y++)
            {
                for (int x = coord1.X; x <= coord2.X; x++)
                {
                    lights[y * length + x] = instructor(instruction, lights[y * length + x]);
                }
            }
        }

        return lights;
    }
}