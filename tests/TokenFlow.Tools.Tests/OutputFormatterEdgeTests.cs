using TokenFlow.Tools.Utilities;

namespace TokenFlow.Tools.Tests
{
    public class OutputFormatterEdgeTests
    {
        private class Complex
        {
            public string Name { get; set; }
            public double Value { get; set; }
            public decimal Price { get; set; }
            public string? Note { get; set; }
        }

        [Fact]
        public void Write_ShouldDefaultToTable_WhenFormatIsNull()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OutputFormatter.Write(new Complex { Name = "X" }, null);
            var output = sw.ToString();

            Assert.Contains("Name", output);
        }

        [Fact]
        public void Write_ShouldWriteEmptyFile_ForQuietModeWithOutput()
        {
            var temp = Path.GetTempFileName();
            try
            {
                OutputFormatter.Write(new Complex { Name = "Q" }, "quiet", temp);
                var content = File.ReadAllText(temp);
                Assert.Equal(string.Empty, content);
            }
            finally
            {
                File.Delete(temp);
            }
        }

        [Fact]
        public void Write_ShouldFallbackToToString_WhenUnknownFormat()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OutputFormatter.Write(new Complex { Name = "Test" }, "foobar");
            var output = sw.ToString();

            Assert.Contains("Test", output);
        }

        [Fact]
        public void ToTable_ShouldReturnNoData_WhenEmptyEnumerable()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OutputFormatter.Write(new List<Complex>(), "table");
            var output = sw.ToString();

            Assert.Contains("(no data)", output);
        }

        [Fact]
        public void ToCsv_ShouldReturnEmpty_WhenEmptyEnumerable()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            OutputFormatter.Write(new List<Complex>(), "csv");
            var output = sw.ToString();

            // Empty output (no header or values)
            Assert.True(string.IsNullOrWhiteSpace(output));
        }

        [Fact]
        public void SafeToString_ShouldHandleVariousTypes()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var data = new Complex { Name = "Item", Value = 1.23, Price = 9.99m, Note = null };
            OutputFormatter.Write(data, "table");
            var output = sw.ToString();

            Assert.Contains("Item", output);
            Assert.Contains("1.23", output);
            Assert.Contains("9.99", output);
        }

        [Fact]
        public void EscapeCsv_ShouldQuoteStringsWithSpecialCharacters()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            var data = new List<Complex>
            {
                new Complex { Name = "X,Y\"Z\n", Value = 1.0, Price = 2.0m, Note = "Line\nBreak" }
            };

            OutputFormatter.Write(data, "csv");
            var output = sw.ToString();

            Assert.Contains("\"", output); // contains quotes for escaped CSV
        }
    }
}
