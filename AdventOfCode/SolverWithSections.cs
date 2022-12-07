namespace AdventOfCode
{
    public abstract class SolverWithSections : SolverBase<string[]>
    {
        public override string[] ParseInput(string input) => input.SplitIntoSections();
    }
}
