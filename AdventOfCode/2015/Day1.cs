
using AdventOfCode.Utility;

namespace AdventOfCode._2015
{
    public class Day1 : Solution<Day1>
    {
        public Day1(InputParser.Mode parseMode) : base(parseMode) {}
        
        /// <summary>
        ///     Finds the floor santa is on after parsing all instructions.
        /// </summary>
        public override long Part1()
        {
            int floor = 0;
            foreach (var line in Input)
            {
                foreach (char c in line)
                {
                    floor += GetNextFloor(c);
                }
            }

            return floor;
        }

        /// <summary>
        ///     Gets the first position in the input where santa enters the basement.
        /// </summary>
        public override long Part2()
        {
            int floor = 0;
            int position = 0;
            foreach (var line in Input)
            {
                foreach (char c in line)
                {
                    position++;
                    floor += GetNextFloor(c);
                    if (floor < 0)
                    {
                        break;
                    }
                }
            }

            return floor;
        }

        private int GetNextFloor(char c)
        {
            return c switch
            {
                '(' => 1,
                ')' => -1,
                _ => 0,
            };
        }
    }
}
