namespace AdventOfCode.Y2021.Solvers
{
    public class Day04 : SolverWithSections
    {
        public override object SolvePart1(string[] input)
        {
            var draws = input[0].Split(',');
            var boards = ToBoards(input[1..]);
            var winningDraw = 0;
            var winningBoard = Array.Empty<string[]>();
            foreach (var draw in draws)
            {
                foreach (var board in boards)
                {
                    if (HasDrawAndMark(board, draw) && IsBingo(board))
                    {
                        winningBoard = board;
                        winningDraw = int.Parse(draw);
                        break;
                    }
                }
                if (winningBoard.Length > 0)
                {
                    break;
                }
            }
            return SumUnmarkedNumbers(winningBoard) * winningDraw;
        }

        public override object SolvePart2(string[] input)
        {
            var draws = input[0].Split(',');
            var boards = ToBoards(input[1..]);
            var losingDraw = 0;
            var losingBoard = Array.Empty<string[]>();
            foreach (var draw in draws)
            {
                for (int i = boards.Count - 1; i >= 0; i--)
                {
                    if (HasDrawAndMark(boards[i], draw) && IsBingo(boards[i]))
                    {
                        if (boards.Count == 1)
                        {
                            losingBoard = boards[0];
                            losingDraw = int.Parse(draw);
                            break;
                        }
                        boards.RemoveAt(i);
                    }
                }
                if (losingBoard.Length > 0)
                {
                    break;
                }
            }
            return SumUnmarkedNumbers(losingBoard) * losingDraw;
        }

        private static List<string[][]> ToBoards(string[] sections)
        {
            return sections
                .Select(board => board
                    .SplitIntoLines()
                    .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .ToArray())
                .ToList();
        }

        private static bool HasDrawAndMark(string[][] board, string draw)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (board[i][j] == draw)
                    {
                        board[i][j] = "X";
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsBingo(string[][] board)
        {
            var rows = board.Count(row => row.All(cell => cell == "X"));
            var columns = Enumerable.Range(0, 5).Count(column => board.All(row => row[column] == "X"));
            return rows > 0 || columns > 0;
        }

        private static int SumUnmarkedNumbers(string[][] board) => board.SelectMany(row => row).Where(cell => cell != "X").Sum(int.Parse);
    }
}
