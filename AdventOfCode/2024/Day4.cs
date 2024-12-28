using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day4 : Solution<Day4>
{
    private const string Xmas = "XMAS";
    private const int WordLength = 4; 
    
    public Day4(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        var wordGrid = Input.Select(line => line.ToCharArray()).ToArray();
        int validWords = 0;
        for (int y = 0; y < wordGrid.Length; y++)
        {
            for (int x = 0; x < wordGrid[y].Length; x++)
            {
                validWords += GetValidXmas(wordGrid, y, x);
            }
        }

        return validWords;
    }
    
    public override long Part2()
    {
        var wordGrid = Input.Select(line => line.ToCharArray()).ToArray();
        int validCrosses = 0;
        for (int y = 0; y < wordGrid.Length; y++)
        {
            for (int x = 0; x < wordGrid[y].Length; x++)
            {
                if (GetValidXmas2(wordGrid, y, x)) validCrosses++;
            }
        }

        return validCrosses;
    }

    private int GetValidXmas(char[][] grid, int y, int x)
    {
        var directions = new [] { (-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0) };
        int count = 0;
        foreach ((int X, int Y) direction in directions)
        {
            bool found = true;
            for (int i = 0; i < WordLength; i++)
            {
                if (Xmas[i] != GetGridValueSafe(grid, y + i * direction.Y, x + i * direction.X))
                {
                    found = false;
                    break;
                }
            }

            if (found) count++;
        }

        return count;
    }
    
    private bool GetValidXmas2(char[][] grid, int y, int x)
    {
        bool downRight = false;
        bool downLeft = false;
        if (grid[y][x] != 'A')
            return false;

        if ((GetGridValueSafe(grid, y - 1, x - 1) == 'M' && GetGridValueSafe(grid, y + 1, x + 1) == 'S')
            || (GetGridValueSafe(grid, y - 1, x - 1) == 'S' && GetGridValueSafe(grid, y + 1, x + 1) == 'M'))
            downRight = true;
        
        if ((GetGridValueSafe(grid, y + 1, x - 1) == 'M' && GetGridValueSafe(grid, y - 1, x + 1) == 'S')
            || (GetGridValueSafe(grid, y + 1, x - 1) == 'S' && GetGridValueSafe(grid, y - 1, x + 1) == 'M'))
            downLeft = true;

        return downRight && downLeft;
    }

    private char GetGridValueSafe(char[][] grid, int y, int x)
    {
        if (y < 0 || y >= grid.Length || x < 0 || x >= grid[0].Length) return char.MinValue;

        return grid[y][x];
    }
}