namespace AdventOfCode
{
    public abstract class SolverWithLineGroups : SolverBase<string[]>
    {
        public override string[] ParseInput(string input) => input.TrimEnd(Environment.NewLine.ToCharArray()).Split(Environment.NewLine + Environment.NewLine);
    }
}
