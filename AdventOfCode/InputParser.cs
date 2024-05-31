using System.Runtime.CompilerServices;

namespace AdventOfCode
{
    public class InputParser
    {
        private static Dictionary<string, int> _dayToNumberLookup = new()
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
            ["ten"] = 10,
            ["eleven"] = 11,
            ["twelve"] = 12,
            ["thirteen"] = 13,
            ["fourteen"] = 14,
            ["fifteen"] = 15,
            ["sixteen"] = 16,
            ["seventeen"] = 17,
            ["eighteen"] = 18,
            ["nineteen"] = 19,
            ["twenty"] = 20,
            ["twentyone"] = 21,
            ["twentytwo"] = 22,
            ["twentythree"] = 23,
            ["twentyfour"] = 24,
            ["twentyfive"] = 25,
        };

        public static IEnumerable<string> GetInputRaw([CallerFilePath] string solutionPath = "")
        {
            var inputPath = ParseInputPath(solutionPath);
            
            using var file = new StreamReader(inputPath);
            while (!file.EndOfStream)
            {
                yield return file.ReadLine()!;
            }
        }

        private static string ParseInputPath(string solutionPath)
        {
            var index = solutionPath.LastIndexOf('\\');
            var absolutePath = solutionPath[..index];
            var callerFile = solutionPath[(index + 1)..];

            index = absolutePath.LastIndexOf("\\", StringComparison.Ordinal);
            var year = absolutePath[index..];

            callerFile = callerFile[..^3];
            callerFile = callerFile.ToLower();
            var dayValue = callerFile[3..];
            if (!_dayToNumberLookup.TryGetValue(dayValue, out var value))
            {
                throw new InvalidOperationException($"The file at {solutionPath} was not a valid input path");
            }

            return "./" + year + "/Input/" + callerFile[..3] + "_" + value + ".txt";
        }
    }
}
