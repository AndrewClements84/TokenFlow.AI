using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Tests.Registry
{
    public class ModelRegistryTests
    {
        [Fact]
        public void LoadFromJsonString_ShouldRegisterModels()
        {
            var json = @"[
                { 'Id':'test-model', 'Family':'test', 'TokenizerName':'approx', 'MaxInputTokens':100, 'InputCost':0.01, 'OutputCost':0.02 }
            ]";

            var registry = new ModelRegistry();
            registry.LoadFromJsonString(json);

            var model = registry.GetById("test-model");
            Assert.NotNull(model);
            Assert.Equal("test", model.Family);
        }

        [Fact]
        public void LoadFromJsonFile_ShouldRegisterModels()
        {
            var json = @"[
                { 'Id':'file-model', 'Family':'openai', 'TokenizerName':'approx', 'MaxInputTokens':200, 'InputCost':0.02, 'OutputCost':0.03 }
            ]";

            var path = Path.GetTempFileName();
            File.WriteAllText(path, json);

            var registry = new ModelRegistry();
            registry.LoadFromJsonFile(path);

            var model = registry.GetById("file-model");
            Assert.NotNull(model);
            Assert.Equal("openai", model.Family);

            File.Delete(path);
        }

        [Fact]
        public void LoadFromJsonString_ShouldIgnoreMalformedJson()
        {
            var registry = new ModelRegistry();
            registry.LoadFromJsonString("INVALID_JSON");

            Assert.NotNull(registry.GetById("gpt-4o")); // defaults remain intact
        }

        [Fact]
        public void LoadFromJsonFile_ShouldIgnoreMissingFile()
        {
            var registry = new ModelRegistry();
            registry.LoadFromJsonFile("does_not_exist.json");

            Assert.NotNull(registry.GetById("gpt-4o"));
        }

        [Fact]
        public void Register_ShouldReplace_ExistingModel()
        {
            var registry = new ModelRegistry();
            var original = registry.GetById("gpt-4o");

            var replacement = new ModelSpec("gpt-4o", "test", "approx", 10, 5, 0.1m, 0.2m);
            registry.Register(replacement);

            var updated = registry.GetById("gpt-4o");
            Assert.Equal("test", updated.Family);
        }

        [Fact]
        public void TryGet_ShouldReturnTrue_WhenModelExists()
        {
            var registry = new ModelRegistry();
            bool found = registry.TryGet("gpt-4o", out var model);

            Assert.True(found);
            Assert.NotNull(model);
            Assert.Equal("openai", model.Family);
        }

        [Fact]
        public void TryGet_ShouldReturnFalse_WhenModelMissing()
        {
            var registry = new ModelRegistry();
            bool found = registry.TryGet("does-not-exist", out var model);

            Assert.False(found);
            Assert.Null(model);
        }
    }
}
