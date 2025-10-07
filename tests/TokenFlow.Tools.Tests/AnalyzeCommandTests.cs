using TokenFlow.AI.Registry;
using TokenFlow.Tools.Commands;

namespace TokenFlow.Tools.Tests
{
    public class AnalyzeCommandTests
    {
        [Fact]
        public void Run_ShouldOutputJson_WhenFormatIsJson()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var registry = new ModelRegistry();

            // Act
            var exitCode = AnalyzeCommand.Run("Hello", registry, "json", "gpt-4o");

            // Assert
            var output = sw.ToString();
            Assert.Equal(0, exitCode);
            Assert.Contains("\"TokenCount\"", output);
            Assert.Contains("\"ModelId\"", output);
        }

        [Fact]
        public void Run_ShouldPrintTable_WhenFormatIsTable()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var registry = new ModelRegistry();

            var exitCode = AnalyzeCommand.Run("Hello TokenFlow!", registry, "table", "gpt-4o");
            var output = sw.ToString();

            Assert.Equal(0, exitCode);
            Assert.Contains("Model:", output);
            Assert.Contains("Tokens:", output);
            Assert.Contains("Estimated cost", output);
        }

        [Fact]
        public void Run_ShouldReturnError_WhenTextIsEmpty()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exitCode = AnalyzeCommand.Run("", new ModelRegistry(), "table", "gpt-4o");

            var output = sw.ToString();
            Assert.Equal(1, exitCode);
            Assert.Contains("Please provide text to analyze", output);
        }
    }
}

