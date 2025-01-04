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
        // Fence price = area * perimeter.
        var map = GetInputAsCharMap();

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
        // TODO: Calculate normal while on edge. Recurse on corners.
        // Fence price = area * perimeter.
        var map = GetInputAsCharMap();

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
                int sides = CalculateSides(map, plot);
                
                // Calculate price.
                totalPrice += plot.Count * sides;
                
                // Calculate plot perimeter.
                visitedPositions.AddRange(plot);
            }
        }

        return totalPrice;
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
            if (IsEdge(plot, new Vector2Int(pos.X - 1, pos.Y))) perimeter++;
            if (IsEdge(plot, new Vector2Int(pos.X + 1, pos.Y))) perimeter++;
            if (IsEdge(plot, new Vector2Int(pos.X, pos.Y - 1))) perimeter++;
            if (IsEdge(plot, new Vector2Int(pos.X, pos.Y + 1))) perimeter++;
        }

        return perimeter;
    }

    private int CalculateSides(char[][] nodeMap, HashSet<Vector2Int> plot)
    {
        int sides = 0;
        var directions = new Vector2Int[]
        {
            new (-1, 0), // Left
            new (0, -1), // Up
            new (1, 0),  // Right
            new (0, 1)   // Down
        };
        
        foreach (Vector2Int direction in directions)
        {
            var edges = plot.Where(pos => IsEdge(plot, pos + direction));

            var group = Math.Abs(direction.X) > 0
                ? edges.GroupBy(edge => edge.X, edge => edge.Y).Select(group => group.OrderBy(y => y).ToList())
                : edges.GroupBy(edge => edge.Y, edge => edge.X).Select(group => group.OrderBy(y => y).ToList());
            
            sides += group.Sum(grouping => grouping.Zip(grouping.Skip(1), (current, next) => Math.Abs(next - current))
                .Count(gap => gap > 1) + 1);
        }

        return sides;
        
        // If corner, you have two paths to follow.
        // While normal is same and not corner, still on side.
        // When corner found, add side.
        // Start from corner
        Vector2Int currentCorner = plot.First(pos => IsCorner(plot, pos));
        var traversedCorners = new HashSet<Vector2Int>();
        Vector2Int edgePosition = GetEdges(plot, currentCorner).Last();
        while (!traversedCorners.Contains(edgePosition))
        {
            traversedCorners.Add(edgePosition);
            
            Vector2Int normal = edgePosition - currentCorner;
            Vector2Int traversalDirection = normal.Rotate(90); // Get direction right of normal.
            currentCorner = TraverseSide(currentCorner, normal, traversalDirection, plot);
            edgePosition = GetEdges(plot, currentCorner).Last();
        }

        return traversedCorners.Count;
    }

    private Vector2Int TraverseSide(Vector2Int startingPosition, Vector2Int normal, Vector2Int traversalDirection, HashSet<Vector2Int> plot)
    {
        Vector2Int testPosition = startingPosition + traversalDirection;
        while (IsEdge(plot, testPosition + normal) && !IsCorner(plot, testPosition))
        {
            testPosition += traversalDirection;
        }
        
        // TODO: Make sure last test position was in fact a corner.
        return testPosition;
    }
    
    bool IsEdge(HashSet<Vector2Int> plot, Vector2Int position)
    {
        return !plot.Contains(position);
    }

    bool IsCorner(HashSet<Vector2Int> plot, Vector2Int position)
    {
        return GetEdges(plot, position).Count >= 2;
    }

    List<Vector2Int> GetEdges(HashSet<Vector2Int> plot, Vector2Int position)
    {
        var edgePositions = new List<Vector2Int>();
        if (!plot.Contains(position)) return edgePositions;

        var westEdge = new Vector2Int(position.X - 1, position.Y);
        var northEdge = new Vector2Int(position.X, position.Y - 1);
        var eastEdge = new Vector2Int(position.X + 1, position.Y);
        var southEdge = new Vector2Int(position.X, position.Y + 1);
        
        if (IsEdge(plot, westEdge)) edgePositions.Add(westEdge);
        if (IsEdge(plot, northEdge)) edgePositions.Add(northEdge);
        if (IsEdge(plot, eastEdge)) edgePositions.Add(eastEdge);
        if (IsEdge(plot, southEdge)) edgePositions.Add(southEdge);

        return edgePositions;
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