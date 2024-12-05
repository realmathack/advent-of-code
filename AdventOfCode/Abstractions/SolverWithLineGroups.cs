namespace AdventOfCode
{
    public abstract class SolverWithLineGroups : SolverBase<string[]>
    {
        public override string[] ParseInput(string input) => input.SplitIntoLineGroups();
    }
}
