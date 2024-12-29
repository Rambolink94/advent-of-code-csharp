using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day10 : Solution<Day10>
{
    public Day10(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        var map = Input.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        Vector2Int[] possibleDirections =
        {
            new(-1, 0),
            new(0, -1),
            new(1, 0),
            new(0, 1)
        };

        int totalScore = 0;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 0)
                {
                    var reachedPositions = new HashSet<Vector2Int>();
                    foreach (Vector2Int direction in possibleDirections)
                    {
                        TraversePath(map, new Vector2Int(x, y), direction, possibleDirections, reachedPositions);
                    }

                    totalScore += reachedPositions.Count;
                }
            }
        }

        return totalScore;
    }

    public override long Part2()
    {
        var map = Input.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        Vector2Int[] possibleDirections =
        {
            new(-1, 0),
            new(0, -1),
            new(1, 0),
            new(0, 1)
        };

        int totalRating = 0;
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 0)
                {
                    int rating = 0;
                    foreach (Vector2Int direction in possibleDirections)
                    {
                        TraversePath2(map, new Vector2Int(x, y), direction, possibleDirections, ref rating);
                    }

                    totalRating += rating;
                }
            }
        }

        return totalRating;
    }

    private void TraversePath(int[][] map, Vector2Int position, Vector2Int direction, Vector2Int[] possibleDirections,
        HashSet<Vector2Int> reachedPositions)
    {
        if (!IsDirectionValid(map, position, direction))
        {
            // Trail broke.
            return;
        }

        // End of trail.
        Vector2Int newPosition = position + direction;
        if (GetAltitude(map, newPosition) == 9)
        {
            reachedPositions.Add(newPosition);
        }

        // Try all valid directions.
        foreach (Vector2Int dir in possibleDirections)
        {
            TraversePath(map, newPosition, dir, possibleDirections, reachedPositions);
        }
    }
    
    private void TraversePath2(int[][] map, Vector2Int position, Vector2Int direction, Vector2Int[] possibleDirections,
        ref int rating)
    {
        if (!IsDirectionValid(map, position, direction))
        {
            // Trail broke.
            return;
        }

        // End of trail.
        Vector2Int newPosition = position + direction;
        if (GetAltitude(map, newPosition) == 9)
        {
            rating++;
        }

        // Try all valid directions.
        foreach (Vector2Int dir in possibleDirections)
        {
            TraversePath2(map, newPosition, dir, possibleDirections, ref rating);
        }
    }

    private bool IsOnMap(int[][] map, Vector2Int position)
    {
        return position.X >= 0 && position.X < map[0].Length && position.Y >= 0 && position.Y < map.Length;
    }

    private bool IsDirectionValid(int[][] map, Vector2Int position, Vector2Int direction)
    {
        Vector2Int newPos = position + direction;
        if (IsOnMap(map, newPos))
        {
            // If altitude is 1 more than current.
            int newAltitude = GetAltitude(map, newPos);
            return newAltitude == GetAltitude(map, position) + 1;
        }

        return false;
    }

    private int GetAltitude(int[][] map, Vector2Int position)
    {
        return map[position.Y][position.X];
    }
}