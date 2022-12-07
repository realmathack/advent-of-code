namespace AdventOfCode2022.Abstractions
{
    public interface ISolver
    {
        public object SolvePart1();
        public object SolvePart2();
        public void SetInput(string input);
    }
}
