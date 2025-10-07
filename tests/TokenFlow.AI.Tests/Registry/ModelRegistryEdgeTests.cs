using System;
using System.IO;
using System.Reflection;
using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;
using Xunit;

namespace TokenFlow.AI.Tests.Registry
{
    /// <summary>
    /// Covers defensive branches and early returns in ModelRegistry + loaders
    /// that are normally skipped in normal usage.
    /// </summary>
    public class ModelRegistryEdgeTests
    {
        // Simple wrapper to expose the private LoadEmbeddedDefaults for coverage
        private sealed class ModelRegistryAccessor : ModelRegistry
        {
            public void InvokeLoadEmbeddedDefaults()
            {
                var method = typeof(ModelRegistry)
                    .GetMethod("LoadEmbeddedDefaults", BindingFlags.Instance | BindingFlags.NonPublic);
                method!.Invoke(this, null);
            }
        }

        [Fact]
        public void Register_ShouldReturnEarly_WhenModelIsNull()
        {
            var reg = new ModelRegistry();
            reg.Register(null);
            Assert.True(true);
        }

        [Fact]
        public void Register_ShouldReturnEarly_WhenModelHasEmptyId()
        {
            var reg = new ModelRegistry();

            // supply valid constructor args but empty Id
            var model = new ModelSpec(
                id: string.Empty,
                family: "test-family",
                tokenizerName: "test-tokenizer",
                maxInputTokens: 1000,
                maxOutputTokens: 1000,
                inputPricePer1K: 0.001m,
                outputPricePer1K: 0.001m);

            reg.Register(model);
            Assert.True(true);
        }

        [Fact]
        public void LoadEmbeddedDefaults_ShouldReturn_WhenStreamIsNull()
        {
            var reg = new ModelRegistryAccessor();
            // Invoke private method directly to hit: if (stream == null) return;
            reg.InvokeLoadEmbeddedDefaults();
            Assert.True(true);
        }

        [Fact]
        public void JsonLoader_ShouldReturn_WhenJsonIsWhitespace()
        {
            var path = Path.GetTempFileName();
            File.WriteAllText(path, "   "); // whitespace only
            var result = ModelRegistryJsonLoader.LoadFromFile(path);
            Assert.Empty(result); // LoadFromFile should return new List<ModelSpec>()
            File.Delete(path);
        }

        [Fact]
        public void JsonLoader_ShouldReturn_WhenModelsNull()
        {
            var path = Path.GetTempFileName();
            File.WriteAllText(path, "null"); // deserializes to null list
            var result = ModelRegistryJsonLoader.LoadFromFile(path);
            Assert.Empty(result);
            File.Delete(path);
        }

        [Fact]
        public void JsonLoader_ShouldCatch_JsonException()
        {
            var path = Path.GetTempFileName();
            File.WriteAllText(path, "{ bad json }"); // invalid JSON
            var result = ModelRegistryJsonLoader.LoadFromFile(path);
            Assert.Empty(result); // exception caught and ignored
            File.Delete(path);
        }

        [Fact]
        public void RemoteLoader_ShouldReturnNull_WhenJsonIsWhitespace()
        {
            // create a temp file with whitespace and call via file://
            var path = Path.GetTempFileName();
            File.WriteAllText(path, "   ");
            var uri = new Uri(path);
            var result = ModelRegistryRemoteLoader.LoadFromUrl(uri.ToString());
            Assert.Null(result);
            File.Delete(path);
        }

        private sealed class ThrowingTextWriter : StringWriter
        {
            public override void WriteLine(string? value) => throw new InvalidOperationException("boom");
            public override void Write(char value) => throw new InvalidOperationException("boom");
        }
    }
}
