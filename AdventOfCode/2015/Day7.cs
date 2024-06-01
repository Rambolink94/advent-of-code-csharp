using AdventOfCode.Utility;

namespace AdventOfCode._2015;

public class Day7 : Solution<Day7>
{
    public Day7(InputParser.Mode parseMode, int option = 0) : base(parseMode, option) {}

    public override int Part1()
    {
        var wires = new Dictionary<string, ushort>();
        foreach (var line in Input)
        {
            var parts = line.Split("->", StringSplitOptions.TrimEntries);
            var output = parts[^1];
            var opParts = parts[0].Split(' ', StringSplitOptions.TrimEntries);
            switch(opParts)
            {
                case ["NOT", ..]:
                    NotOp(opParts[1], output, wires);
                    break;
                case [{ } value]:
                    AssignmentOp(opParts[0], output, wires);
                    break;
                default:
                    GateOp(opParts[0], opParts[2], opParts[1], output, wires);
                    break;
            }
        }

        return wires["a"];
    }

    public override int Part2()
    {
        throw new NotImplementedException();
    }

    private void NotOp(string input, string output, Dictionary<string, ushort> wires)
    {
        AddWires(wires, input, output);

        wires[output] = (ushort)~wires[input];
    }

    private void AssignmentOp(string input, string output, Dictionary<string, ushort> wires)
    {
        AddWires(wires, input, output);
        
        wires[output] = ushort.TryParse(input, out var value) ? value : wires[input];
    }

    private void GateOp(string inputA, string inputB, string op, string output, Dictionary<string, ushort> wires)
    {
        AddWires(wires, inputA, inputB, output);

        wires[output] = op switch
        {
            "RSHIFT" => (ushort)(wires[inputA] >> ushort.Parse(inputB)),
            "LSHIFT" => (ushort)(wires[inputA] << ushort.Parse(inputB)),
            "OR" => (ushort)(wires[inputA] | wires[inputB]),
            "AND" => (ushort)(wires[inputA] & wires[inputB]),
            _ => throw new InvalidOperationException($"{op} is an invalid operation"),
        };
    }

    private void AddWires(Dictionary<string, ushort> wires, params string[] wiresToAdd)
    {
        foreach (string wire in wiresToAdd)
        {
            if (!ushort.TryParse(wire, out _))
            {
                _ = wires.TryAdd(wire, 0);
            }
        }
    }
}