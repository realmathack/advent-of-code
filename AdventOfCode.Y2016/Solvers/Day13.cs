namespace AdventOfCode.Y2016.Solvers
{
    public class Day13(Coords _goal) : SolverWithText
    {
        public Day13() : this(new(31, 39)) { }

        public override object SolvePart1(string input) => new AStar(int.Parse(input)).FindShortestPath(new Coords(1, 1), _goal).Count;

        public override object SolvePart2(string input)
        {
            var designerFavNumber = int.Parse(input);
            var nodes = new List<Coords>();
            var cursor = new Coords(51, 0);
            while (cursor.X >= 0)
            {
                nodes.AddRange(Enumerable.Range(0, cursor.X + 1).Select(i => new Coords(i, cursor.Y)).Where(potential => !IsWall(potential, designerFavNumber)));
                cursor = cursor.DownLeft;
            }
            var distances = new Dijkstra(designerFavNumber).GetShortestDistances(new Coords(1, 1), nodes);
            return distances.Values.Count(distance => distance <= 50);
        }

        private static List<Coords> FindPossibleNeighbors(Coords node, int designerFavNumber)
        {
            var neighbors = new List<Coords>();
            if (node.X > 0 && !IsWall(node.Left,  designerFavNumber)) { neighbors.Add(node.Left); }
            if (node.Y > 0 && !IsWall(node.Up,    designerFavNumber)) { neighbors.Add(node.Up); }
            if (              !IsWall(node.Right, designerFavNumber)) { neighbors.Add(node.Right); }
            if (              !IsWall(node.Down,  designerFavNumber)) { neighbors.Add(node.Down); }
            return neighbors;
        }

        private static bool IsWall(Coords coords, int designerFavNumber)
        {
            var value = coords.X * coords.X + 3 * coords.X + 2 * coords.X * coords.Y + coords.Y + coords.Y * coords.Y;
            value += designerFavNumber;
            var binary = Convert.ToString(value, 2);
            return binary.Count(bit => bit == '1') % 2 == 1;
        }

        private class AStar(int designerFavNumber) : Graphs.AStar<Coords>
        {
            protected override int CalculateHeuristic(Coords node, Coords goal) => node.DistanceTo(goal);
            protected override List<Coords> FindNeighbors(Coords node) => FindPossibleNeighbors(node, designerFavNumber);
        }

        private class Dijkstra(int designerFavNumber) : Graphs.Dijkstra<Coords>
        {
            protected override List<Coords> FindNeighbors(Coords node) => FindPossibleNeighbors(node, designerFavNumber);
        }
    }
}
