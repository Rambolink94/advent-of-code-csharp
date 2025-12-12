using AdventOfCode._2025;
using AdventOfCode.Utility;

namespace AdventOfCode.Tests.Unit._2025;

public class Day1Tests
{
    [Fact]
    public void Part2_ShouldReturn6_WhenCalled()
    {
        // Arrange
        var solution = new Day1(InputParser.Mode.Test);
        
        // Act
        var result = solution.Part2();
        
        // Assert
        result.Should().Be(6);
    }
    
    // TODO: InputParser should be mocked to take in array of strings for tests.
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Part2_ShouldReturn1_WhenCalled(int option)
    {
        // Arrange
        var solution = new Day1(InputParser.Mode.Test, option);
        
        // Act
        var result = solution.Part2();
        
        // Assert
        result.Should().Be(1);
    }
}