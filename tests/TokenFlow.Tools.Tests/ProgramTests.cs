namespace TokenFlow.Tools.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_ShouldRunAnalyzeCommand()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "analyze", "Hello TokenFlow!" };
            var exitCode = Program.Main(args);

            var output = sw.ToString();
            Assert.Equal(0, exitCode);
            Assert.Contains("Model:", output);
        }

        [Fact]
        public void Main_ShouldRunCompareCommand()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "compare", "Hello world", "--models", "gpt-4o,gpt-4o-mini" };
            var exitCode = Program.Main(args);

            var output = sw.ToString();
            Assert.Equal(0, exitCode);
            Assert.Contains("Comparing", output);
        }

        [Fact]
        public void Main_ShouldRunListModelsCommand()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "list-models" };
            var exitCode = Program.Main(args);

            var output = sw.ToString();
            Assert.Equal(0, exitCode);
            Assert.Contains("Models loaded from", output);
        }

        [Fact]
        public void Main_ShouldReturnError_ForUnknownCommand()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "invalidcmd" };
            var exitCode = Program.Main(args);

            var output = sw.ToString();
            Assert.Equal(1, exitCode);
            Assert.Contains("Unknown command", output);
        }

        [Fact]
        public void Main_ShouldShowUsage_WhenNoArgumentsProvided()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exitCode = Program.Main(Array.Empty<string>());
            var output = sw.ToString();

            Assert.Equal(1, exitCode);
            Assert.Contains("Usage:", output);
        }
    }
}
