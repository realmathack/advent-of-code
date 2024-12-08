namespace AdventOfCode.Y2022.Solvers
{
    public class Day16 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var root = ToValves(input);
            return FindHighestFlowRate(30, root, []);
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Fix implementation (elephant) -> build with states, like Y2015.Day22?
            // https://github.com/varienaja/adventofcode/blob/main/src/test/java/org/varienaja/adventofcode2022/Puzzle16.java
            // https://github.com/Janoz-NL/aoc2021/blob/master/src/main/java/com/janoz/aoc/y2022/day16/Day16.java

            var sum = 0;
            var root = ToValves(input);
            var totalValves = root.Distances.Count + 1;
            var visited = new HashSet<Valve> { root };
            var timers = new Dictionary<int, int> { [0] = 26, [1] = 26 };
            var current = new Dictionary<int, Valve> { [0] = root, [1] = root };
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
            // 1737 = too low
        }

        private static int FindHighestFlowRate(int minutesLeft, Valve current, HashSet<Valve> opened)
        {
            if (minutesLeft <= 1) // Moving through a tunnel or opening a valve takes at least 1 minute
            {
                return 0;
            }
            var currentFlowRate = (current.FlowRate > 0) ? current.FlowRate * --minutesLeft : 0;
            var highest = currentFlowRate;
            opened.Add(current);
            foreach (var next in current.Distances)
            {
                if (next.Value >= minutesLeft || opened.Contains(next.Key))
                {
                    continue;
                }
                var flowRateNext = FindHighestFlowRate(minutesLeft - next.Value, next.Key, new(opened));
                var result = currentFlowRate + flowRateNext;
                if (result > highest)
                {
                    highest = result;
                }
            }
            return highest;
        }

        private static (Valve Valve, int Flow, int Distance) FindHighestFlowRate2(HashSet<Valve> visited, int timeLeft, Valve current)
        {
            var highest = (Valve: Valve.Empty, Flow: 0, Distance: 0);
            foreach (var neighbor in current.Distances)
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
            var valves = new Dictionary<string, Valve>();
            var tunnels = new Dictionary<string, string[]>();
            foreach (var line in lines)
            {
                var parts = line.Split(_separator, StringSplitOptions.RemoveEmptyEntries);
                var valveId = parts[1];
                tunnels.Add(valveId, parts[10..]);
                var flowRate = int.Parse(parts[5]);
                if (valveId == "AA" || flowRate > 0)
                {
                    valves[valveId] = new(valveId, flowRate);
                }
            }
            var valveIds = valves.Keys.ToArray();
            for (int i = 0; i < valveIds.Length; i++)
            {
                for (int j = i + 1; j < valveIds.Length; j++)
                {
                    var first = valveIds[i];
                    var second = valveIds[j];
                    var distance = new BFS(tunnels, first).Search(second).Count;
                    valves[first].Distances[valves[second]] = distance;
                    valves[second].Distances[valves[first]] = distance;
                }
            }
            return valves["AA"];
        }

        private class BFS(Dictionary<string, string[]> tunnels, string goal) : Graphs.BFS<string>
        {
            protected override List<string> FindNeighbors(string node) => [.. tunnels[node]];
            protected override bool IsGoal(string node) => node == goal;
        }

        private record class Valve(string Name, int FlowRate)
        {
            public Dictionary<Valve, int> Distances { get; } = [];
            public static Valve Empty => new(string.Empty, 0);
        }
    }
}
