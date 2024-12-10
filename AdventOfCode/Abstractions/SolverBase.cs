namespace AdventOfCode
{
    public abstract class SolverBase<TInput> : ISolver
        where TInput : class
    {
        private TInput? _input = null;

        public void SetInput(string input) => _input = ParseInput(input);
        public object SolvePart1() => SolvePart1(_input ?? throw new InvalidOperationException());
        public object SolvePart2() => SolvePart2(_input ?? throw new InvalidOperationException());

        public abstract TInput ParseInput(string input);
        public abstract object SolvePart1(TInput input);
        public abstract object SolvePart2(TInput input);
    }
}
