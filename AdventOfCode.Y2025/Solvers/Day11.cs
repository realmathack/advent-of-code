namespace AdventOfCode.Y2025.Solvers
{
    public class Day11 : SolverWithLines
    {
        public override object SolvePart1(string[] input)
        {
            var devices = ToDevices(input);
            return FindPathCount(devices["you"], devices["out"], []);
        }

        public override object SolvePart2(string[] input)
        {
            var devices = ToDevices(input);
            var start = devices["svr"];
            var dac = devices["dac"];
            var fft = devices["fft"];
            var end = devices["out"];
            var memo = new Dictionary<(Device Start, Device End), long>();
            return FindPathCount(start, dac, memo) * FindPathCount(dac, fft, memo) * FindPathCount(fft, end, memo)
                + FindPathCount(start, fft, memo) * FindPathCount(fft, dac, memo) * FindPathCount(dac, end, memo);
        }

        private static long FindPathCount(Device current, Device end, Dictionary<(Device Start, Device End), long> memo)
        {
            // https://en.wikipedia.org/wiki/Memoization
            if (memo.TryGetValue((current, end), out var count))
            {
                return count;
            }
            if (current == end)
            {
                return 1L;
            }
            foreach (var output in current.Outputs)
            {
                count += FindPathCount(output, end, memo);
            }
            memo[(current, end)] = count;
            return count;
        }

        private static Dictionary<string, Device> ToDevices(string[] lines)
        {
            var devices = new Dictionary<string, Device>();
            foreach (var line in lines)
            {
                var id = line[0..3];
                if (!devices.TryGetValue(id, out var device))
                {
                    device = new(id, []);
                    devices[id] = device;
                }
                var outputIds = line[5..].Split(' ');
                foreach (var outputId in outputIds)
                {
                    if (!devices.TryGetValue(outputId, out var output))
                    {
                        output = new(outputId, []);
                        devices[outputId] = output;
                    }
                    device.Outputs.Add(output);
                }
            }
            return devices;
        }

        private record class Device(string Id, List<Device> Outputs);
    }
}
