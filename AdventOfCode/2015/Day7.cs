using AdventOfCode.Utility;

namespace AdventOfCode._2015;

public class Day7 : Solution<Day7>
{
    public Day7(InputParser.Mode parseMode, int option = 0) : base(parseMode, option) {}

    public override int Part1()
    {
        var wires = new HashSet<SignalInput>();
        var gates = new List<Gate>();
        var inputs = new List<(SignalInput Signal, Wire output)>();
        foreach (var line in Input)
        {
            var parts = line.Split("->", StringSplitOptions.TrimEntries);
            var output = (Wire)GetOrCreateInput(parts[^1], wires);
            var opParts = parts[0].Split(' ', StringSplitOptions.TrimEntries);
            switch(opParts)
            {
                case ["NOT", ..]:
                    SignalInput wire = GetOrCreateInput(opParts[1], wires);
                    var notGate = new Gate(new [] { wire }, output, "NOT");
                    gates.Add(notGate);
                    break;
                case [not null]:
                    var input = GetOrCreateInput(opParts[0], wires);
                    if (input is Wire w)
                    {
                        var assignmentGate = new Gate(w, output, "ASSIGNMENT");
                        gates.Add(assignmentGate);
                        break;
                    }
                    
                    inputs.Add((input, output));
                    break;
                default:
                    SignalInput wireA = GetOrCreateInput(opParts[0], wires);
                    SignalInput wireB = GetOrCreateInput(opParts[2], wires);
                    
                    var gate = new Gate(new []{ wireA, wireB }, output, opParts[1]);
                    gates.Add(gate);
                    break;
            }
        }

        foreach ((SignalInput signal, Wire output) in inputs)
        {
            output.Signal = signal.Signal;
        }
        
        return wires.First(x => (x as Wire)!.Id == "a").Signal;
    }

    public override int Part2()
    {
        throw new NotImplementedException();
    }

    private SignalInput GetOrCreateInput(string input, HashSet<SignalInput> wires, ushort signal = 0)
    {
        if (ushort.TryParse(input, out var number))
        {
            return new SignalInput(number);
        }

        SignalInput? wire = wires.FirstOrDefault(x => x is Wire wire && wire.Id == input);
        if (wire is null)
        {
            wire = new Wire(input, signal);
            wires.Add(wire);
        }

        return wire;
    }

    private class Gate
    {
        private readonly SignalInput[] _inputs;
        private readonly Wire _output;
        private readonly string _op;

        public Gate(SignalInput[] inputs, Wire output, string op)
        {
            _inputs = inputs;
            _output = output;
            _op = op;

            foreach (SignalInput input in _inputs)
            {
                if (input is Wire wire)
                    wire.SignalReceived += OnSignalReceived;
            }
        }

        public Gate(SignalInput input, Wire output, string op)
            : this(new [] { input }, output, op)
        {
        }

        private void OnSignalReceived()
        {
            if (_inputs.All(x => x.Signal > 0))
            {
                _output.Signal = Process();
            }
        }

        private ushort Process()
        {
            ushort a = _inputs[0].Signal;
            ushort b = _inputs.Length > 1 ? _inputs[1].Signal : (ushort)0;
            
            var result = _op switch
            {
                "NOT" => (ushort)~a,
                "RSHIFT" => (ushort)(a >> b),
                "LSHIFT" => (ushort)(a << b),
                "OR" => (ushort)(a | b),
                "AND" => (ushort)(a & b),
                "ASSIGNMENT" => a,
                _ => throw new InvalidOperationException($"{_op} is an invalid operation"),
            };

            Console.WriteLine($"Processing: {_inputs[0].Signal} {(_inputs.Length > 1 ? _inputs[1].Signal : string.Empty)}" +
                              $" -> {result}");
            
            return result;
        }
    }

    private class SignalInput(ushort signal = 0)
    {
        public virtual ushort Signal { get; set; } = signal;
    }
    
    private class Wire(string id, ushort signal = 0) : SignalInput(signal)
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