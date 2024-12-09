namespace AdventOfCode.Y2024.Solvers
{
    public class Day09 : SolverWithText
    {
        public override object SolvePart1(string input)
        {
            var memory = ToMemoryPart1(input);
            var freeIndex = Array.IndexOf(memory, int.MinValue);
            var filledIndex = Array.FindLastIndex(memory, x => x != int.MinValue);
            if (freeIndex != -1 && filledIndex != -1)
            {
                while (freeIndex < filledIndex)
                {
                    memory[freeIndex] = memory[filledIndex];
                    memory[filledIndex] = int.MinValue;
                    freeIndex = Array.IndexOf(memory, int.MinValue, freeIndex);
                    filledIndex = Array.FindLastIndex(memory, filledIndex, x => x != int.MinValue);
                }
            }
            return CalculateChecksum(memory);
        }

        public override object SolvePart2(string input)
        {
            var (filledBlocks, emptyBlocks) = ToMemoryPart2(input);
            var blocksToMove = new Queue<Block>(filledBlocks.OrderByDescending(x => x.Index));
            while (blocksToMove.TryDequeue(out var current))
            {
                var destinationIndex = emptyBlocks.FindIndex(x => x.Size >= current.Size && x.Index < current.Index);
                if (destinationIndex == -1)
                {
                    continue;
                }
                var destination = emptyBlocks[destinationIndex];
                emptyBlocks.RemoveAt(destinationIndex);
                current.Index = destination.Index;
                if (destination.Size > current.Size)
                {
                    var index = destination.Index + current.Size;
                    emptyBlocks.Insert(destinationIndex, new(int.MinValue, destination.Size - current.Size, index));
                }
            }
            return CalculateChecksum(filledBlocks);
        }

        private static long CalculateChecksum(int[] memory)
        {
            var checksum = 0L;
            var filledBlocks = memory.Where(x => x != int.MinValue).ToArray();
            for (int i = 0; i < filledBlocks.Length; i++)
            {
                checksum += i * memory[i];
            }
            return checksum;
        }

        private static long CalculateChecksum(List<Block> blocks)
        {
            var checksum = 0L;
            var sorted = blocks.OrderBy(x => x.Index).ToArray();
            foreach (var block in sorted)
            {
                for (int i = 0; i < block.Size; i++)
                {
                    checksum += (block.Index + i) * block.Id;
                }
            }
            return checksum;
        }

        private static int[] ToMemoryPart1(string diskMap)
        {
            var memory = new List<int>(diskMap.Length * 5);
            var freeSpace = false;
            var id = 0;
            for (int i = 0; i < diskMap.Length; i++)
            {
                var size = diskMap[i] - '0';
                var blockId = freeSpace ? int.MinValue : id++;
                for (int j = 0; j < size; j++)
                {
                    memory.Add(blockId);
                }
                freeSpace = !freeSpace;
            }
            return [.. memory];
        }

        private static (List<Block> FilledBlocks, List<Block> FreeBlocks) ToMemoryPart2(string diskMap)
        {
            var filledBlocks = new List<Block>(diskMap.Length / 2);
            var emptyBlocks = new List<Block>(diskMap.Length / 2);
            var freeSpace = false;
            var index = 0;
            var id = 0;
            for (int i = 0; i < diskMap.Length; i++)
            {
                var size = diskMap[i] - '0';
                if (size > 0)
                {
                    var blockId = freeSpace ? int.MinValue : id++;
                    var block = new Block(blockId, size, index);
                    index += size;
                    (freeSpace ? emptyBlocks : filledBlocks).Add(block);
                }
                freeSpace = !freeSpace;
            }
            return (filledBlocks, emptyBlocks);
        }

        private class Block(int id, int size, int index)
        {
            public int Id => id;
            public int Size => size;
            public int Index { get; set; } = index;
        }
    }
}
