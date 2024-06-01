using AdventOfCode._2015;

namespace AdventOfCode.Tests.Unit._2015;

public class Day5Tests
{
    [Fact]
    public Task Part1_ShouldReturn2_WhenCalled()
    {
        // Arrange
        var solution = new Day5(InputParser.Mode.Test);
        
        // Act
        var result = solution.Part1();
        
        // Assert
        result.Should().Be(2, "because only two nice strings should have been found");
        return Task.CompletedTask;
    }
}