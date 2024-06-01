// See https://aka.ms/new-console-template for more information
using AdventOfCode;
using AdventOfCode._2015;
using AdventOfCode.Utility;

var parseMode = Enum.Parse<InputParser.Mode>(args[0], true);
var solutions = new List<Solution>
{
    new Day1(parseMode),
    new Day2(parseMode),
    new Day3(parseMode),
    new Day4(parseMode),
    new Day5(parseMode),
    new Day6(parseMode),
};

var result = solutions[5].Part2();
Console.WriteLine(result);

