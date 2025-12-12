using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2025.Solvers
{
    public class Day09 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var tiles = input.Select(Coords.Parse).ToArray();
            var largest = 0L;
            for (int a = 0; a < tiles.Length; a++)
            {
                for (int b = a + 1; b < tiles.Length; b++)
                {
                    var area = (1L + Math.Abs(tiles[a].Y - tiles[b].Y)) * (1L + Math.Abs(tiles[a].X - tiles[b].X));
                    if (area > largest)
                    {
                        largest = area;
                    }
                }
            }
            return largest;
        }

        public override object SolvePart2(string[] input)
        {
            var tiles = input.Select(Coords.Parse).ToArray();
            var (projectedGrid, projectedXs, projectedYs) = ProjectGrid(tiles);
            return ToRectangles(tiles)
                .OrderByDescending(rectangle => rectangle.Area)
                .First(rectangle => IsInside(rectangle, projectedGrid, projectedXs, projectedYs))
                .Area;
        }

        private static bool IsInside(Rectangle rectangle, bool?[][] projectedGrid, Dictionary<int, int> projectedXs, Dictionary<int, int> projectedYs)
        {
            for (int y = projectedYs[rectangle.TopY]; y <= projectedYs[rectangle.BottomY]; y++)
            {
                for (int x = projectedXs[rectangle.LeftX]; x <= projectedXs[rectangle.RightX]; x++)
                {
                    if (projectedGrid[y][x] == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static (bool?[][] ProjectedGrid, Dictionary<int, int> ProjectedXs, Dictionary<int, int> ProjectedYs) ProjectGrid(Coords[] tiles)
        {
            var distinctXs = tiles.Select(tile => tile.X).Distinct().OrderBy(x => x).ToArray();
            var distinctYs = tiles.Select(tile => tile.Y).Distinct().OrderBy(y => y).ToArray();
            var projectedXs = distinctXs.Select((x, i) => new KeyValuePair<int, int>(x, i + 1)).ToDictionary();
            var projectedYs = distinctYs.Select((y, i) => new KeyValuePair<int, int>(y, i + 1)).ToDictionary();
            var projectedGrid = new bool?[distinctYs.Length + 2][];
            for (int y = 0; y < projectedGrid.Length; y++)
            {
                projectedGrid[y] = new bool?[distinctXs.Length + 2];
            }
            DrawOutline(projectedGrid, projectedXs[tiles[^1].X], projectedYs[tiles[^1].Y], projectedXs[tiles[0].X], projectedYs[tiles[0].Y]);
            for (int i = 0; i < tiles.Length - 1; i++)
            {
                DrawOutline(projectedGrid, projectedXs[tiles[i].X], projectedYs[tiles[i].Y], projectedXs[tiles[i + 1].X], projectedYs[tiles[i + 1].Y]);
            }
            FloodFillOutside(projectedGrid);
            FillInside(projectedGrid);
            return (projectedGrid, projectedXs, projectedYs);
        }

        private static void FillInside(bool?[][] projectedGrid)
        {
            for (int y = 0; y < projectedGrid.Length; y++)
            {
                for (int x = 0; x < projectedGrid[y].Length; x++)
                {
                    if (projectedGrid[y][x] is null)
                    {
                        projectedGrid[y][x] = true;
                    }
                }
            }
        }

        private static void FloodFillOutside(bool?[][] projectedGrid)
        {
            var queue = new Queue<Coords>();
            queue.Enqueue(new(0, 0));
            while (queue.TryDequeue(out var current))
            {
                foreach (var neighbor in current.Neighbors)
                {
                    if (!projectedGrid.IsOutOfBounds(neighbor) && projectedGrid[neighbor.Y][neighbor.X] is null)
                    {
                        projectedGrid[neighbor.Y][neighbor.X] = false;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        private static void DrawOutline(bool?[][] projectedGrid, int x1, int y1, int x2, int y2)
        {
            if (x1 > x2)
            {
                (x1, x2) = (x2, x1);
            }
            if (y1 > y2)
            {
                (y1, y2) = (y2, y1);
            }
            for (int y = y1; y < y2 + 1; y++)
            {
                for (int x = x1; x < x2 + 1; x++)
                {
                    projectedGrid[y][x] = true;
                }
            }
        }

        private static List<Rectangle> ToRectangles(Coords[] tiles)
        {
            var rectangles = new List<Rectangle>();
            for (int a = 0; a < tiles.Length; a++)
            {
                for (int b = a + 1; b < tiles.Length; b++)
                {
                    rectangles.Add(new(tiles[a], tiles[b]));
                }
            }
            return rectangles;
        }

        private readonly record struct Rectangle
        {
            public Rectangle(Coords a, Coords b)
            {
                LeftX = (a.X < b.X) ? a.X : b.X;
                TopY = (a.Y < b.Y) ? a.Y : b.Y;
                RightX = (a.X > b.X) ? a.X : b.X;
                BottomY = (a.Y > b.Y) ? a.Y : b.Y;
                Area = (1L + RightX - LeftX) * (1L + BottomY - TopY);
            }

            public int LeftX { get; init; }
            public int TopY { get; init; }
            public int RightX { get; init; }
            public int BottomY { get; init; }
            public long Area { get; init; }
        }
    }
}
