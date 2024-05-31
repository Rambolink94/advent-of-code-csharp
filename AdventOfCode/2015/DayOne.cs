
namespace AdventOfCode._2015
{
    public class DayOne : Solution
    {
        /// <summary>
        ///     Finds the floor santa is on after parsing all instructions.
        /// </summary>
        public override void PartOne()
        {
            int floor = 0;
            foreach (var line in InputParser.GetInputRaw())
            {
                foreach (char c in line)
                {
                    floor += GetNextFloor(c);
                }
            }

            Console.WriteLine(floor);
        }

        /// <summary>
        ///     Gets the first position in the input where santa enters the basement.
        /// </summary>
        public override void PartTwo()
        {
            int floor = 0;
            int position = 0;
            foreach (var line in InputParser.GetInputRaw())
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

            Console.WriteLine(position);
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
