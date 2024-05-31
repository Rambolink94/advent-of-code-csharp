namespace AdventOfCode._2015;

public class Day3 : Solution
{
    public Day3(InputParser.Mode parseMode) : base(parseMode) {}

    public override void Part1()
    {
        var visited = new HashSet<Point>();
        var position = new Point(0, 0);
        foreach (var line in Input)
        {
            
        }
    }

    public override void Part2()
    {
        throw new NotImplementedException();
    }

    private struct Point(int x, int y)
    {
        public int X = x;
        public int Y = y;
    }
}