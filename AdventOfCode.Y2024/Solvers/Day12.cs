using Coords = AdventOfCode.Coords<int>;

namespace AdventOfCode.Y2024.Solvers
{
    public class Day12 : SolverWithCharGrid
    {
        public override object SolvePart1(char[][] grid) => ToRegions(grid).Sum(region => region.Plots.Count * region.Perimeter);
        public override object SolvePart2(char[][] grid) => ToRegions(grid, true).Sum(region => region.Plots.Count * region.Sides);

        private static List<Region> ToRegions(char[][] grid, bool calculateSides = false)
        {
            var regions = new List<Region>();
            var plotsInRegions = new HashSet<Coords>();
            var potentials = new Queue<Coords>();
            potentials.Enqueue(new(0, 0));
            while (potentials.TryDequeue(out var current))
            {
                if (plotsInRegions.Contains(current))
                {
                    continue;
                }
                var region = new Region(grid[current.Y][current.X]);
                regions.Add(region);
                var regionPlots = new Queue<Coords>();
                regionPlots.Enqueue(current);
                while (regionPlots.TryDequeue(out var plot))
                {
                    if (!region.Plots.Add(plot))
                    {
                        continue;
                    }
                    var perimeter = 0;
                    perimeter += CheckPlot(grid, potentials, regionPlots, region, plot.Left);
                    perimeter += CheckPlot(grid, potentials, regionPlots, region, plot.Up);
                    perimeter += CheckPlot(grid, potentials, regionPlots, region, plot.Right);
                    perimeter += CheckPlot(grid, potentials, regionPlots, region, plot.Down);
                    region.Perimeter += perimeter;
                    if (calculateSides)
                    {
                        region.Sides += CountCorners(grid, region, plot); // Number of sides is always the same as the number of corners
                    }
                }
                plotsInRegions.UnionWith(region.Plots);
            }
            return regions;
        }

        private static int CheckPlot(char[][] grid, Queue<Coords> potentials, Queue<Coords> regionPlots, Region region, Coords potential)
        {
            if (grid.IsOutOfBounds(potential))
            {
                return 1;
            }
            if (grid[potential.Y][potential.X] == region.PlantType)
            {
                if (!region.Plots.Contains(potential))
                {
                    regionPlots.Enqueue(potential);
                }
                return 0;
            }
            potentials.Enqueue(potential);
            return 1;
        }

        private static int CountCorners(char[][] grid, Region region, Coords plot)
        {
            var corners = 0;
            var leftPlantType  = GetPlantType(grid, plot.Left);
            var upPlantType    = GetPlantType(grid, plot.Up);
            var rightPlantType = GetPlantType(grid, plot.Right);
            var downPlantType  = GetPlantType(grid, plot.Down);
            if (IsCorner(region.PlantType, leftPlantType , upPlantType  , GetPlantType(grid, plot.UpLeft   ))) corners++;
            if (IsCorner(region.PlantType, rightPlantType, upPlantType  , GetPlantType(grid, plot.UpRight  ))) corners++;
            if (IsCorner(region.PlantType, rightPlantType, downPlantType, GetPlantType(grid, plot.DownRight))) corners++;
            if (IsCorner(region.PlantType, leftPlantType , downPlantType, GetPlantType(grid, plot.DownLeft ))) corners++;
            return corners;
        }

        private static bool IsCorner(char current, char horizontalNeighbor, char verticalNeighbor, char diagonalNeighbor)
        {
            return (horizontalNeighbor != current && verticalNeighbor != current)                                   // convex corner
                || (horizontalNeighbor == current && verticalNeighbor == current && diagonalNeighbor != current);   // concave corner
        }

        private static char GetPlantType(char[][] grid, Coords plot) => grid.IsOutOfBounds(plot) ? ' ' : grid[plot.Y][plot.X];

        private record class Region(char PlantType)
        {
            public HashSet<Coords> Plots { get; set; } = [];
            public int Perimeter { get; set; }
            public int Sides { get; set; }
        }
    }
}
