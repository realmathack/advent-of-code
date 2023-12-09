namespace AdventOfCode.Y2022.Solvers
{
    public class Day16 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var root = ToValves(input);
            return FindHighestFlowRate(root, [root], 30);
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Fix implementation (elephant)
            var sum = 0;
            var root = ToValves(input);
            var totalValves = root.Neighbors.Count + 1;
            var visited = new HashSet<Valve> { root };
            var timers = new Dictionary<int, int> { { 0, 26 }, { 1, 26 } };
            var current = new Dictionary<int, Valve> { { 0, root }, { 1, root } };
            while (timers.All(timer => timer.Value > 0) && visited.Count < totalValves)
            {
                var person = (timers[0] == timers[1] && current[0] != current[1]) ?
                    ((FindHighestFlowRate2(visited, timers[0], current[0]).Flow > FindHighestFlowRate2(visited, timers[1], current[1]).Flow) ? 0 : 1) :
                    timers.OrderByDescending(timer => timer.Value).ThenBy(timer => timer.Key).First().Key;
                var (valve, flow, distance) = FindHighestFlowRate2(visited, timers[person], current[person]);
                timers[person] -= distance;
                sum += flow;
                visited.Add(valve);
                current[person] = valve;
            }
            return sum;
        }

        private static int FindHighestFlowRate(Valve current, HashSet<Valve> visited, int minutesLeft)
        {
            if (minutesLeft <= 0)
            {
                return 0;
            }
            var highest = new List<int>();
            var currentFlowRate = (current.FlowRate > 0) ? current.FlowRate * --minutesLeft : 0;
            highest.Add( currentFlowRate);
            foreach (var neighbor in current.Neighbors)
            {
                if (visited.Contains(neighbor.Key))
                {
                    continue;
                }
                var flowRateNeighbor = FindHighestFlowRate(neighbor.Key, [.. visited, neighbor.Key], minutesLeft - neighbor.Value);
                if (flowRateNeighbor > 0)
                {
                    highest.Add(currentFlowRate + flowRateNeighbor);
                }
            }
            return highest.Max();
        }

        private static (Valve Valve, int Flow, int Distance) FindHighestFlowRate2(HashSet<Valve> visited, int timeLeft, Valve current)
        {
            var highest = (Valve: Valve.Empty, Flow: 0, Distance: 0);
            foreach (var neighbor in current.Neighbors)
            {
                if (visited.Contains(neighbor.Key))
                {
                    continue;
                }
                var flow = neighbor.Key.FlowRate * (timeLeft - (1 + neighbor.Value));
                if (flow > highest.Flow)
                {
                    highest = (neighbor.Key, flow, 1 + neighbor.Value);
                }
            }
            return highest;
        }

        private static readonly char[] _separator = [' ', '=', ';', ','];
        private static Valve ToValves(string[] lines)
        {
            var tmpList = new Dictionary<string, (int FlowRate, string[] Tunnels)>();
            foreach (var line in lines)
            {
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                tmpList.Add(parts[1], (int.Parse(parts[5]), parts[10..]));
            }
            var valves = new Dictionary<string, Valve>();
            var valvesWithFlowRate = tmpList.Where(tmp => tmp.Value.FlowRate > 0).Select(tmp => tmp.Key).ToArray();
            foreach (var tmp in tmpList.Where(tmp => tmp.Key == "AA" || valvesWithFlowRate.Contains(tmp.Key)))
            {
                if (!valves.TryGetValue(tmp.Key, out var valve))
                {
                    valve = new(tmp.Key);
                    valves.Add(tmp.Key, valve);
                }
                valve.FlowRate = tmp.Value.FlowRate;
                foreach (var target in valvesWithFlowRate.Where(valve => valve != tmp.Key))
                {
                    var distance = FindShortestPath(tmpList, tmp.Key, target);
                    if (!valves.TryGetValue(target, out var neighbor))
                    {
                        neighbor = new(target);
                        valves.Add(target, neighbor);
                    }
                    valve.Neighbors.Add(neighbor, distance);
                }
            }
            return valves["AA"];
        }

        private static int FindShortestPath(Dictionary<string, (int FlowRate, string[] Tunnels)> list, string start, string target)
        {
            var parents = new Dictionary<string, string>();
            var queue = new Queue<string>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var tunnel in list[current].Tunnels)
                {
                    if (parents.ContainsKey(tunnel))
                    {
                        continue;
                    }
                    parents.Add(tunnel, current);
                    queue.Enqueue(tunnel);
                }
            }
            var length = 0;
            while (target != start)
            {
                length++;
                target = parents[target];
            }
            return length;
        }

        private record class Valve(string Name)
        {
            public int FlowRate { get; set; } = 0;
            public Dictionary<Valve, int> Neighbors { get; } = [];
            public static Valve Empty => new(string.Empty);
        }
    }
}
