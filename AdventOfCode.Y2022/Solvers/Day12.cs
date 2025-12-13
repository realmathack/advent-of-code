using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2022.Solvers
{
    public class Day12 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (grid, start, end) = ToGrid(input);
            return new AStar(grid).FindShortestPath(start, end).Distance;
        }

        public override object SolvePart2(string[] input)
        {
            var (grid, _, end) = ToGrid(input);
            return new Dijkstra(grid, 'a').FindShortestPath(end).Distance;
        }

        private static List<Coords> FindPossibleNeighbors(char[][] grid, Coords current)
        {
            var neighbors = new List<Coords>();
            var currentHeight = grid[current.Y][current.X];
            if (current.X > 0                          && (grid[current.Y][current.X - 1] - currentHeight) <= 1) { neighbors.Add(current.Left); }
            if (current.Y > 0                          && (grid[current.Y - 1][current.X] - currentHeight) <= 1) { neighbors.Add(current.Up); }
            if (current.X < grid[current.Y].Length - 1 && (grid[current.Y][current.X + 1] - currentHeight) <= 1) { neighbors.Add(current.Right); }
            if (current.Y < grid.Length - 1            && (grid[current.Y + 1][current.X] - currentHeight) <= 1) { neighbors.Add(current.Down); }
            return neighbors;
        }

        private static List<Coords> FindPossibleNeighborsReverse(char[][] grid, Coords current)
        {
            var neighbors = new List<Coords>();
            var currentHeight = grid[current.Y][current.X];
            if (current.X > 0                          && (grid[current.Y][current.X - 1] - currentHeight) >= -1) { neighbors.Add(current.Left); }
            if (current.Y > 0                          && (grid[current.Y - 1][current.X] - currentHeight) >= -1) { neighbors.Add(current.Up); }
            if (current.X < grid[current.Y].Length - 1 && (grid[current.Y][current.X + 1] - currentHeight) >= -1) { neighbors.Add(current.Right); }
            if (current.Y < grid.Length - 1            && (grid[current.Y + 1][current.X] - currentHeight) >= -1) { neighbors.Add(current.Down); }
            return neighbors;
        }

        private static (char[][] Grid, Coords Start, Coords End) ToGrid(string[] lines)
        {
            var grid = new char[lines.Length][];
            Coords? start = null;
            Coords? end = null;
            int x;
            for (int y = 0; y < lines.Length; y++)
            {
                grid[y] = lines[y].ToCharArray();
                if (start is null && (x = lines[y].IndexOf('S')) != -1)
                {
                    start = new Coords(x, y);
                    grid[y][x] = 'a';
                }
                if (end is null && (x = lines[y].IndexOf('E')) != -1)
                {
                    end = new Coords(x, y);
                    grid[y][x] = 'z';
                }
            }
            if (start is null || end is null)
            {
                throw new ImpossibleException("Start and/or End not found!");
            }
            return (grid, start.Value, end.Value);
        }

        private class AStar(char[][] grid) : Graphs.AStar<Coords>
        {
            protected override int CalculateHeuristic(Coords node, Coords goal) => node.DistanceTo(goal);
            protected override List<Coords> FindNeighbors(Coords node) => FindPossibleNeighbors(grid, node);
        }

        private class Dijkstra(char[][] grid, char goal) : Graphs.Dijkstra<Coords>
        {
            protected override List<Coords> FindNeighbors(Coords node) => FindPossibleNeighborsReverse(grid, node);
            protected override bool IsGoal(Coords node) => grid[node.Y][node.X] == goal;
        }
    }
}
