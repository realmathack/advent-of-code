using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day15 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var (grid, boxes, robot) = ToGridPart1(input[0]);
            var moves = input[1].Replace(Environment.NewLine, string.Empty);
            foreach (var move in moves)
            {
                var offset = GetOffset(move);
                var next = robot + offset;
                var boxesToMove = new HashSet<Coords>();
                while (boxes.Contains(next))
                {
                    boxesToMove.Add(next);
                    next += offset;
                }
                if (grid[next.Y][next.X] == '#')
                {
                    continue;
                }
                robot += offset;
                boxes.ExceptWith(boxesToMove);
                foreach (var box in boxesToMove)
                {
                    boxes.Add(box + offset);
                }
            }
            return boxes.Sum(box => box.Y * 100 + box.X);
        }

        public override object SolvePart2(string[] input)
        {
            var (grid, boxes, robot) = ToGridPart2(input[0]);
            var moves = input[1].Replace(Environment.NewLine, string.Empty);
            foreach (var move in moves)
            {
                var offset = GetOffset(move);
                var next = robot + offset;
                if (grid[next.Y][next.X] == '#')
                {
                    continue;
                }
                var hitWall = false;
                var boxesToMove = new HashSet<(Coords Left, Coords Right)>();
                if (boxes.ContainsKey(next))
                {
                    var queue = new Queue<Coords>();
                    var visited = new HashSet<Coords>();
                    queue.Enqueue(next - offset);
                    while (queue.TryDequeue(out var current))
                    {
                        next = current + offset;
                        if (!visited.Add(next))
                        {
                            continue;
                        }
                        if (grid[next.Y][next.X] == '#')
                        {
                            hitWall = true;
                            break;
                        }
                        if (boxes.TryGetValue(next, out var box))
                        {
                            boxesToMove.Add(box);
                            queue.Enqueue(box.Left);
                            queue.Enqueue(box.Right);
                        }
                    }
                }
                if (hitWall)
                {
                    continue;
                }
                robot += offset;
                foreach (var (left, right) in boxesToMove)
                {
                    boxes.Remove(left);
                    boxes.Remove(right);
                }
                foreach (var (left, right) in boxesToMove)
                {
                    var newLeft = left + offset;
                    var newRight = right + offset;
                    boxes.Add(newLeft, (newLeft, newRight));
                    boxes.Add(newRight, (newLeft, newRight));
                }
            }
            return boxes.Values.Distinct().Sum(box => box.Left.Y * 100 + box.Left.X);
        }

        private static Coords GetOffset(char direction) => direction switch
        {
            '^' => Coords.OffsetUp,
            '>' => Coords.OffsetRight,
            'v' => Coords.OffsetDown,
            '<' => Coords.OffsetLeft,
            _ => throw new ArgumentException($"Unknown direction: {direction}", nameof(direction))
        };

        private static (char[][] Grid, HashSet<Coords> Boxes, Coords Robot) ToGridPart1(string input)
        {
            var lines = input.SplitIntoLines();
            var grid = new char[lines.Length][];
            var boxes = new HashSet<Coords>();
            Coords? robot = null;
            for (int y = 0; y < grid.Length; y++)
            {
                var line = lines[y];
                var row = new char[line.Length];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == 'O')
                    {
                        boxes.Add(new(x, y));
                        row[x] = '.';
                        continue;
                    }
                    if (line[x] == '@')
                    {
                        robot = new(x, y);
                        row[x] = '.';
                        continue;
                    }
                    row[x] = line[x];
                }
                grid[y] = row;
            }
            if (robot is null)
            {
                throw new InvalidOperationException("Robot not found!");
            }
            return (grid, boxes, robot.Value);
        }

        private static (char[][] Grid, Dictionary<Coords, (Coords Left, Coords Right)> Boxes, Coords Robot) ToGridPart2(string input)
        {
            var lines = input.SplitIntoLines();
            var grid = new char[lines.Length][];
            var boxes = new Dictionary<Coords, (Coords Left, Coords Right)>();
            Coords? robot = null;
            for (int y = 0; y < grid.Length; y++)
            {
                var line = lines[y];
                var row = new char[line.Length * 2];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == 'O')
                    {
                        var box = (new Coords(x * 2, y), new Coords(x * 2 + 1, y));
                        boxes.Add(new(x * 2, y), box);
                        boxes.Add(new(x * 2 + 1, y), box);
                        row[x * 2] = '.';
                        row[x * 2 + 1] = '.';
                        continue;
                    }
                    if (line[x] == '@')
                    {
                        robot = new(x * 2, y);
                        row[x * 2] = '.';
                        row[x * 2 + 1] = '.';
                        continue;
                    }
                    row[x * 2] = line[x];
                    row[x * 2 + 1] = line[x];
                }
                grid[y] = row;
            }
            if (robot is null)
            {
                throw new InvalidOperationException("Robot not found!");
            }
            return (grid, boxes, robot.Value);
        }
    }
}
