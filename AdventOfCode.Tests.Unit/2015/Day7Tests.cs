using AdventOfCode._2015;
using AdventOfCode.Utility;

namespace AdventOfCode.Tests.Unit._2015;

public class Day7Tests
{
    [Fact]
    public Task Part1_ShouldReturn2_WhenCalled()
    {
        // Arrange
        var solution = new Day7(InputParser.Mode.Test, 1);
        
        // Act
        var result = solution.Part1();
        
        // Assert
        result.Should().Be(65412, "because the wire's signal should have been set");
        return Task.CompletedTask;
    }
}