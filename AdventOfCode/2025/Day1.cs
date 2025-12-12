using AdventOfCode.Utility;

namespace AdventOfCode._2025;

public class Day1(InputParser.Mode parseMode, int option = 0) : Solution<Day1>(parseMode, option)
{
    public override long Part1()
    {
        int totalZeros = 0;
        int currentValue = 50;
        foreach (string line in Input)
        {
            string direction = line[0].ToString();
            int distance = int.Parse(line[1..]);
            currentValue += direction == "R" ? distance : -distance;
            
            // Clamp value between 0-99
            while (currentValue >= 100) currentValue -= 100;
            while (currentValue < 0) currentValue += 100;
            
            if (currentValue == 0) totalZeros++;
            
            Console.WriteLine($"- The dial is rotated `{line}` to point at `{currentValue}`.");
        }
        
        return totalZeros;
    }
    
    public override long Part2()
    {
        int totalZeros = 0;
        int dialValue = 50;
        foreach (string line in Input)
        {
            int startingDial = dialValue;
            int startingZeros = totalZeros;
            
            string direction = line[0].ToString();
            int distance = int.Parse(line[1..]);

            totalZeros += distance / 100;
            distance %= 100;
            
            dialValue += direction == "R" ? distance : -distance;

            switch (dialValue)
            {
                case >= 100:
                    if (startingDial != 0) totalZeros++;
                    dialValue -= 100;
                    break;
                case < 0:
                    if (startingDial != 0) totalZeros++;
                    dialValue += 100;
                    break;
                case 0:
                    totalZeros++;
                    break;
            }
            
            string timesString = $" during this rotation it points at `0` {totalZeros - startingZeros} time(s).";
            Console.WriteLine($"- The dial is rotated `{line}` to point at `{dialValue}`" +
                              $";{(totalZeros - startingZeros > 0 ? timesString : "")}");
        }
        
        return totalZeros;
    }
}