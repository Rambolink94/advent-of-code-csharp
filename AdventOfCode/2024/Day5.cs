using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day5 : Solution<Day5>
{
    private Dictionary<int, List<int>> orderingRules = new();
    
    public Day5(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        int updateTotal = 0;
        foreach (string line in Input)
        {
            if (line.Contains('|'))
            {
                var parts = line.Split('|').Select(int.Parse).ToArray();
                if (orderingRules.ContainsKey(parts[0]))
                {
                    orderingRules[parts[0]].Add(parts[1]);
                }
                else
                {
                    orderingRules.Add(parts[0], new List<int> { parts[1] });
                }
            }
            else
            {
                var update = line.Split(',').Select(int.Parse).ToArray();
                if (IsUpdateValid(update))
                {
                    Console.WriteLine(update[update.Length / 2]);
                    updateTotal += update[update.Length / 2];
                }
            }
        }

        return updateTotal;
    }

    public override long Part2()
    {
        var invalidUpdates = new List<List<int>>();
        int updateTotal = 0;
        foreach (string line in Input)
        {
            if (line.Contains('|'))
            {
                var parts = line.Split('|').Select(int.Parse).ToArray();
                if (orderingRules.ContainsKey(parts[0]))
                {
                    orderingRules[parts[0]].Add(parts[1]);
                }
                else
                {
                    orderingRules.Add(parts[0], new List<int> { parts[1] });
                }
            }
            else
            {
                var update = line.Split(',').Select(int.Parse).ToList();
                
                // Determine if it is in the correct order.
                if (!IsUpdateValid(update))
                {
                    invalidUpdates.Add(update);
                }
            }
        }
        
        foreach (var update in invalidUpdates)
        {
            update.Sort((a, b) =>
            {
                if (orderingRules.TryGetValue(a, out var values))
                {
                    if (values.Contains(b))
                    {
                        return -1;
                    }
                }
                    
                if (orderingRules.TryGetValue(b, out values))
                {
                    if (values.Contains(a))
                    {
                        return 1;
                    }
                }
                    
                // If no rules, then it's fine to stay here.
                return 0;
            });
                
            // Make sure update is valid after sorting.
            if (!IsUpdateValid(update))
            {
                Console.WriteLine("STILL INVALID!");
            }
            else
            {
                Console.WriteLine(update[update.Count / 2]);
                updateTotal += update[update.Count / 2];
            }
        }

        return updateTotal;
    }

    private bool IsUpdateValid(IReadOnlyList<int> update)
    {
        // Determine if it is in the correct order.
        for (int i = update.Count - 1; i >= 0; i--)
        {
            for (int j = i; j >= 0; j--)
            {
                if (orderingRules.TryGetValue(update[i], out var values))
                {
                    if (values.Contains(update[j]))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}