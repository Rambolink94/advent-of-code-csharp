using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace AdventOfCode
{
    public static class InputParser
    {
        public static IEnumerable<string> GetInputRaw<T>(Mode inputMode)
        {
            var inputPath = ParseInputPath(typeof(T));

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

        private static string ParseInputPath(Type type)
        {
            if (type.Namespace is null) throw new ArgumentException($"{type} must have a valid namespace.");
            
            var year = type.Namespace[(type.Namespace.LastIndexOf('.') + 2)..];
            var dayValue = type.Name[3..];
            if (!int.TryParse(dayValue, out int value))
            {
                throw new InvalidOperationException($"File path cannot be determined from type {type}");
            }

            return $"./Input/{year}/day_{value}.txt";
        }

        public enum Mode
        {
            Test,
            Real
        }
    }
}
