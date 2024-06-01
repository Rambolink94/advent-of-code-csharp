namespace AdventOfCode._2015;

public class Day5 : Solution
{
    public Day5(InputParser.Mode parseMode) : base(parseMode) {}

    private string vowels = "aeiou";
    private string[] invalidStrings =
    {
        "ab",
        "cd",
        "pq",
        "xy",
    };
    
    public override void Part1()
    {
        int niceStrings = 0;
        foreach (var line in InputParser.GetInputRaw(_parseMode))
        {
            int vowelCount = 0;
            int doubleCount = 0;
            char lastChar = ' ';
            bool isNice = false;
            foreach (var c in line)
            {
                if (vowels.Contains(c))
                    vowelCount++;
                if (c == lastChar)
                    doubleCount++;
                
                if (vowelCount >= 3 && doubleCount > 0)
                    isNice = true;

                var pair = new string(new [] { lastChar, c });
                if (invalidStrings.Contains(pair))
                {
                    isNice = false;
                    break;
                }

                lastChar = c;
            }

            niceStrings += isNice ? 1 : 0;
        }
        
        Console.WriteLine(niceStrings);
    }

    public override void Part2()
    {
        throw new NotImplementedException();
    }
}