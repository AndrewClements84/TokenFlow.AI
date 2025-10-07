using System;
using System.Collections.Generic;
using System.IO;
using TokenFlow.Tools.Utilities;
using Xunit;

namespace TokenFlow.Tools.Tests
{
    public class OutputFormatterTests
    {
        private class Dummy
        {
            public string Name { get; set; } = "Alpha";
            public int Count { get; set; } = 42;
        }

        [Fact]
        public void Write_ShouldProduceJson()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OutputFormatter.Write(new Dummy(), "json");
            var output = sw.ToString();

            Assert.Contains("\"Name\"", output);
            Assert.Contains("\"Count\"", output);
        }

        [Fact]
        public void Write_ShouldProduceCsv_ForList()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var list = new List<Dummy> { new Dummy(), new Dummy() };
            OutputFormatter.Write(list, "csv");

            var output = sw.ToString();
            Assert.Contains("Name,Count", output);
        }

        [Fact]
        public void Write_ShouldProduceTable_ForList()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var list = new List<Dummy> { new Dummy() };
            OutputFormatter.Write(list, "table");

            var output = sw.ToString();
            Assert.Contains("Name", output);
            Assert.Contains("Count", output);
        }

        [Fact]
        public void Write_ShouldBeQuiet_WhenFormatIsQuiet()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OutputFormatter.Write(new Dummy(), "quiet");
            Assert.True(string.IsNullOrWhiteSpace(sw.ToString()));
        }

        [Fact]
        public void Write_ShouldWriteToFile_WhenOutputPathProvided()
        {
            var temp = Path.GetTempFileName();
            try
            {
                OutputFormatter.Write(new Dummy(), "json", temp);
                Assert.True(File.Exists(temp));
                var content = File.ReadAllText(temp);
                Assert.Contains("\"Name\"", content);
            }
            finally
            {
                File.Delete(temp);
            }
        }

        [Fact]
        public void Write_ShouldHandleNullData_Gracefully()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OutputFormatter.Write(null, "table");
            OutputFormatter.Write(null, "csv");
            OutputFormatter.Write(null, "json");
            OutputFormatter.Write(null, "quiet");
            Assert.True(true); // no exception
        }
    }
}

