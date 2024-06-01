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
                case [not null]:
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

        var a = wires.TryGetValue(inputA, out var valueA) ? valueA : ushort.Parse(inputA);
        var b = wires.TryGetValue(inputB, out var valueB) ? valueB : ushort.Parse(inputB);
        
        wires[output] = op switch
        {
            "RSHIFT" => (ushort)(a >> b),
            "LSHIFT" => (ushort)(a << b),
            "OR" => (ushort)(a | b),
            "AND" => (ushort)(a & b),
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

    private class Operation
    {
        public SignalInput[] Inputs { get; }
        public Wire Output { get; }

        private string _op;
        private int _activeSignals;

        public Operation(SignalInput[] inputs, Wire output, string op)
        {
            Inputs = inputs;
            Output = output;
            _op = op;

            foreach (SignalInput input in Inputs)
            {
                if (input is Wire wire)
                    wire.SignalReceived += OnSignalReceived;
            }
        }

        private void OnSignalReceived()
        {
            if (Inputs.All(x => x.Signal > 0))
            {
                Output.Signal = TriggerOperation();
            }
        }

        private ushort TriggerOperation()
        {
            ushort a = Inputs[0].Signal;
            ushort b = Inputs.Length > 1 ? Inputs[1].Signal : (ushort)0;
            
            return _op switch
            {
                "NOT" => (ushort)~a,
                "RSHIFT" => (ushort)(a >> b),
                "LSHIFT" => (ushort)(a << b),
                "OR" => (ushort)(a | b),
                "AND" => (ushort)(a & b),
                _ => throw new InvalidOperationException($"{_op} is an invalid operation"),
            };
        }
    }

    private class SignalInput(ushort signal = 0)
    {
        public virtual ushort Signal { get; set; } = signal;
    }
    
    private class Wire(string id) : SignalInput
    {
        private ushort _signal;
        
        public string Id { get; } = id;

        public override ushort Signal
        {
            get => _signal;
            set
            {
                _signal = value;
                SignalReceived?.Invoke();
            }
        }

        public event Action? SignalReceived;
    }
}