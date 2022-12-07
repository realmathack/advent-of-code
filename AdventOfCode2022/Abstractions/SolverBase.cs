namespace AdventOfCode2022.Abstractions
{
    public abstract class SolverBase<TInput> : ISolver
    {
        private string _rawInput = string.Empty;
        private TInput? _input;

        public void SetInput(string input) => _rawInput = input;
        public TInput Input => _input ??= ParseInput(_rawInput);
        public object SolvePart1() => SolvePart1(Input);
        public object SolvePart2() => SolvePart2(Input);

        public abstract TInput ParseInput(string input);
        public abstract object SolvePart1(TInput input);
        public abstract object SolvePart2(TInput input);
    }
}
