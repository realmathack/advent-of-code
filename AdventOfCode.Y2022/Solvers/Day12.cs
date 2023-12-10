namespace AdventOfCode.Y2022.Solvers
{
    public class Day12 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var (grid, start, end) = ToGrid(input);
            return AStar(grid, start, end).Count;
        }

        public override object SolvePart2(string[] input)
        {
            var (grid, _, end) = ToGrid(input);
            return Dijkstra(grid, end, 'a').Count;
        }

        private static List<Coords> Dijkstra(char[][] grid, Coords start, char goal)
        {
            var visited = new HashSet<Coords>();
            var parents = new Dictionary<Coords, Coords>();
            var scores = new Dictionary<Coords, int>();
            var queue = new PriorityQueue<Coords, int>();
            queue.Enqueue(start, 0);
            while (queue.TryDequeue(out var current, out _))
            {
                if (grid[current.Y][current.X] == goal)
                {
                    return ReconstructPath(parents, current);
                }
                visited.Add(current);
                foreach (var neighbor in FindPossibleNeighborsReverse(grid, current))
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    var tentativeScore = GetScore(scores, current) + 1;
                    if (tentativeScore < GetScore(scores, neighbor))
                    {
                        queue.Enqueue(neighbor, tentativeScore);
                        scores[neighbor] = tentativeScore;
                        parents[neighbor] = current;
                    }
                }
            }
            return [];
        }

        private static List<Coords> AStar(char[][] grid, Coords start, Coords goal)
        {
            var openSet = new Dictionary<Coords, int> { { start, 0 } };
            var parents = new Dictionary<Coords, Coords>();
            var gScores = new Dictionary<Coords, int>() { { start, 0 } };
            var fScores = new Dictionary<Coords, int>() { { start, start.DistanceTo(goal) } };
            while (openSet.Count > 0)
            {
                var current = openSet.First(coord => coord.Value == openSet.Min(coord => coord.Value)).Key;
                if (current == goal)
                {
                    return ReconstructPath(parents, current);
                }
                openSet.Remove(current);
                foreach (var neighbor in FindPossibleNeighbors(grid, current))
                {
                    var tentativeGScore = gScores[current] + 1;
                    if (tentativeGScore < GetScore(gScores, neighbor))
                    {
                        parents[neighbor] = current;
                        gScores[neighbor] = tentativeGScore;
                        var fScore = tentativeGScore + neighbor.DistanceTo(goal);
                        fScores[neighbor] = fScore;
                        openSet.TryAdd(neighbor, fScore);
                    }
                }
            }
            return [];
        }

        private static List<Coords> ReconstructPath(Dictionary<Coords, Coords> parents, Coords current)
        {
            var path = new List<Coords>();
            while (parents.TryGetValue(current, out current))
            {
                path.Add(current);
            }
            return path;
        }

        private static List<Coords> FindPossibleNeighbors(char[][] grid, Coords current)
        {
            var neighbors = new List<Coords>();
            var currentHeight = grid[current.Y][current.X];
            if (current.X > 0                           && (grid[current.Y][current.X - 1] - currentHeight) <= 1) { neighbors.Add(current.Left); }
            if (current.Y > 0                           && (grid[current.Y - 1][current.X] - currentHeight) <= 1) { neighbors.Add(current.Up); }
            if (current.X < grid[current.Y].Length - 1  && (grid[current.Y][current.X + 1] - currentHeight) <= 1) { neighbors.Add(current.Right); }
            if (current.Y < grid.Length - 1             && (grid[current.Y + 1][current.X] - currentHeight) <= 1) { neighbors.Add(current.Down); }
            return neighbors;
        }

        private static List<Coords> FindPossibleNeighborsReverse(char[][] grid, Coords current)
        {
            var neighbors = new List<Coords>();
            var currentHeight = grid[current.Y][current.X];
            if (current.X > 0                           && (grid[current.Y][current.X - 1] - currentHeight) >= -1) { neighbors.Add(current.Left); }
            if (current.Y > 0                           && (grid[current.Y - 1][current.X] - currentHeight) >= -1) { neighbors.Add(current.Up); }
            if (current.X < grid[current.Y].Length - 1  && (grid[current.Y][current.X + 1] - currentHeight) >= -1) { neighbors.Add(current.Right); }
            if (current.Y < grid.Length - 1             && (grid[current.Y + 1][current.X] - currentHeight) >= -1) { neighbors.Add(current.Down); }
            return neighbors;
        }

        private static int GetScore(Dictionary<Coords, int> scores, Coords current) => scores.TryGetValue(current, out var score) ? score : int.MaxValue;

        private static (char[][] Grid, Coords Start, Coords End) ToGrid(string[] lines)
        {
            var grid = new char[lines.Length][];
            Coords? start = null;
            Coords? end = null;
            int col;
            for (int row = 0; row < lines.Length; row++)
            {
                grid[row] = lines[row].ToCharArray();
                if (start is null && (col = lines[row].IndexOf('S')) != -1)
                {
                    start = new Coords(col, row);
                    grid[row][col] = 'a';
                }
                if (end is null && (col = lines[row].IndexOf('E')) != -1)
                {
                    end = new Coords(col, row);
                    grid[row][col] = 'z';
                }
            }
            if (start is null || end is null)
            {
                throw new InvalidOperationException("Start and/or End not found!");
            }
            return (grid, start.Value, end.Value);
        }
    }
}
