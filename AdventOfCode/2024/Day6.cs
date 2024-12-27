using System.Numerics;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day6 : Solution<Day6>
{
    private readonly InputParser.Mode _parseMode;
    public Day6(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
        this._parseMode = parseMode;
    }

    public override int Part1()
    {
        var visitedPositions = new HashSet<(int, int)>();
        Vector2 guardPos = Vector2.Zero;
        var map = new List<string>();
        int yIndex = 0;
        foreach (string line in Input)
        {
            yIndex++;
            if (line.Contains('^'))
            {
                guardPos = new Vector2(line.IndexOf('^'), yIndex);
            }
            
            map.Add(line);
        }
        
        // While guard still in bounds
        Vector2 direction = -Vector2.UnitY;
        while (InBounds(map, guardPos))
        {
            Vector2 testPos = guardPos + direction;
            if (!InBounds(map, testPos)) break;
            
            if (map[(int)testPos.Y][(int)testPos.X] == '#')
            {
                // Hit wall
                direction = Rotate(direction);
            }
            else
            {
                guardPos = testPos;
                visitedPositions.Add(((int)guardPos.X, (int)guardPos.Y));
            }
        }

        return visitedPositions.Count;
    }
    
    public override int Part2()
    {
        int validPositions = 0;
        Vector2 guardPos;
        Vector2 initialGuardPos = Vector2.Zero;
        var map = new List<string>();
        int yIndex = 0;
        foreach (string line in Input)
        {
            yIndex++;
            if (line.Contains('^'))
            {
                initialGuardPos = new Vector2(line.IndexOf('^'), yIndex);
                guardPos = initialGuardPos;
            }
            
            map.Add(line);
        }
        
        // While guard still in bounds
        int maxSteps = _parseMode == InputParser.Mode.Test ? 50 : 5000;
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                //Console.WriteLine("X: {0} Y: {1}", x, y);
                if (x == (int)initialGuardPos.X && y == (int)initialGuardPos.Y) continue;
                
                bool isStuck = true;
                int steps = 0;
                guardPos = initialGuardPos;
                Vector2 direction = -Vector2.UnitY;
                var visitedPositions = new HashSet<(int, int)>();
                while (InBounds(map, guardPos) && steps < maxSteps * 100)
                {
                    Vector2 testPos = guardPos + direction;
                    if (!InBounds(map, testPos))
                    {
                        isStuck = false;
                        break;
                    }

                    var tx = (int)testPos.X;
                    var ty = (int)testPos.Y;
                    if (steps >= maxSteps * 10 && tx == x && ty == y)
                    {
                        PrintArea(map, visitedPositions, guardPos, new Vector2(x, y), 10);
                        break;
                    }

                    if ((tx == x && ty == y) || map[(int)testPos.Y][(int)testPos.X] == '#')
                    {
                        // Hit wall
                        direction = Rotate(direction);
                    }
                    else
                    {
                        steps++;
                        guardPos = testPos;
                        visitedPositions.Add(((int)guardPos.X, (int)guardPos.Y));
                    }
                }

                if (isStuck)
                {
                    validPositions++;
                }
            }
        }
        
        return validPositions;
    }

    private bool InBounds(IReadOnlyList<string> map, Vector2 position)
    {
        return position.X >= 0 && position.X < map[0].Length && position.Y >= 0 && position.Y < map.Count;
    }

    private Vector2 Rotate(Vector2 vector)
    {
        const float angle = MathF.PI / 2;
        float cosTheta = MathF.Cos(angle);
        float sinTheta = MathF.Sin(angle);
        
        return new Vector2(
            MathF.Round(cosTheta * vector.X - sinTheta * vector.Y),
            MathF.Round(sinTheta * vector.X + cosTheta * vector.Y)
        );
    }

    private void PrintArea(IReadOnlyList<string> map, HashSet<(int, int)> visitedPositions, Vector2 center, Vector2 blocker, int area = 3)
    {
        for (int y = (int)center.Y - area; y < center.Y + area; y++)
        {
            for (int x = (int)center.X - area; x < center.X + area; x++)
            {
                if (InBounds(map, new Vector2(x, y)))
                {
                    char symbol = map[y][x];
                    if (x == (int)center.X && y == (int)center.Y)
                        symbol = '@';
                    else if (x == (int)blocker.X && y == (int)blocker.Y)
                        symbol = '0';
                    else if (visitedPositions.Contains((x, y)))
                        symbol = '*';
                    
                    Console.Write(symbol);
                }
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }
}