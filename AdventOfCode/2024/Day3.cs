using System.Text.RegularExpressions;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day3 : Solution<Day3>
{
    public Day3(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        int total = 0;
        foreach (var chars in Input.Select(line => line.ToCharArray()))
        {
            var pattern = new Regex(@"^[0-9mul(),]*$");
            
            bool foundMul = false;
            for (int i = 0; i < chars.Length; i++)
            {
                if (!pattern.IsMatch(chars[i].ToString()))
                {
                    // Invalidate
                    foundMul = false;
                    continue;
                }
                
                if (i + 3 < chars.Length && chars.AsSpan(i, 3).SequenceEqual("mul".AsSpan()))
                {
                    foundMul = true;
                    i += 3;
                }

                if (!foundMul) continue;
                
                string[] buffer = { "", "" };
                if (chars[i] == '(')
                {
                    i++;
                    int bufferIndex = 0;
                    for (; i < chars.Length; i++)
                    {
                        if (!pattern.IsMatch(chars[i].ToString()))
                        {
                            break;
                        }

                        if (char.IsDigit(chars[i]))
                        {
                            buffer[bufferIndex] += chars[i];
                        }
                        else if (chars[i] == ',')
                        {
                            bufferIndex++;
                        }
                        else if (chars[i] == ')' && buffer[bufferIndex].Length > 0)
                        {
                            total += int.Parse(buffer[0]) * int.Parse(buffer[1]);
                            break;
                        }
                    }
                }
            }
        }

        return total;
    }
    
    public override long Part2()
    {
        int total = 0;
        bool foundMul = false;
        bool mulActive = true;
        foreach (var chars in Input.Select(line => line.ToCharArray()))
        {
            var pattern = new Regex(@"^[0-9muld(),]*$");
            for (int i = 0; i < chars.Length; i++)
            {
                if (!pattern.IsMatch(chars[i].ToString()))
                {
                    // Invalidate
                    foundMul = false;
                    continue;
                }
                
                if (IsOperator(chars, i, "don't()"))
                {
                    mulActive = false;
                    i += 7;
                }
                else if (IsOperator(chars, i, "do()"))
                {
                    mulActive = true;
                    i += 4;
                }
                
                if (IsOperator(chars, i, "mul"))
                {
                    foundMul = true;
                    i += 3;
                }

                if (!foundMul || !mulActive) continue;
                
                string[] buffer = { "", "" };
                if (chars[i] == '(')
                {
                    i++;
                    int bufferIndex = 0;
                    for (; i < chars.Length; i++)
                    {
                        if (!pattern.IsMatch(chars[i].ToString()))
                        {
                            break;
                        }

                        if (char.IsDigit(chars[i]))
                        {
                            buffer[bufferIndex] += chars[i];
                        }
                        else if (chars[i] == ',' && bufferIndex != 1)
                        {
                            bufferIndex++;
                        }
                        else if (chars[i] == ')' && buffer[bufferIndex].Length > 0)
                        {
                            Console.WriteLine("MUL({0}, {1})", buffer[0], buffer[1]);
                            total += int.Parse(buffer[0]) * int.Parse(buffer[1]);
                            break;
                        }
                    }
                }
            }
        }

        return total;
    }

    private bool IsOperator(char[] chars, int currentIndex, string searchString)
    {
        int length = searchString.Length;
        return currentIndex + length < chars.Length &&
               chars.AsSpan(currentIndex, length).SequenceEqual(searchString.AsSpan());
    }
}