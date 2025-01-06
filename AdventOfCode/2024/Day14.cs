using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day14 : Solution<Day14>
{
    private InputParser.Mode _parseMode;
    public Day14(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
        _parseMode = parseMode;
    }

    public override long Part1()
    {
        Vector2Int mapSize = _parseMode == InputParser.Mode.Test
            ? new Vector2Int(11, 7)
            : new Vector2Int(101, 103);
        
        var robots = new List<Robot>();
        foreach (var line in Input)
        {
            var robot = line
                .Split(' ')
                .Select(parts => parts.Split('=')[1].Split(','))
                .Select(parts => new Vector2Int(int.Parse(parts[0]), int.Parse(parts[1])))
                .Chunk(2)
                .Select(vectors => new Robot
                {
                    Position = vectors[0],
                    Velocity = vectors[1]
                })
                .First();
            
            robots.Add(robot);
        }

        SimulateRobots(robots, mapSize, 100);
        
        // Calculate Quadrant totals.
        int quadrant1 = 0;
        int quadrant2 = 0;
        int quadrant3 = 0;
        int quadrant4 = 0;
        int halfWidth = mapSize.X / 2;
        int halfHeight = mapSize.Y / 2;
        foreach (Robot robot in robots)
        {
            if (robot.Position.X < halfWidth && robot.Position.Y < halfHeight)
                quadrant1++;
            else if (robot.Position.X > halfWidth && robot.Position.Y < halfHeight)
                quadrant2++;
            else if (robot.Position.X < halfWidth && robot.Position.Y > halfHeight)
                quadrant3++;
            else if (robot.Position.X > halfWidth && robot.Position.Y > halfHeight)
                quadrant4++;
        }

        return quadrant1 * quadrant2 * quadrant3 * quadrant4;
    }

    public override long Part2()
    {
        Vector2Int mapSize = _parseMode == InputParser.Mode.Test
            ? new Vector2Int(11, 7)
            : new Vector2Int(101, 103);
        
        var robots = new List<Robot>();
        foreach (var line in Input)
        {
            var robot = line
                .Split(' ')
                .Select(parts => parts.Split('=')[1].Split(','))
                .Select(parts => new Vector2Int(int.Parse(parts[0]), int.Parse(parts[1])))
                .Chunk(2)
                .Select(vectors => new Robot
                {
                    Position = vectors[0],
                    Velocity = vectors[1]
                })
                .First();
            
            robots.Add(robot);
        }

        // For this puzzle, I wasn't sure how to determine that the christmas tree
        // was present without simply printing during the simulation until I saw it visually.
        // I could hard code the coordinate values and make a comparison, but that didn't seem worth it.
        SimulateRobots(robots, mapSize, int.MaxValue);
        
        // 6587 : Correct
        return 1;
    }

    private void SimulateRobots(List<Robot> robots, Vector2Int mapSize, int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            foreach (Robot robot in robots)
            {
                robot.Position = ClampPosition(robot.Position + robot.Velocity, mapSize);
            }
        }
        
        PrintMap(robots, mapSize);
    }

    private Vector2Int ClampPosition(Vector2Int position, Vector2Int mapSize)
    {
        int xRange = mapSize.X;
        int yRange = mapSize.Y;
        int x = (position.X % xRange + xRange) % xRange;
        int y = (position.Y % yRange + yRange) % yRange;

        return new Vector2Int(x, y);
    }

    private void PrintMap(List<Robot> robots, Vector2Int mapSize)
    {
        var robotPositions = robots.Select(robot => robot.Position).ToList();
        for (int y = 0; y < mapSize.Y; y++)
        {
            for (int x = 0; x < mapSize.X; x++)
            {
                int count = robotPositions.Count(a => a == new Vector2Int(x, y));
                Console.Write(count > 0 ? count : ".");
            }
            Console.WriteLine();
        }
    }

    class Robot
    {
        public Vector2Int Position;
        public Vector2Int Velocity;
    }
}