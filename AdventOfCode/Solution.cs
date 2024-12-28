using AdventOfCode.Utility;

namespace AdventOfCode
{
    // TODO: Implement a logger class.
    public abstract class Solution<T> : Solution
        where T : class
    {
        private readonly InputParser.Mode _parseMode;
        private readonly int _option;
        protected IEnumerable<string> Input => InputParser.GetInputRaw<T>(_parseMode, _option);
        
        protected Solution(InputParser.Mode parseMode, int option = 0)
            : base(typeof(T))
        {
            _parseMode = parseMode;
            _option = option;
        }
    }

    public abstract class Solution
    {
        public Solution(Type type)
        {
            SolutionType = type;
        }
        
        public Type SolutionType { get; }
        
        public abstract long Part1();

        public abstract long Part2();
    }
}
