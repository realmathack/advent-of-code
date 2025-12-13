using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day15 : SolverWithLineGroups
    {
        public override object SolvePart1(string[] input)
        {
            var (walls, boxes, robot) = ToGridPart1(input[0]);
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
                if (walls.Contains(next))
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
            var (walls, boxes, robot) = ToGridPart2(input[0]);
            var moves = input[1].Replace(Environment.NewLine, string.Empty);
            foreach (var move in moves)
            {
                var offset = GetOffset(move);
                var next = robot + offset;
                if (walls.Contains(next))
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
                        if (walls.Contains(next))
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

        private static (HashSet<Coords> Walls, HashSet<Coords> Boxes, Coords Robot) ToGridPart1(string input)
        {
            var lines = input.SplitIntoLines();
            var walls = new HashSet<Coords>();
            var boxes = new HashSet<Coords>();
            Coords? robot = null;
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == 'O')
                    {
                        boxes.Add(new(x, y));
                        continue;
                    }
                    if (line[x] == '@')
                    {
                        robot = new(x, y);
                        continue;
                    }
                    if (line[x] == '#')
                    {
                        walls.Add(new(x, y));
                    }
                }
            }
            if (robot is null)
            {
                throw new ImpossibleException("Robot not found!");
            }
            return (walls, boxes, robot.Value);
        }

        private static (HashSet<Coords> Walls, Dictionary<Coords, (Coords Left, Coords Right)> Boxes, Coords Robot) ToGridPart2(string input)
        {
            var lines = input.SplitIntoLines();
            var walls = new HashSet<Coords>();
            var boxes = new Dictionary<Coords, (Coords Left, Coords Right)>();
            Coords? robot = null;
            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == 'O')
                    {
                        var box = (new Coords(x * 2, y), new Coords(x * 2 + 1, y));
                        boxes.Add(new(x * 2, y), box);
                        boxes.Add(new(x * 2 + 1, y), box);
                        continue;
                    }
                    if (line[x] == '@')
                    {
                        robot = new(x * 2, y);
                        continue;
                    }
                    if (line[x] == '#')
                    {
                        walls.Add(new(x * 2, y));
                        walls.Add(new(x * 2 + 1, y));
                    }
                }
            }
            if (robot is null)
            {
                throw new ImpossibleException("Robot not found!");
            }
            return (walls, boxes, robot.Value);
        }
    }
}
