namespace AdventOfCode
{
    public abstract class Solution
    {
        private readonly InputParser.Mode _parseMode;
        
        protected Solution(InputParser.Mode parseMode)
        {
            _parseMode = parseMode;
        }

        public IEnumerable<string> Input => InputParser.GetInputRaw(_parseMode);

        public abstract void Part1();

        public abstract void Part2();
    }
}
