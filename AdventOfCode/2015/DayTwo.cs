namespace AdventOfCode._2015;

public class DayTwo : Solution
{
    public override void PartOne()
    {
        double squareFeet = 0d;
        foreach (var line in InputParser.GetInputRaw())
        {
            var dimensions = line.Split('x').Select(double.Parse).ToArray();   // l x w x h

            var surfaceArea = SurfaceArea(dimensions[0], dimensions[1], dimensions[2]);
            var smallestSide = SmallestSide(dimensions[0], dimensions[1], dimensions[2]);

            squareFeet += surfaceArea + smallestSide;
        }
        
        Console.WriteLine(squareFeet);
    }

    public override void PartTwo()
    {
        throw new NotImplementedException();
    }

    private double SurfaceArea(double length, double width, double height)
    {
        return (2 * length * width) + (2 * width * height) + (2 * height * length);
    }

    private double SmallestSide(double length, double width, double height)
    {
        double a = length * width;
        double b = width * height;
        double c = height * length;

        return new double[] {a, b, c}.Min();
    }
}