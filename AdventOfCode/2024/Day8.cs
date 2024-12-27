using System.Text.RegularExpressions;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day8 : Solution<Day8>
{
    public Day8(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override int Part1()
    {
        var nodeMap = Input.Select(line => line.ToCharArray()).ToArray();
        var antiNodeMap = new HashSet<Vector2Int>();
        var foundNodes = new Dictionary<char, List<Vector2Int>>();
        for (int y = 0; y < nodeMap.Length; y++)
        {
            for (int x = 0; x < nodeMap[y].Length; x++)
            {
                var node = nodeMap[y][x];
                var nodePosition = new Vector2Int(x, y);
                if (Regex.IsMatch(node.ToString(), "[a-zA-Z0-9]"))
                {
                    if (foundNodes.TryGetValue(node, out var foundNode))
                    {
                        foreach (Vector2Int position in foundNode)
                        {
                            // Only place anti-nodes at locations with a distance of 2.
                            var distance = CalculateDistance(x, y, position.X, position.Y);
                            if (distance >= 1)
                            {
                                Vector2Int antiNode1 = 2 * position - nodePosition;
                                Vector2Int antiNode2 = 2 * nodePosition - position;

                                if (InNodeMapRange(nodeMap, antiNode1)) antiNodeMap.Add(antiNode1);
                                if (InNodeMapRange(nodeMap, antiNode2)) antiNodeMap.Add(antiNode2);
                            }
                        }
                        
                        foundNodes[node].Add(nodePosition);
                    }
                    else
                    {
                        foundNodes.Add(node, new List<Vector2Int> { nodePosition });
                    }
                }
            }
        }

        PrintMap(nodeMap, antiNodeMap);

        return antiNodeMap.Count;
    }

    public override int Part2()
    {
        var nodeMap = Input.Select(line => line.ToCharArray()).ToArray();
        var antiNodeMap = new HashSet<Vector2Int>();
        var foundNodes = new Dictionary<char, List<Vector2Int>>();
        // Gather Nodes
        for (int y = 0; y < nodeMap.Length; y++)
        {
            for (int x = 0; x < nodeMap[y].Length; x++)
            {
                var node = nodeMap[y][x];
                var nodePosition = new Vector2Int(x, y);
                if (Regex.IsMatch(node.ToString(), "[a-zA-Z0-9]"))
                {
                    if (foundNodes.TryGetValue(node, out var foundNode))
                    {
                        foundNodes[node].Add(nodePosition);
                    }
                    else
                    {
                        foundNodes.Add(node, new List<Vector2Int> { nodePosition });
                    }
                }
            }
        }

        foreach (var node in foundNodes)
        {
            
        }
        
        PrintMap(nodeMap, antiNodeMap);

        return antiNodeMap.Count;
    }

    private void PrintMap(char[][] nodeMap, IReadOnlySet<Vector2Int> antiNodeMap)
    {
        for (int y = 0; y < nodeMap.Length; y++)
        {
            for (int x = 0; x < nodeMap[y].Length; x++)
            {
                char c = nodeMap[y][x];
                if (antiNodeMap.Contains(new Vector2Int(x, y))) c = '#';
                else if (c == '#') c = '.';
                Console.Write(c);
            }
            
            Console.WriteLine();
        }
    }
    
    private bool InNodeMapRange(char[][] nodeMap, Vector2Int position)
    {
        return position.X >= 0
               && position.X < nodeMap[0].Length
               && position.Y >= 0
               && position.Y < nodeMap.Length;
    }

    private int CalculateDistance(int x1, int y1, int x2, int y2)
    {
        return (int)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }
}