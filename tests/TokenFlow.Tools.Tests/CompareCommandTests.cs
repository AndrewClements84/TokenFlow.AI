using TokenFlow.AI.Registry;
using TokenFlow.Tools.Commands;

namespace TokenFlow.Tools.Tests
{
    public class CompareCommandTests
    {
        [Fact]
        public void Run_ShouldCompareMultipleModels()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var registry = new ModelRegistry();
            var models = new[] { "gpt-4o", "gpt-4o-mini" };

            var exitCode = CompareCommand.Run("Hello world", models, registry);
            var output = sw.ToString();

            Assert.Equal(0, exitCode);
            Assert.Contains("Comparing", output);
            Assert.Contains("gpt-4o", output);
        }

        [Fact]
        public void Run_ShouldReturnError_WhenNoModelsProvided()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exitCode = CompareCommand.Run("Hello", Array.Empty<string>(), new ModelRegistry());
            var output = sw.ToString();

            Assert.Equal(1, exitCode);
            Assert.Contains("Please specify at least one model", output);
        }

        [Fact]
        public void Run_ShouldReturnError_WhenTextMissing()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exitCode = CompareCommand.Run("", new[] { "gpt-4o" }, new ModelRegistry());
            var output = sw.ToString();

            Assert.Equal(1, exitCode);
            Assert.Contains("Please provide text to compare models with", output);
        }

        [Fact]
        public void Run_ShouldCreateRegistry_WhenRegistryIsNull()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var models = new[] { "gpt-4o" };
            var exitCode = CompareCommand.Run("Hello world", models, null);
            var output = sw.ToString();

            Assert.Equal(0, exitCode);
            Assert.Contains("Comparing", output);
        }

        [Fact]
        public void Run_ShouldHandleModelError_Gracefully()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var models = new[] { "nonexistent-model" }; // will trigger exception
            var exitCode = CompareCommand.Run("Hello world", models, new ModelRegistry());
            var output = sw.ToString();

            Assert.Equal(0, exitCode); // still returns success (handled gracefully)
            Assert.Contains("ERROR:", output);
        }
    }
}

