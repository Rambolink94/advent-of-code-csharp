﻿using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015;

public class Day4 : Solution
{
    public Day4(InputParser.Mode parseMode) : base(parseMode) {}

    public override void Part1()
    {
        int value = -1;
        foreach (var line in InputParser.GetInputRaw(_parseMode))
        {
            value = ComputeLowestKeyNumber(line, 5);
        }
        
        Console.WriteLine(value);
    }

    public override void Part2()
    {
        int value = -1;
        foreach (var line in InputParser.GetInputRaw(_parseMode))
        {
            value = ComputeLowestKeyNumber(line, 6);
        }
        
        Console.WriteLine(value);
    }

    private int ComputeLowestKeyNumber(string key, int zeroCount)
    {
        for (int i = 0; i < int.MaxValue; i++)
        {
            var md5 = Md5(key + i);
            if (md5[..zeroCount] == new string('0', zeroCount))
                return i;
        }

        return -1;
    }
    
    private string Md5(string key)
    {

        using var md5 = MD5.Create();
        var keyBytes = Encoding.ASCII.GetBytes(key);
        var hashBytes = MD5.HashData(keyBytes);

        return Convert.ToHexString(hashBytes);
    }
}