using System.Net.Http.Headers;

namespace AdventOfCode._2015;

public class Day2 : Solution
{
    public Day2(InputParser.Mode parseMode) : base(parseMode)
    {
    }
    
    public override void Part1()
    {
        double squareFeet = 0d;
        foreach (var line in Input)
        {
            var dimensions = line.Split('x').Select(double.Parse).ToArray();   // l x w x h

            var surfaceArea = SurfaceArea(dimensions[0], dimensions[1], dimensions[2]);
            var smallestSide = dimensions.Min();

            squareFeet += surfaceArea + smallestSide;
        }
        
        Console.WriteLine(squareFeet);
    }

    public override void Part2()
    {
        double totalRibbon = 0d;
        foreach (var line in Input)
        {
            var dimensions = line.Split('x').Select(double.Parse).ToArray();   // l x w x h

            double ribbingPerimeter = SmallestPerimeter(dimensions[0], dimensions[1], dimensions[2]);
            double volume = dimensions.Aggregate((product, value) => product * value);

            totalRibbon += ribbingPerimeter + volume;
        }
        
        Console.WriteLine(totalRibbon);
    }

    private double SurfaceArea(double l, double w, double h)
    {
        return (2 * l * w) + (2 * w * h) + (2 * h * l);
    }

    private double SmallestPerimeter(double l, double w, double h)
    {
        double a = l * 2 + w * 2;
        double b = w * 2 + h * 2;
        double c = h * 2 + l * 2;

        return new double[] { a, b, c }.Min();
    }
}