namespace AdventOfCode.Y2022.Tests
{
    public class Test07
    {
        [Fact]
        public void TestPart1()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart1();

            Assert.Equal(95437, result);
        }

        [Fact]
        public void TestPart2()
        {
            var subject = new Day07();
            subject.SetInput(_input);

            var result = subject.SolvePart2();

            Assert.Equal(24933642, result);
        }

        private const string _input = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k
";
    }
}
