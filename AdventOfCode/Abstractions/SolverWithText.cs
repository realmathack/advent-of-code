namespace AdventOfCode
{
    public abstract class SolverWithText : SolverBase<string>
    {
        public override string ParseInput(string input) => input.TrimEnd(Environment.NewLine.ToCharArray());
    }
}
