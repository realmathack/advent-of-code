using System.Diagnostics;
using System.Xml.Linq;

namespace AdventOfCode.Y2023.Solvers
{
    public class Day20 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var broadcaster = ToModules(input);
            var queue = new Queue<(string Source, Module Destination, Signal Signal)>();
            var counters = new Dictionary<Signal, long>() { { Signal.Low, 0L }, { Signal.High, 0L } };
            for (int i = 0; i < 1000; i++)
            {
                counters[Signal.Low]++;
                queue.Clear();
                queue.Enqueue(("", broadcaster, Signal.Low));
                while (queue.TryDequeue(out var tmp))
                {
                    var (source, module, signal) = tmp;
                    if (module is FlipFlopModule flipFlop)
                    {
                        if (signal == Signal.High)
                        {
                            continue;
                        }
                        flipFlop.State = !flipFlop.State;
                        signal = flipFlop.State ? Signal.High : Signal.Low;
                    }
                    else if (module is ConjunctionModule conjunction)
                    {
                        conjunction.LastSignals[source] = signal;
                        signal = conjunction.LastSignals.Values.All(sig => sig == Signal.High) ? Signal.Low : Signal.High;
                    }
                    else if (module is InverterModule)
                    {
                        signal = (signal == Signal.Low) ? Signal.High : Signal.Low;
                    }
                    foreach (var destination in module.Destinations)
                    {
                        counters[signal]++;
                        queue.Enqueue((module.Name, destination, signal));
                    }
                }
            }
            return counters.Values.Product();
        }

        public override object SolvePart2(string[] input)
        {
            var broadcaster = ToModules(input);
            var queue = new Queue<(string Source, Module Destination, Signal Signal)>();
            var pulses = 0L;
            // TODO: Dit moet duidelijk anders...
            while (true)
            {
                pulses++;
                queue.Clear();
                queue.Enqueue(("", broadcaster, Signal.Low));
                while (queue.TryDequeue(out var tmp))
                {
                    var (source, module, signal) = tmp;
                    if (module is FlipFlopModule flipFlop)
                    {
                        if (signal == Signal.High)
                        {
                            continue;
                        }
                        flipFlop.State = !flipFlop.State;
                        signal = flipFlop.State ? Signal.High : Signal.Low;
                    }
                    else if (module is ConjunctionModule conjunction)
                    {
                        conjunction.LastSignals[source] = signal;
                        signal = conjunction.LastSignals.Values.All(sig => sig == Signal.High) ? Signal.Low : Signal.High;
                    }
                    else if (module is InverterModule)
                    {
                        signal = (signal == Signal.Low) ? Signal.High : Signal.Low;
                    }
                    foreach (var destination in module.Destinations)
                    {
                        if (destination.Name == "rx" && signal == Signal.Low)
                        {
                            return pulses;
                        }
                        queue.Enqueue((module.Name, destination, signal));
                    }
                }
            }
        }

        private static Module ToModules(string[] lines)
        {
            var modules = new Dictionary<string, Module>();
            var destinations = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                var (name, rawDestinations) = line.SplitInTwo(" -> ");
                var type = (name == "broadcaster") ? 'b' : name[0];
                name = (name == "broadcaster") ? name : name[1..];
                modules.Add(name, type switch
                {
                    '%' => new FlipFlopModule(name, []),
                    '&' => new ConjunctionModule(name, []),
                    _ => new(name, [])
                });
                destinations.Add(name, [.. rawDestinations.Split(", ")]);
            }
            var conjunctions = modules.Values.Where(module => module is ConjunctionModule).Select(module => module.Name);
            foreach (var conjunction in conjunctions)
            {
                var sources = destinations.Where(destination => destination.Value.Contains(conjunction)).ToDictionary();
                if (sources.Count == 1)
                {
                    modules[conjunction] = new InverterModule(conjunction, []);
                }
                else
                {
                    ((ConjunctionModule)modules[conjunction]).LastSignals = sources.Select(source => source.Key).ToDictionary(name => name, _ => Signal.Low);
                }
            }
            foreach (var name in modules.Keys)
            {
                modules[name].Destinations.AddRange(destinations[name].Select(destination => modules.TryGetValue(destination, out var tmp) ? tmp : new Module(destination, [])));
            }
            return modules["broadcaster"];
        }

        private enum Signal { Low = 0, High = 1 }
        private record class Module(string Name, List<Module> Destinations);
        private record class FlipFlopModule(string Name, List<Module> Destinations) : Module(Name, Destinations)
        {
            public bool State { get; set; }
        }
        private record class ConjunctionModule(string Name, List<Module> Destinations) : Module(Name, Destinations)
        {
            public Dictionary<string, Signal> LastSignals { get; set; } = [];
        }
        private record class InverterModule(string Name, List<Module> Destinations) : Module(Name, Destinations);
    }
}
