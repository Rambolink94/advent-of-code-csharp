using System.Text;
using AdventOfCode.Utility;

namespace AdventOfCode._2015;

public class Day5 : Solution<Day5>
{
    public Day5(InputParser.Mode parseMode, int option = 0) : base(parseMode, option) {}
    
    public override int Part1()
    {
        string vowels = "aeiou";
        string[] invalidStrings =
        {
            "ab",
            "cd",
            "pq",
            "xy",
        };
        int niceStrings = 0;
        foreach (var line in Input)
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

        return niceStrings;
    }

    public override int Part2()
    {
        int niceStrings = 0;
        foreach (var line in Input)
        {
            int offsetRepeats = 0;
            int pairRepeats = 0;
            var charHistory = new char[line.Length];
            var builder = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (i > 1 && charHistory[i - 2] == c)
                {
                    builder.AppendLine($" - {charHistory[i - 2]}{charHistory[i - 1]}{c}");
                    offsetRepeats++;
                }

                for (int j = 2; j <= i; j++)
                {
                    if (charHistory[j - 2] == charHistory[i - 1]
                        && charHistory[j - 1] == c
                        && i != j)
                    {
                        builder.AppendLine($" - {charHistory[j - 2]}{charHistory[j - 1]}({j - 2},{j - 1}) " +
                                           $": {charHistory[i - 1]}{c}({i - 1},{i})");
                        pairRepeats++;
                    }
                }

                charHistory[i] = c;
            }

            var nice = offsetRepeats > 0 && pairRepeats > 0;
            Console.WriteLine($"{line} - {nice}");
            Console.WriteLine(builder.ToString());
            niceStrings += nice ? 1 : 0;
        }

        return niceStrings;
    }
}