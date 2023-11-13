namespace AdventOfCode.Y2017.Solvers
{
    public class Day09 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var depth = 0;
            var total = 0;
            var garbage = false;
            for (var i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '!':
                        i++;
                        break;
                    case '{':
                        if (!garbage)
                        {
                            depth++;
                        }
                        break;
                    case '}':
                        if (!garbage)
                        {
                            total += depth;
                            depth--;
                        }
                        break;
                    case '<':
                        garbage = true;
                        break;
                    case '>':
                        garbage = false;
                        break;
                    default:
                        break;
                }
            }
            return total;
        }

        public override object SolvePart2(string input)
        {
            var total = 0;
            var garbage = false;
            for (var i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '!':
                        i++;
                        break;
                    case '<':
                        if (garbage)
                        {
                            total++;
                        }
                        garbage = true;
                        break;
                    case '>':
                        garbage = false;
                        break;
                    default:
                        if (garbage)
                        {
                            total++;
                        }
                        break;
                }
            }
            return total;
        }
    }
}
