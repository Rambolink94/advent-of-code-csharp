using AdventOfCode.Utility;

namespace AdventOfCode._2015;

public class Day3 : Solution<Day1>
{
    public Day3(InputParser.Mode parseMode) : base(parseMode) {}

    public override long Part1()
    {
        var position = new Point(0, 0);
        var visited = new HashSet<Point> { position };
        foreach (var line in Input)
        {
            foreach (var c in line)
            {
                Point direction = Point.FromDirection(c);
                position += direction;
                visited.Add(position);
            }
        }

        return visited.Count;
    }

    public override long Part2()
    {
        var santaPosition = new Point(0, 0);
        var robotPosition = new Point(0, 0);
        var visited = new HashSet<Point> { santaPosition };
        bool isSantaTurn = true;
        foreach (var line in Input)
        {
            foreach (var c in line)
            {
                Point direction = Point.FromDirection(c);
                if (isSantaTurn)
                {
                    santaPosition += direction;
                    visited.Add(santaPosition);
                }
                else
                {
                    robotPosition += direction;
                    visited.Add(robotPosition);
                }

                isSantaTurn = !isSantaTurn;
            }
        }

        return visited.Count;
    }

    private struct Point(int x, int y)
    {
        public int X = x;
        public int Y = y;

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point FromDirection(char c)
        {
            return c switch
            {
                '^' => new Point(0, 1),
                'v' => new Point(0, -1),
                '<' => new Point(-1, 0),
                '>' => new Point(1, 0),
                _ => throw new InvalidOperationException($"{c} is not a valid direction!"),
            };
        }
    }
}