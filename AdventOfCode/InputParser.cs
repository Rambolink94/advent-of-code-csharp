using System.Runtime.CompilerServices;

namespace AdventOfCode
{
    public class InputParser
    {
        public static IEnumerable<string> GetInputRaw(Mode inputMode, [CallerFilePath] string solutionPath = "")
        {
            var inputPath = ParseInputPath(solutionPath);

            bool modeFound = false;
            using var file = new StreamReader(inputPath);
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;

                if (!modeFound)
                {
                    if (!line.StartsWith('#'))
                        continue;
                    
                    var mode = Enum.Parse<Mode>(line[1..], true);
                    if (mode == inputMode)
                        modeFound = true;
                    
                    continue;
                }
                
                if (line.StartsWith('#'))
                {
                    yield break;
                }

                yield return line;
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
            var dayValue = callerFile[3..];
            if (int.TryParse(dayValue, out int value))
            {
                throw new InvalidOperationException($"The file at {solutionPath} was not a valid input path");
            }

            return "./" + year + "/Input/" + callerFile[..3] + "_" + value + ".txt";
        }

        public enum Mode
        {
            Test,
            Real
        }
    }
}
