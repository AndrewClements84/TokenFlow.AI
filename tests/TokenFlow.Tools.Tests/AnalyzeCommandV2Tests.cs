using System;
using System.IO;
using TokenFlow.Tools.Commands;
using Xunit;

namespace TokenFlow.Tools.Tests
{
    public class AnalyzeCommandV2Tests
    {
        [Fact]
        public void Run_ShouldOutputJson_WhenFormatIsJson()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exit = AnalyzeCommand.Run("Hello world", null, "json", "gpt-4o", null);
            var output = sw.ToString();

            Assert.Equal(0, exit);
            Assert.Contains("\"TokenCount\"", output);
        }

        [Fact]
        public void Run_ShouldWriteOutputFile_WhenOutputSpecified()
        {
            var temp = Path.GetTempFileName();
            try
            {
                var exit = AnalyzeCommand.Run("Hello TokenFlow", null, "json", "gpt-4o", temp);
                Assert.Equal(0, exit);

                var text = File.ReadAllText(temp);
                Assert.Contains("\"TokenCount\"", text);
            }
            finally
            {
                File.Delete(temp);
            }
        }

        [Fact]
        public void Run_ShouldHandleQuietFormat()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exit = AnalyzeCommand.Run("Hello", null, "quiet", "gpt-4o", null);
            var output = sw.ToString();

            Assert.Equal(0, exit);
            Assert.True(string.IsNullOrWhiteSpace(output));
        }

        [Fact]
        public void Run_ShouldReturnError_WhenTextIsMissing()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exit = AnalyzeCommand.Run("", null, "json", "gpt-4o", null);
            var output = sw.ToString();

            Assert.Equal(1, exit);
            Assert.Contains("Please provide text", output);
        }

        [Fact]
        public void AnalyzeCommand_ShouldProduceAlignedTableOutput()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exit = AnalyzeCommand.Run("TokenFlow CLI test", null, "table", "gpt-4o");
            var output = sw.ToString();

            Assert.Equal(0, exit);
            Assert.Contains("Model", output);
            Assert.Contains("Estimated", output);
        }

        [Fact]
        public void AnalyzeCommand_ShouldProduceCsvOutput()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var exit = AnalyzeCommand.Run("TokenFlow CLI test", null, "csv", "gpt-4o");
            var output = sw.ToString();

            Assert.Equal(0, exit);
            Assert.Contains("ModelId", output);
            Assert.Contains(",", output);
        }

        [Fact]
        public void Run_ShouldCatchException_AndReturnErrorCode()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Invalid model ID forces TokenFlowClient to throw or fail internally
            var exit = AnalyzeCommand.Run("text", null, "table", modelId: "invalid_model", outputPath: null);
            var output = sw.ToString();

            // Assert
            Assert.Equal(1, exit);
            Assert.Contains("Error:", output);
        }
    }
}

