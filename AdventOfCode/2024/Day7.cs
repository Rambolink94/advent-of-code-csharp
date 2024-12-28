using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day7 : Solution<Day7>
{
    public Day7(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        long calibrationTotal = 0;
        var operators = new[] { "+", "*" };
        foreach (var line in Input)
        {
            var parts = line.Split(':');
            var expectedValue = long.Parse(parts[0]);
            var operands = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            var results = new List<(long Result, string Equation)>();
            GenerateCombo(operators, operands, 1, operands[0], operands[0].ToString(), results);

            foreach ((long Result, string Equation) result in results.DistinctBy(a => a.Result)
                         .Where((result, equation) => result.Result == expectedValue))
            {
                Console.WriteLine($"{expectedValue}: {result.Equation} = {result.Result}");
                calibrationTotal += result.Result;
            }
        }

        // 12918052603 - Wrong (too low)
        // 2654750657661 - Wrong (too high)
        // 2654749936343 - Correct!
        Console.WriteLine(calibrationTotal);
        return 0;
        
        // Lessons Learned: Be careful with longs and ints. Conversions can occur and data lost without warning.
    }

    private void GenerateCombo(string[] operators, long[] operands, int index, long currentResult, string currentEquation,
        ICollection<(long, string)> results)
    {
        if (index == operands.Length)
        {
            results.Add((currentResult, currentEquation));
            return;
        }

        foreach (var @operator in operators)
        {
            switch (@operator)
            {
                case "+":
                    GenerateCombo(operators, operands, index + 1, currentResult + operands[index],
                        $"{currentEquation} + {operands[index]}", results);
                    break;
                case "*":
                    GenerateCombo(operators, operands, index + 1, currentResult * operands[index],
                        $"{currentEquation} * {operands[index]}", results);
                    break;
                case "||":
                    var newResult = currentResult + operands[index].ToString();
                    GenerateCombo(operators, operands, index + 1, long.Parse(newResult),
                        $"{currentEquation} || {operands[index]}", results);
                    break;
            }
        }
    }

    public override long Part2()
    {
        long calibrationTotal = 0;
        var operators = new[] { "+", "*", "||" };
        foreach (var line in Input)
        {
            var parts = line.Split(':');
            var expectedValue = long.Parse(parts[0]);
            var operands = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            var results = new List<(long Result, string Equation)>();
            GenerateCombo(operators, operands, 1, operands[0], operands[0].ToString(), results);

            foreach ((long Result, string Equation) result in results.DistinctBy(a => a.Result)
                         .Where((result, equation) => result.Result == expectedValue))
            {
                Console.WriteLine($"{expectedValue}: {result.Equation} = {result.Result}");
                calibrationTotal += result.Result;
            }
        }

        // 12918052603 - Wrong (too low)
        // 2654750657661 - Wrong (too high)
        // 2654749936343 - Correct!
        Console.WriteLine(calibrationTotal);
        return 0;
    }
}