using AdventOfCode.Utility;

namespace AdventOfCode._2024;

public class Day9 : Solution<Day9>
{
    public Day9(InputParser.Mode parseMode, int option = 0) : base(parseMode, option)
    {
    }

    public override long Part1()
    {
        var data = Input.First().Select(c => int.Parse(c.ToString())).ToArray();
        var diskMap = new List<int>();
        // Construct disk map.
        for (int i = 0, index = 0; i < data.Length; i++)
        {
            if (i % 2 == 1) index++;
            for (int j = 0; j < data[i]; j++)
            {
                if (i % 2 == 1)
                {
                    diskMap.Add(-1);
                    continue;
                }

                diskMap.Add(index);
            }
        }

        PrintDiskMap(diskMap);

        long checksum = 0;
        for (int i = 0, j = diskMap.Count - 1; i < diskMap.Count && j > i; i++)
        {
            if (diskMap[i] == -1)
            {
                while (diskMap[j] == -1) j--;

                if (j < i) break;

                // Swap storage positions.
                (diskMap[i], diskMap[j]) = (diskMap[j], diskMap[i]);
            }

            checksum += int.Parse(diskMap[i].ToString()) * i;

            Console.WriteLine(" = {0}", checksum);
        }

        return checksum;
    }

    public override long Part2()
    {
        var data = Input.First().Select(c => int.Parse(c.ToString())).ToArray();
        var diskMap = new List<int>();
        // Construct disk map.
        int id = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (i % 2 == 1) id++;
            for (int j = 0; j < data[i]; j++)
            {
                if (i % 2 == 1)
                {
                    diskMap.Add(-1);
                    continue;
                }

                diskMap.Add(id);
            }
        }

        PrintDiskMap(diskMap);
        Console.WriteLine();

        while (id > 0)
        {
            int fileIndex = diskMap.IndexOf(id);
            int fileSize = diskMap.LastIndexOf(id) - fileIndex + 1;

            // Look for free space left of file that is large enough for file.
            int openSpace = 0;
            for (int i = 0; i < fileIndex; i++)
            {
                openSpace = diskMap[i] == -1 ? openSpace + 1 : 0;
                if (openSpace == fileSize)
                {
                    for (int j = 0; j < fileSize; j++)
                    {
                        // Swap file data with empty slots.
                        (diskMap[fileIndex + j], diskMap[i - fileSize + j + 1]) =
                            (diskMap[i - fileSize + j + 1], diskMap[fileIndex + j]);
                    }

                    // File has been handled, no more work to do.
                    break;
                }
            }

            id--;
        }

        PrintDiskMap(diskMap);
        Console.WriteLine();

        long checksum = 0;
        for (int i = 0; i < diskMap.Count; i++)
        {
            if (diskMap[i] >= 0)
            {
                checksum += i * diskMap[i];
            }
        }

        return checksum;
    }

    private void PrintDiskMap(List<int> diskMap)
    {
        foreach (var c in diskMap)
        {
            if (c < 0)
            {
                Console.Write('.');
                continue;
            }

            Console.Write(c);
        }
    }

    private void PrintDiskMap(List<(int ID, int Space)> diskMap)
    {
        foreach ((int ID, int Space) c in diskMap)
        {
            for (int i = 0; i < c.Space; i++)
            {
                if (c.ID < 0)
                {
                    Console.Write('.');
                    continue;
                }

                Console.Write(c.ID);
            }
        }
    }
}