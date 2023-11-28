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
            var cameFrom = new Dictionary<Coords, Coords>();
            var scores = new Dictionary<Coords, int>();
            var queue = new PriorityQueue<Coords, int>();
            queue.Enqueue(start, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (grid[current.Y][current.X] == goal)
                {
                    return ReconstructPath(cameFrom, current);
                }
                visited.Add(current);
                foreach (var neighbor in GetPossibleNeighborsReverse(grid, current))
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
                        cameFrom[neighbor] = current;
                    }
                }
            }
            return [];
        }

        private static List<Coords> AStar(char[][] grid, Coords start, Coords goal)
        {
            var openSet = new Dictionary<Coords, int> { { start, 0 } };
            var cameFrom = new Dictionary<Coords, Coords>();
            var gScores = new Dictionary<Coords, int>() { { start, 0 } };
            var fScores = new Dictionary<Coords, int>() { { start, start.DistanceTo(goal) } };
            while (openSet.Count > 0)
            {
                var current = openSet.First(c => c.Value == openSet.Min(x => x.Value)).Key;
                if (current == goal)
                {
                    return ReconstructPath(cameFrom, current);
                }
                openSet.Remove(current);
                foreach (var neighbor in GetPossibleNeighbors(grid, current))
                {
                    var tentativeGScore = gScores[current] + 1;
                    if (tentativeGScore < GetScore(gScores, neighbor))
                    {
                        cameFrom[neighbor] = current;
                        gScores[neighbor] = tentativeGScore;
                        var fScore = tentativeGScore + neighbor.DistanceTo(goal);
                        fScores[neighbor] = fScore;
                        openSet.TryAdd(neighbor, fScore);
                    }
                }
            }
            return [];
        }

        private static List<Coords> ReconstructPath(Dictionary<Coords, Coords> cameFrom, Coords current)
        {
            var path = new List<Coords>();
            while (cameFrom.TryGetValue(current, out current))
            {
                path.Add(current);
            }
            return path;
        }

        private static List<Coords> GetPossibleNeighbors(char[][] grid, Coords current)
        {
            var neighbors = new List<Coords>();
            var currentHeight = grid[current.Y][current.X];
            if (current.X > 0                           && (grid[current.Y][current.X - 1] - currentHeight) <= 1) { neighbors.Add(current.Left); }
            if (current.Y > 0                           && (grid[current.Y - 1][current.X] - currentHeight) <= 1) { neighbors.Add(current.Up); }
            if (current.X < grid[current.Y].Length - 1  && (grid[current.Y][current.X + 1] - currentHeight) <= 1) { neighbors.Add(current.Right); }
            if (current.Y < grid.Length - 1             && (grid[current.Y + 1][current.X] - currentHeight) <= 1) { neighbors.Add(current.Down); }
            return neighbors;
        }

        private static List<Coords> GetPossibleNeighborsReverse(char[][] grid, Coords current)
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

        private static (char[][] grid, Coords start, Coords end) ToGrid(string[] lines)
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
