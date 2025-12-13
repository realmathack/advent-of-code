namespace AdventOfCode
{
    public class ImpossibleException : Exception
    {
        public ImpossibleException() : base("Exception should not be thrown") { }
        public ImpossibleException(string message) : base(message) { }
    }
}
