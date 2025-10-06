using System;
using System.IO;
using TokenFlow.Tools;
using TokenFlow.Tools.Tests.Helpers;
using Xunit;

namespace TokenFlow.Tools.Tests
{
    public class ProgramRegistryTests
    {
        private const string SampleText = "TokenFlow is awesome!";

        [Fact]
        public void Main_ShouldUseEmbeddedRegistry_WhenNoRegistrySpecified()
        {
            var output = ConsoleCaptureHelper.Capture(() =>
            {
                Program.Main(new[] { "count", SampleText });
            });

            Assert.Contains("[TokenFlow.AI] Loaded model registry from", output);
            Assert.Contains("Tokens:", output);
        }

        [Fact]
        public void Main_ShouldUseLocalRegistry_WhenJsonFileProvided()
        {
            // Arrange a temporary JSON file
            var jsonPath = Path.GetTempFileName();
            File.WriteAllText(jsonPath, @"[
                {
                    ""Id"": ""gpt-local"",
                    ""Family"": ""openai"",
                    ""TokenizerName"": ""approx"",
                    ""MaxInputTokens"": 1000,
                    ""MaxOutputTokens"": 500,
                    ""InputPricePer1K"": 0.01,
                    ""OutputPricePer1K"": 0.02
                }
            ]");

            var output = ConsoleCaptureHelper.Capture(() =>
            {
                Program.Main(new[] { "count", SampleText, "--registry", jsonPath });
            });

            Assert.Contains("Loaded model registry from Local", output);
            Assert.Contains("Tokens:", output);

            File.Delete(jsonPath);
        }

        [Fact]
        public void Main_ShouldFallbackToEmbedded_WhenRemoteUrlInvalid()
        {
            var output = ConsoleCaptureHelper.Capture(() =>
            {
                Program.Main(new[]
                {
                    "count",
                    SampleText,
                    "--registry",
                    "https://example.invalid/models.json"
                });
            });

            Assert.Contains("Loaded model registry from Embedded", output);
            Assert.Contains("Tokens:", output);
        }
    }
}

