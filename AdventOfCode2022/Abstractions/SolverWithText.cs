namespace AdventOfCode2022.Abstractions
{
    public abstract class SolverWithText : SolverBase<string>
    {
        public override string ParseInput(string input) => input.Trim();
    }
}
