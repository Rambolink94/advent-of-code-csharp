namespace AdventOfCode
{
    public abstract class Solution
    {
        protected readonly InputParser.Mode _parseMode;
        
        protected Solution(InputParser.Mode parseMode)
        {
            _parseMode = parseMode;
        }

        public abstract void Part1();

        public abstract void Part2();
    }
}
