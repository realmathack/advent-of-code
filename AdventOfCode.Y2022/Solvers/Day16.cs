using System.Text.RegularExpressions;

namespace AdventOfCode.Y2022.Solvers
{
    public partial class Day16 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var root = ToValves(input);
            return FindHighestFlowRate(30, root, []);
        }

        public override object SolvePart2(string[] input)
        {
            // TODO: Very slow, might need a faster implementation
            // https://github.com/varienaja/adventofcode/blob/main/src/test/java/org/varienaja/adventofcode2022/Puzzle16.java
            // https://github.com/Janoz-NL/aoc2021/blob/master/src/main/java/com/janoz/aoc/y2022/day16/Day16.java
            var root = ToValves(input);
            return FindHighestFlowRate(new State([26, 26], [root, root], [root], 0)).TotalFlowRate;
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
                var nextFlowRate = FindHighestFlowRate(minutesLeft - next.Value, next.Key, new(opened));
                var result = currentFlowRate + nextFlowRate;
                if (result > highest)
                {
                    highest = result;
                }
            }
            return highest;
        }

        private static State FindHighestFlowRate(State state)
        {
            if (state.MinutesLeft[0] <= 1 && state.MinutesLeft[1] <= 1)
            {
                return state;
            }
            var actor = (state.MinutesLeft[1] > state.MinutesLeft[0]) ? 1 : 0;
            var minutesLeft = state.MinutesLeft[actor];
            var current = state.Current[actor];
            var highest = state;
            foreach (var next in current.Distances)
            {
                if (next.Value >= minutesLeft || state.Opened.Contains(next.Key))
                {
                    continue;
                }
                var result = FindHighestFlowRate(state.MoveNext(actor, minutesLeft - next.Value - 1, next.Key));
                if (result.TotalFlowRate > highest.TotalFlowRate)
                {
                    highest = result;
                }
            }
            return highest;
        }

        private static Valve ToValves(string[] lines)
        {
            var valves = new Dictionary<string, Valve>();
            var tunnels = new Dictionary<string, string[]>();
            foreach (var line in lines)
            {
                var match = ValveRegex().Match(line);
                var valveId = match.Groups[1].Value;
                tunnels.Add(valveId, match.Groups[3].Value.Split(", "));
                var flowRate = int.Parse(match.Groups[2].Value);
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

        [GeneratedRegex(@"Valve (.{2}) has flow rate=(\d+); tunnels? leads? to valves? (.+)")]
        private static partial Regex ValveRegex();

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

        private record class State(int[] MinutesLeft, Valve[] Next, HashSet<Valve> Opened, int TotalFlowRate)
        {
            public int[] MinutesLeft { get; set; } = MinutesLeft;
            public Valve[] Current { get; set; } = Next;
            public HashSet<Valve> Opened { get; set; } = Opened;
            public int TotalFlowRate { get; set; } = TotalFlowRate;
            public State MoveNext(int actor, int minutesLeft, Valve next) => (actor == 0)
                ? new([minutesLeft, MinutesLeft[1]], [next, Current[1]], new(Opened) { next }, TotalFlowRate + minutesLeft * next.FlowRate)
                : new([MinutesLeft[0], minutesLeft], [Current[0], next], new(Opened) { next }, TotalFlowRate + minutesLeft * next.FlowRate);
        }
    }
}
