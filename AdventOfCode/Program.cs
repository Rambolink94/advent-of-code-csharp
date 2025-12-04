// See https://aka.ms/new-console-template for more information

using AdventOfCode;
using AdventOfCode._2015;
using AdventOfCode.Utility;

var parseMode = Enum.Parse<InputParser.Mode>("TEST", true);
// var parseMode = Enum.Parse<InputParser.Mode>("REAL", true);
var solutions2015 = new List<Solution>
{
    new Day1(parseMode),
    new Day2(parseMode),
    new Day3(parseMode),
    new Day4(parseMode),
    new Day5(parseMode),
    new Day6(parseMode),
    new Day7(parseMode),
};

var solutions2024 = new List<Solution>
{
    new AdventOfCode._2024.Day1(parseMode),
    new AdventOfCode._2024.Day2(parseMode),
    new AdventOfCode._2024.Day3(parseMode),
    new AdventOfCode._2024.Day4(parseMode),
    new AdventOfCode._2024.Day5(parseMode),
    new AdventOfCode._2024.Day6(parseMode),
    new AdventOfCode._2024.Day7(parseMode),
    new AdventOfCode._2024.Day8(parseMode),
    new AdventOfCode._2024.Day9(parseMode),
    new AdventOfCode._2024.Day10(parseMode),
    new AdventOfCode._2024.Day11(parseMode),
    new AdventOfCode._2024.Day12(parseMode),
    new AdventOfCode._2024.Day13(parseMode),
    new AdventOfCode._2024.Day14(parseMode),
    new AdventOfCode._2024.Day15(parseMode, 3),
};

var result = solutions2024[^1].Part2();
Console.WriteLine(result);