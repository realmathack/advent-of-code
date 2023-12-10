namespace AdventOfCode
{
    public abstract class SolverWithLines : SolverBase<string[]>
    {
        public override string[] ParseInput(string input) => input.SplitIntoLines();
    }
}
