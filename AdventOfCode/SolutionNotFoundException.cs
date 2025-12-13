namespace AdventOfCode
{
	public class SolutionNotFoundException : Exception
	{
		public SolutionNotFoundException() : base("Solution not found") { }
        public SolutionNotFoundException(string message) : base(message) { }
	}
}
