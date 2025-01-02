using AdventOfCode.Extensions;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day12 : Solution<Day12>
{
    public Day12(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        var map = GetInputAsCharMap();
        // Fence price = area * perimeter.

        var visitedPositions = new HashSet<Vector2Int>();
        var plotMap = new Dictionary<string, HashSet<Vector2Int>>();

        int totalPrice = 0;
        // Loop through map, flood filling on new plots. Skip visited positions.
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                // Check if plot is already calculated.
                var position = new Vector2Int(x, y);
                if (visitedPositions.Contains(position)) continue;

                var node = map[y][x];
                var plot = FloodFill(map, node, position);
                
                AddPlot(plotMap, plot, node);
                int perimeter = CalculatePerimeter(plot);
                
                // Calculate price.
                totalPrice += plot.Count * perimeter;
                
                // Calculate plot perimeter.
                visitedPositions.AddRange(plot);
            }
        }

        return totalPrice;
    }

    public override long Part2()
    {
        throw new NotImplementedException();
    }

    private HashSet<Vector2Int> FloodFill(char[][] nodeMap, char node, Vector2Int nodePosition)
    {
        var floodFillPositions = new HashSet<Vector2Int>();
        var nodeStack = new Stack<Vector2Int>();
        nodeStack.Push(nodePosition);

        while (nodeStack.Count > 0)
        {
            Vector2Int position = nodeStack.Pop();
            if (TryGetNode(nodeMap, position, out var foundNode)
                && foundNode == node
                && !floodFillPositions.Contains(position))
            {
                floodFillPositions.Add(position);
                nodeStack.Push(new Vector2Int(position.X - 1, position.Y)); // West
                nodeStack.Push(new Vector2Int(position.X + 1, position.Y)); // East
                nodeStack.Push(new Vector2Int(position.X, position.Y - 1)); // North
                nodeStack.Push(new Vector2Int(position.X, position.Y + 1)); // South
            }
        }

        return floodFillPositions;
    }

    private void AddPlot(Dictionary<string, HashSet<Vector2Int>> plotMap, HashSet<Vector2Int> plot, char node)
    {
        int index = 0;
        var key = node + index.ToString();
        while (!plotMap.TryAdd(key, plot))
        {
            index++;
            key = node + index.ToString();
        }
    }

    private int CalculatePerimeter(HashSet<Vector2Int> plot)
    {
        int perimeter = 0;
        foreach (Vector2Int pos in plot)
        {
            if (IsEdge(new Vector2Int(pos.X - 1, pos.Y))) perimeter++;
            if (IsEdge(new Vector2Int(pos.X + 1, pos.Y))) perimeter++;
            if (IsEdge(new Vector2Int(pos.X, pos.Y - 1))) perimeter++;
            if (IsEdge(new Vector2Int(pos.X, pos.Y + 1))) perimeter++;
        }

        return perimeter;
        
        bool IsEdge(Vector2Int position)
        {
            return !plot.Contains(position);
        }
    }

    private bool TryGetNode(char[][] nodeMap, Vector2Int position, out char? node)
    {
        node = null;
        if (position.X < 0 || position.X >= nodeMap[0].Length || position.Y < 0 || position.Y >= nodeMap.Length)
        {
            return false;
        }

        node = nodeMap[position.Y][position.X];
        return true;
    }
}