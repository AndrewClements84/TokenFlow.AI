using TokenFlow.AI.Registry;
using TokenFlow.Tools.Commands;

namespace TokenFlow.Tools.Tests
{
    public class ListModelsCommandTests
    {
        [Fact]
        public void Run_ShouldListModelsFromRegistry()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var registry = new ModelRegistry();

            var exitCode = ListModelsCommand.Run(registry);
            var output = sw.ToString();

            Assert.Equal(0, exitCode);
            Assert.Contains("Models loaded from", output);
            Assert.Contains("Total models:", output);
        }

        [Fact]
        public void Run_ShouldWorkWithNullRegistry()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exitCode = ListModelsCommand.Run(null);
            var output = sw.ToString();

            Assert.Equal(0, exitCode);
            Assert.Contains("Models loaded from", output);
        }
    }
}

