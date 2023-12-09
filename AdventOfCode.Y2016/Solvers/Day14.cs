namespace AdventOfCode.Y2016.Solvers
{
    public class Day14 : SolverWithText
    {
        private int _lastHashIndex = 0;
        private Dictionary<int, string> _hashes = [];

        public override object SolvePart1(string input)
        {
            var keys = new Dictionary<int, string>();
            for (int i = 0; keys.Count < 64; i++)
            {
                if (!_hashes.TryGetValue(i, out var hash))
                {
                    GenerateNextHashBatch(input);
                    hash = _hashes[i];
                }
                if (_lastHashIndex < i + 1001)
                {
                    GenerateNextHashBatch(input);
                }
                if (IsKey(hash, i))
                {
                    keys[i] = hash;
                }
            }
            return keys.Last().Key;
        }

        public override object SolvePart2(string input)
        {
            _lastHashIndex = 0;
            _hashes = [];
            var keys = new Dictionary<int, string>();
            for (int i = 0; keys.Count < 64; i++)
            {
                if (!_hashes.TryGetValue(i, out var hash))
                {
                    GenerateNextHashBatchWithStretching(input);
                    hash = _hashes[i];
                }
                if (_lastHashIndex < i + 1001)
                {
                    GenerateNextHashBatchWithStretching(input);
                }
                if (IsKey(hash, i))
                {
                    keys[i] = hash;
                }
            }
            return keys.Last().Key;
        }

        private void GenerateNextHashBatch(string input)
        {
            var target = _lastHashIndex + 1000;
            for (;  _lastHashIndex < target; _lastHashIndex++)
            {
                _hashes[_lastHashIndex] = (input + _lastHashIndex).ToMD5Hex();
            }
        }

        private void GenerateNextHashBatchWithStretching(string input)
        {
            var target = _lastHashIndex + 1000;
            for (; _lastHashIndex < target; _lastHashIndex++)
            {
                var tmp = input + _lastHashIndex;
                for (int i = 0; i < 2017; i++)
                {
                    tmp = tmp.ToMD5Hex();
                }
                _hashes[_lastHashIndex] = tmp;
            }
        }

        private bool IsKey(string potential, int current)
        {
            char? letter = null;
            for (int i = 0; i < potential.Length - 2; i++)
            {
                if (potential[i] == potential[i+1] && potential[i] == potential[i+2])
                {
                    letter = potential[i];
                    break;
                }
            }
            if (letter is null)
            {
                return false;
            }
            var pattern = new string(letter.Value, 5);
            for (int i = current + 1; i < current + 1001; i++)
            {
                if (_hashes[i].Contains(pattern))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
