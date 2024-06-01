﻿// See https://aka.ms/new-console-template for more information
using AdventOfCode;
using AdventOfCode._2015;

var parseMode = Enum.Parse<InputParser.Mode>(args[0], true);
var solutions = new List<Solution>
{
    new Day1(parseMode),
    new Day2(parseMode),
    new Day3(parseMode),
    new Day4(parseMode),
};

solutions[3].Part2();

