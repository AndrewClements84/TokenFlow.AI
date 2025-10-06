using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Tests.Registry
{
    public class ModelRegistryTests
    {
        private static string CreateTempJson(params string[] ids)
        {
            string path = Path.GetTempFileName();
            var json = "[";
            for (int i = 0; i < ids.Length; i++)
            {
                if (i > 0) json += ",";
                json += $"{{\"Id\":\"{ids[i]}\",\"Family\":\"mock\",\"TokenizerName\":\"approx\",\"MaxInputTokens\":1000,\"InputPricePer1K\":0.001,\"OutputPricePer1K\":0.002}}";
            }
            json += "]";
            File.WriteAllText(path, json);
            return path;
        }

        [Fact]
        public void DefaultConstructor_ShouldLoadEmbeddedDefaults()
        {
            var registry = new ModelRegistry();
            var models = registry.GetAll();

            Assert.NotEmpty(models);
            Assert.Equal("Embedded", registry.LoadSource);
            Assert.Contains(models, m => m.Id == "gpt-4o");
        }

        [Fact]
        public void Constructor_ShouldLoadFromLocalFile()
        {
            string jsonPath = CreateTempJson("local-model");
            var registry = new ModelRegistry(jsonPath);

            Assert.Equal("Local", registry.LoadSource);
            Assert.True(registry.TryGet("local-model", out var model));
            Assert.Equal("mock", model.Family);

            File.Delete(jsonPath);
        }

        [Fact]
        public void Constructor_ShouldFallbackToEmbedded_WhenFileMissing()
        {
            var registry = new ModelRegistry("nonexistent.json");
            Assert.NotEmpty(registry.GetAll());
            Assert.Equal("Embedded", registry.LoadSource);
        }

        [Fact]
        public void FallbackConstructor_ShouldUseRemote_IfAvailable()
        {
            // simulate remote registry by writing to a file and using file:// URL
            string tempFile = CreateTempJson("remote-model");
            var uri = new Uri(tempFile);
            var registry = new ModelRegistry(uri, null, true);

            Assert.Equal("Remote", registry.LoadSource);
            Assert.True(registry.TryGet("remote-model", out _));

            File.Delete(tempFile);
        }

        [Fact]
        public void FallbackConstructor_ShouldUseLocal_WhenRemoteFails()
        {
            string tempFile = CreateTempJson("local-fallback-model");
            var badUri = new Uri("https://invalid.invalid/models.json");
            var registry = new ModelRegistry(badUri, tempFile, true);

            Assert.Equal("Local", registry.LoadSource);
            Assert.True(registry.TryGet("local-fallback-model", out _));

            File.Delete(tempFile);
        }

        [Fact]
        public void FallbackConstructor_ShouldUseEmbedded_WhenAllFail()
        {
            var badUri = new Uri("https://invalid.invalid/models.json");
            var registry = new ModelRegistry(badUri, "nope.json", true);

            Assert.Equal("Embedded", registry.LoadSource);
            Assert.NotEmpty(registry.GetAll());
        }

        [Fact]
        public void Register_ShouldReplaceExistingModel()
        {
            var registry = new ModelRegistry();
            var model = new ModelSpec("custom", "openai", "approx", 1000, 500, 0.01m, 0.02m);
            registry.Register(model);

            Assert.True(registry.TryGet("custom", out var retrieved));
            Assert.Equal("custom", retrieved.Id);
        }

        [Fact]
        public void TryGet_ShouldReturnFalse_WhenNotFound()
        {
            var registry = new ModelRegistry();
            Assert.False(registry.TryGet("nonexistent", out _));
        }

        [Fact]
        public void GetById_ShouldReturnModel_WhenExists()
        {
            var registry = new ModelRegistry();
            var first = registry.GetAll().First();
            var found = registry.GetById(first.Id);

            Assert.NotNull(found);
            Assert.Equal(first.Id, found.Id);
        }

        [Fact]
        public void GetAll_ShouldReturnReadOnlyList()
        {
            var registry = new ModelRegistry();
            var list = registry.GetAll();

            // Verify it's a ReadOnlyCollection
            Assert.IsAssignableFrom<System.Collections.ObjectModel.ReadOnlyCollection<ModelSpec>>(list);
        }
    }
}
