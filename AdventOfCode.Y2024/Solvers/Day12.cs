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
                }
                if (calculateSides)
                {
                    region.Sides = 4; // TODO: Calculate sides
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

        private class Region(char plantType)
        {
            public char PlantType { get; } = plantType;
            public HashSet<Coords> Plots { get; set; } = [];
            public int Perimeter { get; set; }
            public int Sides { get; set; }
        }
    }
}
