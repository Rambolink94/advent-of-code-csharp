using AdventOfCode.Utility;

namespace AdventOfCode._2015;

public class Day7 : Solution<Day7>
{
    public Day7(InputParser.Mode parseMode, int option = 0) : base(parseMode, option) {}

    public override long Part1()
    {
        var wires = new HashSet<Wire>();
        var gates = new List<Gate>();
        var inputs = new List<InitialSignal>();
        foreach (var line in Input)
        {
            var parts = line.Split("->", StringSplitOptions.TrimEntries);
            var output = (Wire)GetOrCreateInput(parts[^1], wires);
            var opParts = parts[0].Split(' ', StringSplitOptions.TrimEntries);
            switch(opParts)
            {
                case ["NOT", ..]:
                    InputOutput wire = GetOrCreateInput(opParts[1], wires, output);
                    var notGate = new Gate(wire, output, "NOT");
                    gates.Add(notGate);
                    break;
                case [not null]:
                    var input = GetOrCreateInput(opParts[0], wires, output);
                    if (input is Wire w)
                    {
                        var assignmentGate = new Gate(w, output, "ASSIGNMENT");
                        gates.Add(assignmentGate);
                        break;
                    }
                    
                    inputs.Add((InitialSignal)input);
                    break;
                default:
                    InputOutput wireA = GetOrCreateInput(opParts[0], wires);
                    InputOutput wireB = GetOrCreateInput(opParts[2], wires);
                    
                    var gate = new Gate(new List<InputOutput> { wireA, wireB }, output, opParts[1]);
                    gates.Add(gate);
                    break;
            }
        }

        foreach (InitialSignal signal in inputs)
        {
            signal.Propagate();
        }
        
        return wires.First(x => x.Id == "a").Signal;
    }

    public override long Part2()
    {
        throw new NotImplementedException();
    }

    private InputOutput GetOrCreateInput(string input, HashSet<Wire> wires, InputOutput? output = null)
    {
        if (ushort.TryParse(input, out var number))
        {
            return new InitialSignal(number, output);
        }

        Wire? wire = wires.FirstOrDefault(x => x.Id == input);
        if (wire is null)
        {
            wire = new Wire(input, output);
            wires.Add(wire);
        }

        return wire;
    }

    private abstract class InputOutput
    {
        protected List<InputOutput> Inputs { get; init; } = new List<InputOutput>();
        protected InputOutput? Output { get; private set; }

        protected InputOutput(ushort signal = 0, InputOutput? output = null)
        {
            Signal = signal;
            Output = output;
            Output?.AddInput(this);
        }

        public void AddInput(InputOutput input)
        {
            if (!Inputs.Contains(input))
            {
                Inputs.Add(input);
            }
        }
        
        public void SetOutput(InputOutput output)
        {
            if (Output is null)
            {
                Output = output;
            }
        }

        public ushort Signal { get; set; }
        
        public abstract void Propagate();
    }
    
    private class Gate : InputOutput
    {
        private readonly string _op;
        private int _propagationRequests;

        public Gate(List<InputOutput> inputs, InputOutput output, string op)
        {
            Inputs = inputs;
            SetOutput(output);
            _op = op;

            foreach (var input in Inputs)
            {
                input.SetOutput(this);
            }
            
            output.AddInput(this);
        }
        
        public Gate(InputOutput input, InputOutput output, string op)
            : this(new List<InputOutput> { input }, output, op)
        {
        }
        
        public override void Propagate()
        {
            if (++_propagationRequests >= Inputs.Count)
            {
                Signal = Process();
                Output?.Propagate();
            }
        }

        public ushort Process()
        {
            var a = Inputs[0].Signal;
            var b = Inputs.Count > 1 ? Inputs[1].Signal : (ushort)0;
            return _op switch
            {
                "NOT" => (ushort)~a,
                "RSHIFT" => (ushort)(a >> b),
                "LSHIFT" => (ushort)(a << b),
                "OR" => (ushort)(a | b),
                "AND" => (ushort)(a & b),
                "ASSIGNMENT" => a,
                _ => throw new InvalidOperationException($"{_op} is an invalid operation"),
            };
        }
    }

    private class InitialSignal(ushort signal, InputOutput? output) : InputOutput(signal, output)
    {
        public override void Propagate()
        {
            Output?.Propagate();
        }
    }

    private class Wire(string id, InputOutput? output = null) : InputOutput(output: output)
    {
        public string Id { get; } = id;
        
        public override void Propagate()
        {
            Signal = Inputs[0].Signal;
            Output?.Propagate();
        }
    }
}