namespace AdventOfCode2022.Abstractions
{
    public abstract class SolverWithLines : SolverBase<string[]>
    {
        public override string[] ParseInput(string input) => input.SplitIntoLines();
    }
}
