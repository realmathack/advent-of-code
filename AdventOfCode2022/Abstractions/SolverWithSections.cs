namespace AdventOfCode2022.Abstractions
{
    public abstract class SolverWithSections : SolverBase<string[]>
    {
        public override string[] ParseInput(string input) => input.SplitIntoSections();
    }
}
