namespace AdventOfCode
{
    public abstract class Solution
    {
        // TODO: Add Input IEnumerator that gets path from namespace and typename.
        protected readonly InputParser.Mode _parseMode;
        
        protected Solution(InputParser.Mode parseMode)
        {
            _parseMode = parseMode;
        }

        public abstract void Part1();

        public abstract void Part2();
    }
}
