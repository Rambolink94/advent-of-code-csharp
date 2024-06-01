namespace AdventOfCode
{
    public abstract class Solution<T> : Solution
        where T : class
    {
        private readonly InputParser.Mode _parseMode;
        protected IEnumerable<string> Input => InputParser.GetInputRaw<T>(_parseMode);
        
        protected Solution(InputParser.Mode parseMode)
            : base(typeof(T))
        {
            _parseMode = parseMode;
        }
    }

    public abstract class Solution
    {
        public Solution(Type type)
        {
            SolutionType = type;
        }
        
        public Type SolutionType { get; }
        
        public abstract int Part1();

        public abstract int Part2();
    }
}
