using System.Data;
using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day15 : Solution<Day15>
{
    public Day15(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        var map = new List<char[]>();
        var moves = new List<Vector2Int>();
        var robotPos = Vector2Int.Zero;
        bool parsingMap = true;
        foreach (var line in Input)
        {
            if (line[0] != '#')
            {
                parsingMap = false;
            }
            
            if (parsingMap)
            {
                char[] chars = line.ToCharArray();
                if (robotPos == Vector2Int.Zero)
                {
                    var index = Array.FindIndex(chars, c => c == '@');
                    if (index >= 0)
                    {
                        robotPos = new Vector2Int(index, map.Count);
                        chars[index] = '.'; // Remove from map.
                    }
                }
                
                map.Add(chars);
            }
            else
            {
                var vectors = line.Select(c =>
                {
                    return c switch
                    {
                        '<' => new Vector2Int(-1, 0),
                        '>' => new Vector2Int(1, 0),
                        '^' => new Vector2Int(0, -1),
                        'v' => new Vector2Int(0, 1),
                        _ => throw new DataException($"'{c}' is an invalid character.")
                    };
                });
                
                moves.AddRange(vectors);
            }
        }

        PrintMap(map, robotPos);
        foreach (Vector2Int move in moves)
        {
            robotPos = Move(map, robotPos, move);
        }
        PrintMap(map, robotPos);

        // 1526018 : Correct
        return map.SelectMany((row, y) => row.Select((c, x) => c == 'O' ? 100 * y + x : 0)).Sum();
    }

    public override long Part2()
    {
        throw new NotImplementedException();
    }

    private Vector2Int Move(List<char[]> map, Vector2Int originalPosition, Vector2Int movementDirection)
    {
        Vector2Int newPosition = originalPosition + movementDirection;
        
        // Blocked
        if (IsWall(map, newPosition)) return originalPosition;

        if (IsBox(map, newPosition))
        {
            // Handle moving boxes.
            if (!MoveBox(map, newPosition, movementDirection))
            {
                return originalPosition;
            }
        }

        return newPosition;
    }

    private bool MoveBox(List<char[]> map, Vector2Int boxPosition, Vector2Int movementDirection)
    {
        Vector2Int newPosition = boxPosition + movementDirection;

        if (IsWall(map, newPosition)) return false;

        if (IsBox(map, newPosition))
        {
            if (!MoveBox(map, newPosition, movementDirection))
            {
                return false;
            }
        }

        map[boxPosition.Y][boxPosition.X] = '.';
        map[newPosition.Y][newPosition.X] = 'O';

        return true;
    }

    private bool IsWall(List<char[]> map, Vector2Int position)
    {
        return map[position.Y][position.X] == '#';
    }
    
    private bool IsBox(List<char[]> map, Vector2Int position)
    {
        return map[position.Y][position.X] == 'O';
    }

    private void PrintMap(List<char[]> map, Vector2Int robotPosition)
    {
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                Console.Write(robotPosition == new Vector2Int(x, y) ? '@' : map[y][x]);
            }
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }
}