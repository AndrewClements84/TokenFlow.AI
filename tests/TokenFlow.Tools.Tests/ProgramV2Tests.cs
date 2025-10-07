using System;
using System.IO;
using Xunit;

namespace TokenFlow.Tools.Tests
{
    public class ProgramV2Tests
    {
        [Fact]
        public void Main_ShouldReadFromInputFile()
        {
            var tempInput = Path.GetTempFileName();
            File.WriteAllText(tempInput, "Hello from file!");

            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "analyze", "--input", tempInput, "--format", "json" };
            var exit = TokenFlow.Tools.Program.Main(args);

            var output = sw.ToString();
            Assert.Equal(0, exit);
            Assert.Contains("\"TokenCount\"", output);

            File.Delete(tempInput);
        }

        [Fact]
        public void Main_ShouldWriteOutputFile()
        {
            var tempOut = Path.GetTempFileName();
            File.Delete(tempOut); // ensure clean start

            var args = new[] { "analyze", "Hello world", "--format", "json", "--output", tempOut };
            var exit = TokenFlow.Tools.Program.Main(args);

            Assert.Equal(0, exit);
            Assert.True(File.Exists(tempOut));
            Assert.Contains("\"TokenCount\"", File.ReadAllText(tempOut));

            File.Delete(tempOut);
        }

        [Fact]
        public void Main_ShouldHandleQuietFormat()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "analyze", "Quiet mode test", "--format", "quiet" };
            var exit = TokenFlow.Tools.Program.Main(args);

            var output = sw.ToString();
            Assert.Equal(0, exit);
            Assert.True(string.IsNullOrWhiteSpace(output));
        }

        [Fact]
        public void Main_ShouldFailGracefully_WhenInputFileMissing()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "analyze", "--input", "nonexistent.txt" };
            var exit = TokenFlow.Tools.Program.Main(args);
            var output = sw.ToString();

            Assert.Equal(1, exit);
            Assert.Contains("Failed to read input file", output);
        }

        [Fact]
        public void Main_ShouldShowHelp_ForHelpCommand()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "help" };
            var exit = TokenFlow.Tools.Program.Main(args);
            var output = sw.ToString();

            Assert.Equal(0, exit);
            Assert.Contains("Usage:", output);
        }

        [Fact]
        public void Main_ShouldCatchException_WhenInputFileUnreadable()
        {
            var tempPath = Path.GetTempFileName();

            // Lock the file so File.ReadAllText() throws
            using var stream = new FileStream(tempPath, FileMode.Open, FileAccess.Read, FileShare.None);

            using var sw = new StringWriter();
            Console.SetOut(sw);

            var args = new[] { "analyze", "--input", tempPath, "--format", "json" };
            var exit = TokenFlow.Tools.Program.Main(args);

            var output = sw.ToString();
            Assert.Equal(1, exit);
            Assert.Contains("Failed to read input file", output);

            stream.Close();
            File.Delete(tempPath);
        }

    }
}

