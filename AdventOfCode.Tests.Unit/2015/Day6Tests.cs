using AdventOfCode._2015;
using AdventOfCode.Utility;

namespace AdventOfCode.Tests.Unit._2015;

public class Day6Tests
{
    [Fact]
    public Task Part1_ShouldReturn2_WhenCalled()
    {
        // Arrange
        var solution = new Day6(InputParser.Mode.Test, 1);
        
        // Act
        var result = solution.Part1();
        
        // Assert
        result.Should().Be(1000, "because the first row of 1000 lights should have been toggled");
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task Part2_ShouldReturn2_WhenCalled()
    {
        // Arrange
        var solution = new Day6(InputParser.Mode.Test, 2);
        
        // Act
        var result = solution.Part2();
        
        // Assert
        result.Should().Be(2_000_001, "because only two nice strings should have been found");
        return Task.CompletedTask;
    }
}